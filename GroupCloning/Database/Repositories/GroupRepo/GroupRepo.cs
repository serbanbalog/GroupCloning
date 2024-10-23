using GroupCloning.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupCloning.Database.Repositories.GroupRepo;

public class GroupRepo : IRepository<Group>
{
    private readonly GroupCloningDbContext _dbContext;
    private readonly DbSet<Group> _dbSet;

    public GroupRepo(GroupCloningDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<Group>();
    }
    
    public async Task<List<Group>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<List<Group>> GetByGroupNumberAsync(int groupNumber)
    {
        return await _dbSet.Where(x=> x.GroupNumber == groupNumber).ToListAsync();
    }

    public async Task AddAsync(Group entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(Group entity)
    {
        _dbSet.Update(entity);
    }

    public void Remove(Group entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}