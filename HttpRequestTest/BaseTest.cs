using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using IdentityModel.Client;

//https://andrewlock.net/introduction-to-integration-testing-with-xunit-and-testserver-in-asp-net-core/
//https://auth0.com/blog/xunit-to-test-csharp-code/

namespace HttpRequestTest
{
    public abstract class BaseTest
    {
        public  static string baseUrl = "https://qa-easyvend.dgcdn.net/TMSGameAPI/";

        public BaseTest()
        {
            
        }

        public static async Task<string> GetToken()
        {
            HttpClient client = new HttpClient();
            string clientId = "689B9564-0212-4C9B-9427-0A2F2808C42A";
            string clientSecret = "hX3LywR5TXvQWAFS6R5tnuaZEt3QJsvrcXpv7UquRCmXWYrvDyp4G75ZxACAtXmV";
            string scope = "deviceApi";
            string url = "https://qa-easyvend.dgcdn.net/TMSGameAPI/connect/token";

            //IdentityModel.Client library
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = url,
                ClientId = clientId,
                ClientSecret = clientSecret,
                Scope = scope
            });

            return tokenResponse.AccessToken;
        }


        public async Task<HttpClient> GetClient()
        {
            var token = await GetToken();
            HttpClient client = new HttpClient();
            client.SetBearerToken(token);

            return client;
        }
    }
}
