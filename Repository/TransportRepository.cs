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
            var transport = Database.Transports.Find(id);
            return transport == null ? Maybe.None : transport;
        }

        public IEnumerable<Transport> GetAll()
        {
            return Database.Transports;
        }

        public Result<Transport> Update(Transport transport)
        {
            Result<Transport> result = Database.Transports.Update(transport).Entity;
            Database.SaveChanges();
            return result;
        }
    }
}
