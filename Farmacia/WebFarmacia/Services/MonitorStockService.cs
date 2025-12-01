using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebFarmacia.Models;

namespace WebFarmacia.Services
{
    public interface IMonitorStockService
    {
        Task CheckLowStockAlertsAsync();
        Task CheckExpiringReservationsAsync();
    }

    public class MonitorStockService : BackgroundService, IMonitorStockService
    {
        private readonly FarmaciaContext _context;
        private readonly IEmailService _emailService;
        private readonly ILogger<MonitorStockService> _logger;

        public MonitorStockService(
            FarmaciaContext context,
            IEmailService emailService,
            ILogger<MonitorStockService> logger)
        {
            _context = context;
            _emailService = emailService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Verificar medicamentos con bajo stock cada hora
                    await CheckLowStockAlertsAsync();
                    
                    // Verificar reservas próximas a expirar cada 6 horas
                    await CheckExpiringReservationsAsync();
                    
                    // Esperar 1 hora antes de la próxima verificación
                    await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error en el servicio de monitoreo de stock");
                    await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); // Esperar menos si hay error
                }
            }
        }

        public async Task CheckLowStockAlertsAsync()
        {
            try
            {
                var medicamentosBajoStock = await _context.Medicamentos
                    .Where(m => m.Estado == 1 && m.Stock <= 10) // Stock <= 10
                    .Include(m => m.IdCategoriaNavigation)
                    .Include(m => m.IdLaboratorioNavigation)
                    .ToListAsync();

                foreach (var medicamento in medicamentosBajoStock)
                {
                    _logger.LogWarning($"Medicamento con bajo stock: {medicamento.Nombre} - Stock: {medicamento.Stock}");
                    
                    try
                    {
                        var success = await _emailService.SendMedicamentoBajoStockAsync(medicamento);
                        if (success)
                        {
                            _logger.LogInformation($"Alerta enviada para {medicamento.Nombre}");
                        }
                        else
                        {
                            _logger.LogError($"No se pudo enviar alerta para {medicamento.Nombre}");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Error enviando alerta para {medicamento.Nombre}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar medicamentos con bajo stock");
            }
        }

        public async Task CheckExpiringReservationsAsync()
        {
            try
            {
                var reservasPorExpirar = await _context.Reservas
                    .Where(r => r.Estado == "PENDIENTE")
                    .Include(r => r.IdClienteNavigation)
                    .Include(r => r.IdMedicamentoNavigation)
                    .Where(r => r.FechaVencimientoReserva <= DateTime.Now.AddDays(3)) // Expiran en 3 días o menos
                    .Where(r => !r.Notas.Contains("ENVIADO_EMAIL_EXPIRACION")) // No enviar duplicados
                    .ToListAsync();

                foreach (var reserva in reservasPorExpirar)
                {
                    _logger.LogWarning($"Reserva por expirar: #{reserva.IdReserva} - {reserva.IdMedicamentoNavigation?.Nombre}");
                    
                    try
                    {
                        var success = await _emailService.SendReservaCercanaExpiracionAsync(reserva, reserva.EmailContacto);
                        if (success)
                        {
                            // Marcar como enviado para evitar duplicados
                            if (string.IsNullOrEmpty(reserva.Notas))
                            {
                                reserva.Notas = "ENVIADO_EMAIL_EXPIRACION";
                            }
                            else
                            {
                                reserva.Notas += "; ENVIADO_EMAIL_EXPIRACION";
                            }
                            await _context.SaveChangesAsync();
                            _logger.LogInformation($"Recordatorio enviado para reserva #{reserva.IdReserva}");
                        }
                        else
                        {
                            _logger.LogError($"No se pudo enviar recordatorio para reserva #{reserva.IdReserva}");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Error enviando recordatorio para reserva #{reserva.IdReserva}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar reservas próximas a expirar");
            }
        }
    }
}