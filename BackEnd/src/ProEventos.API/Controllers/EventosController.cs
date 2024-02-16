using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.Extensions.Logging;
using ProEventos.Application.Contract;
using ProEventos.Domain;
using ProEventos.Persistence;
using ProEventos.Persistence.Context;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EventosController : ControllerBase
    {
        readonly IEventoService _context;

        public EventosController(IEventoService context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evento>>> Get()
        {   
            var listaEventos = await _context.GetAllEventosAsync(true);

            return Ok(listaEventos);
        }

        [HttpGet("{ID:int}")]
        public async Task<ActionResult<Evento>> GetByID(int ID)
        {
            var evento = await _context.GetAllEventosByIdAsync(ID, true);
            if (evento == null) return BadRequest(new { message = "Evento não encontrado." });

            return Ok(evento);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Evento novoEvento)
        {
            var eventoAdded = await _context.AddEvento(novoEvento);
            if (eventoAdded == null) return BadRequest(new {message = "Falha ao criar novo Evento."});

            return Created(nameof(Post), new { message = "Evento criado com sucesso.", evento = eventoAdded });
        }

        [HttpPut("{eventoId:int}")]
        public async Task<ActionResult> Put(int eventoId, Evento evento)
        {
            if (eventoId != evento.EventoId) return BadRequest(new { message = "Evento não encontrado." });

            var eventoUpdated = await _context.UpdateEvento(eventoId, evento);

            return Ok(new { evento = eventoUpdated });
        }

        [HttpDelete("{eventoId:int}")]
        public async Task<ActionResult> Delete(int eventoId)
        {
            var eventoDeleted = await _context.DeleteEventos(eventoId);

            if (!eventoDeleted) return BadRequest(new { message = "Não foi possível excluir o evento." });

            return Ok();
        }
    }
}
