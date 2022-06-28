using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model.test
{
    public class Child
    {
        public Guid Id { get; set; }
        public string ChildName { get; set; }
        public virtual ICollection<House> Houses { get; set; }
        public Guid CurrentHouseId { get; set; }
    }
}
