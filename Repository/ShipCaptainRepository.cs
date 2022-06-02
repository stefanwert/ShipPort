using Core.Model.Workers;
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
    public class ShipCaptainRepository : IShipCaptainRepository
    {
        private Database Database;

        public ShipCaptainRepository(Database database)
        {
            Database = database;
        }
        public Result<ShipCaptain> Create(ShipCaptain shipCaptain)
        {
            Result<ShipCaptain> ret = Database.ShipCaptains.Add(shipCaptain).Entity;
            Database.SaveChanges();
            return ret;
        }

        public Maybe<ShipCaptain> DeleteById(Guid id)
        {
            var crew = Database.ShipCaptains.First(x => x.Id == id);
            Database.ShipCaptains.Remove(crew);
            Database.SaveChanges();
            return crew;
        }

        public Maybe<ShipCaptain> FindById(Guid id)
        {
            var shipCaptain = Database.ShipCaptains.Find(id);
            return shipCaptain == null ? Maybe.None : shipCaptain;
        }

        public IEnumerable<ShipCaptain> GetAll()
        {
            return Database.ShipCaptains.Include(x => x.ShipPort);
        }

        public Result<ShipCaptain> Update(ShipCaptain shipCaptain)
        {
            Result<ShipCaptain> result = Database.ShipCaptains.Update(shipCaptain).Entity;
            Database.SaveChanges();
            return result;
        }
    }
}
