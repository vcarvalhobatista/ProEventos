using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProEventos.Domain;
using ProEventos.Persistence;
using ProEventos.Persistence.Context;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        readonly ProEventosContext _context;

        public EventosController(ProEventosContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Evento>>> Get()
        {
            return await _context.Eventos.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetByID(int ID)
        {
            await _context.Eventos.FirstOrDefaultAsync(x => x.EventoId == ID);
            return  Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Evento evento)
        {
            _context.Eventos.Add(evento);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put(Evento evento)
        {
            _context.Entry<Evento>(evento).State = EntityState.Modified;                       
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
