using cinemaServer.Models.PureModels;
using cinemaServer.Models.Response.Payload;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace cinemaServer.Tests
{
    public class MovieTests
    {
        private JsonSerializerOptions jsonOptions;

        [SetUp]
        public void Setup()
        {
            jsonOptions = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() }
            };
        }

        [Test]
        public async Task TestListOfMovies()
        {
            await using var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
            var client = factory.CreateClient();

            var res = await client.GetAsync("/movie?limit=33");
            var resAsString = await res.Content.ReadAsStringAsync();
            Payload<List<Movie>> deserialized = JsonSerializer.Deserialize<Payload<List<Movie>>>(resAsString, jsonOptions);


            // Assert payload functioned
            Assert.IsTrue(res.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.That(res?.RequestMessage?.Method.Method, Is.EqualTo("GET"));
            Assert.That(deserialized.Status, Is.EqualTo("success"));
            Assert.That(deserialized.ResponseTime, Is.EqualTo(DateTime.Now).Within(TimeSpan.FromMilliseconds(500)));

            Assert.That(deserialized.Data.Count, Is.EqualTo(33));
        }

        [Test]
        public async Task TestSpecificMovie()
        {
            await using var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
            var client = factory.CreateClient();

            var res = await client.GetAsync("/movie/3");
            var resAsString = await res.Content.ReadAsStringAsync();
            Payload<Movie> deserialized = JsonSerializer.Deserialize<Payload<Movie>>(resAsString, jsonOptions);

            Console.WriteLine($"{deserialized.Data}");

            // Assert payload functioned
            Assert.IsTrue(res.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.That(res?.RequestMessage?.Method.Method, Is.EqualTo("GET"));
            Assert.That(deserialized.Status, Is.EqualTo("success"));
            Assert.That(deserialized.ResponseTime, Is.EqualTo(DateTime.Now).Within(TimeSpan.FromMilliseconds(500)));

            Assert.That(deserialized.Data.Title, Is.EqualTo("Some Title"));
        }
    }
}