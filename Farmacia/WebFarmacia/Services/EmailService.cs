using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using WebFarmacia.Models;

namespace WebFarmacia.Services
{
    public interface IEmailService
    {
        Task<bool> SendReservaConfirmationAsync(Reserva reserva, string clienteEmail);
        Task<bool> SendReservaCancelationAsync(Reserva reserva, string clienteEmail);
        Task<bool> SendMedicamentoBajoStockAsync(Medicamento medicamento);
        Task<bool> SendReservaCercanaExpiracionAsync(Reserva reserva, string clienteEmail);
    }

    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task<bool> SendReservaConfirmationAsync(Reserva reserva, string clienteEmail)
        {
            try
            {
                var subject = "Confirmación de Reserva - Farmacia";
                var body = GenerateReservaConfirmationBody(reserva);
                
                return await SendEmailAsync(clienteEmail, subject, body);
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SendReservaCancelationAsync(Reserva reserva, string clienteEmail)
        {
            try
            {
                var subject = "Cancelación de Reserva - Farmacia";
                var body = GenerateReservaCancelationBody(reserva);
                
                return await SendEmailAsync(clienteEmail, subject, body);
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SendMedicamentoBajoStockAsync(Medicamento medicamento)
        {
            try
            {
                var subject = "Alerta: Medicamento con Bajo Stock - Farmacia";
                var body = GenerateMedicamentoBajoStockBody(medicamento);
                
                // Enviar a administradores y empleados
                var adminEmails = GetAdminEmails();
                var success = true;
                
                foreach (var email in adminEmails)
                {
                    if (!await SendEmailAsync(email, subject, body))
                    {
                        success = false;
                    }
                }
                
                return success;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SendReservaCercanaExpiracionAsync(Reserva reserva, string clienteEmail)
        {
            try
            {
                var subject = "Recordatorio: Reserva por Expirar - Farmacia";
                var body = GenerateReservaExpiracionBody(reserva);
                
                return await SendEmailAsync(clienteEmail, subject, body);
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            // En una implementación real, aquí usarías un servicio de envío de emails como:
            // - SendGrid
            // - MailKit
            // - SMTP directamente
            // - Azure SendGrid
            
            // Para este ejemplo, simularemos el envío
            Console.WriteLine($"EMAIL ENVIADO A: {toEmail}");
            Console.WriteLine($"ASUNTO: {subject}");
            Console.WriteLine($"CUERPO: {body}");
            
            return true; // Simular envío exitoso
        }

        private string GenerateReservaConfirmationBody(Reserva reserva)
        {
            return $@"
                <html>
                <head>
                    <style>
                        body {{ font-family: Arial, sans-serif; line-height: 1.6; }}
                        .header {{ background-color: #007bff; color: white; padding: 20px; text-align: center; }}
                        .content {{ padding: 20px; }}
                        .info {{ background-color: #f8f9fa; padding: 15px; border-radius: 5px; margin: 10px 0; }}
                        .footer {{ background-color: #6c757d; color: white; padding: 10px; text-align: center; margin-top: 20px; }}
                    </style>
                </head>
                <body>
                    <div class='header'>
                        <h2>Confirmación de Reserva</h2>
                    </div>
                    <div class='content'>
                        <p>Estimado/a cliente,</p>
                        <p>Le informamos que su reserva ha sido confirmada exitosamente.</p>
                        
                        <div class='info'>
                            <h3>Detalles de la Reserva:</h3>
                            <p><strong>Número de Reserva:</strong> #{reserva.IdReserva}</p>
                            <p><strong>Medicamento:</strong> {reserva.IdMedicamentoNavigation?.Nombre}</p>
                            <p><strong>Cantidad:</strong> {reserva.Cantidad} unidades</p>
                            <p><strong>Fecha de Reserva:</strong> {reserva.FechaReserva:dd/MM/yyyy}</p>
                            <p><strong>Fecha de Vencimiento:</strong> {reserva.FechaVencimientoReserva:dd/MM/yyyy}</p>
                            <p><strong>Total:</strong> {reserva.Total:C}</p>
                            <p><strong>Teléfono de Contacto:</strong> {reserva.TelefonoContacto}</p>
                            {(!string.IsNullOrEmpty(reserva.EmailContacto) ? $"<p><strong>Email:</strong> {reserva.EmailContacto}</p>" : "")}
                        </div>
                        
                        <p><strong>Motivo:</strong> {reserva.Motivo}</p>
                        {(!string.IsNullOrEmpty(reserva.Notas) ? $"<p><strong>Notas Adicionales:</strong> {reserva.Notas}</p>" : "")}
                        
                        <p>Gracias por confiar en nuestra farmacia.</p>
                        <p>Si tiene alguna pregunta, no dude en contactarnos.</p>
                    </div>
                    <div class='footer'>
                        <p>&copy; {DateTime.Now.Year} Farmacia. Todos los derechos reservados.</p>
                    </div>
                </body>
                </html>
            ";
        }

        private string GenerateReservaCancelationBody(Reserva reserva)
        {
            return $@"
                <html>
                <head>
                    <style>
                        body {{ font-family: Arial, sans-serif; line-height: 1.6; }}
                        .header {{ background-color: #dc3545; color: white; padding: 20px; text-align: center; }}
                        .content {{ padding: 20px; }}
                        .info {{ background-color: #f8f9fa; padding: 15px; border-radius: 5px; margin: 10px 0; }}
                        .footer {{ background-color: #6c757d; color: white; padding: 10px; text-align: center; margin-top: 20px; }}
                    </style>
                </head>
                <body>
                    <div class='header'>
                        <h2>Cancelación de Reserva</h2>
                    </div>
                    <div class='content'>
                        <p>Estimado/a cliente,</p>
                        <p>Lamentamos informarle que su reserva ha sido cancelada.</p>
                        
                        <div class='info'>
                            <h3>Detalles de la Reserva Cancelada:</h3>
                            <p><strong>Número de Reserva:</strong> #{reserva.IdReserva}</p>
                            <p><strong>Medicamento:</strong> {reserva.IdMedicamentoNavigation?.Nombre}</p>
                            <p><strong>Cantidad:</strong> {reserva.Cantidad} unidades</p>
                            <p><strong>Fecha de Reserva:</strong> {reserva.FechaReserva:dd/MM/yyyy}</p>
                            <p><strong>Estado:</strong> {reserva.Estado}</p>
                        </div>
                        
                        <p>Si necesita realizar una nueva reserva, no dude en contactarnos.</p>
                        <p>Disculpe las molestias ocasionadas.</p>
                    </div>
                    <div class='footer'>
                        <p>&copy; {DateTime.Now.Year} Farmacia. Todos los derechos reservados.</p>
                    </div>
                </body>
                </html>
            ";
        }

        private string GenerateMedicamentoBajoStockBody(Medicamento medicamento)
        {
            return $@"
                <html>
                <head>
                    <style>
                        body {{ font-family: Arial, sans-serif; line-height: 1.6; }}
                        .header {{ background-color: #ffc107; color: #000; padding: 20px; text-align: center; }}
                        .content {{ padding: 20px; }}
                        .warning {{ background-color: #fff3cd; border: 1px solid #ffeaa7; padding: 15px; border-radius: 5px; margin: 10px 0; }}
                        .footer {{ background-color: #6c757d; color: white; padding: 10px; text-align: center; margin-top: 20px; }}
                    </style>
                </head>
                <body>
                    <div class='header'>
                        <h2>Alerta: Medicamento con Bajo Stock</h2>
                    </div>
                    <div class='content'>
                        <div class='warning'>
                            <h3>⚠️ ALERTA DE STOCK BAJO ⚠️</h3>
                            <p>El siguiente medicamento ha alcanzado el nivel mínimo de stock:</p>
                        </div>
                        
                        <div class='info'>
                            <h3>Detalles del Medicamento:</h3>
                            <p><strong>Nombre:</strong> {medicamento.Nombre}</p>
                            <p><strong>Código:</strong> {medicamento.Codigo}</p>
                            <p><strong>Stock Actual:</strong> {medicamento.Stock} unidades</p>
                            <p><strong>Precio de Venta:</strong> {medicamento.PrecioVenta:C}</p>
                            <p><strong>Fecha de Vencimiento:</strong> {medicamento.FechaVencimiento:dd/MM/yyyy}</p>
                            <p><strong>Requiere Receta:</strong> {(medicamento.RequiereReceta ? "Sí" : "No")}</p>
                        </div>
                        
                        <p><strong>Acción Requerida:</strong></p>
                        <ul>
                            <li>Realizar pedido de reposición inmediato</li>
                            <li>Verificar con laboratorio sobre disponibilidad</li>
                            <li>Considerar ordenar stock de seguridad adicional</li>
                        </ul>
                    </div>
                    <div class='footer'>
                        <p>&copy; {DateTime.Now.Year} Farmacia. Todos los derechos reservados.</p>
                    </div>
                </body>
                </html>
            ";
        }

        private string GenerateReservaExpiracionBody(Reserva reserva)
        {
            return $@"
                <html>
                <head>
                    <style>
                        body {{ font-family: Arial, sans-serif; line-height: 1.6; }}
                        .header {{ background-color: #17a2b8; color: white; padding: 20px; text-align: center; }}
                        .content {{ padding: 20px; }}
                        .info {{ background-color: #f8f9fa; padding: 15px; border-radius: 5px; margin: 10px 0; }}
                        .footer {{ background-color: #6c757d; color: white; padding: 10px; text-align: center; margin-top: 20px; }}
                    </style>
                </head>
                <body>
                    <div class='header'>
                        <h2>Recordatorio: Reserva por Expirar</h2>
                    </div>
                    <div class='content'>
                        <p>Estimado/a cliente,</p>
                        <p>Le recordamos que su reserva está por expirar y necesita ser confirmada.</p>
                        
                        <div class='info'>
                            <h3>Detalles de la Reserva:</h3>
                            <p><strong>Número de Reserva:</strong> #{reserva.IdReserva}</p>
                            <p><strong>Medicamento:</strong> {reserva.IdMedicamentoNavigation?.Nombre}</p>
                            <p><strong>Cantidad:</strong> {reserva.Cantidad} unidades</p>
                            <p><strong>Fecha de Reserva:</strong> {reserva.FechaReserva:dd/MM/yyyy}</p>
                            <p><strong>Fecha de Vencimiento:</strong> {reserva.FechaVencimientoReserva:dd/MM/yyyy}</p>
                            <p><strong>Teléfono de Contacto:</strong> {reserva.TelefonoContacto}</p>
                        </div>
                        
                        <p><strong>Próximos Pasos:</strong></p>
                        <ul>
                            <li>Confirme su reserva llamando a nuestra farmacia</li>
                            <li>Visítenos para recoger su medicamento</li>
                            <li>Si ya no necesita la reserva, por favor cancelela</li>
                        </ul>
                        
                        <p>Gracias por su atención.</p>
                    </div>
                    <div class='footer'>
                        <p>&copy; {DateTime.Now.Year} Farmacia. Todos los derechos reservados.</p>
                    </div>
                </body>
                </html>
            ";
        }

        private List<string> GetAdminEmails()
        {
            // En una implementación real, esto vendría de la base de datos
            // o de una configuración
            return new List<string> { "admin@farmacia.com", "gerente@farmacia.com" };
        }
    }

    public class EmailSettings
    {
        public string SmtpServer { get; set; } = string.Empty;
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; } = string.Empty;
        public string SmtpPassword { get; set; } = string.Empty;
        public string FromEmail { get; set; } = string.Empty;
        public string FromName { get; set; } = string.Empty;
    }
}