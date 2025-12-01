using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFarmacia.Models;

namespace WebFarmacia.Services
{
    public interface IValidationService
    {
        List<string> ValidateReserva(Reserva reserva, FarmaciaContext context);
        List<string> ValidateMedicamento(Medicamento medicamento);
        List<string> ValidateCliente(Cliente cliente);
        List<string> ValidateEmpleado(Empleado empleado);
        List<string> ValidateUsuario(Usuario usuario);
        bool IsValidEmail(string email);
        bool IsValidPhone(string phone);
        bool IsValidCedula(string cedula);
    }

    public class ValidationService : IValidationService
    {
        public List<string> ValidateReserva(Reserva reserva, FarmaciaContext context)
        {
            var errors = new List<string>();

            if (reserva == null)
            {
                errors.Add("El objeto reserva es nulo.");
                return errors;
            }

            // Validación de cliente
            var cliente = context.Clientes.FindAsync(reserva.IdCliente).Result;
            if (cliente == null)
            {
                errors.Add("El cliente seleccionado no existe.");
            }
            else if (cliente.Estado != 1)
            {
                errors.Add("El cliente seleccionado no está activo.");
            }

            // Validación de medicamento
            var medicamento = context.Medicamentos.FindAsync(reserva.IdMedicamento).Result;
            if (medicamento == null)
            {
                errors.Add("El medicamento seleccionado no existe.");
            }
            else if (medicamento.Estado != 1)
            {
                errors.Add("El medicamento seleccionado no está activo.");
            }
            else if (medicamento.Stock < reserva.Cantidad)
            {
                errors.Add($"No hay suficiente stock. Stock disponible: {medicamento.Stock}, Cantidad solicitada: {reserva.Cantidad}");
            }

            // Validación de cantidad
            if (reserva.Cantidad <= 0)
            {
                errors.Add("La cantidad debe ser mayor a cero.");
            }
            else if (reserva.Cantidad > 100) // Límite de seguridad
            {
                errors.Add("La cantidad no puede exceder las 100 unidades.");
            }

            // Validación de fechas
            if (reserva.FechaReserva == default)
            {
                errors.Add("La fecha de reserva es inválida.");
            }
            else if (reserva.FechaReserva < DateTime.Today)
            {
                errors.Add("La fecha de reserva no puede ser anterior a hoy.");
            }

            if (reserva.FechaVencimientoReserva == default)
            {
                errors.Add("La fecha de vencimiento de la reserva es inválida.");
            }
            else if (reserva.FechaVencimientoReserva < reserva.FechaReserva)
            {
                errors.Add("La fecha de vencimiento debe ser posterior a la fecha de reserva.");
            }
            else if (reserva.FechaVencimientoReserva > DateTime.Today.AddMonths(6))
            {
                errors.Add("La fecha de vencimiento no puede exceder los 6 meses.");
            }

            // Validación de campos de contacto
            if (string.IsNullOrWhiteSpace(reserva.TelefonoContacto))
            {
                errors.Add("El teléfono de contacto es obligatorio.");
            }
            else if (!IsValidPhone(reserva.TelefonoContacto))
            {
                errors.Add("El formato del teléfono no es válido.");
            }

            if (!string.IsNullOrWhiteSpace(reserva.EmailContacto) && !IsValidEmail(reserva.EmailContacto))
            {
                errors.Add("El formato del email no es válido.");
            }

            // Validación de motivo
            if (string.IsNullOrWhiteSpace(reserva.Motivo))
            {
                errors.Add("El motivo de la reserva es obligatorio.");
            }
            else if (reserva.Motivo.Length < 5)
            {
                errors.Add("El motivo debe tener al menos 5 caracteres.");
            }
            else if (reserva.Motivo.Length > 200)
            {
                errors.Add("El motivo no puede exceder los 200 caracteres.");
            }

            // Validación de notas
            if (!string.IsNullOrWhiteSpace(reserva.Notas) && reserva.Notas.Length > 500)
            {
                errors.Add("Las notas no pueden exceder los 500 caracteres.");
            }

            // Validación de total
            if (reserva.Total <= 0)
            {
                errors.Add("El total debe ser mayor a cero.");
            }
            else if (medicamento != null && reserva.Total != medicamento.PrecioVenta * reserva.Cantidad)
            {
                errors.Add("El total calculado no coincide con el precio del medicamento.");
            }

            return errors;
        }

        public List<string> ValidateMedicamento(Medicamento medicamento)
        {
            var errors = new List<string>();

            if (medicamento == null)
            {
                errors.Add("El objeto medicamento es nulo.");
                return errors;
            }

            // Validación de nombre
            if (string.IsNullOrWhiteSpace(medicamento.Nombre))
            {
                errors.Add("El nombre del medicamento es obligatorio.");
            }
            else if (medicamento.Nombre.Length < 3)
            {
                errors.Add("El nombre debe tener al menos 3 caracteres.");
            }
            else if (medicamento.Nombre.Length > 100)
            {
                errors.Add("El nombre no puede exceder los 100 caracteres.");
            }

            // Validación de código
            if (string.IsNullOrWhiteSpace(medicamento.Codigo))
            {
                errors.Add("El código del medicamento es obligatorio.");
            }
            else if (medicamento.Codigo.Length < 3)
            {
                errors.Add("El código debe tener al menos 3 caracteres.");
            }
            else if (medicamento.Codigo.Length > 20)
            {
                errors.Add("El código no puede exceder los 20 caracteres.");
            }

            // Validación de precio
            if (medicamento.PrecioVenta <= 0)
            {
                errors.Add("El precio de venta debe ser mayor a cero.");
            }
            else if (medicamento.PrecioVenta > 10000)
            {
                errors.Add("El precio de venta no puede exceder los 10000.");
            }

            // Validación de stock
            if (medicamento.Stock < 0)
            {
                errors.Add("El stock no puede ser negativo.");
            }
            else if (medicamento.Stock > 10000)
            {
                errors.Add("El stock no puede exceder las 10000 unidades.");
            }

            // Validación de fecha de vencimiento
            if (medicamento.FechaVencimiento == default)
            {
                errors.Add("La fecha de vencimiento es inválida.");
            }
            else if (medicamento.FechaVencimiento < DateTime.Today)
            {
                errors.Add("La fecha de vencimiento no puede ser anterior a hoy.");
            }

            return errors;
        }

