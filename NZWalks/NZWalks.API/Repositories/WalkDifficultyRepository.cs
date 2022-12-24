using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public class WalkDifficultyRepository : IWalkDifficultyRepository
{
    private readonly ApplicationDbContext context;

    public WalkDifficultyRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
    {
        return await context.WalkDifficulty.ToListAsync();
    }

    public async Task<WalkDifficulty?> GetAsync(Guid id)
    {
        return await context.WalkDifficulty
            .FirstOrDefaultAsync(walkDifficulty => walkDifficulty.Id == id);
    }
    public async Task<WalkDifficulty?> AddAsync(WalkDifficulty walkDifficulty)
    {
        walkDifficulty.Id = Guid.NewGuid();
        await context.WalkDifficulty.AddAsync(walkDifficulty);
        await context.SaveChangesAsync();
        return walkDifficulty;
    }

    public async Task<WalkDifficulty?> DeleteAsync(Guid id)
    {
        var walkDifficulty = await context.WalkDifficulty.FirstOrDefaultAsync(walkDifficulty => walkDifficulty.Id == id);

        if (walkDifficulty is null)
        {
            return null;
        }

        context.WalkDifficulty.Remove(walkDifficulty);
        await context.SaveChangesAsync();

        return walkDifficulty;
    }

    public async Task<WalkDifficulty?> UpdateAsync(Guid id, WalkDifficulty walkDifficulty)
    {
        var hasWalkDifficulty = await context.WalkDifficulty.FirstOrDefaultAsync(walkDifficulty => walkDifficulty.Id == id);

        if (hasWalkDifficulty is null)
        {
            return null;
        }

        hasWalkDifficulty.Code = walkDifficulty.Code;

        await context.SaveChangesAsync();

        return hasWalkDifficulty;
    }
}
