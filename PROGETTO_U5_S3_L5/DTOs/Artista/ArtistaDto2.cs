using System.ComponentModel.DataAnnotations;
using PROGETTO_U5_S3_L5.DTOs.Evento;

namespace PROGETTO_U5_S3_L5.DTOs.Artista {
    public class ArtistaDto2 {
        [Required]
        public required Guid ArtistaId {
            get; set;
        }

        [Required]
        public required string Nome {
            get; set;
        }

        [Required]
        public required string Genere {
            get; set;
        }

        [Required]
        public required string Biografia {
            get; set;
        }
    }
}
