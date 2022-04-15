using Core.Model;
using Core.Model.Workers;
using Core.Service;
using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;
using WebShipPort.DTO;

namespace WebShipPort.Factory
{
    public class TransportFactory
    {
        private readonly ShipCaptainService _shipCaptainService;

        //private readonly ShipService _shipService;

        private readonly CrewService _crewService;

        //private readonly ShipPortService _shipPortService;


        public TransportFactory(ShipCaptainService shipCaptainService, CrewService crewService)
        {
            _shipCaptainService = shipCaptainService;
            _crewService = crewService;
        }
        public Result<Transport> Create(TransportDTO transportDTO)
        {
            //does not need to have ship captain
            var currentShipCaptain = _shipCaptainService.FindById(transportDTO.CurrentShipCaptain.Id);


            if (transportDTO.ShipCaptains == null)
                return Result.Failure<Transport>("You didnt sent trasnport ship captains !");

            List<ShipCaptain> shipCaptains = new List<ShipCaptain>();
            foreach (var shipCaptain in transportDTO.ShipCaptains)
            {
                var shipC = _shipCaptainService.FindById(shipCaptain.Id);
                if (shipC.HasValue)
                    shipCaptains.Add(shipC.Value);
            }

            List<Crew> crew = new List<Crew>();
            foreach(var crewMember in transportDTO.Crew)
            {
                var crewM = _crewService.FindById(crewMember.Id);
                if (crewM.HasValue)
                    crew.Add(crewM.Value);
            }
            //add logic for ShipService and ShipPortService
            Result<Transport> transport = Transport.Create(transportDTO.Id, transportDTO.TimeFrom, transportDTO.TimeTo, null, shipCaptains, crew, null, null, transportDTO.TransportState, currentShipCaptain.GetValueOrDefault());
            if(transport.IsFailure)
                return Result.Failure<Transport>("Error while creating transport :"+transport.Error);
            return transport;
        }
    }
}
