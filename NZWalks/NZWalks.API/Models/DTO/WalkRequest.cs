using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO;

public class WalkRequest
{
    //[Required]
    public string Name { get; set; }

    //[Required]
    //[Range(1, double.MaxValue)]
    public double Length { get; set; }

    #region Foreign key
    //[Required]
    public Guid RegionId { get; set; }

    //[Required]
    public Guid WalkDifficultyId { get; set; }
    #endregion
}
