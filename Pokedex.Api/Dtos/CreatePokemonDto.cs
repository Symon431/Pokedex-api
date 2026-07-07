using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Pokedex.Api.Dtos
{
    public record CreatePokemonDto(
        [Required][StringLength(50)]string Name,
        [Required][StringLength(20)]string Type,
        [Range(1, 100)]int HP, //Health Points
        [Range(1, 100)]int Attack, //Atack Strength
        [Range(1, 100)]int Defense, //Defense Strength
        string? ImageUrl
    );
}