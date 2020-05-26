
using System;
using System.Threading;
using System.Threading.Tasks;
using Fivet.ZeroIce.model;
using Ice;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace Fivet.Server
{

    internal class FivetService : IHostedService
    {

        private readonly ILogger<FivetService> _logger;
        private readonly Communicator _communicator;
        private readonly int _port = 8080;
        public FivetService(ILogger<FivetService> logger)
        {
            _logger = logger;
            _communicator = buildCommunicator();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Starting the FivetService...");
            //The Adapter 
            var adapter = _communicator.createObjectAdapterWithEndpoints("Contratos", "tcp -z -t 15000 -p " + _port);

            //The interface    
            //TheSystem theSystem = new TheSystemImpl();

            Contratos contratos = new ContratosImpl();
            adapter.add(contratos, Util.stringToIdentity("Contratos"));
            adapter.activate();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting the FivetService...");
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

        public class TheSystemImpl : TheSystemDisp_
        {

            public override long getDelay(long clientTime, Current current = null)
            {
                return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - clientTime;
            }
        }

        public class ContratosImpl : ContratosDisp_
        {
            public override Ficha obtenerFicha(int numero, Current current){
                Ficha myFicha = new Ficha(1,2,"Perro","18-11-1996","Salchicha",Sexo.MACHO,"Rubio",TipoPaciente.INTERNO);
                return myFicha;

            }

            public override bool registrarFicha(Ficha ficha, Current current = null){

                return false;
            }

            public override bool registrarDuenio(Persona persona, Current current = null){
                return false;
            }

            public override bool registrarControl(Control control, Current current = null){
                return false;
            }

            public override bool agregarFoto(Foto foto, Current current = null){
                return  false;
            }
        }







    }

}