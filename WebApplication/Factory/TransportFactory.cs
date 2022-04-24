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

        private readonly ShipService _shipService;

        private readonly CrewService _crewService;

        private readonly ShipPortService _shipPortService;


        public TransportFactory(ShipCaptainService shipCaptainService, CrewService crewService, ShipService shipService, ShipPortService shipPortService)
        {
            _shipPortService = shipPortService;
            _shipService = shipService;
            _shipCaptainService = shipCaptainService;
            _crewService = crewService;
        }
        public Result<Transport> Create(TransportDTO transportDTO)
        {
            //does not need to have ship captain
            Maybe<ShipCaptain> currentShipCaptain = null;
            if (transportDTO.CurrentShipCaptain != null)
                currentShipCaptain = _shipCaptainService.FindById(transportDTO.CurrentShipCaptain.Id);

            if (transportDTO.ShipCaptains == null && transportDTO.ShipCaptains.Any())
                return Result.Failure<Transport>("You didnt set trasnport ship captains !");

            List<ShipCaptain> shipCaptains = new List<ShipCaptain>();
            foreach (var shipCaptain in transportDTO.ShipCaptains)
            {
                var shipC = _shipCaptainService.FindById(shipCaptain.Id);
                if (shipC.HasValue)
                    shipCaptains.Add(shipC.Value);
            }

            List<Crew> crew = new List<Crew>();
            foreach (var crewMember in transportDTO.Crew)
            {
                var crewM = _crewService.FindById(crewMember.Id);
                if (crewM.HasValue)
                    crew.Add(crewM.Value);
            }

            var ship = _shipService.FindById(transportDTO.Ship.Id);
            if (ship.HasNoValue)
                return Result.Failure<Transport>($"Ship with id:{transportDTO.Ship.Id} dont exist");

            var shipPortFrom = _shipPortService.FindById(transportDTO.ShipPortFrom.Id);
            if (shipPortFrom.HasNoValue)
                return Result.Failure<Transport>($"Ship port from with id:{transportDTO.ShipPortFrom.Id} dont exist");

            var shipPortTo = _shipPortService.FindById(transportDTO.ShipPortTo.Id);
            if (shipPortTo.HasNoValue)
                return Result.Failure<Transport>($"Ship port to with id:{transportDTO.ShipPortTo.Id} dont exist");

            Result<Transport> transport = Transport.Create(transportDTO.Id,
                transportDTO.TimeFrom, transportDTO.TimeTo, ship.Value, shipCaptains, crew,
                shipPortFrom.Value, shipPortTo.Value, transportDTO.TransportState, currentShipCaptain.GetValueOrDefault());

            if (transport.IsFailure)
                return Result.Failure<Transport>("Error while creating transport :" + transport.Error);

            return transport;
        }

        //create dto from object
        //public Result<TransportDTO> Create(Transport transport)
        //{
        //    new TransportDTO()
        //    {
                
        //    }
        //}
    }
}
