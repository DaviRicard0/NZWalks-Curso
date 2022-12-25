using FluentValidation;

namespace NZWalks.API.Validators;

public class WalkRequestValidator : AbstractValidator<Models.DTO.WalkRequest>
{
	public WalkRequestValidator()
	{
		RuleFor(x => x.Name).NotEmpty();
		RuleFor(x => x.Length).GreaterThan(0);
    }
}
