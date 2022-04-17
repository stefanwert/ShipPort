using Core.Model;
using Core.Repository;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Database.ShipPorts.Remove(shipPort);
            Database.SaveChanges();
            return shipPort;
        }

        public Maybe<ShipPort> FindById(Guid id)
        {
            var shipPort = Database.ShipPorts.Find(id);
            return shipPort == null ? Maybe.None : shipPort;
        }

        public IEnumerable<ShipPort> GetAll()
        {
            return Database.ShipPorts;
        }

        public Result<ShipPort> Update(ShipPort shipPort)
        {
            Result<ShipPort> result = Database.ShipPorts.Update(shipPort).Entity;
            Database.SaveChanges();
            return result;
        }
    }
}
