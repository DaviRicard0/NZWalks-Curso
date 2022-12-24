using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO;

//public record RegionRequest(string Code, string Name, double Area,double Lat, double Long, long Population);

public class RegionRequest
{
    [Required]
    public string Code { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    [Range(1, Double.PositiveInfinity)]
    public double Area { get; set; }

    [Required]
    [Range(1, Double.PositiveInfinity)]
    public double Lat { get; set; }

    [Required]
    [Range(1, Double.PositiveInfinity)]
    public double Long { get; set; }

    [Required]
    [Range(0, Double.PositiveInfinity)]
    public long Population { get; set; }
}