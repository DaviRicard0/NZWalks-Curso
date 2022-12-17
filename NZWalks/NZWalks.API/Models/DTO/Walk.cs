using NZWalks.API.Models.Domain;

namespace NZWalks.API.Models.DTO;

public class Walk
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double Length { get; set; }

    #region Foreign key
    public Guid RegionId { get; set; }
    public Guid WalkDifficultyId { get; set; }
    #endregion

    #region Navigation Property
    public Region Region { get; set; }
    public WalkDifficulty WalkDifficulty { get; set; }
    #endregion
}
