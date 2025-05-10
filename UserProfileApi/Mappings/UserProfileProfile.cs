using AutoMapper;
using UserProfileApi.Models;
using UserProfileApi.DTOs;

namespace UserProfileApi.Mappings
{
	public class UserProfileProfile : Profile
	{
		public UserProfileProfile()
		{
			CreateMap<UserProfileDto, UserProfile>();
			CreateMap<UserProfile, UserProfileDto>();
		}
	}
}
