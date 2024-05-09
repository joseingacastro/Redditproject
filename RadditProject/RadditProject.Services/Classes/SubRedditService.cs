using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RadditProject.Services.Interfaces;
using RadditProject.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadditProject.Services.Classes
{
    public class SubRedditService : ISubRedditService
    {
        public async Task<Root> GetSubRedditData(TokenResponse tokenResponse,string api)
        {
            var parameters = new FormUrlEncodedContent(new[]
               {
            new KeyValuePair<string, string>("Authorization", tokenResponse.access_token)
            });

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenResponse.access_token);
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Post Analysis App");
                var response = await httpClient.GetAsync(api);

                if (response.IsSuccessStatusCode)
                {
                    Root subRedditData = JsonConvert.DeserializeObject<Root>(await response.Content.ReadAsStringAsync());
                    return subRedditData;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
