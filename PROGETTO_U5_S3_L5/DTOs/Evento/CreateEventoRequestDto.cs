using System.ComponentModel.DataAnnotations;

namespace PROGETTO_U5_S3_L5.DTOs.Evento {
    public class CreateEventoRequestDto {
        [Required]
        public string Titolo {
            get; set;
        }

        [Required]
        public DateTime Data {
            get; set;
        }

        [Required]
        public string Luogo {
            get; set;
        }

        [Required]
        public Guid ArtistaId {
            get; set;
        }
    }
}
