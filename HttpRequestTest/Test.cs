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
using IdentityModel.Client;
using Xunit;
using System.Net.Http.Json;

namespace HttpRequestTest
{
    public class TestClass : BaseTest
    {
        public TestClass() : base()
        {

        }

        [Fact]
        public async Task GET_Test()
        {
            var client = await GetClient();

            var url = baseUrl + "api/v1/device/getdevicekeytags?deviceId=1&locationId=3";
            var response = await client.GetAsync(url);
            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);

            
        }
    }
}
