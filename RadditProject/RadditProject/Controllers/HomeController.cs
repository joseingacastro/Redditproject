using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using RestSharp;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;
using Microsoft.Extensions.Configuration;
using RadditProject.Services.Interfaces;
using RadditProject.Services.Model;
using RadditProject.Services.Classes;
using Reddit.Things;

namespace RadditProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class HomeController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly ISubRedditService _subRedditService;
        public HomeController(ITokenService tokenService, IConfiguration configuration, ISubRedditService subRedditService)
        {
            _tokenService = tokenService;
            _configuration = configuration;
            _subRedditService = subRedditService;
        }

        [HttpGet]
        [Route("getSubreddit")]
        public async Task<Root> getSubredditData(string subredditName)
        {
            try
            {
                var appsetting = _configuration.GetSection("RedditConfig").Get<AppSettings>();
                return await GetData(appsetting.SubRedittAPI.Replace("redditName", subredditName));
                
            }
            catch (Exception ex)
            {
                throw new Exception("Getting some issue while fetching API's",ex.InnerException);
            }
        }

        [HttpGet]
        [Route("getSubredditPost")]
        public async Task<Root> getSubredditPostData(string subredditName)
        {
            try
            {
                var appsetting = _configuration.GetSection("RedditConfig").Get<AppSettings>();
                return await GetData(appsetting.SubRedditPost.Replace("redditName", subredditName));
            }
            catch (Exception ex)
            {
                throw new Exception("Getting some issue while fetching API's", ex.InnerException);
            }
        }
        private async Task<Root> GetData(string api)
        {
            try
            {
                var appsetting = _configuration.GetSection("RedditConfig").Get<AppSettings>();
                var accessToken = await _tokenService.GetRedditToken(appsetting);
                return await _subRedditService.GetSubRedditData(accessToken, api);
            }
            catch (Exception ex)
            {
                throw new Exception("Getting some issue while fetching API's", ex.InnerException);
            }
        }


    }
}


