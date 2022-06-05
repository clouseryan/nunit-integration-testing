using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Testing.Pokemon.Data.DataAccess;

namespace Testing.Pokemon.Services
{
    public abstract class BaseService
    {
        internal readonly PokeContext _context;
        internal readonly ILogger _logger;
        public BaseService(PokeContext pokeContext, ILogger logger)
        {
            _context = pokeContext;
            _logger = logger;
        }
    }
}
