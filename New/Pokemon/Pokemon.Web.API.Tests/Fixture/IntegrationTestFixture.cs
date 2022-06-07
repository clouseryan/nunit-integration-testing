using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Pokemon.Data.DataAccess;
using Pokemon.Web.API.Tests.Tools;

namespace Pokemon.Web.API.Tests.Fixture;

[TestFixture]
public class IntegrationTestFixture
{
    HttpClient Client;

    [OneTimeSetUp]
    public async Task Init()
    {
        var application = new WebApplicationFactory<Program>();

        var context = application.Services.GetRequiredService<PokeContext>();
        await context.Pokemon.AddAsync(EntityTools.BuildPokemon());
        Client = application.CreateClient();
    }

    [SetUp]
    public async Task BeforeEach()
    {
        
    }

    [TearDown]
    public async Task AfterEach()
    {
        
    }

    [OneTimeTearDown]
    public async Task Destroy()
    {
        
    }
}