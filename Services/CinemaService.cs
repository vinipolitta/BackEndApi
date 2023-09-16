using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BackEndApi.Data;
using BackEndApi.Data.Dtos;
using BackEndApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEndApi.Services
{
    public class CinemaService
    {
        private readonly FilmeContext _context;
        private readonly IMapper _mapper;

        public CinemaService(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Cinema AdicionaCinema(CreateCinemaDto cinemaDto)
        {
            Cinema cinema = _mapper.Map<Cinema>(cinemaDto);
            _context.Cinemas.Add(cinema);
            _context.SaveChanges();
            return cinema;
        }

        public IEnumerable<ReadCinemaDto> RecuperaCinemas(int? enderecoId = null)
        {
            IQueryable<Cinema> query = _context.Cinemas;

            if (enderecoId.HasValue)
            {
                query = query.Where(cinema => cinema.EnderecoId == enderecoId);
            }

            List<ReadCinemaDto> cinemas = _mapper.Map<List<ReadCinemaDto>>(query.ToList());
            return cinemas;
        }

        public ReadCinemaDto RecuperaCinemasPorId(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema != null)
            {
                ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
                return cinemaDto;
            }
            return null; // Ou você pode lançar uma exceção NotFound aqui
        }

        public bool AtualizaCinema(int id, UpdateCinemaDto cinemaDto)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null)
            {
                return false;
            }
            _mapper.Map(cinemaDto, cinema);
            _context.SaveChanges();
            return true;
        }

        public bool DeletaCinema(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null)
            {
                return false;
            }
            _context.Remove(cinema);
            _context.SaveChanges();
            return true;
        }
    }

}