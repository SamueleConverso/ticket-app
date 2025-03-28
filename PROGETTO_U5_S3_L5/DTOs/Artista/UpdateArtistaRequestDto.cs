using System.ComponentModel.DataAnnotations;

namespace PROGETTO_U5_S3_L5.DTOs.Artista {
    public class UpdateArtistaRequestDto {
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
