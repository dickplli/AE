using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net;
using System.Net.Http;
using Xunit;

namespace AE.Test
{
    public class Get_Ships_ReturnOk
    {
        public HttpClient Client { get; }

        public Get_Ships_ReturnOk()
        {
            var server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>());
            Client = server.CreateClient();
        }

        [Fact]
        public async void Test1()
        {
            //act
            var response = await Client.GetAsync("/api/Ships");

            //assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
