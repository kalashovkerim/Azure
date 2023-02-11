using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
namespace Business.Services.Classes
{
    public class GenerateUserPassword
    {
        private static string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*";
        public static string Generate()
        {
            var rand = new Random();
            string password = "";

            int length = 8;
            for (int i = 0; i < length; i++)
            {
                password += Chars[rand.Next(0, Chars.Length)];
            }
            return password;
        }

       
    }
}
