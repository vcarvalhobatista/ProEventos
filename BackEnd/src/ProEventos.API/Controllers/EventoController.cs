using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Model;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
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
        public EventoController()
        {

        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return _evento;
        }

        [HttpGet("{id}")]
        public IEnumerable<Evento> GetByID(int ID)
        {
            return _evento.Where(x => x.EventoId == ID);
        }

        [HttpPost]

        public string Post()
        {
            return "Exemplo de Post";
        }
    }
}
