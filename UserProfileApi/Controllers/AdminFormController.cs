using Microsoft.AspNetCore.Mvc;
using UserProfileApi.Services;
using UserProfileApi.Models;
using AutoMapper;
using UserProfileApi.DTOs;

namespace UserProfileApi.Controllers
{
	[Route("api/admin/form")]
	[ApiController]
	public class AdminFormController : ControllerBase
	{
		private readonly IFormManagementService _formManagementService;
		private readonly IMapper _mapper;
		public AdminFormController(IFormManagementService formManagementService, IMapper mapper)
		{
			_formManagementService = formManagementService;
			_mapper = mapper;
		}


		[HttpGet]
		public async Task<ActionResult<List<ProfileField>>> GetProfileFields()
		{
			var fields = await _formManagementService.GetAllFieldsAsync();
			return Ok(fields);
		}

		[HttpPost]
		public async Task<IActionResult> AddField([FromBody] ProfileFieldDto newFieldDto)
		{
			if (newFieldDto == null) return BadRequest();
			var newField = _mapper.Map<ProfileField>(newFieldDto);
			await _formManagementService.AddFieldAsync(newField);
			return Ok();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateField(int id, [FromBody] ProfileFieldDto updatedFieldDto)
		{
			if (updatedFieldDto == null) return BadRequest();

			try
			{
				var updatedField = _mapper.Map<ProfileField>(updatedFieldDto);
				await _formManagementService.UpdateFieldAsync(id, updatedField);
				return Ok();
			}
			catch (KeyNotFoundException)
			{
				return NotFound("Field not found");
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> RemoveField(int id)
		{
			try
			{
				await _formManagementService.RemoveFieldAsync(id);
				return Ok();
			}
			catch (KeyNotFoundException)
			{
				return NotFound("Field not found");
			}
		}
	}
}
