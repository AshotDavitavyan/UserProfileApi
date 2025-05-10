using AutoMapper;
using UserProfileApi.DTOs;
using UserProfileApi.Models;


namespace UserProfileApi.Mappings
{
	public class ProfileFieldProfile : Profile
	{
		public ProfileFieldProfile() 
		{
			CreateMap<ProfileField, ProfileFieldDto>();
			CreateMap<ProfileFieldDto, ProfileField>()
				.ForMember(dest => dest.Id, opt => opt.Ignore());
		}
	}
}