        public List<string> ValidateCliente(Cliente cliente)
        {
            var errors = new List<string>();

            if (cliente == null)
            {
                errors.Add("El objeto cliente es nulo.");
                return errors;
            }

            // Validación de nombres
            if (string.IsNullOrWhiteSpace(cliente.Nombres))
            {
                errors.Add("Los nombres del cliente son obligatorios.");
            }
            else if (cliente.Nombres.Length < 3)
            {
                errors.Add("Los nombres deben tener al menos 3 caracteres.");
            }
            else if (cliente.Nombres.Length > 100)
            {
                errors.Add("Los nombres no pueden exceder los 100 caracteres.");
            }

            // Validación de apellidos
            if (string.IsNullOrWhiteSpace(cliente.Apellidos))
            {
                errors.Add("Los apellidos del cliente son obligatorios.");
            }
            else if (cliente.Apellidos.Length < 3)
            {
                errors.Add("Los apellidos deben tener al menos 3 caracteres.");
            }
            else if (cliente.Apellidos.Length > 100)
            {
                errors.Add("Los apellidos no pueden exceder los 100 caracteres.");
            }

            // Validación de cédula
            if (string.IsNullOrWhiteSpace(cliente.CedulaIdentidad))
            {
                errors.Add("La cédula de identidad es obligatoria.");
            }
            else if (!IsValidCedula(cliente.CedulaIdentidad))
            {
                errors.Add("El formato de la cédula de identidad no es válido.");
            }

            // Validación de teléfono
            if (cliente.Telefono.HasValue && cliente.Telefono.Value <= 0)
            {
                errors.Add("El teléfono debe ser un número positivo.");
            }

            return errors;
        }

        public List<string> ValidateEmpleado(Empleado empleado)
        {
            var errors = new List<string>();

            if (empleado == null)
            {
                errors.Add("El objeto empleado es nulo.");
                return errors;
            }

            // Validación de nombres
            if (string.IsNullOrWhiteSpace(empleado.Nombres))
            {
                errors.Add("Los nombres del empleado son obligatorios.");
            }
            else if (empleado.Nombres.Length < 3)
            {
                errors.Add("Los nombres deben tener al menos 3 caracteres.");
            }
            else if (empleado.Nombres.Length > 50)
            {
                errors.Add("Los nombres no pueden exceder los 50 caracteres.");
            }

            // Validación de cédula
            if (string.IsNullOrWhiteSpace(empleado.CedulaIdentidad))
            {
                errors.Add("La cédula de identidad es obligatoria.");
            }
            else if (!IsValidCedula(empleado.CedulaIdentidad))
            {
                errors.Add("El formato de la cédula de identidad no es válido.");
            }

            // Validación de celular
            if (empleado.Celular <= 0)
            {
                errors.Add("El celular debe ser un número positivo.");
            }

            // Validación de cargo
            if (string.IsNullOrWhiteSpace(empleado.Cargo))
            {
                errors.Add("El cargo del empleado es obligatorio.");
            }
            else if (empleado.Cargo.Length < 3)
            {
                errors.Add("El cargo debe tener al menos 3 caracteres.");
            }
            else if (empleado.Cargo.Length > 50)
            {
                errors.Add("El cargo no puede exceder los 50 caracteres.");
            }

            return errors;
        }

        public List<string> ValidateUsuario(Usuario usuario)
        {
            var errors = new List<string>();

            if (usuario == null)
            {
                errors.Add("El objeto usuario es nulo.");
                return errors;
            }

            // Validación de nombre de usuario
            if (string.IsNullOrWhiteSpace(usuario.Usuario1))
            {
                errors.Add("El nombre de usuario es obligatorio.");
            }
            else if (usuario.Usuario1.Length < 3)
            {
                errors.Add("El nombre de usuario debe tener al menos 3 caracteres.");
            }
            else if (usuario.Usuario1.Length > 50)
            {
                errors.Add("El nombre de usuario no puede exceder los 50 caracteres.");
            }

            // Validación de contraseña
            if (string.IsNullOrWhiteSpace(usuario.Clave))
            {
                errors.Add("La contraseña es obligatoria.");
            }
            else if (usuario.Clave.Length < 6)
            {
                errors.Add("La contraseña debe tener al menos 6 caracteres.");
            }
            else if (usuario.Clave.Length > 255)
            {
                errors.Add("La contraseña no puede exceder los 255 caracteres.");
            }

            return errors;
        }

        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            // Eliminar espacios, guiones, paréntesis y otros caracteres
            var cleanPhone = Regex.Replace(phone, @"[^\d]", "");
            
            return cleanPhone.Length >= 7 && cleanPhone.Length <= 15;
        }

        public bool IsValidCedula(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula))
                return false;

            // Eliminar espacios y caracteres especiales
            var cleanCedula = Regex.Replace(cedula, @"[^\d]", "");
            
            // Validación básica para cédula boliviana (formato: 1234567 o 12345678)
            return cleanCedula.Length >= 6 && cleanCedula.Length <= 8;
        }
    }
}