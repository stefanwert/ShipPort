using Core.Model;
using Core.Model.test;
using Core.Model.TransportStates;
using Core.Model.Workers;
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
            Result<Transport> ret = Database.Transports.Add(transport).Entity;
            Database.SaveChanges();
            return ret;
        }


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
                .Include(x => x.Cargos)
                .Include(x => x.Ship);
        }

        public Result<Transport> Update(Transport transport)
        {
            //Result<Transport> result = Database.Transports.Update(transport).Entity;
            //var test = Database.Transports.Local.Any(e => e.Id == transport.Id);
            Database.SaveChanges();
            return FindById(transport.Id).Value;
        }

        public IEnumerable<Transport> GetAllTransporting()
        {
            var t = GetAll();
            return t.Where(x => 
            x.TransportState.ToString().Equals(Transporting.Name));
        }
        public IEnumerable<Transport> GetAllCreating()
        {
            return GetAll()
                .Where(x => x.TransportState.ToString().Equals(CreatingTransport.Name));
        }
        public ICollection<Transport> FindByShipPortId(Guid id)
        {
            return Database.Transports.Include(x => x.Crew)
                .Include(x => x.ShipCaptains)
                .Include(x => x.ShipPortFrom)
                .Include(x => x.ShipPortTo)
                .Include(x => x.Ship)
                .Include(x => x.CurrentShipCaptain)
                .Include(x => x.Cargos)
                .Where(x => x.ShipPortFrom.Id == id || x.ShipPortTo.Id == id).ToList();
        }

    }
}
