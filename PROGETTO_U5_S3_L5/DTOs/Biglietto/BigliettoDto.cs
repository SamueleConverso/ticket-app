using PROGETTO_U5_S3_L5.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PROGETTO_U5_S3_L5.DTOs.Evento;
using PROGETTO_U5_S3_L5.DTOs.ApplicationUser;

namespace PROGETTO_U5_S3_L5.DTOs.Biglietto {
    public class BigliettoDto {
        public Guid BigliettoId {
            get; set;
        }

        [Required]
        public DateTime DataAcquisto {
            get; set;
        }

        public EventoDto2 EventoDto2 {
            get; set;
        }

        public ApplicationUserDto ApplicationUserDto {
            get; set;
        }
    }
}
