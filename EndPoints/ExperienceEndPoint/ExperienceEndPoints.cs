using CvHantering.Data;
using CvHantering.DTOs.ExperienceDTO;
using CvHantering.DTOs.PersonDTOs;
using CvHantering.Models;
using CvHantering.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace CvHantering.EndPoints.ExperienceEndPoint
{
    public class ExperienceEndPoints
    {
        public static void RegisterExperienceEndPoints(WebApplication app)
        {
            app.MapPost("/experience/{personId}", async (ExperienceService userService, int personId, ExperienceDto experienceDTO) =>
            {
                // Hämta personen från databasen
                try
                {
                    var experience = await userService.AddExperience(experienceDTO, personId);
                    return Results.Ok(experience);
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound("Person not found");
                }
            });

            app.MapPut("/experience{id}", async (ExperienceService userService, int id, ExperienceDto experienceDTO) =>
            {
                try
                {
                    var experienceChange = await userService.UpdateExperience(experienceDTO, id);
                    return Results.Ok(experienceChange);
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound("Experience not found");

                }
            });

            app.MapDelete("/experience/{id}", async (ExperienceService userService, int id) =>
            {
             try
                {
                    await userService.DeleteExperience(id);
                    return Results.NoContent();
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound("Experience not found");
                }
            });

            app.MapGet("/experience{id}", async (ExperienceService userService, int id) =>
            {
                try
                {
                    var experience = await userService.GetExperienceById(id);
                    return Results.Ok(experience);
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound("Experience not found");
                }
            });


        }


    }
}

