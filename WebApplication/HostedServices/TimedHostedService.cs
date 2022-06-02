using Core.Model;
using Core.Model.TransportStates;
using Core.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebShipPort.HostedServices
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private ShipService _shipService;
        private TransportService _transportService;
        private ShipPortService _shipPortService;
        private ShipCaptainService _shipCaptainService;
        private IConfiguration _configuration;
        private readonly ILogger<TimedHostedService> _logger;
        private Timer _timer = null;
        private double executeOnPeriod;

        public TimedHostedService( ILogger<TimedHostedService> logger, ShipService shipService,
            TransportService transportService, ShipPortService shipPortService, IConfiguration configuration, ShipCaptainService shipCaptainService)
        {
            _logger = logger;
            _shipService = shipService;
            _transportService = transportService;
            _shipPortService = shipPortService;
            _shipCaptainService = shipCaptainService;
            _configuration = configuration;
            executeOnPeriod = Double.Parse(_configuration.GetSection("TimeHostedService").Value);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromMinutes(executeOnPeriod));

            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            foreach (var transport in _transportService.GetAllActive() ?? Enumerable.Empty<Transport>())
            {
                if (IsTransportFinished(transport))
                {
                    SetTransportStatus(transport);
                    var ship = _shipService.ChangeShipLocation(transport);
                    if (ship.IsFailure)
                        continue;
                    _shipCaptainService.ChangeShipCaptainsLocation(transport);
                    //to do 
                    //change crew ship port

                }
            }
        }

        private static bool IsTransportFinished(Transport transport)
        {
            return transport.TimeTo < DateTime.Now;
        }

        private void SetTransportStatus(Transport transport)
        {
            transport.TransportState = new FinishedTransport();
            _transportService.Update(transport);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
        public void Dispose()
        {
            _timer.Dispose();
        }
    }
}
