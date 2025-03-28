using System.ComponentModel.DataAnnotations;

namespace PROGETTO_U5_S3_L5.DTOs.Biglietto {
    public class CreateBigliettoRequestDto {
        [Required]
        public required Guid EventoId {
            get; set;
        }

        [Required]
        public required string UserId {
            get; set;
        }
    }
}
