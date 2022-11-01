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

        private Cargo() { }
        private Cargo(Guid id, string name, int quantity, bool flammable)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Flammable = flammable;
        }

        public static Result<Cargo> Create(Guid id, string name, int quentity, bool flammable)
        {
            if (String.IsNullOrWhiteSpace(name))
                return Result.Failure<Cargo>("You didnt enterd name for cargo");
            if (quentity <= 0)
                return Result.Failure<Cargo>("Quentity cant be zero or negative");
            Result<Cargo> ret = new Cargo(id, name, quentity, flammable);
            return ret;
        }
    }
}
