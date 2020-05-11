using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Entities = Testing.Pokemon.Data.Entities;

namespace Testing.Pokemon.Data.DataAccess
{
    public class PokeContext : DbContext
    {
        public PokeContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Entities.Pokemon> Pokemon { get; set; }
        public DbSet<Entities.PokemonName> PokemonNames { get; set; }
        public DbSet<Entities.BaseStat> BaseStats { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Entities.Pokemon>(options =>
            {
                options.HasKey(p => p.Id);
                options.HasOne(p => p.BaseStat);
                options.HasOne(p => p.PokemonName);
                options.ToTable("Pokemon", "POK");
                options.Property(x => x.Id).HasColumnName("Number");
            });

            builder.Entity<Entities.PokemonName>(options =>
            {
                options.HasKey(p => p.PokemonId);
                options.ToTable("PokemonNames", "POK");
            });

            builder.Entity<Entities.BaseStat>(options =>
            {
                options.HasKey(p => p.PokemonId);
                options.ToTable("BaseStats", "STT");
                options.Property(x => x.SpAttack).HasColumnName("SpecialAttack");
                options.Property(x => x.SpDefense).HasColumnName("SpecialDefense");
            });
        }
    }
}
