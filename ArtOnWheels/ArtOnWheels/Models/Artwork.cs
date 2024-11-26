using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ArtOnWheels.Models
{
    // Represents an artwork entity in the system
    public class Artwork
    {
        // Primary key for the Artwork entity
        [Key]
        public int ArtworkId { get; set; }

        // Title of the artwork
        public string Title { get; set; }

        // Description of the artwork, providing details or context
        public string Description { get; set; }

        // Year the artwork was created
        public int CreationYear { get; set; }

        // Price of the artwork, represented as a decimal for precision
        public decimal Price { get; set; }

        // URL for an image of the artwork
        public string ImageUrl { get; set; }

        // Foreign key property to link artwork to an artist
        [ForeignKey("Artist")]
        public int ArtistId { get; set; }

        // Navigation property: Each artwork is created by one artist
        public virtual Artist Artist { get; set; }

        // Many-to-Many relationship: An artwork can be featured in multiple exhibitions
        public virtual ICollection<Exhibition> Exhibitions { get; set; }
    }

    // Data Transfer Object (DTO) for transferring Artwork data without full entity structure
    public class ArtworkDto
    {
        // Unique identifier for the artwork, matching primary key in Artwork entity
        public int ArtworkId { get; set; }

        // Title of the artwork, for display or transfer purposes
        public string Title { get; set; }

        // Description of the artwork, providing context for external use
        public string Description { get; set; }

        // Year the artwork was created, used in summary data or listings
        public int CreationYear { get; set; }

        // Price of the artwork, for transfer and display in summaries or listings
        public decimal Price { get; set; }

        // Foreign key to associate artwork with an artist
        public int ArtistId { get; set; }

        // Optionally include artist name to simplify referencing artist information in the DTO
        public string ArtistName { get; set; }

        // List of exhibitions where the artwork is featured, represented as ExhibitionDto objects
        public List<String> ExhibitionNames { get; set; }
    }
}
