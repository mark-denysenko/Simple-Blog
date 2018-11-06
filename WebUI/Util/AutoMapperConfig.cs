using System.Linq;
using AutoMapper;
using BusinessLayer.BusinessModelsDTO;
using Core;


namespace WebUI.Util
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserProfile>()
                    .ForMember("TotalComments", opt => opt.MapFrom(u => u.Comments.Count))
                    .ForMember("TotalPosts", opt => opt.MapFrom(u => u.Posts.Count));

                cfg.CreateMap<Comment, CommentDTO>()
                    .ForMember("Author", opt => opt.MapFrom(c => c.User.Nickname));

                cfg.CreateMap<Post, PostDTO>()
                    .ForMember("Author", opt => opt.MapFrom(u => u.User.Nickname))
                    .ForMember("Comments", opt => opt.MapFrom(p => p.Comments.Select(c => Mapper.Map<CommentDTO>(c))));
            });
        }
    }
}