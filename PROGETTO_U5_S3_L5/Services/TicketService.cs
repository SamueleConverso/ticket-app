using Microsoft.EntityFrameworkCore;
using PROGETTO_U5_S3_L5.Data;
using PROGETTO_U5_S3_L5.DTOs.Artista;
using PROGETTO_U5_S3_L5.DTOs.Biglietto;
using PROGETTO_U5_S3_L5.DTOs.Evento;
using PROGETTO_U5_S3_L5.Models;
using PROGETTO_U5_S3_L5.Services;

namespace PROGETTO_U5_S3_L5.Services {
    public class TicketService {
        private ApplicationDbContext _context;
        private readonly LoggerService _loggerService;

        public TicketService(ApplicationDbContext context, LoggerService loggerService) {
            _context = context;
            _loggerService = loggerService;
        }

        private async Task<bool> SaveAsync() {
            try {
                var rows = await _context.SaveChangesAsync();

                if (rows > 0) {
                    return true;
                } else {
                    return false;
                }
            } catch (Exception ex) {
                //Console.WriteLine(ex.Message);
                _loggerService.LogError("Errore durante il salvataggio: " + ex);
                return false;
            }
        }

        //++++++METODI ARTISTA++++++

        public async Task<bool> AddArtistaAsync(Artista artista) {
            try {
                _context.Artisti.Add(artista);
                return await SaveAsync();
            } catch (Exception ex) {
                //Console.WriteLine(ex.Message);
                _loggerService.LogError("Errore durante l'aggiunta dell'artista: " + ex);
                return false;
            }
        }

        public async Task<List<Artista>> GetAllArtistiAsync() {
            var artisti = new List<Artista>();

            try {
                artisti = await _context.Artisti.Include(a => a.Eventi).ToListAsync();
                return artisti;
            } catch (Exception ex) {
                artisti = new List<Artista>();
                Console.WriteLine(ex.Message);
            }

            return artisti;
        }

        public async Task<Artista> GetArtistaByIdAsync(Guid id) {
            Artista artista = null;

            try {
                artista = await _context.Artisti.Include(a => a.Eventi).FirstOrDefaultAsync(a => a.ArtistaId == id);
                return artista;
            } catch (Exception ex) {
                artista = null;
                Console.WriteLine(ex.Message);
            }

            return artista;
        }

        public async Task<bool> UpdateArtistaAsync(Guid id, UpdateArtistaRequestDto updateArtistaRequestDto) {
            try {
                var artistaTrovato = await GetArtistaByIdAsync(id);

                if (artistaTrovato == null) {
                    return false;
                }

                artistaTrovato.Nome = updateArtistaRequestDto.Nome;
                artistaTrovato.Genere = updateArtistaRequestDto.Genere;
                artistaTrovato.Biografia = updateArtistaRequestDto.Biografia;

                return await SaveAsync();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteArtistaAsync(Guid id) {
            try {
                var artista = await GetArtistaByIdAsync(id);

                if (artista == null) {
                    return false;
                }

                _context.Artisti.Remove(artista);

                return await SaveAsync();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //++++++METODI EVENTO++++++

        public async Task<bool> AddEventoAsync(Evento evento) {
            try {
                _context.Eventi.Add(evento);
                return await SaveAsync();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<List<Evento>> GetAllEventiAsync() {
            var eventi = new List<Evento>();

            try {
                eventi = await _context.Eventi.Include(e => e.Artista).ToListAsync();
                return eventi;
            } catch (Exception ex) {
                eventi = new List<Evento>();
                Console.WriteLine(ex.Message);
            }

            return eventi;
        }

        public async Task<Evento> GetEventoByIdAsync(Guid id) {
            Evento evento = null;

            try {
                evento = await _context.Eventi.Include(e => e.Artista).FirstOrDefaultAsync(e => e.EventoId == id);
                return evento;
            } catch (Exception ex) {
                evento = null;
                Console.WriteLine(ex.Message);
            }

            return evento;
        }

        public async Task<bool> UpdateEventoAsync(Guid id, UpdateEventoRequestDto updateEventoRequestDto) {
            try {
                var eventoTrovato = await GetEventoByIdAsync(id);

                if (eventoTrovato == null) {
                    return false;
                }

                eventoTrovato.Titolo = updateEventoRequestDto.Titolo;
                eventoTrovato.Data = updateEventoRequestDto.Data;
                eventoTrovato.Luogo = updateEventoRequestDto.Luogo;

                return await SaveAsync();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteEventoAsync(Guid id) {
            try {
                var evento = await GetEventoByIdAsync(id);

                if (evento == null) {
                    return false;
                }

                _context.Eventi.Remove(evento);

                return await SaveAsync();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //++++++METODI BIGLIETTO++++++

        public async Task<bool> AddBigliettoAsync(Biglietto biglietto) {
            try {
                _context.Biglietti.Add(biglietto);
                return await SaveAsync();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<List<Biglietto>> GetAllBigliettiAsync() {
            var biglietti = new List<Biglietto>();

            try {
                biglietti = await _context.Biglietti.Include(b => b.Evento).ThenInclude(e => e.Artista).Include(b => b.ApplicationUser).ToListAsync();
                return biglietti;
            } catch (Exception ex) {
                biglietti = new List<Biglietto>();
                Console.WriteLine(ex.Message);
            }

            return biglietti;
        }

        public async Task<Biglietto> GetBigliettoByIdAsync(Guid id) {
            Biglietto biglietto = null;

            try {
                biglietto = await _context.Biglietti.Include(b => b.Evento).ThenInclude(e => e.Artista).Include(b => b.ApplicationUser).FirstOrDefaultAsync(b => b.BigliettoId == id);
                return biglietto;
            } catch (Exception ex) {
                biglietto = null;
                Console.WriteLine(ex.Message);
            }

            return biglietto;
        }

        public async Task<bool> UpdateBigliettoAsync(Guid id, UpdateBigliettoRequestDto updateBigliettoRequestDto) {
            try {
                var bigliettoTrovato = await GetBigliettoByIdAsync(id);

                if (bigliettoTrovato == null) {
                    return false;
                }

                bigliettoTrovato.UserId = updateBigliettoRequestDto.UserId;
                bigliettoTrovato.EventoId = updateBigliettoRequestDto.EventoId;

                return await SaveAsync();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteBigliettoAsync(Guid id) {
            try {
                var biglietto = await GetBigliettoByIdAsync(id);

                if (biglietto == null) {
                    return false;
                }

                _context.Biglietti.Remove(biglietto);

                return await SaveAsync();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<List<Biglietto>> GetAllBigliettiAreaPrivataAsync(string userId) {
            var biglietti = new List<Biglietto>();

            try {
                biglietti = await _context.Biglietti.Where(b => b.UserId == userId).Include(b => b.Evento).ThenInclude(e => e.Artista).Include(b => b.ApplicationUser).ToListAsync();
                return biglietti;
            } catch (Exception ex) {
                biglietti = new List<Biglietto>();
                Console.WriteLine(ex.Message);
            }

            return biglietti;
        }
    }
}
