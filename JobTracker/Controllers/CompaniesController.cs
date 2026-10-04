using JobTracker.DTOs;
using JobTracker.Models;
using JobTracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _service;

        public CompaniesController(ICompanyService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            string? userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            List<Company> companies =
                await _service.GetAllAsync(userId);

            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            string? userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            Company? company = await _service.GetByIdAsync(id, userId);

            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateCompanyDTO dto)
        {
            string? userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            Company company = await _service.CreateAsync(dto, userId);

            return CreatedAtAction(
                nameof(GetById),
                new { id = company.Id },
                company);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            UpdateCompanyDTO dto)
        {
            string? userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            bool updated = await _service.UpdateAsync(id,dto,userId);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string? userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            bool deleted = await _service.DeleteAsync(id,userId);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}