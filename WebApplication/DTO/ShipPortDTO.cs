using Core.Model;
using Core.Model.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShipPort.DTO
{
    public class ShipPortDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime TimeOfCreation { get; set; }

        public ICollection<WorkerDTO> Workers { get; set; }

        public ICollection<ShipDTO> Ships { get; set; }

        public ICollection<WarehouseDTO> Warehouses { get; set; }

        public int NumberOfWorker { get; set; }
        public int NumberOfShips { get; set; }
        public int NumberOfWarehouses { get; set; }
        public int NumberOfWarehousesThatStoreFlammable { get; set; }

        public ShipPortDTO() { }
        public ShipPortDTO(ShipPort shipPort)
        {
            Id = shipPort.Id;
            Name = shipPort.Name;
            TimeOfCreation = shipPort.TimeOfCreation;
            Workers = new List<WorkerDTO>();
            foreach(var worker in shipPort.Workers ?? Enumerable.Empty<Worker>())
            {
                if(worker is ShipCaptain)
                {
                    var workeTemp = new ShipCaptainDTO(worker as ShipCaptain);
                    Workers.Add(workeTemp);
                }
                else if(worker is Crew)
                {
                    var workeTemp = new CrewDTO(worker as Crew);
                    Workers.Add(workeTemp);
                }
                else if(worker is WarehouseClerk)
                {
                    var workeTemp = new WarehouseClerkDTO(worker as WarehouseClerk);
                    Workers.Add(workeTemp);
                }
            }

            Ships = new List<ShipDTO>();
            foreach(var ship in shipPort.Ships ?? Enumerable.Empty<Ship>())
            {
                var shipTemp = new ShipDTO(ship);
                Ships.Add(shipTemp);
            }

            Warehouses = new List<WarehouseDTO>();
            foreach(var warehouse in shipPort.Warehouses ?? Enumerable.Empty<Warehouse>())
            {
                var warehouseTemp = new WarehouseDTO(warehouse);
                Warehouses.Add(warehouseTemp);
            }

            NumberOfWorker = Workers.Count;
            NumberOfShips = Ships.Count;
            NumberOfWarehouses = Warehouses.Count;
            NumberOfWarehousesThatStoreFlammable = Warehouses.Where(x => x.StoreFlammableCargo == true).Count();
        }
    }
}
