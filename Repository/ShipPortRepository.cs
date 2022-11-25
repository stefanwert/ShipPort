using Core.Model;
using Core.Repository;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer
{
    public class ShipPortRepository : IShipPortRepository
    {
        private readonly Database Database;

        public ShipPortRepository(Database database)
        {
            Database = database;
        }
        public Result<ShipPort> Create(ShipPort shipPort)
        {
            Result<ShipPort> ret = Database.ShipPorts.Add(shipPort).Entity;
            Database.SaveChanges();
            return ret;
        }

        public Maybe<ShipPort> DeleteById(Guid id)
        {
            var shipPort = Database.ShipPorts.First(x => x.Id == id);
            shipPort.Deleted = true;
            Database.ShipPorts.Update(shipPort);
            Database.SaveChanges();
            return shipPort;
        }

        public Maybe<ShipPort> FindById(Guid id)
        {
            var shipPort = Database.ShipPorts
                .Where(x=>x.Id == id  && !x.Deleted)
                .Include(x => x.Warehouses)
                .Include(x => x.Workers)
                .Include(x => x.Ships)
                .FirstOrDefault();
            return shipPort == null ? Maybe.None : shipPort;
        }

        public bool ShipPortExist(Guid id)
        {
            var shipPort = Database.ShipPorts
                .Where(x => x.Id == id && !x.Deleted)
                .FirstOrDefault();
            return shipPort == null ? false : true ;
        }

        public Maybe<ShipPort> FindByIdWithOutRelationships(Guid id)
        {
            var shipPort = Database.ShipPorts
                .Where(x => x.Id == id && !x.Deleted)
                .FirstOrDefault();
            return shipPort == null ? Maybe.None : shipPort;
        }

        public IEnumerable<ShipPort> GetAll()
        {
            return Database.ShipPorts
                .Include(x => x.Warehouses)
                .Include(x => x.Workers)
                .Include(x=>x.Ships).Where(s=>!s.Deleted);
        }

        public IEnumerable<ShipPort> GetAllWithOutRelationships()
        {
            return Database.ShipPorts;
        }

        public Result<ShipPort> Update(ShipPort shipPort)
        {
            Result<ShipPort> result = Database.ShipPorts.Update(shipPort).Entity;
            Database.SaveChanges();
            return result;
        }

        public bool DoesShipPortContainWarehouse(Guid warehouseId, Guid shipportId)
        {
            var shipPort = Database.ShipPorts.Include(x => x.Warehouses).Where(x => x.Id == shipportId).First();
            if (shipPort == null)
                return false;
            return shipPort.Warehouses.Where(x => x.Id == warehouseId).Any();
                
        }
    }
}
