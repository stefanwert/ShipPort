using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model.test
{
    public class House
    {
        public Guid Id { get; set; }
        public string HouseName { get; set; }
        public double HouseSize { get; set; }

        public virtual ICollection<Child> Children { get; set; }

    }
}
