using Pokedex.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<PokemonDto> pokemons = [
    new(1, "Pikachu", "Electric", 35, 55, 40, "ksjlkljak"),
    new(2, "Charizard", "Fire/Flying", 78, 84, 78, "kljksl"),
    new(3, "Bulbasur", "Grass/Poison", 45, 49, 49, "ioijwl"),
    new(4, "Squirtle", "Water", 44, 48, 65, "klkjlaj"),
    new(5, "Gengar", "Ghost/Poison", 60, 65, 60, "lkjlja")
];

//GET/pokemon
app.MapGet("/pokemons", () => pokemons);

app.MapGet("/pokemons/{id}", (int id)=> pokemons.Find(pokemon => pokemon.Id == id));

app.Run();
