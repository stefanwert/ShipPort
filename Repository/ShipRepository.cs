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
                .Where(x=> x.Id == id)
                .FirstOrDefault();
            return ship == null ? Maybe.None : ship;
        }

        public IEnumerable<Ship> GetAll()
        {
            return Database.Ships;
        }

        public Result<Ship> Update(Ship ship)
        {
            Result<Ship> result = Database.Ships.Update(ship).Entity;
            //var shipToChangeMaybe = FindById(ship.Id);
            //if (shipToChange.HasNoValue)
            //    return Result.Failure<Ship>("There is no ship with this id");

            //var shipToChange = shipToChangeMaybe.Value;
            //shipToChange.Name = ship.Name;


            Database.SaveChanges();
            return FindById(ship.Id).Value;
        }

        public ICollection<Ship> FindByShipPortId(Guid id)
        {
            return Database.Ships.Where(x => x.ShipPortId == id).ToList();
        }
    }
}
