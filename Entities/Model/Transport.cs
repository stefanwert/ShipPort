using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Model
{
    public class Transport
    {
        public Guid Id { get; }

        public DateTime TimeFrom { get; }

        public DateTime TimeTo { get; }
    }
}
