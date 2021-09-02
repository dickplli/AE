using AE.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using Xunit;

namespace AE.Test
{
    public class Put_Ships_Velocity_ReturnNoContent
    {
        public HttpClient Client { get; }

        private static readonly Random random = new();

        public Put_Ships_Velocity_ReturnNoContent()
        {
            var server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>());
            Client = server.CreateClient();
        }

        [Fact]
        public async void Test1()
        {
            Ship ship = new()
            {
                Velocity = random.NextDouble() * 25
            };

            //act
            var content = new StringContent(JsonConvert.SerializeObject(ship), Encoding.UTF8, MediaTypeNames.Application.Json);

            var response = await Client.PutAsync("/api/Ships/ship2/Velocity", content);

            //assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
