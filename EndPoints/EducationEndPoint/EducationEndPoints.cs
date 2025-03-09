using CvHantering.Data;
using CvHantering.DTOs.ExperienceDTO;
using CvHantering.DTOs.PersonDTOs;
using CvHantering.Models;
using Microsoft.EntityFrameworkCore;
using CvHantering.DTOs.EducationDTO;
using CvHantering.Services;

namespace CvHantering.EndPoints.EducationEndPoint
{
    public class EducationEndPoints
    {
        public static void RegisterEducationEndPoints(WebApplication app)
        {

            app.MapGet("/education/{personId}", async (EducationService userService, int personId) =>
            {
                try
                {
                    var educations = await userService.GetEducationById(personId);
                    return Results.Ok(educations);
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound("No education found");
                }
            });

            app.MapPost("/education/{personId}", async (EducationService userService, int? personId, EducationDto educationDTO) =>
            {
                // Hämta personen från databasen
                try
                {
                    var education = await userService.AddEducation(educationDTO, personId.Value);
                    return Results.Ok(education);
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound("Person not found");
                }
            });

            app.MapPut("/education/{id}", async (EducationService userService, int id, EducationDto educationDTO) =>
            {
                try
                {
                    var educationChange = await userService.UpdateEducation(educationDTO, id);
                    return Results.Ok(educationChange);
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound("Education not found");
                }
            });

            app.MapDelete("/education/{id}", async (EducationService userService, int id) =>
            {
                try
                {
                    await userService.DeleteEducation(id);
                    return Results.NoContent();
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound("Education not found");
                }
            });

        }
    }
}
