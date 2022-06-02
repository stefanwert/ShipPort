using Core.Model;
using Core.Repository;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ShipRepository : IShipRepository
    {
        private Database Database;

        public ShipRepository(Database database)
        {
            Database = database;
        }
        public Result<Ship> Create(Ship ship)
        {
            Result<Ship> ret = Database.Ships.Add(ship).Entity;
            Database.SaveChanges();
            return ret;
        }

        public Maybe<Ship> DeleteById(Guid id)
        {
            var ship = Database.Ships.First(x => x.Id == id);
            Database.Ships.Remove(ship);
            Database.SaveChanges();
            return ship;
        }

        public Maybe<Ship> FindById(Guid id)
        {
            var ship = Database.Ships
                .Include(x=>x.ShipPort)
                .Where(x=> x.Id == id)
                .FirstOrDefault();
            return ship == null ? Maybe.None : ship;
        }

        public IEnumerable<Ship> GetAll()
        {
            return Database.Ships.Include(x => x.ShipPort);
        }

        public Result<Ship> Update(Ship ship)
        {
            Result<Ship> result = Database.Ships.Update(ship).Entity;
            Database.SaveChanges();
            return result;
        }
    }
}
