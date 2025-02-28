using UserProfileApi.Data;
using UserProfileApi.Models;

namespace UserProfileApi.Services
{
	public interface IUserProfileService
	{
		Task<UserProfile?> GetUserProfileAsync(int id);
		Task<UserProfile> CreateProfileAsync(UserProfile profile);
		Task UpdateProfileAsync(int id, UserProfile profile);
		Task DeleteProfileAsync(int id);
		Task<string> SaveAvatarAsync(IFormFile file);
	}
	public class UserProfileService : IUserProfileService
	{
		private readonly AppDbContext _context;
		private readonly string _avatarsFolderUrl = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "avatars");
		public UserProfileService(AppDbContext context)
		{
			_context = context;
		}
		public async Task<UserProfile?> GetUserProfileAsync(int id)
		{
			return await _context.UserProfiles.FindAsync(id);
		}

		public async Task<UserProfile> CreateProfileAsync(UserProfile profile) 
		{
			_context.UserProfiles.Add(profile);
			await _context.SaveChangesAsync();
			return profile;
		}

		public async Task UpdateProfileAsync(int id, UserProfile updatedProfile)
		{
			var profile = await _context.UserProfiles.FindAsync(id);
			if (profile == null)
				throw new KeyNotFoundException("User Profile not found.");
			profile.NickName = updatedProfile.NickName;
			profile.AvatarUrl = updatedProfile.AvatarUrl;
			profile.Gender = updatedProfile.Gender;
			profile.ExpertiseAreas = updatedProfile.ExpertiseAreas;
			profile.DateOfBirth = updatedProfile.DateOfBirth;
			profile.TimeOfBirth	= updatedProfile.TimeOfBirth;
			profile.PlaceOfBirth = updatedProfile.PlaceOfBirth;
			profile.PlaceOfResidency = updatedProfile.PlaceOfResidency;
			profile.ExtraFields = updatedProfile.ExtraFields;
			await _context.SaveChangesAsync();
		}

		public async Task DeleteProfileAsync(int id)
		{
			var profile = await _context.UserProfiles.FindAsync(id);
			if (profile == null)
				throw new KeyNotFoundException("User Profile not found.");
			_context.UserProfiles.Remove(profile);
			await _context.SaveChangesAsync();
		}

		public async Task<string> SaveAvatarAsync(IFormFile file)
		{
			var avatarPath = Path.Combine(_avatarsFolderUrl, file.FileName);
			Directory.CreateDirectory(_avatarsFolderUrl);
			using (var stream = new FileStream(avatarPath, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}
			return avatarPath;
		}
	}
}
