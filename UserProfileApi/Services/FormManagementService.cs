using Microsoft.EntityFrameworkCore;
using UserProfileApi.Data;
using UserProfileApi.Models;

namespace UserProfileApi.Services
{
	public interface IFormManagementService
	{
		Task AddFieldAsync(ProfileField newField);
		Task UpdateFieldAsync(int id, ProfileField updatedField);
		Task RemoveFieldAsync(int id);
		Task<List<ProfileField>> GetAllFieldsAsync();
	}
	public class FormManagementService : IFormManagementService
	{
		public readonly AppDbContext _context;
		public FormManagementService(AppDbContext context) { _context = context; }
		public async Task AddFieldAsync(ProfileField newField)
		{
			_context.ProfileFields.Add(newField);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateFieldAsync(int fieldId, ProfileField updatedField)
		{
			var existingField = await _context.ProfileFields.FindAsync(fieldId);
			if (existingField == null)
				throw new KeyNotFoundException("Field not found");
			_context.Entry(existingField).CurrentValues.SetValues(updatedField);
			await _context.SaveChangesAsync();
		}

		public async Task RemoveFieldAsync(int id)
		{
			var field = await _context.ProfileFields.FindAsync(id);
			if (field == null) throw new KeyNotFoundException("Field not found");

			_context.ProfileFields.Remove(field);
			await _context.SaveChangesAsync();
		}

		public async Task<List<ProfileField>> GetAllFieldsAsync()
		{
			return await _context.ProfileFields.ToListAsync();
		}
	}
}
