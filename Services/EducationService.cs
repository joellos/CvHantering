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
    public class EducationService
    {
        private readonly CvDbContext context;
        public EducationService(CvDbContext _context)
        {
            context = _context;
        }

        public async Task<List<EducationDto>> GetEducationById(int id)
        {
            var person = await context.Persons.Include(p => p.Educations).FirstOrDefaultAsync(p => p.Id == id);
            if (person == null)
            {
                throw new KeyNotFoundException("Person not found");
            }
            var educations = person.Educations.Select(e => new EducationDto
            {
                EducationSchool = e.School,
                EducationDegree = e.Degree,
                EducationStartDate = e.StartDate,
                EducationEndDate = e.EndDate
            }).ToList();
            return educations;
        }

        public async Task<EducationDto> AddEducation(EducationDto educationDTO, int Id)
        {
            var persontoFind = await context.Persons.Include(p => p.Educations)
 .FirstOrDefaultAsync(p => p.Id == Id);


            if (persontoFind == null)
            {
                throw new KeyNotFoundException("Person not found");
            }

            var validationResults = ValidatorHelper.ValidateInput(educationDTO);
            if (validationResults.Count > 0)
            {
                var error = string.Join(", ", validationResults.Select(v => v.ErrorMessage));
                throw new ArgumentException("Validation failed");
            }

            // Skapa den nya erfarenheten från DTO:n och koppla den till personen
            var education = new Education
            {
                School = educationDTO.EducationSchool,
                Degree = educationDTO.EducationDegree,
                StartDate = educationDTO.EducationStartDate,
                EndDate = educationDTO.EducationEndDate,
                Person = persontoFind // Koppla erfarenheten till personen
            };

            // Lägg till erfarenheten i databasen
            context.Educations.Add(education);
            await context.SaveChangesAsync();
            return educationDTO;
        }

        public async Task<EducationDto> UpdateEducation(EducationDto educationDTO, int id)
        {
            var educationChange = await context.Educations.FirstOrDefaultAsync(e => e.Id == id);
            if (educationChange == null)
            {
                throw new KeyNotFoundException("Education not found");
            }

            var validationResults = ValidatorHelper.ValidateInput(educationDTO);
            if (validationResults.Count > 0)
            {
                var error = string.Join(", ", validationResults.Select(v => v.ErrorMessage));
                throw new ArgumentException("Validation failed");
            }
            educationChange.School = educationDTO.EducationSchool;
            educationChange.Degree = educationDTO.EducationDegree;
            educationChange.StartDate = educationDTO.EducationStartDate;
            educationChange.EndDate = educationDTO.EducationEndDate;
            await context.SaveChangesAsync();
            return educationDTO;
        }

        public async Task<EducationDto> DeleteEducation(int id)
        {

            var education = await context.Educations.FirstOrDefaultAsync(e => e.Id == id);
            if (education == null)
            {
                throw new KeyNotFoundException("Education not found");
            }
            context.Educations.Remove(education);
            try
            {
                await context.SaveChangesAsync();
                return null;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Failed to delete education.");

            }
        }
    }
}
