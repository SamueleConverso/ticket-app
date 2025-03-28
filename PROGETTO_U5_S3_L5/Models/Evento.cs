using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROGETTO_U5_S3_L5.Models {
    [Table("Eventi")]
    public class Evento {
        [Key]
        public Guid EventoId {
            get; set;
        }

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

        [ForeignKey("ArtistaId")]
        public Artista Artista {
            get; set;
        }

        public ICollection<Biglietto> Biglietti {
            get; set;
        }
    }
}
