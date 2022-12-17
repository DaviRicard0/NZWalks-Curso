using AutoMapper;

namespace NZWalks.API.Models.Profiles;

public class WalksProfile : Profile
{
    public WalksProfile()
    {
        CreateMap<Domain.Walk, DTO.Walk>()
            .ReverseMap();

        CreateMap<Domain.WalkDifficulty, DTO.WalkDifficulty>()
            .ReverseMap();
    }
}
