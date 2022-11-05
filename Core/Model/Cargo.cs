using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class Cargo
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public bool Flammable { get; private set; }
        public string Image { get; private set; }
        public Guid? TransportId { get; private set; }
        public Guid? WarehouseId { get; private set; }


        private Cargo() { }
        private Cargo(Guid id, string name, int quantity, bool flammable, string image)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Flammable = flammable;
            Image = image;
        }

        private Cargo(Guid id, string name, int quantity, bool flammable, string image, Guid? transportId, Guid? warehouseId)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Flammable = flammable;
            Image = image;
            TransportId = transportId;
            WarehouseId = warehouseId;
        }

        public static Result<Cargo> Create(Guid id, string name, int quentity, bool flammable, string image)
        {
            if (String.IsNullOrWhiteSpace(name))
                return Result.Failure<Cargo>("You didnt enterd name for cargo");
            if (quentity <= 0)
                return Result.Failure<Cargo>("Quentity cant be zero or negative");
            Result<Cargo> ret = new Cargo(id, name, quentity, flammable, image);
            return ret;
        }

        public static Result<Cargo> Create(Guid id, string name, int quentity, bool flammable, string image, Guid? transportId, Guid? warehouseId)
        {
            if (String.IsNullOrWhiteSpace(name))
                return Result.Failure<Cargo>("You didnt enterd name for cargo");
            if (quentity <= 0)
                return Result.Failure<Cargo>("Quentity cant be zero or negative");
            Result<Cargo> ret = new Cargo(id, name, quentity, flammable, image, transportId, warehouseId);
            return ret;
        }
    }
}
