using System;
using System.Collections.Generic;
using System.Linq;
using Core.Model;
using Core.Repository;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly Database Database;

        public WarehouseRepository(Database database)
        {
            Database = database;
        }

        public Result<Warehouse> Create(Warehouse warehouse)
        {
            Result<Warehouse> ret = Database.Warehouses.Add(warehouse).Entity;
            Database.SaveChanges();
            return ret;
        }

        public Maybe<Warehouse> DeleteById(Guid id)
        {
            var warehouse = Database.Warehouses.First(x=>x.Id == id);
            Database.Warehouses.Remove(warehouse);
            Database.SaveChanges();
            return warehouse;
        }

        public Maybe<Warehouse> FindById(Guid id)
        {
            var warehouse = Database.Warehouses.Find(id);
            return warehouse == null? Maybe.None : warehouse;
        }

        public IEnumerable<Warehouse> GetAll()
        {
            return Database.Warehouses;
        }

        public IEnumerable<Warehouse> GetAllWithCargos()
        {
            return Database.Warehouses.Include(x => x.Cargos);
        }

        public Result<Warehouse> Update(Warehouse warehouse)
        {
            Result<Warehouse> result = Database.Warehouses.Update(warehouse).Entity;
            Database.SaveChanges();
            return result.Value;
        }

        public ICollection<Warehouse> FindByShipPortId(Guid id)
        {
            var shipPort = Database.ShipPorts.Include(x => x.Warehouses).Where(s => s.Id == id).FirstOrDefault();
            return shipPort?.Warehouses;
        }

    }
}
