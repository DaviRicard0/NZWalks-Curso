using AutoMapper;

namespace NZWalks.API.Models.Profiles;

public class WalkDifficultyProfile : Profile
{
    public WalkDifficultyProfile()
    {
        CreateMap<Domain.WalkDifficulty, DTO.WalkDifficulty>()
            .ReverseMap();
    }
}
