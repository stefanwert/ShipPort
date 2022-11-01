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
    public class CargoRepository : ICargoRepository
    {
        private Database Database;
        public CargoRepository(Database database)
        {
            Database = database;
        }
        public Result<Cargo> Create(Cargo cargo)
        {
            Result<Cargo> ret = Database.Cargos.Add(cargo).Entity;
            Database.SaveChanges();
            return ret;
        }

        public Maybe<Cargo> DeleteById(Guid id)
        {
            var cargo = Database.Cargos.First(x => x.Id == id);
            Database.Cargos.Remove(cargo);
            Database.SaveChanges();
            return cargo;
        }

        public Maybe<Cargo> FindById(Guid id)
        {
            var cargo = Database.Cargos.Find(id);
            return cargo == null ? Maybe.None : cargo;
        }

        public IEnumerable<Cargo> GetAll()
        {
            return Database.Cargos;
        }

        public Result<Cargo> Update(Cargo cargo)
        {
            Result<Cargo> result = Database.Cargos.Update(cargo).Entity;
            Database.SaveChanges();
            return result;
        }
    }
}
