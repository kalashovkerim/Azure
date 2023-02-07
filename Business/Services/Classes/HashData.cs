using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Business.Services.Classes
{
    public class HashData
    {
        public string _data { get; set; }

        public HashData(string data)
        {
            _data = data;
        }

        public string DoHash()
        {
            string hashedData = BCrypt.Net.BCrypt.HashPassword(_data);
            return hashedData;
        }
        public bool Verify(string _hash)
        {
            bool hashVerify = BCrypt.Net.BCrypt.Verify(_data, _hash);
            return hashVerify;
        }
    }
}
