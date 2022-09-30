using AutoMapper;
using Model.DTOs;
using Model.Models;

namespace Model.Profiles
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<Post,ReadPostDTO>();
            CreateMap<CreatePostDTO, Post>();

            CreateMap<UpdatePostDTO, Post>().ReverseMap();

            CreateMap<Comment, ReadCommentDTO>();
            CreateMap<CreateCommentDTO, Comment>();
            CreateMap<UpdateCommentDTO, Comment>().ReverseMap();

        }
    }
}
