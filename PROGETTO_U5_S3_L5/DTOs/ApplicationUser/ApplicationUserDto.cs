using System.ComponentModel.DataAnnotations;

namespace PROGETTO_U5_S3_L5.DTOs.ApplicationUser {
    public class ApplicationUserDto {
        [Required]
        public required string Id {
            get; set;
        }


        [Required]
        public required string FirstName {
            get; set;
        }

        [Required]
        public required string LastName {
            get; set;
        }
    }
}
