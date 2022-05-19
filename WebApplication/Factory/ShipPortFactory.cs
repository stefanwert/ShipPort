using Core.Model;
using Core.Model.Workers;
using Core.Service;
using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;
using WebShipPort.DTO;

namespace WebShipPort.Factory
{
    public class ShipPortFactory
    {
        private readonly ShipService _shipService;
        private readonly WarehouseService _warehouseService;
        private readonly ShipCaptainService _shipCaptainService;
        private readonly WarehouseClerkService _warehouseClerkService;
        private readonly CrewService _crewService;


        public ShipPortFactory(ShipService shipService, WarehouseService warehouseService,ShipCaptainService shipCaptainService, WarehouseClerkService warehouseClerkService, CrewService crewService)
        {
            _shipService = shipService;
            _warehouseService = warehouseService;
            _shipCaptainService = shipCaptainService;
            _warehouseClerkService = warehouseClerkService;
            _crewService = crewService;
        }
        public Result<ShipPort> Create(ShipPortDTO shipPortDTO)
        {
            List<Worker> workers = PopulateWorkers(shipPortDTO);
            List<Ship> ships = PopulateShips(shipPortDTO);
            List<Warehouse> warehouses = PopulateWarehouses(shipPortDTO);

            var ship = ShipPort.Create(shipPortDTO.Id, shipPortDTO.Name, shipPortDTO.TimeOfCreation, workers, ships, warehouses);
            if (ship.IsFailure)
                return Result.Failure<ShipPort>(ship.Error);
            return ship;
        }

        private List<Warehouse> PopulateWarehouses(ShipPortDTO shipPortDTO)
        {
            var warehouses = new List<Warehouse>();
            foreach (var warehouseDto in shipPortDTO.Warehouses ?? Enumerable.Empty<WarehouseDTO>())
            {
                var maybeWarehouse = _warehouseService.FindById(warehouseDto.Id);
                if (maybeWarehouse.HasNoValue)
                    continue;
                warehouses.Add(maybeWarehouse.Value);
            }

            return warehouses;
        }

        private List<Ship> PopulateShips(ShipPortDTO shipPortDTO)
        {
            var ships = new List<Ship>();
            foreach (var shipDto in shipPortDTO.Ships ?? Enumerable.Empty<ShipDTO>())
            {
                var maybeShip = _shipService.FindById(shipDto.Id);
                if (maybeShip.HasNoValue)
                    continue;
                ships.Add(maybeShip.Value);
            }

            return ships;
        }

        private List<Worker> PopulateWorkers(ShipPortDTO shipPortDTO)
        {
            var workers = new List<Worker>();
            foreach (var workerDto in shipPortDTO.Workers ?? Enumerable.Empty<WorkerDTO>())
            {
                var captain = _shipCaptainService.FindById(workerDto.Id);
                if (captain.HasValue)
                {
                    workers.Add(captain.Value);
                    continue;
                }

                var crew = _crewService.FindById(workerDto.Id);
                if (crew.HasValue)
                {
                    workers.Add(crew.Value);
                    continue;
                }

                var warhouseClerk = _warehouseClerkService.FindById(workerDto.Id);
                if (warhouseClerk.HasValue)
                {
                    workers.Add(warhouseClerk.Value);
                    continue;
                }

            }

            return workers;
        }
    }
}
