using com.tweetapp.Domain.lib.Entities;
using com.tweetapp.Services.lib.Dtos;
using com.tweetapp.Services.Repositories.lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.tweetapp.Services.lib.Repositories
{
    public interface ITweeterRepository
    {
        Task<ServiceResponse<DetailTweetDto>> AddTweet(CreatetweeterDto tweet);
        Task<ServiceResponse<List<ListTweetDto>>> GetAllTweets();
        Task<ServiceResponse<List<ListTweetDto>>> GetAllTweetsByUserName(string userName);
        Task<ServiceResponse<DetailTweetDto>> UpdateTweet(UpdateTweetDto tweet);
        Task<ServiceResponse<string>> DeleteTweet(int id);
        Task<ServiceResponse<List<ListTweetDto>>> ReplyOnTweet(int id, CreateReplyDto createReply);
        Task<ServiceResponse<List<ListTweetDto>>> LikeTweet(int id);

    }
}
