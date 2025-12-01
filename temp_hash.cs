using System;
using System.Security.Cryptography;
using System.Text;

class Program {
    static void Main() {
        using (SHA256 sha256 = SHA256.Create()) {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes("123456" + "SIS457-1nf0!"));
            Console.WriteLine(Convert.ToBase64String(bytes));
        }
    }
}
