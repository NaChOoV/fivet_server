using System;
using Fivet.ZeroIce.model;
using Ice;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Fivet.ZeroIce
{

    /// <summary>
    /// The Implementation of TheSysstem interface.
    /// </summary>
    public class TheSystemImpl : TheSystemDisp_
    {
        /// <summary>
        /// The Logger.
        /// </summary>
        private readonly ILogger<TheSystemImpl> _logger;

        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="serviceScopeFactory"></param>
        public TheSystemImpl(ILogger<TheSystemImpl> logger, 
                        IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _logger.LogDebug("Building TheSystemImpl ..");
        }

        /// <summary>
        ///  Return the difference in time between Server and Client.
        /// </summary>
        /// <param name="clientTime"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        public override long getDelay(long clientTime, Current current = null)
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - clientTime;
        }
    }

    


}