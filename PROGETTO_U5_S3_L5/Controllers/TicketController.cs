using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PROGETTO_U5_S3_L5.DTOs.ApplicationUser;
using PROGETTO_U5_S3_L5.DTOs.Artista;
using PROGETTO_U5_S3_L5.DTOs.Biglietto;
using PROGETTO_U5_S3_L5.DTOs.Evento;
using PROGETTO_U5_S3_L5.Models;
using PROGETTO_U5_S3_L5.Services;

namespace PROGETTO_U5_S3_L5.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketController : ControllerBase {
        private readonly TicketService _ticketService;
        private readonly LoggerService _loggerService;

        public TicketController(TicketService ticketService, LoggerService loggerService) {
            _ticketService = ticketService;
            _loggerService = loggerService;
        }

        //++++++METODI ARTISTA++++++

        [Authorize(Roles = "Amministratore")]
        [HttpPost("artista")]
        public async Task<IActionResult> AddArtista([FromBody] CreateArtistaRequestDto createArtistaRequestDto) {
            var newArtista = new Artista {
                Nome = createArtistaRequestDto.Nome,
                Genere = createArtistaRequestDto.Genere,
                Biografia = createArtistaRequestDto.Biografia
            };

            var result = await _ticketService.AddArtistaAsync(newArtista);

            if (!result) {
                var logErrorMessage = "Errore nell'aggiunta dell'artista";
                _loggerService.LogError(logErrorMessage);
                return BadRequest(new CreateArtistaResponseDto {
                    Message = logErrorMessage
                });
            }

            var logInfoMessage = "Artista aggiunto con successo";
            _loggerService.LogInformation(logInfoMessage);
            return Ok(new CreateArtistaResponseDto {
                Message = logInfoMessage
            });
        }

        [Authorize(Roles = "Amministratore")]
        [HttpGet("artista")]
        public async Task<IActionResult> GetAllArtisti() {
            var artistiList = await _ticketService.GetAllArtistiAsync();

            if (artistiList == null) {
                var logErrorMessage = "Errore nel recupero degli artisti";
                _loggerService.LogError(logErrorMessage);
                return BadRequest(new {
                    message = logErrorMessage
                });
            }

            if (!artistiList.Any()) {
                return NoContent();
            }

            var artistiResponse = artistiList.Select(a => new ArtistaDto() {
                ArtistaId = a.ArtistaId,
                Nome = a.Nome,
                Genere = a.Genere,
                Biografia = a.Genere,
                EventiDto = a.Eventi != null ? a.Eventi.Select(e => new EventoDto {
                    EventoId = e.EventoId,
                    Titolo = e.Titolo,
                    Data = e.Data,
                    Luogo = e.Luogo
                }).ToList() : null
            });

            var logInfoMessage = "Artisti trovati con successo";
            _loggerService.LogInformation(logInfoMessage);
            return Ok(new {
                message = $"Numero artisti trovati: {artistiResponse.Count()}",
                artisti = artistiResponse
            });
        }

        [Authorize(Roles = "Amministratore")]
        [HttpGet("artista/{id:guid}")]
        public async Task<IActionResult> GetArtistaById(Guid id) {
            var artistaToFind = await _ticketService.GetArtistaByIdAsync(id);

            if (artistaToFind == null) {
                var logErrorMessage = "Errore nel recupero dell'artista";
                _loggerService.LogError(logErrorMessage);
                return BadRequest(new {
                    message = logErrorMessage
                });
            }

            var artistaResponse = new ArtistaDto {
                ArtistaId = artistaToFind.ArtistaId,
                Nome = artistaToFind.Nome,
                Genere = artistaToFind.Genere,
                Biografia = artistaToFind.Biografia,
                EventiDto = artistaToFind.Eventi != null ? artistaToFind.Eventi.Select(e => new EventoDto {
                    EventoId = e.EventoId,
                    Titolo = e.Titolo,
                    Data = e.Data,
                    Luogo = e.Luogo
                }).ToList() : null
            };

            var logInfoMessage = "Artista trovato con successo";
            _loggerService.LogInformation(logInfoMessage);
            return Ok(new {
                message = logInfoMessage,
                artista = artistaResponse
            });
        }

