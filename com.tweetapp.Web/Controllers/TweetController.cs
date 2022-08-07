using com.tweetapp.Services.lib.Dtos;
using com.tweetapp.Services.lib.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetapp.web.Controllers
{
    [Authorize]
    public class TweetController : BaseController
    {
        private readonly ITweeterRepository _tweeterRepository;

        public TweetController(ITweeterRepository tweeterRepository)
        {
            this._tweeterRepository = tweeterRepository;
        }

        [HttpGet("tweets/all")]
        public async Task<IActionResult> GetAllTweets()
        {
            var response = await _tweeterRepository.GetAllTweets();
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("tweets/{userName}")]
        public async Task<IActionResult> GetAllTweets(string userName)
        {
            var response = await _tweeterRepository.GetAllTweetsByUserName(userName);
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("tweets/{userName}/add")]
        public async Task<IActionResult> AddTweet(string userName, [FromBody] CreatetweeterDto createtweeterDto)
        {
            var response = await _tweeterRepository.AddTweet(createtweeterDto);
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpPut("tweets/{username}/update/{id}")]
        public async Task<IActionResult> UpdateTweet(string userName, int id, [FromBody] UpdateTweetDto updateTweetDto)
        {
            var response = await _tweeterRepository.UpdateTweet(updateTweetDto);
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpDelete("tweets/{username}/delete/{id}")]
        public async Task<IActionResult> DeleteTweet(string userName, int id)
        {
            var response = await _tweeterRepository.DeleteTweet(id);
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("tweets/{userName}/reply/{id}")]
        public async Task<IActionResult> ReplyTweet(string userName, int id, [FromBody] CreateReplyDto replyDto)
        {
            var response = await _tweeterRepository.ReplyOnTweet(id, replyDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("tweets/{userName}/like/{id}")]
        public async Task<IActionResult> LikeTweet(string userName, int id)
        {
            var response =await _tweeterRepository.LikeTweet(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}
