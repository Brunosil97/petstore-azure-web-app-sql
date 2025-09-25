using System.ComponentModel.DataAnnotations;

namespace relational_pet_store.Models;

public class NamedList
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<DogList> DogLists { get; set; } = new List<DogList>();
    public ICollection<CatList> CatLists { get; set; } = new List<CatList>();
}