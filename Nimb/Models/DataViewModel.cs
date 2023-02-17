using NimbRepository.Model.Storekeeper;
using System.Collections.Generic;

namespace Nimb.Models
{
    public class DataViewModel
    {
        public IEnumerable<Good> Goods { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }
    }
}
