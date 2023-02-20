using System.Text;

namespace Business.Services.Classes
{
    public class GenerateUserPassword
    {
        private static string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*";
        public static StringBuilder Generate()
        {
            var rand = new Random();
            StringBuilder password = new();

            int length = 8;
            for (int i = 0; i < length; i++)
            {
                password.Append(Chars[rand.Next(0, Chars.Length)]);
            }
            return password;
        }
    }
}
