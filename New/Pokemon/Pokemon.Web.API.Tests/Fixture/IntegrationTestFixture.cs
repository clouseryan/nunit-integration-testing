using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Pokemon.Data.DataAccess;
using Pokemon.Web.API.Tests.Tools;

namespace Pokemon.Web.API.Tests.Fixture;

[TestFixture]
public class IntegrationTestFixture
{
    internal HttpClient Client;
    private WebApplicationFactory<Program> Application;
    private PokeContext Context;

    [OneTimeSetUp]
    public async Task Init()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "integrationTesting");
        Application = new WebApplicationFactory<Program>();

        Context = Application.Services.GetRequiredService<PokeContext>();
        await Context.Database.EnsureCreatedAsync();
        await Context.Pokemon.AddAsync(EntityTools.BuildPokemon());
        await Context.SaveChangesAsync();
        Client = Application.CreateClient();
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
        await Context?.Database?.EnsureDeletedAsync();
        Context?.Dispose();
        Client?.Dispose();
        Application?.Dispose();
    }
}