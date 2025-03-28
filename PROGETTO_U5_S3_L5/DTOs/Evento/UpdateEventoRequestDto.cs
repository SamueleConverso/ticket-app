using System.ComponentModel.DataAnnotations;

namespace PROGETTO_U5_S3_L5.DTOs.Evento {
    public class UpdateEventoRequestDto {
        [Required]
        public required string Titolo {
            get; set;
        }

        [Required]
        public required DateTime Data {
            get; set;
        }

        [Required]
        public required string Luogo {
            get; set;
        }
    }
}
