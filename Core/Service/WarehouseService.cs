using System;
using System.Collections.Generic;
using System.Text;
using Core.Model;
using Core.Repository;
using CSharpFunctionalExtensions;

namespace Core.Service
{
    public class WarehouseService
    {
        private readonly IWarehouseRepository WarehouseRepository;
        public WarehouseService(IWarehouseRepository warehouseRepository)
        {
            WarehouseRepository = warehouseRepository;
        }

        public Result<Warehouse> Create(Warehouse warehouse)
        {
            Result<Warehouse> ret = WarehouseRepository.Create(warehouse);
            return Result.Success(ret.Value);
        }

        public Maybe<Warehouse> DeleteById(Guid id)
        {
            Maybe<Warehouse> Warehouse = FindById(id);
            if (Warehouse.HasNoValue)
                return Maybe.None;
            return WarehouseRepository.DeleteById(id);
        }

        public Maybe<Warehouse> FindById(Guid id)
        {
            var warehouse = WarehouseRepository.FindById(id);
            return warehouse == null ? Maybe.None : warehouse;
        }

        public IEnumerable<Warehouse> GetAll()
        {
            return WarehouseRepository.GetAll();
        }

        public Result<Warehouse> Update(Warehouse warehouse)
        {
            Result<Warehouse> ret = WarehouseRepository.Update(warehouse);
            return Result.Success(ret.Value);
        }
    }
}
