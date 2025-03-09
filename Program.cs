using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CvHantering.Data;
using CvHantering.EndPoints.PersonEndPoint;
using CvHantering.EndPoints.ExperienceEndPoint;
using CvHantering.EndPoints.EducationEndPoint;
using CvHantering.EndPoints.GitHubEndPoint;
using System.Text.Json;
using CvHantering.Services;

namespace CvHantering
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<PersonService>();
            builder.Services.AddScoped<ExperienceService>();
            builder.Services.AddScoped<EducationService>();
            builder.Services.AddScoped<GithubService>();
            builder.Services.AddDbContext<CvDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            PersonEndPoints.RegisterPersonEndPoints(app);
            ExperienceEndPoints.RegisterExperienceEndPoints(app);
            EducationEndPoints.RegisterEducationEndPoints(app);
            GitHubEndPoints.RegisterGitHubEndPoints(app);


            app.Run();

            app.MapGet("JsonDataDemo", async () =>
            {
                HttpClient client = new HttpClient();

                var response = await client.GetAsync("https://jsonplaceholder.typicode.com/todos/1");

                var json = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                
                var data = JsonSerializer.Deserialize<ToDoDTO>(json, options);

                return Results.Ok(data);
            });
        }
    }
}
