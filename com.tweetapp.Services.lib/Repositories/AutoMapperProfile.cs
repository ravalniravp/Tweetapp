using AutoMapper;
using com.tweetapp.Domain.lib.Entities;
using com.tweetapp.Services.lib.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.tweetapp.Services.Repositories.lib
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, RegisterUserDto>()
                .ReverseMap();
            CreateMap<User, UserListDto>()
                .ForMember(dest => dest.FullName, opt => 
                {
                    opt.MapFrom(src => src.FirstName + " " + src.LastName);
                })
                .ReverseMap();
            CreateMap<User,RegisterResponseUserDto>()
                .ReverseMap();
            CreateMap<Tweet, CreatetweeterDto>()
                .ForMember(dest => dest.Tags, opt =>
                {
                    opt.MapFrom(src => src.Tags);
                })
                .ReverseMap();
            CreateMap<Tag, CreatetagDto>()
                .ForMember(dest => dest.TagPersonId, opt =>
                {
                    opt.MapFrom(src => src.TagPersonId);
                })
                .ReverseMap();
            CreateMap<Tweet, DetailTweetDto>()
                .ReverseMap();
            CreateMap<Tag, DetailTagDto>()
                .ForMember(dest => dest.TagedPerson, opt =>
                {
                    opt.MapFrom(src => src.TagPerson.UserName);
                })
                .ReverseMap();
            CreateMap<Tweet, ListTweetDto>()
                .ForMember(dest => dest.UserName, opt =>
                {
                    opt.MapFrom(src => src.CreatedBy.UserName);
                })
                .ForMember(dest => dest.Tags, opt =>
                {
                    opt.MapFrom(src => src.Tags);
                })
                .ForMember(dest => dest.ReplyDtos, opt =>
                {
                    opt.MapFrom(sr => sr.TweetReplies);
                })
                .ForMember(dest => dest.Likes, opt =>
                {
                    opt.MapFrom(sr => sr.Likes.Count(l => l.IsLike == true));
                })
                .ReverseMap();
            CreateMap<Tag, ListTagDto>()
                    .ForMember(src => src.TagedUser, opt =>
                    {
                        opt.MapFrom(dest => dest.TagPerson.UserName);
                    })
                    .ReverseMap();
            CreateMap<Tweet, UpdateTweetDto>()
                    .ForMember(src => src.Tags, opt =>
                    {
                        opt.MapFrom(dest => dest.Tags);
                    })
                    .ReverseMap();
            CreateMap<Tag, UpdateTagDto>()
                .ForMember(src => src.TagPerosnId, opt =>
                {
                    opt.MapFrom(dest => dest.TagPersonId);
                })
                .ReverseMap();
            CreateMap<TweetReply, ListReplyDto>()
                .ForMember(dest => dest.ReplyBy, opt =>
                {
                    opt.MapFrom(src => src.RepliedBy.UserName);
                })
                .ReverseMap();
            CreateMap<TweetReply, CreateReplyDto>()
                .ForMember(dest => dest.replyTagDtos, opt =>
                {
                    opt.MapFrom(src => src.ReplyTags);
                })
                .ReverseMap();
            CreateMap<ReplyTag, CreateReplyTagDto>()
                .ForMember(dest => dest.TagPersonId, opt =>
                {
                    opt.MapFrom(src => src.TagedUserId);
                })
                .ReverseMap();

        }
    }
}
