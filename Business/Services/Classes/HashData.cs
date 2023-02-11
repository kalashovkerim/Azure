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
        public string Data { get; set; }

        public HashData(string data)
        {
            Data = data;
        }

        public string DoHash()
        {
            string hashedData = BCrypt.Net.BCrypt.HashPassword(Data);
            return hashedData;
        }
        public bool Verify(string _hash)
        {
            bool hashVerify = BCrypt.Net.BCrypt.Verify(Data, _hash);
            return hashVerify;
        }
    }
}
