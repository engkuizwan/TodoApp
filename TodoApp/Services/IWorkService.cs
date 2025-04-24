using TodoApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoApp.Services
{
    public interface IWorkService
    {
        Task<List<Work>> GetAllWorksAsync();
        Task<Work?> GetWorkByIdAsync(int id);
        Task AddWorkAsync(Work newWork);
        Task<bool> UpdateWorkAsync(Work updatedWork);
        Task<bool> DeleteWorkAsync(int id);
    }
}
