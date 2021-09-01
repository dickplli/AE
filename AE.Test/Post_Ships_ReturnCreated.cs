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
    public class Post_Ships_ReturnCreated
    {
        public HttpClient Client { get; }

        private static readonly Random random = new();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public Post_Ships_ReturnCreated()
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
                Id = RandomString(10),
                Name = RandomString(20),
                Latitude = random.NextDouble() * 90,
                Longitude = random.NextDouble() * 360 - 180,
                Velocity = random.NextDouble() * 25
            };

            //act
            var content = new StringContent(JsonConvert.SerializeObject(ship), Encoding.UTF8, MediaTypeNames.Application.Json);

            var response = await Client.PostAsync("/api/Ships", content);

            //assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
