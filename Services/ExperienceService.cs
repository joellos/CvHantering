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
    public class ExperienceService
    {
        private readonly CvDbContext context;
        public ExperienceService(CvDbContext _context)
        {
            context = _context;
        }
        public async Task<ExperienceDto> AddExperience(ExperienceDto experienceDTO, int Id)
        {
            var persontoFind = await context.Persons.Include(p => p.Experiences)
.FirstOrDefaultAsync(p => p.Id == Id);


            if (persontoFind == null)
            {
                throw new KeyNotFoundException("Person not found");
            }

            var validationResults = ValidatorHelper.ValidateInput(experienceDTO);
            if (validationResults.Count > 0)
            {
                var error = string.Join(", ", validationResults.Select(v => v.ErrorMessage));
                throw new ArgumentException("Validation failed");
            }
            // Skapa den nya erfarenheten från DTO:n och koppla den till personen
            var experience = new Experience
            {
                Company = experienceDTO.ExperienceCompany,
                Title = experienceDTO.ExperienceTitle,
                Years = experienceDTO.ExperienceYears,
                Description = experienceDTO.ExperienceDescription,
                Person = persontoFind // Koppla erfarenheten till personen
            };

            // Lägg till erfarenheten i databasen
            context.Experiences.Add(experience);
            await context.SaveChangesAsync();
            return experienceDTO;
            // Returnera enbart den skapade erfarenheten (utan relaterad person)

        }

        public async Task<ExperienceDto> UpdateExperience(ExperienceDto experienceDTO, int id)
        {
            var experienceChange = await context.Experiences.FirstOrDefaultAsync(e => e.Id == id);
            if (experienceChange == null)
            {
                throw new KeyNotFoundException("Experience not found");
            }
            var validationResults = ValidatorHelper.ValidateInput(experienceDTO);
            if (validationResults.Count > 0)
            {
                var error = string.Join(", ", validationResults.Select(v => v.ErrorMessage));
                throw new ArgumentException("Validation failed");
            }

            experienceChange.Company = experienceDTO.ExperienceCompany;
            experienceChange.Title = experienceDTO.ExperienceTitle;
            experienceChange.Years = experienceDTO.ExperienceYears;
            experienceChange.Description = experienceDTO.ExperienceDescription;
            await context.SaveChangesAsync();
            return experienceDTO;
        }

        public async Task<ExperienceDto> DeleteExperience(int id)
        {
            var experience = await context.Experiences.FirstOrDefaultAsync(e => e.Id == id);
            if (experience == null)
            {
                throw new KeyNotFoundException("Experience not found");
            }
            context.Experiences.Remove(experience);
            try
            {
                await context.SaveChangesAsync();
                return null;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Experience could not be deleted");
            }
        }

        public async Task<ExperienceDto> GetExperienceById(int id)
        {
            var experience = await context.Experiences.FirstOrDefaultAsync(e => e.Id == id);
            if (experience == null)
            {
                throw new KeyNotFoundException("Experience not found");
            }
            return new ExperienceDto
            {
                ExperienceCompany = experience.Company,
                ExperienceTitle = experience.Title,
                ExperienceYears = experience.Years,
                ExperienceDescription = experience.Description
            };
        }
    }
}
