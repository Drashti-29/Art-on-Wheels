using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ArtOnWheels.Models
{
    // Represents an exhibition entity in the system
    public class Exhibition
    {
        // Primary key for the Exhibition entity
        [Key]
        public int ExhibitionId { get; set; }

        // Name of the exhibition
        public string ExhibitionName { get; set; }

        // Location where the exhibition is held
        public string Location { get; set; }

        // Date of the exhibition event
        public DateTime Date { get; set; }

        // Navigation property: List of artworks featured in the exhibition (Many-to-Many relationship)
        public virtual ICollection<Artwork> Artworks { get; set; }

        // Navigation property: List of staff members associated with the exhibition (Many-to-Many relationship)
        public virtual ICollection<Staff> Staffs { get; set; }
    }

    // Data Transfer Object (DTO) for transferring Exhibition data without the full entity structure
    public class ExhibitionDto
    {
        // Unique identifier for the exhibition, matching the primary key in Exhibition entity
        public int ExhibitionId { get; set; }

        // Name of the exhibition, for display or transfer purposes
        public string ExhibitionName { get; set; }

        // Location of the exhibition, used in listings or summaries
        public string Location { get; set; }

        // Date of the exhibition event, for scheduling and display
        public DateTime Date { get; set; }

        // Collection of ArtworkDto objects to provide details about each artwork in the exhibition
        public List<ArtworkDto> Artworks { get; set; }

        // Collection of StaffDto objects to provide details about each staff member associated with the exhibition
        public List<StaffDto> Staffs { get; set; }
    }
}
