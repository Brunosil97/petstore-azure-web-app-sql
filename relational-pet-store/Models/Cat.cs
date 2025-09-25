using System.ComponentModel.DataAnnotations;

namespace relational_pet_store.Models;

public class Cat
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Breed { get; set; } = string.Empty;

    public int Age { get; set; }

    [StringLength(20)]
    public string Color { get; set; } = string.Empty;

    public bool IsIndoor { get; set; }

    public bool IsDeclawed { get; set; }

    public bool IsGoodWithKids { get; set; }

    public bool IsGoodWithOtherPets { get; set; }

    public int PlayfulnessLevel { get; set; } // 1-10 scale

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<CatList> CatLists { get; set; } = new List<CatList>();
}