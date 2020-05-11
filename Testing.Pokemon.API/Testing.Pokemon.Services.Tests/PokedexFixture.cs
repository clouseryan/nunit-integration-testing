using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Testing.Pokemon.Data.DataAccess;

namespace Testing.Pokemon.Services.Tests
{
    [TestFixture]
    public class PokedexFixture
    {
        internal PokeContext PokeContext;
        internal PokedexServices Services;
        internal Mock<ILogger> MockedLogger;

        /// <summary>
        /// Ran before any test execution
        /// </summary>
        [OneTimeSetUp]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<PokeContext>()
                .UseSqlServer("Server=localhost;Initial Catalog=VTT_Test;MultipleActiveResultSets=true;User ID=SA;Password=Test123!;");

            PokeContext = new PokeContext(builder.Options);
            PokeContext.Database.EnsureCreated();

            MockedLogger = new Mock<ILogger>();

            Services = new PokedexServices(PokeContext, MockedLogger.Object);
        }

        /// <summary>
        /// Ran after ALL tests have finishe executing
        /// </summary>
        [OneTimeTearDown]
        public void Teardown()
        {
            if (PokeContext != null)
            {
                PokeContext.Database.EnsureDeleted();
                PokeContext.Dispose();
            }
        }

        /// <summary>
        /// Ran before EACH test
        /// </summary>
        [SetUp]
        public void BeforeEach()
        {

        }

        /// <summary>
        /// Ran after EACH test
        /// </summary>
        [TearDown]
        public void AfterEach()
        {

        }
    }
}
