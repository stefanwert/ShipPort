using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Model.Workers;

namespace WebShipPort.DTO
{
    public class CrewDTO: WorkerDTO
    {
        public int SailingHoursTotal { get; set; }

        public CrewRole Role { get; set; }
        
        public CrewDTO() { }

        public CrewDTO(Crew crew):base(crew)
        {
            SailingHoursTotal = crew.SailingHoursTotal;
            Role = crew.Role;
        }
    }
}
