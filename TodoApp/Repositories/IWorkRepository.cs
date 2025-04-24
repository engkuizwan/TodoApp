using TodoApp.Model;

namespace TodoApp.Repositories
{
    public interface IWorkRepository
    {
        Task<List<Work>> GetAllWorksAsync();
        Task<Work?> GetWorkByIdAsync(int id);
        Task AddWorkAsync(Work newWork);
        Task<bool> UpdateWorkAsync(Work updatedWork);
        Task<bool> DeleteWorkAsync(int id);
    }
}
