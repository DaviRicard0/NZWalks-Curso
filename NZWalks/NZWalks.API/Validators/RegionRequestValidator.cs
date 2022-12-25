using FluentValidation;

namespace NZWalks.API.Validators;

public class RegionRequestValidator:AbstractValidator<Models.DTO.RegionRequest>
{
	public RegionRequestValidator()
	{
		RuleFor(x => x.Code).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Area).GreaterThan(0);
        RuleFor(x => x.Lat).GreaterThan(0);
        RuleFor(x => x.Long).GreaterThan(0);
        RuleFor(x => x.Population).GreaterThanOrEqualTo(0);
    }
}
