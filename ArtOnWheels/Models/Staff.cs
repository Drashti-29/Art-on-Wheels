using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtOnWheels.Models
{
    // Represents a staff member involved in exhibitions
    public class Staff
    {
        // Primary key for the Staff entity
        [Key]
        public int StaffId { get; set; }

        // First name of the staff member
        public string FirstName { get; set; }

        // Last name of the staff member
        public string LastName { get; set; }

        // Position or job title of the staff member (e.g., "Coordinator", "Guide")
        public string Position { get; set; }

        // Contact information for the staff member (e.g., phone or email)
        public string Contact { get; set; }

        // Foreign key linking this staff member to a specific exhibition
        [ForeignKey("Exhibition")]
        public int ExhibitionId { get; set; }

        // Navigation property: the exhibition this staff member is associated with
        public virtual Exhibition Exhibition { get; set; }
    }

    // Data Transfer Object (DTO) for transferring Staff data without the full entity structure
    public class StaffDto
    {
        // Unique identifier for the staff member, matching the primary key in Staff entity
        public int StaffId { get; set; }

        // First name of the staff member
        public string FirstName { get; set; }

        // Last name of the staff member
        public string LastName { get; set; }

        // Position or job title of the staff member
        public string Position { get; set; }

        // Contact information for the staff member
        public string Contact { get; set; } 

        // Foreign key linking this staff member to a specific exhibition
        public int ExhibitionId { get; set; }

        // Optional: name of the exhibition for reference in the DTO, useful for displaying the exhibition name alongside staff details
        public string ExhibitionName { get; set; }
    }
}
