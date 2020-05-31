
using System;
using System.Threading;
using System.Threading.Tasks;
using Fivet.ZeroIce.model;
using Ice;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace Fivet.Server
{

    internal class FivetService : IHostedService, IDisposable
    {
        /// <summary>
        /// The Logger
        /// </summary>
        private readonly ILogger<FivetService> _logger;

        /// <summary>
        /// The Communicator.
        /// </summary>
        private readonly Communicator _communicator;

        /// <summary>
        /// The Port.
        /// </summary>
        private readonly int _port = 8080;

        /// <summary>
        /// The System.
        /// </summary>
        private readonly TheSystemDisp_ _theSystem;

        /// <summary>
        /// The Contratos.
        /// </summary>
        private readonly ContratosDisp_ _contratos;

        /// <summary>
        /// The FivetService.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="theSystem"></param>
        /// <param name="contratos"></param>
        public FivetService(ILogger<FivetService> logger, TheSystemDisp_ theSystem, ContratosDisp_ contratos)
        {
            _logger = logger;
            _logger.LogDebug("Building FivetService ..");
            _theSystem = theSystem;
            _contratos = contratos;
            _communicator = buildCommunicator();
        }

        /// <summary>
        /// Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Starting the FivetService...");

            //The Adapter 
            var adapter = _communicator.createObjectAdapterWithEndpoints("TheSystem", "tcp -z -t 15000 -p " + _port);

            // Register in the communicator
            adapter.add(_theSystem, Util.stringToIdentity("TheSystem"));

            // Activation
            adapter.activate();

            // Done.
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping the FivetService...");

            _communicator.shutdown();

            _logger.LogDebug("Communicator Stopped!");

            return Task.CompletedTask;
        }

        private Communicator buildCommunicator()
        {
            _logger.LogDebug("Initializing Communicator v{0} ({1}) .. ", Ice.Util.stringVersion(), Ice.Util.intVersion());

            //ZeroC Properties
            Properties properties = Util.createProperties();
            // https://doc.zeroc.com/ice/latest/property-reference/ice-trace
            // properties.setProperty("Ice.Trace.Admin.Properties", "1");
            // properties.setProperty("Ice.Trace.Locator", "2");
            // properties.setProperty("Ice.Trace.Network", "3");
            // properties.setProperty("Ice.Trace.Protocol", "1");
            // properties.setProperty("Ice.Trace.Slicing", "1");
            // properties.setProperty("Ice.Trace.ThreadPool", "1");
            // properties.setProperty("Ice.Compression.Level", "9");

            InitializationData initializationData = new InitializationData();
            initializationData.properties = properties;
            return Ice.Util.initialize(initializationData);
        }

        /// <summary>
        /// Clear the memory.
        /// </summary>
        public void Dispose()
        {
            _communicator.destroy();
        }




    }

}