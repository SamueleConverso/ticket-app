﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PROGETTO_U5_S3_L5.DTOs.Artista;
using PROGETTO_U5_S3_L5.DTOs.Biglietto;
using PROGETTO_U5_S3_L5.DTOs.Evento;
using PROGETTO_U5_S3_L5.Models;
using PROGETTO_U5_S3_L5.Services;

namespace PROGETTO_U5_S3_L5.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase {
        private readonly TicketService _ticketService;

        public TicketController(TicketService ticketService) {
            _ticketService = ticketService;
        }

        //++++++METODI ARTISTA++++++

        [HttpPost("artista")]
        public async Task<IActionResult> AddArtista([FromBody] CreateArtistaRequestDto createArtistaRequestDto) {
            var newArtista = new Artista {
                Nome = createArtistaRequestDto.Nome,
                Genere = createArtistaRequestDto.Genere,
                Biografia = createArtistaRequestDto.Biografia
            };

            var result = await _ticketService.AddArtistaAsync(newArtista);

            if (!result) {
                return BadRequest(new CreateArtistaResponseDto {
                    Message = "Errore nell'aggiunta dell'artista"
                });
            }

            return Ok(new CreateArtistaResponseDto {
                Message = "Artista aggiunto con successo"
            });
        }

        [HttpGet("artista")]
        public async Task<IActionResult> GetAllArtisti() {
            var artistiList = await _ticketService.GetAllArtistiAsync();

            if (artistiList == null) {
                return BadRequest(new {
                    message = "Errore nel recupero degli artisti"
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

            return Ok(new {
                message = $"Numero artisti trovati: {artistiResponse.Count()}",
                artisti = artistiResponse
            });
        }

        [HttpGet("artista/{id:guid}")]
        public async Task<IActionResult> GetArtistaById(Guid id) {
            var artistaToFind = await _ticketService.GetArtistaByIdAsync(id);

            if (artistaToFind == null) {
                return BadRequest(new {
                    message = "Errore nel recupero dell'artista"
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

            return Ok(new {
                message = "Artista trovato",
                artista = artistaResponse
            });
        }

        [HttpPut("artista/{id:guid}")]
        public async Task<IActionResult> UpdateArtista(Guid id, [FromBody] UpdateArtistaRequestDto updateArtistaRequestDto) {
            var result = await _ticketService.UpdateArtistaAsync(id, updateArtistaRequestDto);

            if (!result) {
                return BadRequest(new UpdateArtistaResponseDto {
                    Message = "Errore nella modifica"
                });
            }

            return Ok(new UpdateArtistaResponseDto {
                Message = "Artista aggiornato con successo"
            });
        }

        [HttpDelete("artista/{id:guid}")]
        public async Task<IActionResult> DeleteArtista(Guid id) {
            var result = await _ticketService.DeleteArtistaAsync(id);

            if (!result) {
                return BadRequest(new {
                    message = "Errore nella cancellazione dell'artista"
                });
            }

            return Ok(new {
                message = "Artista cancellato con successo"
            });
        }

        //++++++METODI EVENTO++++++

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
                return BadRequest(new CreateArtistaResponseDto {
                    Message = "Errore nell'aggiunta dell'evento"
                });
            }

            return Ok(new CreateArtistaResponseDto {
                Message = "Evento aggiunto con successo"
            });
        }

        [HttpGet("evento")]
        public async Task<IActionResult> GetAllEventi() {
            var eventiList = await _ticketService.GetAllEventiAsync();

            if (eventiList == null) {
                return BadRequest(new {
                    message = "Errore nel recupero degli eventi"
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

            return Ok(new {
                message = $"Numero eventi trovati: {eventiResponse.Count()}",
                eventi = eventiResponse
            });
        }

        [HttpGet("evento/{id:guid}")]
        public async Task<IActionResult> GetEventoById(Guid id) {
            var eventoToFind = await _ticketService.GetEventoByIdAsync(id);

            if (eventoToFind == null) {
                return BadRequest(new {
                    message = "Errore nel recupero dell'evento"
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

            return Ok(new {
                message = "Evento trovato",
                evento = eventoResponse
            });
        }

        [HttpPut("evento/{id:guid}")]
        public async Task<IActionResult> UpdateEvento(Guid id, [FromBody] UpdateEventoRequestDto updateEventoRequestDto) {
            var result = await _ticketService.UpdateEventoAsync(id, updateEventoRequestDto);

            if (!result) {
                return BadRequest(new UpdateEventoResponseDto {
                    Message = "Errore nella modifica"
                });
            }

            return Ok(new UpdateEventoResponseDto {
                Message = "Evento aggiornato con successo"
            });
        }

        [HttpDelete("evento/{id:guid}")]
        public async Task<IActionResult> DeleteEvento(Guid id) {
            var result = await _ticketService.DeleteEventoAsync(id);

            if (!result) {
                return BadRequest(new {
                    message = "Errore nella cancellazione dell'evento"
                });
            }

            return Ok(new {
                message = "Evento cancellato con successo"
            });
        }

        //++++++METODI BIGLIETTO++++++

        [HttpPost("biglietto")]
        public async Task<IActionResult> AddBiglietto([FromBody] CreateBigliettoRequestDto createBigliettoRequestDto) {
            var newBiglietto = new Biglietto {
                EventoId = createBigliettoRequestDto.EventoId,
                UserId = createBigliettoRequestDto.UserId
            };

            var result = await _ticketService.AddBigliettoAsync(newBiglietto);

            if (!result) {
                return BadRequest(new CreateBigliettoResponseDto {
                    Message = "Errore nell'aggiunta del biglietto"
                });
            }

            return Ok(new CreateBigliettoResponseDto {
                Message = "Biglietto aggiunto con successo"
            });
        }
    }
}
