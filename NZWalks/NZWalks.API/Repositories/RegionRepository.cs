using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public class RegionRepository : IRegionRepository
{
    private readonly ApplicationDbContext context;

    public RegionRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Region>> GetAllAsync()
    {
        return await context.Regions.ToListAsync();
    }

    public async Task<Region?> GetAsync(Guid id)
    {
        return await context.Regions.FirstOrDefaultAsync(region => region.Id == id);
    }
    public async Task<Region?> AddAsync(Region region)
    {
        region.Id = Guid.NewGuid();
        await context.Regions.AddAsync(region);
        await context.SaveChangesAsync();
        return region;
    }

    public async Task<Region?> DeleteAsync(Guid id)
    { 
        var region = await context.Regions.FirstOrDefaultAsync(region => region.Id == id);

        if (region is null)
        {
            return null;
        }

        context.Regions.Remove(region);
        await context.SaveChangesAsync();

        return region;
    }

    public async Task<Region?> UpdateAsync(Guid id,Region region)
    {
        var hasRegion = await context.Regions.FirstOrDefaultAsync(region => region.Id == id);

        if (hasRegion is null)
        {
            return null;
        }

        hasRegion.Code= region.Code;
        hasRegion.Name= region.Name;
        hasRegion.Area= region.Area;
        hasRegion.Lat= region.Lat;
        hasRegion.Long= region.Long;
        hasRegion.Population= region.Population;

        await context.SaveChangesAsync();

        return hasRegion;
    }
}
