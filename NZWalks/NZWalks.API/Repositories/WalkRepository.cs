using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public class WalkRepository : IWalkRepository
{
    private readonly ApplicationDbContext context;

    public WalkRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Walk>> GetAllAsync()
    {
        return await context.Walks
            .Include(x => x.Region)
            .Include(x => x.WalkDifficulty)
            .ToListAsync();
    }

    public async Task<Walk?> GetAsync(Guid id)
    {
        return await context.Walks
            .Include(x => x.Region)
            .Include(x => x.WalkDifficulty)
            .FirstOrDefaultAsync(walk => walk.Id == id);
    }
    public async Task<Walk?> AddAsync(Walk walk)
    {
        walk.Id = Guid.NewGuid();
        await context.Walks.AddAsync(walk);
        await context.SaveChangesAsync();
        return walk;
    }

    public async Task<Walk?> DeleteAsync(Guid id)
    {
        var walk = await context.Walks.FirstOrDefaultAsync(walk => walk.Id == id);

        if (walk is null)
        {
            return null;
        }

        context.Walks.Remove(walk);
        await context.SaveChangesAsync();

        return walk;
    }

    public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
    {
        var hasWalk = await context.Walks.FirstOrDefaultAsync(walk => walk.Id == id);

        if (hasWalk is null)
        {
            return null;
        }

        hasWalk.Name = walk.Name;
        hasWalk.Length = walk.Length;
        hasWalk.WalkDifficultyId = walk.WalkDifficultyId;
        hasWalk.RegionId= walk.RegionId;

        await context.SaveChangesAsync();

        return hasWalk;
    }
}
