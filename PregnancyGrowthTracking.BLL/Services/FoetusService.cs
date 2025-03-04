using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PregnancyGrowthTracking.DAL.DTOs;
using PregnancyGrowthTracking.DAL.Entities;
using PregnancyGrowthTracking.DAL.Repositories;

namespace PregnancyGrowthTracking.BLL.Services
{
    public class FoetusService : IFoetusService
    {
        private readonly IFoetusRepository _foetusRepository;

        public FoetusService(IFoetusRepository foetusRepository)
        {
            _foetusRepository = foetusRepository;
        }

        public async Task<FoetusResponseDto> CreateFoetusAsync(int userId, FoetusCreateDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Gender))
            {
                throw new ArgumentException("Name and Gender are required.");
            }
            bool isNameExists = await _foetusRepository.IsFoetusNameExistsAsync(userId, request.Name);
            if (isNameExists)
            {
                throw new ArgumentException("You already have a foetus with this name. Please choose a different name.");
            }


            var foetus = new Foetus
            {
                UserId = userId,
                Name = request.Name,
                Gender = request.Gender,
                GestationalAge = 0, // Ban đầu tuổi thai là 0 tuần
                ExpectedBirthDate = null // Để NULL khi tạo mới
            };

            var createdFoetus = await _foetusRepository.CreateFoetusAsync(foetus);

            return new FoetusResponseDto
            {
                UserId = createdFoetus.UserId ?? 0,
                FoetusId = createdFoetus.FoetusId,
                Name = createdFoetus.Name,
                Gender = createdFoetus.Gender
            };
        }
        public async Task<IEnumerable<FoetusResponseDto>> GetFoetusesByUserIdAsync(int userId)
        {
            var foetuses = await _foetusRepository.GetFoetusesByUserIdAsync(userId);

            return foetuses.Select(f => new FoetusResponseDto
            {
                FoetusId = f.FoetusId,
                UserId = f.UserId ?? 0,
                Name = f.Name,
                Gender = f.Gender,
                ExpectedBirthDate = f.ExpectedBirthDate,
                GestationalAge = f.GestationalAge
            }).ToList();
        }
        public async Task<bool> DeleteFoetusAsync(int foetusId, int userId)
        {
            return await _foetusRepository.DeleteFoetusAsync(foetusId, userId);
        }
    }

}
