using System.Net;
using System.Text;
using System.Text.Json;
using Pokemon.Web.API.Tests.Fixture;
using Pokemon.Web.API.Tests.Tools;

namespace Pokemon.Web.API.Tests;

public class CrudTests : IntegrationTestFixture
{
    [Test]
    public async Task Verify_Create()
    {
        var model = ModelTools.BuildPokemon();
        var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
        var response = await Client.PostAsync("api/Pokemon", content);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task Verify_Get()
    {
        var response = await Client.GetAsync($"api/Pokemon/1");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK),
            $"Expected {HttpStatusCode.OK} but got {response.StatusCode}");

        var pokemon = JsonSerializer.Deserialize<Data.Models.Pokemon>(await response.Content.ReadAsStringAsync());

        Assert.Multiple(() =>
        {
            Assert.That(pokemon.Id, Is.EqualTo(1));
            Assert.That(pokemon.EnglishName, Is.EqualTo("Bulbasaur"));
        });
    }

    [Test]
    public async Task Verify_Not_Found_Error()
    {
        var response = await Client.GetAsync($"api/Pokemon/9999999");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound),
            $"Expected {HttpStatusCode.NotFound} but got {response.StatusCode}");
    }

    [Test]
    public async Task Verify_Update()
    {
        
    }

    [Test]
    public async Task Verify_Delete()
    {
        
    }
}