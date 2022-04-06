using System;
using System.Collections.Generic;
using System.Text;
using Core.Model.Workers;
using Core.Repository;
using CSharpFunctionalExtensions;

namespace Core.Service
{
    public class WarehouseClerkService
    {
        private readonly IWarehouseClerkRepository WarehouseClerkRepository;
        public WarehouseClerkService(IWarehouseClerkRepository warehouseClerkRepository)
        {
            WarehouseClerkRepository = warehouseClerkRepository;
        }

        public Result<WarehouseClerk> Create(WarehouseClerk warehouseClerk)
        {
            Result<WarehouseClerk> ret = WarehouseClerkRepository.Create(warehouseClerk);
            return Result.Success(ret.Value);
        }

        public Maybe<WarehouseClerk> DeleteById(Guid id)
        {
            Maybe<WarehouseClerk> WarehouseClerk = FindById(id);
            if (WarehouseClerk.HasNoValue)
                return Maybe.None;
            return WarehouseClerkRepository.DeleteById(id);
        }

        public Maybe<WarehouseClerk> FindById(Guid id)
        {
            var warehouseClerk = WarehouseClerkRepository.FindById(id);
            return warehouseClerk == null ? Maybe.None : warehouseClerk;
        }

        public IEnumerable<WarehouseClerk> GetAll()
        {
            return WarehouseClerkRepository.GetAll();
        }

        public Result<WarehouseClerk> Update(WarehouseClerk warehouse)
        {
            Result<WarehouseClerk> ret = WarehouseClerkRepository.Update(warehouse);
            return Result.Success(ret.Value);
        }
    }
}
