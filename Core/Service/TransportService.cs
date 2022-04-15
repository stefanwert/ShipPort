using Core.Model;
using Core.Repository;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Service
{
    public class TransportService
    {
        private readonly ITransportRepository TransportRepository;

        public TransportService(ITransportRepository transportRepository)
        {
            TransportRepository = transportRepository;
        }

        public Result<Transport> Create(Transport transport)
        {
            Result<Transport> ret = TransportRepository.Create(transport);
            return Result.Success(ret.Value);
        }

        public Maybe<Transport> DeleteById(Guid id)
        {
            Maybe<Transport> Transport = FindById(id);
            if (Transport.HasNoValue)
                return Maybe.None;
            return TransportRepository.DeleteById(id);
        }

        public Maybe<Transport> FindById(Guid id)
        {
            var transport = TransportRepository.FindById(id);
            return transport == null ? Maybe.None : transport;
        }

        public IEnumerable<Transport> GetAll()
        {
            return TransportRepository.GetAll();
        }

        public Result<Transport> Update(Transport transport)
        {
            Result<Transport> ret = TransportRepository.Update(transport);
            return Result.Success(ret.Value);
        }
    }
}
