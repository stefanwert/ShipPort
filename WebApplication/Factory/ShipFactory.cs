﻿using Core.Model;
using Core.Service;
using CSharpFunctionalExtensions;
using WebShipPort.DTO;

namespace WebShipPort.Factory
{
    public class ShipFactory
    {
        private readonly ShipPortService ShipPortService;

        public ShipFactory(ShipPortService shipPortService)
        {
            ShipPortService = shipPortService;
        }
        public Result<Ship> Create(ShipDTO shipDTO)
        {
            var shipPort = ShipPortService.FindById(shipDTO.ShipPort.Id);
            if (shipPort.HasNoValue)
                return Result.Failure<Ship>($"Ship port with id:{shipDTO.ShipPort.Id} dont exist !");

            var shipCreated = Ship.Create(shipDTO.Id, shipDTO.Name, shipDTO.Price, shipPort.Value);
            if (shipCreated.IsFailure)
                return Result.Failure<Ship>(shipCreated.Error);

            return shipCreated.Value;
        }
    }
}