namespace UserProfileApi.Models
{
	public class UserProfile
	{
		public int Id { get; set; }
		public string NickName { get; set; } = string.Empty;
		public string AvatarUrl { get; set; } = string.Empty;
		public string Gender { get; set; } = string.Empty;
		public List<string> ExpertiseAreas { get; set; } = new();
		public DateTime DateOfBirth { get; set; }
		public TimeSpan TimeOfBirth { get; set; }
		public string PlaceOfBirth { get; set; } = string.Empty;
		public string PlaceOfResidency { get; set; } = string.Empty;
		public Dictionary<string, string> ExtraFields { get; set; } = new();
	}
}
