using MedReminder.Api.Services.Interfaces;
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
            try
            {
                var medication = await _medicationService.CreateMedicationAsync(medicationDTO);
                return CreatedAtAction(nameof(CreateMedicationAsync), new { id = medication.Id }, medication);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetMedicationByUserIdAsync(int userId)
        {
            var medications = await _medicationService.GetMedicationByUserIdAsync(userId);
            return Ok(medications);
        }
    }
}
