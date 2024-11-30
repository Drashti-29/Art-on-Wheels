using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ArtOnWheels.Models
{
    // Represents a car entity in the system, which can carry artworks to exhibitions
    public class Car
    {
        // Primary key for the Car entity
        [Key]
        public int CarId { get; set; }

        // Type of car (e.g., "Vintage", "Race", "Electric") used for carrying artworks
        public required string Type { get; set; }

        // URL for an image of the car, used for display purposes
        public string ImageUrl { get; set; }

        // Navigation property: List of exhibitions where this car has been used
        public virtual ICollection<Exhibition> Exhibitions { get; set; }

        // Navigation property: List of artworks that this car has transported
        public virtual ICollection<Artwork> Artworks { get; set; }
    }

    // Data Transfer Object (DTO) for transferring Car data without full entity structure
    public class CarDto
    {
        // Unique identifier for the car, matching primary key in Car entity
        public int CarId { get; set; }

        // Type of car (e.g., "Vintage", "Race", "Electric") for identification purposes
        public required string Type { get; set; }

        // Image URL for the car, used in display contexts
        public string ImageUrl { get; set; }

        // Collection of artwork IDs associated with this car, useful for simple referencing of artworks
        public List<int> ArtworkIds { get; set; } = new List<int>();

        // Optionally, collection of ArtworkDto objects for detailed information about each artwork
        public List<String> ArtworkTiles { get; set; }

     }
}
