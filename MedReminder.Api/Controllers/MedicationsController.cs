using MedReminder.Api.Services.Interfaces;
using MedReminder.Api.Tools;
using MedReminder.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MedReminder.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MedicationsController : ControllerBase
    {
        private readonly IMedicationService _medicationService;

        public MedicationsController(IMedicationService medicationService)
        {
            _medicationService = medicationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMedicationAsync(MedicationDTO medicationDTO)
        {
            return Ok(await _medicationService.CreateMedicationAsync(medicationDTO));
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetMedicationByUserIdAsync(int userId)
        {
            var medications = await _medicationService.GetMedicationByUserIdAsync(userId);
            if (medications == null) { return NotFound(); }
            var medicationDtos = medications.Select(m => m.ToDTO()).ToList();
            return Ok(medicationDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedicationByIdAsync(int id)
        {
            var medication = await _medicationService.GetMedicationByIdAsync(id);
            if (medication == null) { return NotFound(); }
            var medicationDTO = medication.ToDTO();
            return Ok(medicationDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicationAsync(int id)
        {
            if (!await _medicationService.DeleteMedicationAsync(id)) { return NotFound(); }
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedicationAsync(int id, MedicationDTO medicationDTO)
        {
            medicationDTO.Id = id;
            if (!await _medicationService.UpdateMedicationAsync(medicationDTO)) { return NotFound(); }
            return Ok();
        }
    }
}

