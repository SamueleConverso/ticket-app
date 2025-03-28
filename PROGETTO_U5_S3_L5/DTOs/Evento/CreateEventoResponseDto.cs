using System.ComponentModel.DataAnnotations;

namespace PROGETTO_U5_S3_L5.DTOs.Evento {
    public class CreateEventoResponseDto {
        [Required]
        public required string Message {
            get; set;
        }
    }
}
