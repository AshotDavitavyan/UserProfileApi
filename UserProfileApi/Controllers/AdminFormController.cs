using Microsoft.AspNetCore.Mvc;
using UserProfileApi.Services;
using UserProfileApi.Models;

namespace UserProfileApi.Controllers
{
	[Route("api/admin/form")]
	[ApiController]
	public class AdminFormController : ControllerBase
	{
		private readonly IFormManagementService _formManagementService;
		public AdminFormController(IFormManagementService formManagementService)
		{
			_formManagementService = formManagementService;
		}

		[HttpGet]
		public async Task<ActionResult<List<ProfileField>>> GetProfileFields()
		{
			var fields = await _formManagementService.GetAllFieldsAsync();
			return Ok(fields);
		}

		[HttpPost]
		public async Task<IActionResult> AddField([FromBody] ProfileField newField)
		{
			if (newField == null) return BadRequest();

			await _formManagementService.AddFieldAsync(newField);
			return Ok();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateField(int id, [FromBody] ProfileField updatedField)
		{
			if (updatedField == null) return BadRequest();

			try
			{
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
