using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Core.Model.Workers;
using Core.Repository;

namespace DataLayer
{
    public class WarehouseClerkRepository : IWarehouseClerkRepository
    {
        private Database Database;

        public WarehouseClerkRepository(Database database)
        {
            Database = database;
        }

        public Result<WarehouseClerk> Create(WarehouseClerk warehouseClerk)
        {
            Result<WarehouseClerk> ret = Database.WarehouseClerks.Add(warehouseClerk).Entity;
            Database.SaveChanges();
            return ret;
        }

        public Maybe<WarehouseClerk> DeleteById(Guid id)
        {
            var warehouseClerk = Database.WarehouseClerks.First(x => x.Id == id);
            Database.WarehouseClerks.Remove(warehouseClerk);
            Database.SaveChanges();
            return warehouseClerk;
        }

        public Maybe<WarehouseClerk> FindById(Guid id)
        {
            var warehouseClerk = Database.WarehouseClerks.Find(id);
            return warehouseClerk == null ? Maybe.None : warehouseClerk;
        }

        public IEnumerable<WarehouseClerk> GetAll()
        {
            return Database.WarehouseClerks;
        }

        public Result<WarehouseClerk> Update(WarehouseClerk warehouseClerk)
        {
            Result<WarehouseClerk> result = Database.WarehouseClerks.Update(warehouseClerk).Entity;
            Database.SaveChanges();
            return result;
        }
    }
}
