using AutoMapper;
using Hozifa.Entities;
using Hozifa.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hozifa.Mappers
{
    public class PostMapper : Profile
    {
        public PostMapper()
        {
            CreateMap<PostViewModel, Post>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.PostTitle))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.PostDesc))
               .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.PostAuthor))
               .ReverseMap()
               .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
