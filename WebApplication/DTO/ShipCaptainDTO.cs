using Core.Model.Workers;

namespace WebShipPort.DTO
{
    public class ShipCaptainDTO : WorkerDTO
    {
        public int SailingHoursTotal { get; set; }

        public int SailingHoursAsCaptain { get; set; }

        public ShipCaptainDTO() { }

        public ShipCaptainDTO(ShipCaptain shipCaptain):base(shipCaptain)
        {
            SailingHoursTotal = shipCaptain.SailingHoursTotal;
            SailingHoursAsCaptain = shipCaptain.SailingHoursAsCaptain;
        }
    }
}
