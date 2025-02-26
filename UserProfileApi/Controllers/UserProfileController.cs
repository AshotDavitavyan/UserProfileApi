using Microsoft.AspNetCore.Mvc;
using UserProfileApi.Services;
using UserProfileApi.Models;

namespace UserProfileApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserProfileController : Controller
	{
		public readonly IUserProfileService _userProfileService;
		public UserProfileController(IUserProfileService userProfileService)
		{
			_userProfileService = userProfileService;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<UserProfile>> GetUserProfile(int id)
		{
			var profile = await _userProfileService.GetUserProfileAsync(id);
			if (profile == null)
				return NotFound();
			return Ok(profile);
		}

		[HttpPost]
		public async Task<ActionResult> CreateProfile([FromBody]UserProfile profile)
		{
			var createdProfile = await _userProfileService.CreateProfileAsync(profile);
			return CreatedAtAction(nameof(GetUserProfile), new { id = createdProfile.Id}, createdProfile);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateProfile(int id, [FromBody] UserProfile updatedProfile)
		{
			await _userProfileService.UpdateProfileAsync(id, updatedProfile);
			return NoContent();
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteProfile(int id)
		{
			await _userProfileService.DeleteProfileAsync(id);
			return NoContent();
		}

		[HttpPost("upload-avatar")]
		public async Task<IActionResult> UploadAvatar(IFormFile file)
		{
			if (file.Length > 100 * 1024)
				return BadRequest("File size exceeds 100KB");
			var avatarPath = await _userProfileService.SaveAvatarAsync(file);
			return Ok(new { AvatarPath = avatarPath});
		}
	}
}
