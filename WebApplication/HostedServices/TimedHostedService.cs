using Core.Model;
using Core.Model.TransportStates;
using Core.Service;
using DataLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        //    private ShipService _shipService;
        //    private TransportService _transportService;
        //    private ShipPortService _shipPortService;
        //    private ShipCaptainService _shipCaptainService;
        private IConfiguration _configuration;
        private readonly ILogger<TimedHostedService> _logger;
        private Timer _timer = null;
        private double executeOnPeriod;
        private IServiceScopeFactory _serviceScopeFactory;

        //public TimedHostedService( ILogger<TimedHostedService> logger, ShipService shipService,
        //    TransportService transportService, ShipPortService shipPortService, IConfiguration configuration, ShipCaptainService shipCaptainService)
        //{
        //    _logger = logger;
        //    _shipService = shipService;
        //    _transportService = transportService;
        //    _shipPortService = shipPortService;
        //    _shipCaptainService = shipCaptainService;
        //    _configuration = configuration;
        //    executeOnPeriod = Double.Parse(_configuration.GetSection("TimeHostedService").Value);
        //}
        public TimedHostedService(ILogger<TimedHostedService> logger, IConfiguration configuration, IServiceScopeFactory serviceScopeFactory)
        {
            _configuration = configuration;
            _logger = logger;
            executeOnPeriod = Double.Parse(_configuration.GetSection("TimeHostedService").Value);
            _serviceScopeFactory = serviceScopeFactory;
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
            Console.WriteLine("hello");
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                //ShipPortService shipPortService = (ShipPortService)scope.ServiceProvider.GetRequiredService(typeof(ShipPortService));
                //var shipports = shipPortService.GetAll();
                TransportService transportService = (TransportService)scope.ServiceProvider.GetRequiredService(typeof(TransportService));
                Database database = (Database)scope.ServiceProvider.GetRequiredService(typeof(Database));
                foreach(var transport in transportService.GetAllTransporting())
                {
                    if (DateTime.Compare(transport.TimeTo, DateTime.Now) <= 0 && transport.IsCurrentStateTransporting())
                    {
                        transport.TransportState = new FinishedTransport();
                    }
                        
                }
                var t = transportService.GetAllCreateing();
                foreach (var transport in t)
                {
                    if (DateTime.Compare(transport.TimeFrom, DateTime.Now) <= 0 && transport.IsCurrentStateCreateing())
                    {
                        transport.TransportState = Transporting.Create().Value;
                    }
                }
                database.SaveChanges();
            }
            //foreach (var transport in _transportService.GetAllActive())
            //{
            //    if (IsTransportFinished(transport))
            //    {
            //        SetTransportStatus(transport);
            //        var ship = _shipService.ChangeShipLocation(transport);
            //        if (ship.IsFailure)
            //            continue;
            //        _shipCaptainService.ChangeShipCaptainsLocation(transport);
            //        //to do 
            //        //change crew ship port

            //    }
            //}
        }

        private static bool IsTransportFinished(Transport transport)
        {
            return transport.TimeTo < DateTime.Now;
        }

        //private void SetTransportStatus(Transport transport)
        //{
        //    transport.TransportState = new FinishedTransport();
        //    _transportService.Update(transport);
        //}

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
