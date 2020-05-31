using Fivet.Dao;
using Fivet.ZeroIce.model;
using Ice;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Fivet.ZeroIce
{

    /// <summary>
    /// Implementation of the Contratos.
    /// </summary>
    public class ContratosImpl : ContratosDisp_
    {
        /// <summary>
        ///  The logger
        /// </summary>
        private readonly ILogger<ContratosImpl> _logger;

        /// <summary>
        ///  The provider of DbContext
        /// </summary>
        private readonly IServiceScopeFactory _serviceScopeFactory;

        /// <summary>
        ///  The Contructor.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="serviceProviderFactory"></param>
        public ContratosImpl(ILogger<ContratosImpl> logger,
                                    IServiceScopeFactory serviceProviderFactory)
        {
            _logger = logger;
            _logger.LogDebug("Building the ContratosImpl ..");
            _serviceScopeFactory = serviceProviderFactory;

            // Create the database
            _logger.LogInformation("Creating the Database ..");
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                FivetContext fc = scope.ServiceProvider.GetService<FivetContext>();
                fc.Database.EnsureCreated();
                fc.SaveChanges();
            }

            _logger.LogDebug("Done. ");

        }

        public override Ficha obtenerFicha(int numero, Current current = null)
        {
            throw new System.NotImplementedException();
        }

        public override bool registrarFicha(Ficha ficha, Current current = null)
        {
            throw new System.NotImplementedException();
        }

        public override bool registrarDuenio(Persona persona, Current current = null)
        {
            // Using the local scope
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                FivetContext fc = scope.ServiceProvider.GetService<FivetContext>();
                fc.Personas.Add(persona);
                fc.SaveChanges();
                return true;
            }
        }

        public override bool registrarControl(Control control, Current current = null)
        {
            throw new System.NotImplementedException();
        }

        public override bool agregarFoto(Foto foto, Current current = null)
        {
            throw new System.NotImplementedException();
        }

        

    }

}