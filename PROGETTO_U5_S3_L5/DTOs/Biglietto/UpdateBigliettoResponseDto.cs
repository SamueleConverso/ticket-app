using System.ComponentModel.DataAnnotations;

namespace PROGETTO_U5_S3_L5.DTOs.Biglietto {
    public class UpdateBigliettoResponseDto {
        [Required]
        public required string Message {
            get; set;
        }
    }
}
