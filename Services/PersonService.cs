using CvHantering.Data;
using CvHantering.DTOs.PersonDTOs;
using CvHantering.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using CvHantering.DTOs.EducationDTO;
using CvHantering.DTOs.ExperienceDTO;
using System;


namespace CvHantering.Services
{
    public class PersonService
    {
        private readonly CvDbContext context;
        public PersonService(CvDbContext _context) 
        {
            context = _context;
        }

        public async Task<List<PersonDTO>> GetAllUsers()
        {
            var persons = await context.Persons
               .Include(person => person.Educations)
               .Include(person => person.Experiences)
               .Select(person => new PersonDTO
               {
                   PersonName = person.Name,
                   PersonEmail = person.Email,
                   PersonPhone = person.Phone,
                   PersonDescription = person.Description,
                   Educations = person.Educations.Select(
                       e => new EducationDto
                       {
                           EducationSchool = e.School,
                           EducationDegree = e.Degree,
                           EducationStartDate = e.StartDate,
                           EducationEndDate = e.EndDate
                       }).ToList(),
                   Experiences = person.Experiences
                   .Select(e => new ExperienceDto
                   {
                       ExperienceCompany = e.Company,
                       ExperienceTitle = e.Title,
                       ExperienceYears = e.Years,
                       ExperienceDescription = e.Description,
                   }).ToList()
               }).ToListAsync();
            if(persons == null)
            {
                throw new KeyNotFoundException("No users found");
            }

            return persons;
        }

        public async Task<PersonDTOCreate> CreateUser(PersonDTOCreate personDto)
        {

            var validationResults = ValidatorHelper.ValidateInput(personDto);
            if (validationResults.Count > 0)
            {
                var error = string.Join(", ", validationResults.Select(v => v.ErrorMessage));
                throw new ArgumentException("Validation failed");
            }
            var personCreate = new Person
            {
                Name = personDto.PersonName,
                Email = personDto.PersonEmail,
                Phone = personDto.PersonPhone,
                Description = personDto.PersonDescription
            };

            context.Persons.Add(personCreate);
            await context.SaveChangesAsync();

            return personDto;
        }

        public async Task<PersonDTO> GetUserById(int id)
        {
            var person = await context.Persons
.Include(p => p.Educations)
.Include(p => p.Experiences)
.FirstOrDefaultAsync(p => p.Id == id);

            if (person == null)
            {
                throw new KeyNotFoundException("Person not found");
            }


            return new PersonDTO
            {
                PersonName = person.Name,
                PersonEmail = person.Email,
                PersonPhone = person.Phone,
                PersonDescription = person.Description,
                Educations = person.Educations.Select(
                    e => new EducationDto
                    {
                        EducationSchool = e.School,
                        EducationDegree = e.Degree,
                        EducationStartDate = e.StartDate,
                        EducationEndDate = e.EndDate
                    }).ToList(),
                Experiences = person.Experiences.Select(
                    e => new ExperienceDto
                    {
                        ExperienceCompany = e.Company,
                        ExperienceTitle = e.Title,
                        ExperienceYears = e.Years,
                        ExperienceDescription = e.Description
                    }).ToList()

            };
        }

           public async Task<PersonDTOCreate> UpdateUser(int id, PersonDTOCreate personDto)
        {
            var personToChange = await context.Persons.FirstOrDefaultAsync(p => p.Id == id);

            if (personToChange == null)
            {
                throw new KeyNotFoundException("Person not found");
            }
            var validationResults = ValidatorHelper.ValidateInput(personDto);
            if (validationResults.Count > 0)
            {
                var error = string.Join(", ", validationResults.Select(v => v.ErrorMessage));
                throw new ArgumentException("Validation failed");
            }

            personToChange.Name = personDto.PersonName;
            personToChange.Email = personDto.PersonEmail;
            personToChange.Phone = personDto.PersonPhone;
            personToChange.Description = personDto.PersonDescription;

            await context.SaveChangesAsync();
            return personDto;
        }
          
        
    }
}
