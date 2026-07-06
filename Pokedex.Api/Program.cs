using Pokedex.Api.Dtos;

const string GETpokemonEndpointName = "GETpokemon";

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
app.MapGet("/pokemons", () => pokemons).WithName(GETpokemonEndpointName);

//Get/pokemon/id
app.MapGet("/pokemons/{id}", (int id)=> pokemons.Find(pokemon => pokemon.Id == id));

//POST/pokemon
app.MapPost("/pokemons", (CreatePokemonDto newPokemon) =>
{
    PokemonDto pokemon = new(
        pokemons.Count + 1,
        newPokemon.Name,
        newPokemon.Type,
        newPokemon.HP,
        newPokemon.Attack,
        newPokemon.Defense,
        newPokemon.ImageUrl
    );

    pokemons.Add(pokemon);

    return Results.CreatedAtRoute(GETpokemonEndpointName, new {id = pokemon.Id}, pokemon);
});

// PUT/pokemon/{id}
app.MapPut("/pokemons/{id}", (int id, UpdatePokemonDto updatePokemon) =>
{
    var index = pokemons.FindIndex(pokemon => pokemon.Id == id);

    pokemons[index] = new PokemonDto(
        id,
        updatePokemon.Name,
        updatePokemon.Type,
        updatePokemon.HP,
        updatePokemon.Attack,
        updatePokemon.Defense,
        updatePokemon.ImageUrl
    );

    return Results.NoContent();

});




app.Run();
