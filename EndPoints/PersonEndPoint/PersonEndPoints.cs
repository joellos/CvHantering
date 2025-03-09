using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore;
using CvHantering.Models;
using CvHantering.Data;
using CvHantering.DTOs.PersonDTOs;
using CvHantering.DTOs.EducationDTO;
using CvHantering.DTOs.ExperienceDTO;
using CvHantering.Services;


namespace CvHantering.EndPoints.PersonEndPoint
{
    public class PersonEndPoints
    {
        public static void RegisterPersonEndPoints(WebApplication app) 
        {
            app.MapGet("/persons", async (PersonService useService) =>
            {
                try
                {
                    var persons = await useService.GetAllUsers();
                    return Results.Ok(persons);
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound("No users found");

                }
            });

            app.MapPost("/persons", async (PersonService useService, PersonDTOCreate personToCreate) =>
            {
                // Skapa en ny person baserat på inkommande DTO
                try
                {
                    var persontoCreate = await useService.CreateUser(personToCreate);
                    return Results.Ok(persontoCreate);
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound("Person could not be created");
                }
            });

            app.MapGet("/person/{id}", async (PersonService userService, int id) =>
            {
                // Hämta personen från databasen
                try
                {
                    var person = await userService.GetUserById(id);
                    return Results.Ok(person);
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound("Person not found");
                }
            });

            app.MapPut("/person/{id}", async (PersonService userService, int id, PersonDTOCreate personDTO) =>
            {
                try
                {
                    var person = await userService.UpdateUser(id, personDTO);
                    return Results.Ok(person);
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound("Person not found");
                }
            });



        }
    }
}
