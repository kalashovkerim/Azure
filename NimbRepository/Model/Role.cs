using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimbRepository.Model
{
    public class Role : IEntity
    {
        
        public int Id { get; set; }
        public string Position { get; set; }
    }
}
