using AutoMapper;
using com.tweetapp.DAL.lib;
using com.tweetapp.Domain.lib.Entities;
using com.tweetapp.Services.lib.Dtos;
using com.tweetapp.Services.Repositories.lib;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace com.tweetapp.Services.lib.Repositories
{
    public class TweeterRepository : ITweeterRepository
    {
        private readonly AppDBContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;

        private int currentUserId
        { get
            {
                return int.Parse(_httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            } 
        }

        public TweeterRepository(AppDBContext context,
                                 IHttpContextAccessor httpContext,
                                 IMapper mapper)
        {
            this._context = context;
            this._httpContext = httpContext;
            this._mapper = mapper;
        }

        public async Task<ServiceResponse<DetailTweetDto>> AddTweet(CreatetweeterDto tweet)
        {
            
            
            var response = new ServiceResponse<DetailTweetDto>();
            var createTweet = _mapper.Map<Tweet>(tweet);
            
            await _context.AddAsync(createTweet);
            createTweet.CreatedById = currentUserId;
            createTweet.UpdatedById = currentUserId;

            foreach (var tag in createTweet.Tags)
            {
                tag.CreatedById = currentUserId;
            }

            await _context.SaveChangesAsync();

            response.Data = _mapper.Map<DetailTweetDto>(createTweet);
            response.Message = "New tweet created";
            return response;

        }

        public async Task<ServiceResponse<string>> DeleteTweet(int id)
        {
            var response = new ServiceResponse<string>();
            var tweet = await _context.Tweets
                                      .Include(t => t.Tags)
                                      .FirstOrDefaultAsync(t => t.Id == id);
            if (tweet == null)
            {
                response.Success = false;
                response.Message = "No record found";
                return response;
            }

            _context.Tweets.Remove(tweet);
            await _context.SaveChangesAsync();
            response.Data = "Record deleted successfully";
            return response;

        }

        public async Task<ServiceResponse<List<ListTweetDto>>> GetAllTweets()
        {
            var response = new ServiceResponse<List<ListTweetDto>>();
            List<ListTweetDto> listTweetDto = await GetTweetListDto();
            if (listTweetDto == null)
            {
                response.Success = false;
                response.Message = "No record found";
                return response;
            }
            response.Data = listTweetDto;
            response.Message = "Fatch tweet data successfully";
            return response;
        }

        public async Task<ServiceResponse<List<ListTweetDto>>> GetAllTweetsByUserName(string userName)
        {
            var response = new ServiceResponse<List<ListTweetDto>>();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == userName.ToLower());
            var tweets = await GetTweetListDto();
            if (tweets == null)
            {
                response.Success = false;
                response.Message = "No record found";
                return response;
            }
            response.Data = tweets;
            response.Message = "Fatch tweet data successfully";
            return response;
        }

        public async Task<ServiceResponse<List<ListTweetDto>>> ReplyOnTweet(int id, CreateReplyDto createReply)
        {
            
            var response = new ServiceResponse<List<ListTweetDto>>();
            var tweet = await _context.Tweets.FirstOrDefaultAsync(t => t.Id == id);
            if (tweet == null)
            {
                response.Success = false;
                response.Message = "No tweet found for reply";
                return response;
            }
            var tweetReply = _mapper.Map<TweetReply>(createReply);
            tweetReply.TweetId = tweet.Id;
            tweetReply.RepliedById = currentUserId;
            foreach (var tag in tweetReply.ReplyTags)
            {
                tag.TweetId = tweet.Id;
                tag.CreatedById = currentUserId;
            }
            await _context.AddAsync(tweetReply);
            await _context.SaveChangesAsync();
            List<ListTweetDto> listTweetDto = await GetTweetListDto();
            response.Data = listTweetDto;
            response.Message = "Reply created successfully";
            return response;
        }

        private async Task<List<ListTweetDto>> GetTweetListDto()
        {
            return await _context.Tweets
                                        .Include(t => t.Tags)
                                            .ThenInclude(u => u.TagPerson)
                                        .Include(t => t.TweetReplies)
                                            .ThenInclude(u => u.RepliedBy)
                                        .Include(u => u.CreatedBy)
                                        .Include(l => l.Likes)
                                            .ThenInclude(u => u.User)
                                        .Select(s => _mapper.Map<ListTweetDto>(s))
                                        .ToListAsync();
        }

        public async Task<ServiceResponse<DetailTweetDto>> UpdateTweet(UpdateTweetDto tweet)
        {
            
            var response = new ServiceResponse<DetailTweetDto>();
            var updateTweeter = await _context.Tweets
                                              .Include(t => t.Tags)
                                                .ThenInclude(u => u.TagPerson)
                                              .Include(u => u.CreatedBy)
                                              .FirstOrDefaultAsync(t => t.Id == tweet.Id);
            if(currentUserId != updateTweeter.CreatedById)
            {
                response.Success = false;
                response.Message = "You can not update this tweet";
                return response;
            }    
            updateTweeter.Message = tweet.Message;
            updateTweeter.UpdatedById = currentUserId;
            
            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<DetailTweetDto>(updateTweeter);
            response.Message = "Tweet updated successfully";
            return response;
        }

        public async Task<ServiceResponse<List<ListTweetDto>>> LikeTweet(int id)
        {
            var response = new ServiceResponse<List<ListTweetDto>>();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == currentUserId);
            var tweet = await _context.Tweets
                                      .FirstOrDefaultAsync(t => t.Id == id);
            if (tweet == null)
            {
                response.Success = false;
                response.Message = "No tweet found.";
                return response;
            }

            var like = new Likes
            {
                Tweet = tweet,
                User = user,
                IsLike = true
            };
            await _context.AddAsync(like);
            await _context.SaveChangesAsync();

            response.Data = await GetTweetListDto();
            response.Message = "Like the tweet successfully";
            return response;
        }
    }
}
