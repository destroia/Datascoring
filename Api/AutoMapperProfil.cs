using AutoMapper;
using Models;
using ModelsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public class AutoMapperProfil : Profile
    {
        public AutoMapperProfil()
        {
            CreateMap<User, UserDto>();
            CreateMap<ChangedPassword, ChangedPasswordDto>();
            CreateMap<Login, LoginDto>();

            CreateMap<UserDto, User>();
        }
    }
}
