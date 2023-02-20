using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Classes
{
    public class GenerateUserLogin
    {
        public static StringBuilder FromName(string _name)
        {
            StringBuilder login = new();
            var random = new Random();

            for (int i = 0; i < _name.Length / 2; i++)
            {
                login.Append(_name[i]);
            }
            
            for (int i = 0; i < 4; i++)
            {
                int num = random.Next(0, 10);
                login.Append(num.ToString());
            }
            return login; 
        }
    }
}
