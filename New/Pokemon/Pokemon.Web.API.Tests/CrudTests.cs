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

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

        var pokemon = JsonSerializer.Deserialize<Data.Models.Pokemon>(await response.Content.ReadAsStringAsync());
        
        Assert.Multiple(() =>
        {
            Assert.That(pokemon.Id, Is.EqualTo(model.Id), $"Expected ids to match, but response id was {pokemon.Id} and not {model.Id}");
            Assert.That(pokemon.Attack, Is.EqualTo(model.Attack), $"Expected attacks to match, but response attack was {pokemon.Attack} and not {model.Attack}");
            Assert.That(pokemon.Defense, Is.EqualTo(model.Defense), $"Expected defense to match, but response defense was {pokemon.Defense} and not {model.Defense}");
            Assert.That(pokemon.Speed, Is.EqualTo(model.Speed), $"Expected speed to match, but response speed was {pokemon.Speed} and not {model.Speed}");
            Assert.That(pokemon.Types, Is.EqualTo(model.Types), $"Expected types to match, but response types were {JsonSerializer.Serialize(pokemon.Types)} and not {JsonSerializer.Serialize(model.Types)}");
            Assert.That(pokemon.ChineseName, Is.EqualTo(model.ChineseName), $"Expected chinese names to match, but response chinese name was {pokemon.ChineseName} and not {model.ChineseName}");
            Assert.That(pokemon.JapaneseName, Is.EqualTo(model.JapaneseName), $"Expected japanese names to match, but response japanese name was {pokemon.JapaneseName} and not {model.JapaneseName}");
            Assert.That(pokemon.EnglishName, Is.EqualTo(model.EnglishName), $"Expected english names to match, but response english name was {pokemon.EnglishName} and not {model.EnglishName}");
            Assert.That(pokemon.HitPoints, Is.EqualTo(model.HitPoints), $"Expected hit points to match, but response hit points was {pokemon.HitPoints} and not {model.HitPoints}");
            Assert.That(pokemon.SpecialAttack, Is.EqualTo(model.SpecialAttack), $"Expected special attacks to match, but response special attack was {pokemon.SpecialAttack} and not {model.SpecialAttack}");
            Assert.That(pokemon.SpecialDefense, Is.EqualTo(model.SpecialDefense), $"Expected special defense to match, but response special defense was {pokemon.SpecialDefense} and not {model.SpecialDefense}");
        });
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
        var model = ModelTools.BuildPokemon(id: 3);
        var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
        var response = await Client.PostAsync("api/Pokemon", content);
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created), $"The initial pokemon failed to create, response status code was {response.StatusCode} and not {HttpStatusCode.Created}");

        var created = JsonSerializer.Deserialize<Data.Models.Pokemon>(await response.Content.ReadAsStringAsync());

        created.Attack = 50;
        created.Defense = 100;

        var updatedContent = new StringContent(JsonSerializer.Serialize(created), Encoding.UTF8, "application/json");
        var updatedResponse = await Client.PutAsync($"api/Pokemon/{created.Id}", updatedContent);
        
        Assert.That(updatedResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), $"The updated response status code was {updatedResponse.StatusCode} and not {HttpStatusCode.OK}");
        
        Assert.Multiple(async () =>
        {
            var updatedPokemon = JsonSerializer.Deserialize<Data.Models.Pokemon>(await updatedResponse.Content.ReadAsStringAsync());
            Assert.That(updatedPokemon.Attack, Is.EqualTo(50), $"Attack was not updated correctly. Expected 50, but was {updatedPokemon.Attack}");
            Assert.That(updatedPokemon.Defense, Is.EqualTo(100), $"Defense was not updated correctly. Expected 100, but was {updatedPokemon.Defense}");
        });
    }

    [Test]
    public async Task Verify_Delete()
    {
        var model = ModelTools.BuildPokemon(id: 4);
        var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
        var response = await Client.PostAsync("api/Pokemon", content);
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created), $"The initial pokemon failed to create, response status code was {response.StatusCode} and not {HttpStatusCode.Created}");

        var created = JsonSerializer.Deserialize<Data.Models.Pokemon>(await response.Content.ReadAsStringAsync());

        var deleteResponse = await Client.DeleteAsync($"api/Pokemon/{created.Id}");
        
        Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent), $"Status code mismatch. Expected {HttpStatusCode.NoContent} but was {deleteResponse.StatusCode}");
    }

    [Test]
    public async Task Verify_Pokemon_Update_Filter()
    {
        var model = ModelTools.BuildPokemon(id: 5);
        var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
        var response = await Client.PostAsync("api/Pokemon", content);
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created), $"The initial pokemon failed to create, response status code was {response.StatusCode} and not {HttpStatusCode.Created}");

        var created = JsonSerializer.Deserialize<Data.Models.Pokemon>(await response.Content.ReadAsStringAsync());
        
        created.Attack = 50;
        created.Defense = 100;

        var updatedContent = new StringContent(JsonSerializer.Serialize(created), Encoding.UTF8, "application/json");
        var updatedResponse = await Client.PutAsync("api/Pokemon/500", updatedContent);
        
        Assert.That(updatedResponse.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        var bodyContent = await updatedResponse.Content.ReadAsStringAsync();
        
        Assert.That(bodyContent, Is.EqualTo("route and object data mismatch"));
    }
}