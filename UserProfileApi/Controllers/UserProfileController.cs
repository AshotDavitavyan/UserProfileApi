using Microsoft.AspNetCore.Mvc;
using UserProfileApi.Services;
using UserProfileApi.Models;
using UserProfileApi.DTOs;
using AutoMapper;

namespace UserProfileApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserProfileController : Controller
	{
		private readonly IUserProfileService _userProfileService;
		private readonly IMapper _mapper;
		public UserProfileController(IUserProfileService userProfileService, IMapper mapper)
		{
			_userProfileService = userProfileService;
			_mapper = mapper;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<UserProfileDto>> GetUserProfile(int id)
		{
			var profile = await _userProfileService.GetUserProfileAsync(id);
			if (profile == null)
				return NotFound();
			var profileDto = _mapper.Map<UserProfileDto>(profile);
			return Ok(profileDto);
		}

		[HttpPost]
		public async Task<ActionResult> CreateProfile([FromBody]UserProfileDto profileDto)
		{
			if (profileDto == null) return BadRequest();
			var userProfile = _mapper.Map<UserProfile>(profileDto);
			var createdProfile = await _userProfileService.CreateProfileAsync(userProfile);
			var createdProfileDto = _mapper.Map<UserProfileDto>(createdProfile);
			return CreatedAtAction(nameof(GetUserProfile), new { id = createdProfile.Id}, createdProfileDto);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateProfile(int id, [FromBody] UserProfileDto updatedProfileDto)
		{
			var updatedProfile = _mapper.Map<UserProfile>(updatedProfileDto);
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
