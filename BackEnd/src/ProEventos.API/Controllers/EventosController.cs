using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProEventos.Application.Contract;
using ProEventos.Domain;
using ProEventos.Persistence;
using ProEventos.Persistence.Context;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            return Ok(new {eventos = await _context.GetAllEventosAsync(true)});
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetByID(int ID)
        {
            var evento = await _context.GetAllEventosByIdAsync(ID, true);
            if (evento == null) return BadRequest(new {message = "Evento não encontrado."});            
            
            return  Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Evento novoEvento)
        {
            var eventoAdded = _context.AddEvento(novoEvento);
            if(eventoAdded == null) return BadRequest();
            
            return Created("", new {message = "Evento criado com sucesso.", evento = eventoAdded});
        }

        [HttpPut("{eventoId:int}")]
        public async Task<ActionResult> Put(int eventoId,Evento evento)
        {
            if (eventoId != evento.EventoId) return BadRequest(new { message = "Evento não encontrado."});
            
            var eventoUpdated = await _context.UpdateEvento(eventoId, evento);
            
            return Ok   (new {evento = eventoUpdated});
        }

        [HttpDelete("{eventoId:int}")]
        public async Task<ActionResult> Delete(int eventoId)
        {
            var eventoDeleted = await _context.DeleteEventos(eventoId);

            if (!eventoDeleted) return BadRequest(new { message = "Não foi possível excluir o evento."});
            
            return Ok();
        }
    }
}
