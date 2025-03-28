using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROGETTO_U5_S3_L5.Models {
    [Table("Artisti")]
    public class Artista {
        [Key]
        public Guid ArtistaId {
            get; set;
        }

        [Required]
        public string Nome {
            get; set;
        }

        [Required]
        public string Genere {
            get; set;
        }

        [Required]
        public string Biografia {
            get; set;
        }

        public ICollection<Evento> Eventi {
            get; set;
        }
    }
}
