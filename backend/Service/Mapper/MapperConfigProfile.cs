using Repository.DTOs.Auth;
using Repository.DTOs.User;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Service.Mapper
{
    public class MapperConfigProfile: Profile
    {
        public MapperConfigProfile()
        {
            CreateMap<RegisterDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            CreateMap<LoginDTORequest, User>().ReverseMap();

        }
    }
}
