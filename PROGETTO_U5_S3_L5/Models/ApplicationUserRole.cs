using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace PROGETTO_U5_S3_L5.Models {
    public class ApplicationUserRole : IdentityUserRole<string> {
        [Required]
        public Guid UserId {
            get; set;
        }

        [Required]
        public Guid RoleId {
            get; set;
        }

        [Required]
        public DateTime Date {
            get; set;
        }

        [ForeignKey("UserId")]
        public ApplicationUser User {
            get; set;
        }

        [ForeignKey("RoleId")]
        public ApplicationRole Role {
            get; set;
        }
    }
}