using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Model;
using TodoApp.Repositories;

public class WorkRepository : IWorkRepository
{
    private readonly AppDbContext _context;

    public WorkRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Work>> GetAllWorksAsync()
    {
        return await _context.Works.ToListAsync();
    }

    public async Task<Work?> GetWorkByIdAsync(int id)
    {
        return await _context.Works.FirstOrDefaultAsync(w => w.id == id);
    }

    public async Task AddWorkAsync(Work work)
    {
        await _context.Works.AddAsync(work);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateWorkAsync(Work updatedWork)
    {
        var existing = await _context.Works.FirstOrDefaultAsync(w => w.id == updatedWork.id);
        if (existing == null) return false;

        existing.name = updatedWork.name;
        existing.description = updatedWork.description;
        existing.status = updatedWork.status;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteWorkAsync(int id)
    {
        var work = await _context.Works.FirstOrDefaultAsync(w => w.id == id);
        if (work == null) return false;

        _context.Works.Remove(work);
        await _context.SaveChangesAsync();
        return true;
    }

}
