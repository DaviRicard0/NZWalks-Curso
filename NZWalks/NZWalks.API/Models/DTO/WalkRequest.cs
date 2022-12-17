namespace NZWalks.API.Models.DTO;

public class WalkRequest
{
    public string Name { get; set; }
    public double Length { get; set; }

    #region Foreign key
    public Guid RegionId { get; set; }
    public Guid WalkDifficultyId { get; set; }
    #endregion
}
