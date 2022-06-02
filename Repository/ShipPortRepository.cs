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
        private Database Database;

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
                .Include(x=>x.Warehouses)
                .Include(x=>x.Workers)
                .Include(x => x.Ships)
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

        public Result<ShipPort> Update(ShipPort shipPort)
        {
            Result<ShipPort> result = Database.ShipPorts.Update(shipPort).Entity;
            Database.SaveChanges();
            return result;
        }

    }
}