        [Authorize(Roles = "Amministratore")]
        [HttpPut("artista/{id:guid}")]
        public async Task<IActionResult> UpdateArtista(Guid id, [FromBody] UpdateArtistaRequestDto updateArtistaRequestDto) {
            var result = await _ticketService.UpdateArtistaAsync(id, updateArtistaRequestDto);

            if (!result) {
                var logErrorMessage = "Errore nella modifica dell'artista";
                _loggerService.LogError(logErrorMessage);
                return BadRequest(new UpdateArtistaResponseDto {
                    Message = logErrorMessage
                });
            }

            var logInfoMessage = "Artista aggiornato con successo";
            _loggerService.LogInformation(logInfoMessage);
            return Ok(new UpdateArtistaResponseDto {
                Message = logInfoMessage
            });
        }

        [Authorize(Roles = "Amministratore")]
        [HttpDelete("artista/{id:guid}")]
        public async Task<IActionResult> DeleteArtista(Guid id) {
            var result = await _ticketService.DeleteArtistaAsync(id);

            if (!result) {
                var logErrorMessage = "Errore nella cancellazione dell'artista";
                _loggerService.LogError(logErrorMessage);
                return BadRequest(new {
                    message = logErrorMessage
                });
            }

            var logInfoMessage = "Artista cancellato con successo";
            _loggerService.LogInformation(logInfoMessage);
            return Ok(new {
                message = logInfoMessage
            });
        }

        //++++++METODI EVENTO++++++

        [Authorize(Roles = "Amministratore")]
        [HttpPost("evento")]
        public async Task<IActionResult> AddEvento([FromBody] CreateEventoRequestDto createEventoRequestDto) {
            var newEvento = new Evento {
                Titolo = createEventoRequestDto.Titolo,
                Data = createEventoRequestDto.Data,
                Luogo = createEventoRequestDto.Luogo,
                ArtistaId = createEventoRequestDto.ArtistaId
            };

            var result = await _ticketService.AddEventoAsync(newEvento);

            if (!result) {
                var logErrorMessage = "Errore nell'aggiunta' dell'evento";
                _loggerService.LogError(logErrorMessage);
                return BadRequest(new CreateArtistaResponseDto {
                    Message = logErrorMessage
                });
            }

            var logInfoMessage = "Evento aggiunto con successo";
            _loggerService.LogInformation(logInfoMessage);
            return Ok(new CreateArtistaResponseDto {
                Message = logInfoMessage
            });
        }

        [AllowAnonymous]
        [HttpGet("evento")]
        public async Task<IActionResult> GetAllEventi() {
            var eventiList = await _ticketService.GetAllEventiAsync();

            if (eventiList == null) {
                var logErrorMessage = "Errore nel recupero degli eventi";
                _loggerService.LogError(logErrorMessage);
                return BadRequest(new {
                    message = logErrorMessage
                });
            }

            if (!eventiList.Any()) {
                return NoContent();
            }

            var eventiResponse = eventiList.Select(e => new EventoDto2() {
                EventoId = e.EventoId,
                Titolo = e.Titolo,
                Data = e.Data,
                Luogo = e.Luogo,
                ArtistaDto2 = new ArtistaDto2 {
                    ArtistaId = e.Artista.ArtistaId,
                    Nome = e.Artista.Nome,
                    Genere = e.Artista.Genere,
                    Biografia = e.Artista.Biografia
                }
            });

            var logInfoMessage = "Eventi trovati con successo";
            _loggerService.LogInformation(logInfoMessage);
            return Ok(new {
                message = $"Numero eventi trovati: {eventiResponse.Count()}",
                eventi = eventiResponse
            });
        }

        [AllowAnonymous]
        [HttpGet("evento/{id:guid}")]
        public async Task<IActionResult> GetEventoById(Guid id) {
            var eventoToFind = await _ticketService.GetEventoByIdAsync(id);

            if (eventoToFind == null) {
                var logErrorMessage = "Errore nel recupero dell'evento";
                _loggerService.LogError(logErrorMessage);
                return BadRequest(new {
                    message = logErrorMessage
                });
            }

            var eventoResponse = new EventoDto2 {
                EventoId = eventoToFind.EventoId,
                Titolo = eventoToFind.Titolo,
                Data = eventoToFind.Data,
                Luogo = eventoToFind.Luogo,
                ArtistaDto2 = new ArtistaDto2 {
                    ArtistaId = eventoToFind.Artista.ArtistaId,
                    Nome = eventoToFind.Artista.Nome,
                    Genere = eventoToFind.Artista.Genere,
                    Biografia = eventoToFind.Artista.Biografia
                }
            };

            var logInfoMessage = "Evento trovato con successo";
            _loggerService.LogInformation(logInfoMessage);
            return Ok(new {
                message = logInfoMessage,
                evento = eventoResponse
            });
        }

