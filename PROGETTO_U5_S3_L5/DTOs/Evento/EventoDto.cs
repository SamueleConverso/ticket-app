using System.ComponentModel.DataAnnotations;
using PROGETTO_U5_S3_L5.DTOs.Artista;

namespace PROGETTO_U5_S3_L5.DTOs.Evento {
    public class EventoDto {
        [Required]
        public required Guid EventoId {
            get; set;
        }

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
