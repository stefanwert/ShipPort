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
            //does not need to have ship captain at begining
            Maybe<ShipCaptain> currentShipCaptain = null;
            if (transportDTO.CurrentShipCaptain != null)
                currentShipCaptain = _shipCaptainService.FindById(transportDTO.CurrentShipCaptain.Id);

            List<ShipCaptain> shipCaptains = PopulateShipCaptains(transportDTO);

            List<Crew> crew = PopulateCrew(transportDTO);

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

        private List<Crew> PopulateCrew(TransportDTO transportDTO)
        {
            List<Crew> crew = new List<Crew>();
            foreach (var crewMember in transportDTO.Crew ?? Enumerable.Empty<CrewDTO>())
            {
                var crewM = _crewService.FindById(crewMember.Id);
                if (crewM.HasValue)
                    crew.Add(crewM.Value);
            }

            return crew;
        }

        private List<ShipCaptain> PopulateShipCaptains(TransportDTO transportDTO)
        {
            List<ShipCaptain> shipCaptains = new List<ShipCaptain>();
            foreach (var shipCaptain in transportDTO.ShipCaptains ?? Enumerable.Empty<ShipCaptainDTO>())
            {
                var shipC = _shipCaptainService.FindById(shipCaptain.Id);
                if (shipC.HasValue)
                    shipCaptains.Add(shipC.Value);
            }

            return shipCaptains;
        }

    }
}
