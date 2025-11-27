using CadFarmacia;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ClnFarmacia
{
    public class UsuarioCln
    {
        public static Usuario UsuarioLogueado { get; set; }

        public static Usuario ValidarAcceso(string usuario, string clave)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                return context.Usuario
                    .Include("Empleado")
                    .FirstOrDefault(u => u.usuario1 == usuario &&
                                        u.clave == clave &&
                                        u.estado == 1);
            }
        }

        public static string Encriptar(string texto)
        {
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