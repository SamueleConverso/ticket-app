using System.ComponentModel.DataAnnotations;

namespace PROGETTO_U5_S3_L5.DTOs.Artista {
    public class UpdateArtistaResponseDto {
        [Required]
        public required string Message {
            get; set;
        }
    }
}
