namespace Pokedex.Api.Models
{
    public class Pokemon
    {
        public int Id { get; set; }

        public required string Name{ get; set ;} 

        public PokemonType? Type { get; set; }

        public int TypeId { get; set; }

        public required int Hp { get; set; }

        public required int Attack{ get; set; }

        public required int Defense{ get; set; }

        public string? ImageUrl{ get; set; }

    }
}