        [Authorize(Roles = "Amministratore")]
        [HttpPut("evento/{id:guid}")]
        public async Task<IActionResult> UpdateEvento(Guid id, [FromBody] UpdateEventoRequestDto updateEventoRequestDto) {
            var result = await _ticketService.UpdateEventoAsync(id, updateEventoRequestDto);

            if (!result) {
                var logErrorMessage = "Errore nella modifica dell'evento";
                _loggerService.LogError(logErrorMessage);
                return BadRequest(new UpdateEventoResponseDto {
                    Message = logErrorMessage
                });
            }

            var logInfoMessage = "Evento aggiornato con successo";
            _loggerService.LogInformation(logInfoMessage);
            return Ok(new UpdateEventoResponseDto {
                Message = logInfoMessage
            });
        }

        [Authorize(Roles = "Amministratore")]
        [HttpDelete("evento/{id:guid}")]
        public async Task<IActionResult> DeleteEvento(Guid id) {
            var result = await _ticketService.DeleteEventoAsync(id);

            if (!result) {
                var logErrorMessage = "Errore nella ancellazione dell'evento";
                _loggerService.LogError(logErrorMessage);
                return BadRequest(new {
                    message = logErrorMessage
                });
            }

            var logInfoMessage = "Evento cancellato con successo";
            _loggerService.LogInformation(logInfoMessage);
            return Ok(new {
                message = logInfoMessage
            });
        }

        //++++++METODI BIGLIETTO++++++

        [HttpPost("biglietto")]
        public async Task<IActionResult> AddBiglietto([FromBody] CreateBigliettoRequestDto createBigliettoRequestDto) {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var newBiglietto = new Biglietto {
                EventoId = createBigliettoRequestDto.EventoId,
                UserId = userId
            };

            var result = await _ticketService.AddBigliettoAsync(newBiglietto);

            if (!result) {
                var logErrorMessage = "Errore nell'aggiunta del biglietto";
                _loggerService.LogError(logErrorMessage);
                return BadRequest(new CreateBigliettoResponseDto {
                    Message = logErrorMessage
                });
            }

            var logInfoMessage = "Biglietto aggiunto con successo";
            _loggerService.LogInformation(logInfoMessage);
            return Ok(new CreateBigliettoResponseDto {
                Message = logInfoMessage
            });
        }

        [Authorize(Roles = "Amministratore")]
        [HttpGet("biglietto")]
        public async Task<IActionResult> GetAllBiglietti() {
            var bigliettiList = await _ticketService.GetAllBigliettiAsync();

            if (bigliettiList == null) {
                var logErrorMessage = "Errore nel recupero dei biglietti";
                _loggerService.LogError(logErrorMessage);
                return BadRequest(new {
                    message = logErrorMessage
                });
            }

            if (!bigliettiList.Any()) {
                return NoContent();
            }

            var bigliettiResponse = bigliettiList.Select(b => new BigliettoDto() {
                BigliettoId = b.BigliettoId,
                DataAcquisto = b.DataAcquisto,
                EventoDto2 = new EventoDto2 {
                    EventoId = b.Evento.EventoId,
                    Titolo = b.Evento.Titolo,
                    Data = b.Evento.Data,
                    Luogo = b.Evento.Luogo,
                    ArtistaDto2 = new ArtistaDto2 {
                        ArtistaId = b.Evento.Artista.ArtistaId,
                        Nome = b.Evento.Artista.Nome,
                        Genere = b.Evento.Artista.Genere,
                        Biografia = b.Evento.Artista.Biografia
                    }
                },
                ApplicationUserDto = new ApplicationUserDto {
                    Id = b.ApplicationUser.Id,
                    FirstName = b.ApplicationUser.FirstName,
                    LastName = b.ApplicationUser.LastName
                }
            });

            var logInfoMessage = "Biglietti trovati con successo";
            _loggerService.LogInformation(logInfoMessage);
            return Ok(new {
                message = $"Numero biglietti trovati: {bigliettiResponse.Count()}",
                biglietti = bigliettiResponse
            });
        }

