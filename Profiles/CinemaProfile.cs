using AutoMapper;
using BackEndApi.Data.Dtos;
using BackEndApi.Models;

namespace BackEndApi.Profiles
{
    ///text
    public class CinemaProfile : Profile
    {
        ///text
        public CinemaProfile()
        {
            CreateMap<CreateCinemaDto, Cinema>();
            CreateMap<Cinema, ReadCinemaDto>()
                .ForMember(cinemaDto => cinemaDto.Endereco,
                    opt => opt.MapFrom(cinema => cinema.Endereco))
                .ForMember(cinemaDto => cinemaDto.Sessoes,
                    opt => opt.MapFrom(cinema => cinema.Sessoes));
            CreateMap<UpdateCinemaDto, Cinema>();
        }
    }
}
