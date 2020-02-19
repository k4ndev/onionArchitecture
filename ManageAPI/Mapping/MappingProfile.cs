using AutoMapper;
using Core.Models;
using ManageAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageAPI.Mapping
{
    public class MappingProfile :Profile
    {
        private string PhotoUrl = "localhost";
        public MappingProfile()
        {
            

            CreateMap<Artist, ArtistDto>()
           .ForMember(d => d.Poster, opt => opt.MapFrom(src => PhotoUrl + src.Poster))
           .ForMember(d => d.AddedAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<UserDto, User>();
            CreateMap<UserCreateDto, User>();
        }
    }
}
