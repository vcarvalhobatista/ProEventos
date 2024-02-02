using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProEventos.API.Data;
using ProEventos.API.Model;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        readonly DataContext _context;

        public EventosController(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<Evento> _evento = new Evento[]{ 
            new Evento{
                EventoId= 1, 
                Tema="Angular e .Net5", 
                Local="Belo Horizonte", 
                Lote="1º Lote", 
                QtdPessoas= 250 , 
                DataEvento=DateTime.Now.AddDays(2).ToString("dd/MM/yyyy"),
                ImagemURL="foto.png"},

            new Evento{
                EventoId= 2, 
                Tema="SQL Server & .Net Core", 
                Local="João Pessoa", 
                Lote="4º Lote", 
                QtdPessoas= 150 , 
                DataEvento=DateTime.Now.AddDays(5).ToString("dd/MM/yyyy"),
                ImagemURL="foto.png"},
            
            new Evento{
                EventoId= 3, 
                Tema="Angular", 
                Local="São Paulo", 
                Lote="2º Lote", 
                QtdPessoas= 1000 , 
                DataEvento=DateTime.Now.AddDays(9).ToString("dd/MM/yyyy"),
                ImagemURL="foto.png"}
            };


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
    }
}
