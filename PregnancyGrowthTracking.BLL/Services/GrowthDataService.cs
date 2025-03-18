using PregnancyGrowthTracking.DAL.DTOs;
using PregnancyGrowthTracking.DAL.Entities;
using PregnancyGrowthTracking.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.BLL.Services
{
    public class GrowthDataService : IGrowthDataService
    {
        private readonly IGrowthDataRepository _growthDataRepository;
        private readonly IFoetusRepository _foetusRepository;
        private readonly IGrowthStandardRepository _growthStandardRepository;
        private readonly IFoetusService _foetusService;

        public GrowthDataService(IGrowthDataRepository growthDataRepository, IFoetusRepository foetusRepository, IGrowthStandardRepository growthStandardRepository, IFoetusService foetusService)
        {
            _growthDataRepository = growthDataRepository;
            _foetusRepository = foetusRepository;
            _growthStandardRepository = growthStandardRepository;
            _foetusService = foetusService;
        }

        public async Task<bool> IsAddOrUpdate(int foetusId, int userId, GrowthDataDto request)
        {
            bool isOwnedByUser = await _foetusRepository.IsFoetusOwnedByUserAsync(foetusId, userId);
            if (!isOwnedByUser)
            {
                throw new UnauthorizedAccessException("You do not have permission to modify this data.");
            }

            var currentGrowthData = await _growthDataRepository.GetGrowthDataByFoetusIdAndAge(foetusId, request.Age);

            if (currentGrowthData == null)
            {
                return await AddGrowthDataAsync(foetusId, request);
            }
            else
            {
                return await UpdateGrowthDataAsync(foetusId, request);
            }
        }

        public async Task<bool> AddGrowthDataAsync(int foetusId, GrowthDataDto request)
        {
            var foetus = await _foetusRepository.GetFoetusByIdAsync(foetusId);
            if (foetus == null)
            {
                throw new KeyNotFoundException("Foetus not found.");
            }

            if (request.HC == null || request.AC == null || request.FL == null || request.EFW == null)
            {
                throw new ArgumentException("All measurements (HC, AC, FL, EFW) are required when creating new growth data.");
            }

            if (await _growthDataRepository.GetGrowthDataByIdAsync(foetusId) == null)
            {
                await _foetusService.AutoGenerateExpectedBirthDate(foetusId, request.Age);
            }

            // Cập nhật GestationalAge trong bảng Foetus
            await _foetusRepository.UpdateGestationalAgeAsync(foetusId, request.Age);

            int? growthStandardId = await _growthDataRepository.GetGrowthStandardIdByAgeAsync(request.Age);
            if (growthStandardId == null)
            {
                throw new ArgumentException("No growth standard found for the given age.");
            }

            // NẾU ĐÂY LÀ LẦN NHẬP ĐẦU TIÊN THÌ HỆ THỐNG TỰ ĐỘNG GENERATE RA NGÀY DỰ SINH VÀ LƯU VÀO BẢNG FOETUS
            bool hasExistingData = await _growthDataRepository.HasGrowthDataAsync(foetusId);

            if (!hasExistingData && foetus.ExpectedBirthDate == null)
            {
                await _foetusService.AutoGenerateExpectedBirthDate(foetusId, request.Age);
            }

            var growthData = new GrowthDatum
            {
                FoetusId = foetusId,
                Date = DateOnly.FromDateTime(DateTime.UtcNow),
                GrowthStandardId = growthStandardId.Value,
                Age = request.Age,
                Hc = request.HC,
                Ac = request.AC,
                Fl = request.FL,
                Efw = request.EFW
            };

            return await _growthDataRepository.AddGrowthDataAsync(growthData);
        }

        public async Task<IEnumerable<GrowthDataWithAlertResponseDto>> GetGrowthDataByFoetusIdAsync(int foetusId, int userId)
        {
            // Kiểm tra quyền truy cập
            bool isOwnedByUser = await _foetusRepository.IsFoetusOwnedByUserAsync(foetusId, userId);
            if (!isOwnedByUser)
            {
                throw new UnauthorizedAccessException("You do not have permission to view this data.");
            }

            var growthDataList = await _growthDataRepository.GetGrowthDataByFoetusIdAsync(foetusId);
            var result = new List<GrowthDataWithAlertResponseDto>();

            foreach (var gd in growthDataList)
            {
                // Lấy chuẩn tăng trưởng cho tuổi thai tương ứng
                var growthStandard = await _growthStandardRepository.GetGrowthStandardByAgeAsync(gd.Age ?? 0);
                if (growthStandard == null) continue;

                // Tạo DTO cho từng chỉ số với alert
                var responseDto = new GrowthDataWithAlertResponseDto
                {
                    GrowthDataId = gd.GrowthDataId,
                    Age = gd.Age ?? 0,
                    //GrowthStandardId = gd.GrowthStandardId,
                    Date = gd.Date.HasValue ? gd.Date.Value.ToString("yyyy-MM-dd") : null,

                    // Head Circumference (HC)
                    HC = new GrowthMeasurementWithAlert
                    {
                        Value = gd.Hc ?? 0,
                        MinRange = growthStandard.HcMedian * 0.9,
                        MaxRange = growthStandard.HcMedian * 1.1,
                        IsAlert = gd.Hc.Value < growthStandard.HcMedian * 0.9 || gd.Hc.Value > growthStandard.HcMedian * 1.1,
                    },

                    // Abdominal Circumference (AC)
                    AC = new GrowthMeasurementWithAlert
                    {
                        Value = gd.Ac ?? 0,
                        MinRange = growthStandard.AcMedian * 0.9 ?? 0,
                        MaxRange = growthStandard.AcMedian * 1.1 ?? 0,
                        IsAlert = gd.Ac.Value < growthStandard.AcMedian * 0.9 || gd.Ac.Value > growthStandard.AcMedian * 1.1
                    },

                    // Femur Length (FL)
                    FL = new GrowthMeasurementWithAlert
                    {
                        Value = gd.Fl ?? 0,
                        MinRange = growthStandard.FlMedian * 0.9 ?? 0,
                        MaxRange = growthStandard.FlMedian * 1.1 ?? 0,
                        IsAlert = gd.Fl.Value < growthStandard.FlMedian * 0.9 || gd.Fl.Value > growthStandard.FlMedian * 1.1
                    },

                    // Estimated Fetal Weight (EFW)
                    EFW = new GrowthMeasurementWithAlert
                    {
                        Value = gd.Efw ?? 0,
                        MinRange = growthStandard.EfwMedian * 0.9 ?? 0,
                        MaxRange = growthStandard.EfwMedian * 1.1 ?? 0,
                        IsAlert = gd.Efw.Value < growthStandard.EfwMedian * 0.9  || gd.Efw.Value > growthStandard.EfwMedian * 1.1
                    }
                };

                result.Add(responseDto);
            }

            return result.OrderBy(x => x.Age);
        }

        public async Task<bool> UpdateGrowthDataAsync(int foetusId, GrowthDataDto request)
        {
            var growthData = await _growthDataRepository.GetGrowthDataByFoetusIdAndAge(foetusId, request.Age);
            if (growthData == null)
            {
                throw new KeyNotFoundException($"Growth data not found for foetus {foetusId} at age {request.Age}");
            }

            // Chỉ cập nhật các giá trị được cung cấp, giữ nguyên các giá trị cũ nếu không được cung cấp
            if (request.HC.HasValue) growthData.Hc = request.HC;
            if (request.AC.HasValue) growthData.Ac = request.AC;
            if (request.FL.HasValue) growthData.Fl = request.FL;
            if (request.EFW.HasValue) growthData.Efw = request.EFW;

            // Cập nhật ngày chỉnh sửa
            growthData.Date = DateOnly.FromDateTime(DateTime.UtcNow);

            return await _growthDataRepository.UpdateGrowthDataAsync(growthData);
        }

        public async Task<Dictionary<string, GrowthDataAlertDTO>> AlertReturnWithRange(GrowthDataDto growthData)
        {
            GrowthStandard? growthStandard = await _growthStandardRepository.GetGrowthStandardByAgeAsync(growthData.Age);
            if (growthStandard == null)
            {
                throw new KeyNotFoundException($"Growth standard not found for age {growthData.Age}");
            }

            var alerts = new Dictionary<string, GrowthDataAlertDTO>();

            // Kiểm tra HC (Head Circumference)
            if (growthData.HC.HasValue && growthStandard.HcMedian.HasValue)
            {
                double hcRangeMin = growthStandard.HcMedian.Value * 0.9;
                double hcRangeMax = growthStandard.HcMedian.Value * 1.1;

                alerts["HC"] = new GrowthDataAlertDTO
                {
                    IsAlert = growthData.HC.Value < hcRangeMin || growthData.HC.Value > hcRangeMax,
                    CurrentValue = growthData.HC.Value,
                    MinRange = hcRangeMin,
                    MaxRange = hcRangeMax
                };
            }

            // Kiểm tra AC (Abdominal Circumference)
            if (growthData.AC.HasValue && growthStandard.AcMedian.HasValue)
            {
                double acRangeMin = growthStandard.AcMedian.Value * 0.9;
                double acRangeMax = growthStandard.AcMedian.Value * 1.1;
                alerts["AC"] = new GrowthDataAlertDTO
                {
                    IsAlert = growthData.AC.Value < acRangeMin || growthData.AC.Value > acRangeMax,
                    CurrentValue = growthData.AC.Value,
                    MinRange = acRangeMin,
                    MaxRange = acRangeMax
                };
            }

            // Kiểm tra FL (Femur Length)
            if (growthData.FL.HasValue && growthStandard.FlMedian.HasValue)
            {
                double flRangeMin = growthStandard.FlMedian.Value * 0.9;
                double flRangeMax = growthStandard.FlMedian.Value * 1.1;
                alerts["FL"] = new GrowthDataAlertDTO
                {
                    IsAlert = growthData.FL.Value < flRangeMin || growthData.FL.Value > flRangeMax,
                    CurrentValue = growthData.FL.Value,
                    MinRange = flRangeMin,
                    MaxRange = flRangeMax
                };
            }

            // Kiểm tra EFW (Estimated Fetal Weight)
            if (growthData.EFW.HasValue && growthStandard.EfwMedian.HasValue)
            {
                double efwRangeMin = growthStandard.EfwMedian.Value * 0.9;
                double efwRangeMax = growthStandard.EfwMedian.Value * 1.1;
                alerts["EFW"] = new GrowthDataAlertDTO
                {
                    IsAlert = efwRangeMin > growthData.EFW.Value || growthData.EFW.Value > efwRangeMax,
                    CurrentValue = growthData.EFW.Value,
                    MinRange = efwRangeMin,
                    MaxRange = efwRangeMax
                };
            }

            return alerts;
        }
    }
}
