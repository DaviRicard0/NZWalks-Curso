namespace NZWalks.API.Models.DTO;

public record RegionRequest(string Code, string Name, double Area,double Lat, double Long, long Population);