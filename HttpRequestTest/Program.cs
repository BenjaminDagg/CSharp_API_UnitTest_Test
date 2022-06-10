// See https://aka.ms/new-console-template for more information

using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using IdentityModel.Client; //https://developer.joyful.org/docs/example-bearer-token

//postman api testing https://www.c-sharpcorner.com/article/automated-api-testing-with-postman/

namespace HttpRequestTest
{
    public class Repository
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("html_url")]
        public Uri GitHubHomeUrl { get; set; }

        [JsonPropertyName("homepage")]
        public Uri Homepage { get; set; }

        [JsonPropertyName("watchers")]
        public int Watchers { get; set; }
    }


    public class Product
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    class HttpTest
    {
        private static readonly HttpClient client = new HttpClient();
        private static string baseUrl = "https://qa-easyvend.dgcdn.net/TMSGameAPI/";

        private static async Task<List<Repository>> MakeRequest()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            var repositories = await System.Text.Json.JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);

            return repositories;
        
        }

        private static async Task Test()
        {
            var response = await client.GetStreamAsync("https://fakestoreapi.com/products/");
            
            //var resString = await response.Content.ReadAsStringAsync();
            var result = await System.Text.Json.JsonSerializer.DeserializeAsync<List<Product>>(response);
            foreach(var item in result)
            {
                Console.WriteLine(item.Title);
            }

        }

        //IdentityModel.Client library https://developer.joyful.org/docs/example-bearer-token
        private static async Task<string> GetToken()
        {
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


        private static async Task CallGameApi()
        {
            var token = await GetToken();
            client.SetBearerToken(token);

            var url = baseUrl + "api/v1/device/getdevicekeytags?deviceId=1&locationId=3";
            var response = await client.GetAsync(url);
            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);
        }
        
        static async Task Main(string[] args)
        {
            var repos = await MakeRequest();
            /*
            foreach (var repository in repos)
            {
                Console.WriteLine(repository.Name);
                Console.WriteLine(repository.Description);
                Console.WriteLine(repository.GitHubHomeUrl);
                Console.WriteLine(repository.Homepage);
                Console.WriteLine(repository.Watchers);
                Console.WriteLine();
            }
            */
            await CallGameApi();
        }
    }
}