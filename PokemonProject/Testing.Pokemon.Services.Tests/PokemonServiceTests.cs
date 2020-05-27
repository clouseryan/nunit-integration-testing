using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Testing.Pokemon.Data.Entities;
using Testing.Pokemon.Services.Tests.Fixtures;
using EFCore.BulkExtensions;

namespace Testing.Pokemon.Services.Tests
{
    public class PokemonServiceTests : PokedexFixture
    {
        [Test]
        [Category("Integration")]
        public async Task Verify_Save_Correctly()
        {
            var newEntity = TestTools.BuildPokemon(
                id: 0,
                name: TestTools.BuildPokemonName(pokemonId: 0),
                baseStat: TestTools.BuildBaseStat(pokemonId: 0)
            );

            var result = await Services.PokemonService.UpSert(newEntity);

            var dbEntity = await PokeContext.Pokemon
                .AsNoTracking()
                .Where(x => x.Id == result.Id)
                .SingleOrDefaultAsync();

            Assert.NotNull(dbEntity);
        }

        private static Data.Entities.Pokemon[] PokemonForUpdateTest = 
        {
            TestTools.BuildPokemon(
                id: 0, 
                name: TestTools.BuildPokemonName(pokemonId: 0, english: "Squirtle"), 
                baseStat: TestTools.BuildBaseStat(pokemonId: 0)
            ),
            TestTools.BuildPokemon(
                id: 0,
                name: TestTools.BuildPokemonName(pokemonId: 0, english: "Bulbasaur"),
                baseStat: TestTools.BuildBaseStat(pokemonId: 0)
            )
        };

        [Test]
        [Category("Integration")]
        [TestCaseSource(nameof(PokemonForUpdateTest))]
        public async Task Verify_Update_Correctly(Data.Entities.Pokemon newEntity)
        {
            var result = await Services.PokemonService.UpSert(newEntity);

            // detach the entity so it does not get updated during second service call
            PokeContext.Entry(result).State = EntityState.Detached;

            var dbEntity = await PokeContext.Pokemon
                .AsNoTracking()
                .Include(x => x.Name)
                .Where(x => x.Id == result.Id)
                .SingleOrDefaultAsync();

            Assert.NotNull(dbEntity);

            dbEntity.Name.English = "Charmander";

            var updatedResult = await Services.PokemonService.UpSert(dbEntity);

            Assert.AreEqual(result.Id, updatedResult.Id);
            Assert.AreNotEqual(result.Name.English, updatedResult.Name.English);
        }

        [Test]
        [Category("Integration")]
        public async Task Demo_Seeding_Database()
        {
            var pokeJson = File.ReadAllText("./TestData/pokedex.json");
            var pokeDeserialized = JsonSerializer.Deserialize<List<Data.Entities.Pokemon>>(pokeJson);
            var pokeNames = pokeDeserialized.Select(x =>
            {
                x.Name.PokemonId = x.Id;
                return x.Name;
            }).ToList();

            var pokeStats = pokeDeserialized.Select(x =>
            {
                x.BaseStat.PokemonId = x.Id;
                return x.BaseStat;
            }).ToList();

            await PokeContext.BulkInsertAsync<Data.Entities.Pokemon>(pokeDeserialized);
            await PokeContext.BulkInsertAsync<Data.Entities.PokemonName>(pokeNames);
            await PokeContext.BulkInsertAsync<Data.Entities.BaseStat>(pokeStats);

            var totalPokemonInDatabase = await PokeContext.Pokemon.LongCountAsync();

            Assert.AreEqual(pokeDeserialized.Count, totalPokemonInDatabase);
        }
    }
}
