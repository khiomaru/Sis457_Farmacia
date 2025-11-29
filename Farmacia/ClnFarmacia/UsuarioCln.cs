using CadFarmacia;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Data.Entity; // <--- ESTE ERA EL IMPORTANTE QUE FALTABA

namespace ClnFarmacia
{
    public class UsuarioCln
    {
        // Propiedad estática para guardar la sesión en la capa lógica
        public static Usuario UsuarioLogueado { get; set; }

        // En UsuarioCln.cs
        public static Usuario ValidarAcceso(string usuario, string clave)
        {
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(clave))
                return null;

            // ⚠️ SOLO PARA DESARROLLO - NO USAR EN PRODUCCIÓN
            using (var context = new Labsis457FarmaciaEntities())
            {
                return context.Usuario
                    .Include("Empleado")
                    .FirstOrDefault(u =>
                        u.usuario1.Equals(usuario.Trim(), StringComparison.OrdinalIgnoreCase) &&
                        u.clave == clave.Trim() &&  // ← Comparar texto claro
                        u.estado == 1);
            }
        }

        public static string Encriptar(string texto)
        {
            // Algoritmo TripleDES (Coincide con la lógica antigua de proyectos SIS457)
            using (var des = new TripleDESCryptoServiceProvider())
            using (var hashMd5 = new MD5CryptoServiceProvider())
            {
                byte[] keyArray = hashMd5.ComputeHash(Encoding.UTF8.GetBytes("SIS457FARMACIA"));
                des.Key = keyArray;
                des.Mode = CipherMode.ECB;
                des.Padding = PaddingMode.PKCS7;

                ICryptoTransform transform = des.CreateEncryptor();
                byte[] inputArray = Encoding.UTF8.GetBytes(texto);
                byte[] resultArray = transform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                return Convert.ToBase64String(resultArray);
            }
        }
    }
}