        [Authorize(Roles = "Amministratore")]
        [HttpGet("biglietto/{id:guid}")]
        public async Task<IActionResult> GetBigliettoById(Guid id) {
            var bigliettoToFind = await _ticketService.GetBigliettoByIdAsync(id);

            if (bigliettoToFind == null) {
                var logErrorMessage = "Errore nel recupero del biglietto";
                _loggerService.LogError(logErrorMessage);
                return BadRequest(new {
                    message = logErrorMessage
                });
            }

            var bigliettoResponse = new BigliettoDto {
                BigliettoId = bigliettoToFind.BigliettoId,
                DataAcquisto = bigliettoToFind.DataAcquisto,
                EventoDto2 = new EventoDto2 {
                    EventoId = bigliettoToFind.Evento.EventoId,
                    Titolo = bigliettoToFind.Evento.Titolo,
                    Data = bigliettoToFind.Evento.Data,
                    Luogo = bigliettoToFind.Evento.Luogo,
                    ArtistaDto2 = new ArtistaDto2 {
                        ArtistaId = bigliettoToFind.Evento.Artista.ArtistaId,
                        Nome = bigliettoToFind.Evento.Artista.Nome,
                        Genere = bigliettoToFind.Evento.Artista.Genere,
                        Biografia = bigliettoToFind.Evento.Artista.Biografia
                    }
                },
                ApplicationUserDto = new ApplicationUserDto {
                    Id = bigliettoToFind.ApplicationUser.Id,
                    FirstName = bigliettoToFind.ApplicationUser.FirstName,
                    LastName = bigliettoToFind.ApplicationUser.LastName
                }
            };

            var logInfoMessage = "Biglietto trovato con successo";
            _loggerService.LogInformation(logInfoMessage);
            return Ok(new {
                message = logInfoMessage,
                biglietto = bigliettoResponse
            });
        }

        [Authorize(Roles = "Amministratore")]
        [HttpPut("biglietto/{id:guid}")]
        public async Task<IActionResult> UpdateBiglietto(Guid id, [FromBody] UpdateBigliettoRequestDto updateBigliettoRequestDto) {
            var result = await _ticketService.UpdateBigliettoAsync(id, updateBigliettoRequestDto);

            if (!result) {
                var logErrorMessage = "Errore nella modifica del biglietto";
                _loggerService.LogError(logErrorMessage);
                return BadRequest(new UpdateBigliettoResponseDto {
                    Message = logErrorMessage
                });
            }

            var logInfoMessage = "Biglietto aggiornato con successo";
            _loggerService.LogInformation(logInfoMessage);
            return Ok(new UpdateBigliettoResponseDto {
                Message = logInfoMessage
            });
        }

        [Authorize(Roles = "Amministratore")]
        [HttpDelete("biglietto/{id:guid}")]
        public async Task<IActionResult> DeleteBiglietto(Guid id) {
            var result = await _ticketService.DeleteBigliettoAsync(id);

            if (!result) {
                var logErrorMessage = "Errore nella cancellazione del biglietto";
                _loggerService.LogError(logErrorMessage);
                return BadRequest(new {
                    message = logErrorMessage
                });
            }

            var logInfoMessage = "Biglietto cancellato con successo";
            _loggerService.LogInformation(logInfoMessage);
            return Ok(new {
                message = logInfoMessage
            });
        }

        //++++++METODO BIGLIETTO AREA PRIVATA UTENTE++++++

        [HttpGet("biglietto/area-privata")]
        public async Task<IActionResult> GetAllBigliettiAreaPrivata() {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var bigliettiList = await _ticketService.GetAllBigliettiAreaPrivataAsync(userId);

            if (bigliettiList == null) {
                var logErrorMessage = "Errore nel recupero dei biglietti";
                _loggerService.LogError(logErrorMessage);
                return BadRequest(new {
                    message = logErrorMessage
                });
            }

            if (!bigliettiList.Any()) {
                return NoContent();
            }

            var bigliettiResponse = bigliettiList.Select(b => new BigliettoDto() {
                BigliettoId = b.BigliettoId,
                DataAcquisto = b.DataAcquisto,
                EventoDto2 = new EventoDto2 {
                    EventoId = b.Evento.EventoId,
                    Titolo = b.Evento.Titolo,
                    Data = b.Evento.Data,
                    Luogo = b.Evento.Luogo,
                    ArtistaDto2 = new ArtistaDto2 {
                        ArtistaId = b.Evento.Artista.ArtistaId,
                        Nome = b.Evento.Artista.Nome,
                        Genere = b.Evento.Artista.Genere,
                        Biografia = b.Evento.Artista.Biografia
                    }
                },
                ApplicationUserDto = new ApplicationUserDto {
                    Id = b.ApplicationUser.Id,
                    FirstName = b.ApplicationUser.FirstName,
                    LastName = b.ApplicationUser.LastName
                }
            });

            var logInfoMessage = "Biglietti trovati con successo";
            _loggerService.LogInformation(logInfoMessage);
            return Ok(new {
                message = $"Numero biglietti trovati: {bigliettiResponse.Count()}",
                biglietti = bigliettiResponse
            });
        }
    }
}
