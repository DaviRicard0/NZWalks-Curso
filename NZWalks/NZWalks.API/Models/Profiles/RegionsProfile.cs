using AutoMapper;

namespace NZWalks.API.Models.Profiles;

public class RegionsProfile:Profile
{
	public RegionsProfile()
	{
		CreateMap<Domain.Region,DTO.Region>()
			.ReverseMap();
	}
}
