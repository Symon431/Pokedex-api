using Microsoft.EntityFrameworkCore;
using Pokedex.Api.Models;

/* file makes sure the database schema is upto date*/

namespace Pokedex.Api.Data
{
    public static class DataExtensions
    {
        public static void MigrateDb(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<PokemonStoreContext>();

            dbContext.Database.Migrate();
            
        }

        public static void AddPokemonStoreDb(this WebApplicationBuilder builder)
        {
            var connString = builder.Configuration.GetConnectionString("PokemonStore");

            builder.Services.AddNpgsql<PokemonStoreContext>(
                connString,
                optionsAction: options => options.UseSeeding((context, _) =>
                {
                    if (!context.Set<PokemonType>().Any())
                    {
                        context.Set<PokemonType>().AddRange(
                            new PokemonType { Name = "Fire"},
                            new PokemonType {Name = "Water"},
                            new PokemonType { Name = "Grass"}, 
                            new PokemonType { Name = "Electric"}
                        );

                        context.SaveChanges();

                    }
                }));  
        }


    }
}