using Repository.DTOs.Auth;
using Repository.DTOs.User;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Repository.DTOs.Category;

namespace Service.Mapper
{
    public class MapperConfigProfile: Profile
    {
        public MapperConfigProfile()
        {
            // mapper of user
            CreateMap<RegisterDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            CreateMap<LoginDTORequest, User>().ReverseMap();
            // mapper of category
            CreateMap<Category, CategoryReadDto>().ReverseMap();
            CreateMap<CategoryCreateDto, Category>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            //

        }
    }
}
