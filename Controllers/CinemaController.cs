using AutoMapper;
using BackEndApi.Data.Dtos;
using BackEndApi.Data;
using BackEndApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using BackEndApi.Services;

namespace BackEndApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemaController : ControllerBase
    {
        private readonly CinemaService _cinemaService;

        public CinemaController(CinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }

        [HttpPost]
        public IActionResult AdicionaCinema([FromBody] CreateCinemaDto cinemaDto)
        {
            var cinema = _cinemaService.AdicionaCinema(cinemaDto);
            return CreatedAtAction(nameof(RecuperaCinemasPorId), new { Id = cinema.Id }, cinema);
        }

        [HttpGet]
        public IActionResult RecuperaCinemas([FromQuery] int? enderecoId = null)
        {
            var cinemas = _cinemaService.RecuperaCinemas(enderecoId);
            return Ok(cinemas);
        }


        // [HttpGet]
        // public IEnumerable<ReadCinemaDto> RecuperaCinemas([FromQuery] int? enderecoId = null)
        // {
        //     if (enderecoId == null)
        //     {
        //         return _mapper.Map<List<ReadCinemaDto>>(_context.Cinemas.ToList());
        //     }
        //     return _mapper.Map<List<ReadCinemaDto>>(_context.Cinemas.FromSqlRaw($"SELECT Id, Nome, EnderecoId FROM cinemas where cinemas.EnderecoId = {enderecoId}").ToList());
        // }

        [HttpGet("{id}")]
        public IActionResult RecuperaCinemasPorId(int id)
        {
            var cinema = _cinemaService.RecuperaCinemasPorId(id);
            if (cinema != null)
            {
                return Ok(cinema);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
        {
            bool updated = _cinemaService.AtualizaCinema(id, cinemaDto);
            if (updated)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaCinema(int id)
        {
            bool deleted = _cinemaService.DeletaCinema(id);
            if (deleted)
            {
                return NoContent();
            }
            return NotFound();
        }

    }
}
