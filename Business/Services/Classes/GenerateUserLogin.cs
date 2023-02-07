using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Classes
{
    public class GenerateUserLogin
    {
        public static string FromName(string _name)
        {
            string login = "";
            var random = new Random();

            for (int i = 0; i < _name.Length / 2; i++)
            {
                login += _name[i];
            }
            
            for (int i = 0; i < 4; i++)
            {
                int num = random.Next(0, 10);
                login += num.ToString();
            }
            return login; 
        }

    }
}
