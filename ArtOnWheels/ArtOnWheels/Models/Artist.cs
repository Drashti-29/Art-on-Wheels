using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ArtOnWheels.Models
{
    // Represents an artist entity in the system
    public class Artist
    {
        // Primary key for the Artist entity
        [Key]
        public int ArtistId { get; set; }

        // Artist's first name
        public string FirstName { get; set; }

        // Artist's last name
        public string LastName { get; set; }

        // A short biography of the artist
        public string ArtistBio { get; set; }

        // Artist's contact email address
        public string Email { get; set; }

        // Collection of artworks created by the artist, establishing a one-to-many relationship
        public ICollection<Artwork> Artworks { get; set; }
    }

    // Data Transfer Object (DTO) for transferring Artist data without the entire entity structure
    public class ArtistDto
    {
        // Unique identifier for the artist, matching the primary key in the Artist entity
        public int ArtistId { get; set; }

        // Artist's first name, for display or transfer purposes
        public string FirstName { get; set; }

        // Artist's last name, for display or transfer purposes
        public string LastName { get; set; }

        // A summary of the artist's background and career
        public string ArtistBio { get; set; }

        // Artist's contact email, typically used for display or external communication
        public string Email { get; set; }

        // Collection of artwork IDs associated with this artist, useful for transferring only the IDs
        public List<int> ArtworkIds { get; set; } = new List<int>();

        // Optional collection of ArtworkDto objects to provide detailed information about each artwork
        public List<ArtworkDto> Artworks { get; set; } = new List<ArtworkDto>();
    }
}
