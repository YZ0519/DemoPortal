using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.DTO;
using SimplePOSWeb.ViewModel.Auth;
using SimplePOSWeb.ViewModel.OptionSet;
using SimplePOSWeb.ViewModel.Profile;

namespace SimplePOSWeb.Helper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<LoginVM, AuthDto>();
            CreateMap<UserVM, UserDto>().ReverseMap();
            CreateMap<OptionSetDto,OptionSetVM>();
        }
    }
}
