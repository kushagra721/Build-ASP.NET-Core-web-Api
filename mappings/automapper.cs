using AutoMapper;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTOs;

namespace NZwalks.API.mappings
{
    public class automapper: Profile
    {
        public automapper()
        {
            CreateMap<walkpostdto, Walk>().ReverseMap();
            CreateMap<Walk,walkdomaintodto>().ReverseMap();
            CreateMap<updatewalkdto, Walk>().ReverseMap();

        }
    }
}
