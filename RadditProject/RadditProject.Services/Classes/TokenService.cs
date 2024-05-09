using Newtonsoft.Json;
using RadditProject.Services.Interfaces;
using RadditProject.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadditProject.Services.Classes
{
    public class TokenService : ITokenService
    {
        public async Task<TokenResponse> GetRedditToken(AppSettings appSettings)
        {
            try
            {
                string base64Auth = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{appSettings.ClientID}:{appSettings.ClientSecret}"));

                var parameters = new FormUrlEncodedContent(new[]
                {
            new KeyValuePair<string, string>("grant_type", "client_credentials"),
            new KeyValuePair<string, string>("username", appSettings.UserName),
            new KeyValuePair<string, string>("password", appSettings.Password)
            });

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Auth);
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "Post Analysis App");
                    var response = await httpClient.PostAsync(appSettings.AuthAPI, parameters);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        TokenResponse tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(content);
                        return tokenResponse;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
