using Core.Model;
using Core.Model.TransportStates;
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
        private readonly TransportService _transportService;


        public TransportFactory(ShipCaptainService shipCaptainService, CrewService crewService, ShipService shipService, ShipPortService shipPortService, TransportService transportService)
        {
            _shipPortService = shipPortService;
            _shipService = shipService;
            _shipCaptainService = shipCaptainService;
            _crewService = crewService;
            _transportService = transportService;
        }
        public Result<Transport> Create(TransportDTO transportDTO)
        {

            List<ShipCaptain> shipCaptains = PopulateShipCaptains(transportDTO);

            List<Crew> crew = PopulateCrew(transportDTO);

            var ship = _shipService.FindById(transportDTO.Ship.Id);
            if (ship.HasNoValue)
                return Result.Failure<Transport>($"Ship with id:{transportDTO.Ship.Id} dont exist");

            var shipPortFrom = _shipPortService.FindByIdWithOutRelationships(transportDTO.ShipPortFrom.Id);
            if (shipPortFrom.HasNoValue)
                return Result.Failure<Transport>($"Ship port from with id:{transportDTO.ShipPortFrom.Id} dont exist");

            var shipPortTo = _shipPortService.FindByIdWithOutRelationships(transportDTO.ShipPortTo.Id);
            if (shipPortTo.HasNoValue)
                return Result.Failure<Transport>($"Ship port to with id:{transportDTO.ShipPortTo.Id} dont exist");

            Maybe<ShipCaptain> currentShipCaptain = null; 
            if (transportDTO.CurrentShipCaptain != null)
                currentShipCaptain = _shipCaptainService.FindById(transportDTO.CurrentShipCaptain.Id);
            var shipCaptain = currentShipCaptain.HasValue ? currentShipCaptain.Value : null;

            Result<Transport> transport = Transport.Create(transportDTO.Id,
                transportDTO.TimeFrom, transportDTO.TimeTo, ship.Value, shipCaptains, crew,
                shipPortFrom.Value, shipPortTo.Value, transportDTO.TransportState, shipCaptain);

            if (transport.IsFailure)
                return Result.Failure<Transport>("Error while creating transport :" + transport.Error);

            return transport;
        }

        public Result<Transport> CreateForUpdate(TransportDTO transportDTO)
        {
            var transport = _transportService.FindById(transportDTO.Id);
            if (transport.Value.TransportState.ToString().Equals(CreatingTransport.Name))
            {
                if (transportDTO.TimeFrom != null)
                    transport.Value.TimeFrom = transportDTO.TimeFrom;

                if(transportDTO.TimeTo != null)
                    transport.Value.TimeTo = transportDTO.TimeTo;
                if(transportDTO.ShipCaptains != null)
                {
                    List<ShipCaptain> shipCaptains = PopulateShipCaptains(transportDTO);
                    transport.Value.ShipCaptains.Clear();
                    foreach (var sc in shipCaptains)
                        transport.Value.ShipCaptains.Add(sc);
                }
                if(transportDTO.Crew != null)
                {
                    List<Crew> crew = PopulateCrew(transportDTO);
                    transport.Value.Crew.Clear();
                    foreach (var c in crew)
                        transport.Value.Crew.Add(c);
                }

                if(transportDTO.Ship != null)
                    transport.Value.Ship = _shipService.FindById(transportDTO.Ship.Id).Value;
            }
                
            if(transportDTO.CurrentShipCaptain != null && transport.Value.TransportState.ToString().Equals(Transporting.Name))
            {
                var shipCaptain = _shipCaptainService.FindById(transportDTO.CurrentShipCaptain.Id);
                if(shipCaptain.HasValue)
                    transport.Value.CurrentShipCaptainId = shipCaptain.Value.Id;
            }

            return transport.Value;
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
