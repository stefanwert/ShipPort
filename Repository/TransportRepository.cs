using Core.Model;
using Core.Model.test;
using Core.Model.TransportStates;
using Core.Repository;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer
{
    public class TransportRepository : ITransportRepository
    {
        private Database Database;

        public TransportRepository(Database database)
        {
            Database = database;
        }
        public Result<Transport> Create(Transport transport)
        {
            //transport.ShipPortFrom = null;
            //transport.ShipPortTo = null;
            //transport.Ship = null;
            //transport.ShipPortFrom = Database.ShipPorts.Where(x => x.Id == transport.ShipPortFrom.Id).First();
            //transport.ShipPortTo = Database.ShipPorts.Where(x => x.Id == transport.ShipPortTo.Id).First();
            //transport.Ship = Database.Ships.Where(x => x.Id == transport.Ship.Id).First();
            Result<Transport> ret = Database.Transports.Add(transport).Entity;
            Database.SaveChanges();
            return ret;
            //testmethod();
            //return Result.Failure<Transport>("asd");
        }

        //private void testmethod()
        //{
        //    House house = new House()
        //    {
        //        HouseName = "name222",
        //        HouseSize = 2,
        //    };
        //    var houseSaved = Database.Houses.Add(house);
        //    Database.SaveChanges();

        //    var listOfHouses = new List<House>();
        //    listOfHouses.Add(houseSaved.Entity);
        //    Child child = new Child()
        //    {
        //        ChildName = "child Name2222",
        //        Houses = listOfHouses,
        //    };
        //    Database.Children.Add(child);
        //    Database.SaveChanges();
        //}

        public Maybe<Transport> DeleteById(Guid id)
        {
            var transport = Database.Transports.First(x => x.Id == id);
            Database.Transports.Remove(transport);
            Database.SaveChanges();
            return transport;
        }

        public Maybe<Transport> FindById(Guid id)
        {
            var transport = Database.Transports.Where(x => x.Id == id)
                .Include(x => x.Crew)
                .Include(x => x.ShipCaptains)
                .Include(x => x.ShipPortFrom)
                .Include(x => x.ShipPortTo)
                .Include(x=>x.Ship)
                .Include(x=>x.CurrentShipCaptain)
                .FirstOrDefault();
            return transport == null ? Maybe.None : transport;
        }

        public IEnumerable<Transport> GetAll()
        {
            return Database.Transports
                .Include(x => x.Crew)
                .Include(x => x.ShipCaptains)
                .Include(x => x.ShipPortFrom)
                .Include(x => x.ShipPortTo)
                .Include(x=>x.CurrentShipCaptain)
                .Include(x => x.Ship);
        }

        public Result<Transport> Update(Transport transport)
        {
            Result<Transport> result = Database.Transports.Update(transport).Entity;
            Database.SaveChanges();
            return result;
        }

        public IEnumerable<Transport> GetAllActive()
        {
            return Database.Transports.Include(x => x.Crew)
                .Include(x => x.ShipCaptains)
                .Include(x => x.ShipPortFrom)
                .Include(x => x.ShipPortTo)
                .Include(x => x.Ship)
                .Include(x => x.CurrentShipCaptain)
                .Where(x => x.TransportState.Equals(Transporting.Name));
        }
        public ICollection<Transport> FindByShipPortId(Guid id)
        {
            return Database.Transports.Include(x => x.Crew)
                .Include(x => x.ShipCaptains)
                .Include(x => x.ShipPortFrom)
                .Include(x => x.ShipPortTo)
                .Include(x => x.Ship)
                .Include(x => x.CurrentShipCaptain)
                .Where(x => x.ShipPortFrom.Id == id || x.ShipPortTo.Id == id).ToList();
        }

    }
}
