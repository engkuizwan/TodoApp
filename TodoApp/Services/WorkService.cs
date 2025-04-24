using TodoApp.Model;
using TodoApp.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoApp.Services
{
    public class WorkService : IWorkService
    {
        private readonly IWorkRepository _repository;

        public WorkService(IWorkRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Work>> GetAllWorksAsync()
        {
            return await _repository.GetAllWorksAsync();
        }

        public async Task<Work?> GetWorkByIdAsync(int id)
        {
            return await _repository.GetWorkByIdAsync(id);
        }

        public async Task AddWorkAsync(Work newWork)
        {
            await _repository.AddWorkAsync(newWork);
        }

        public async Task<bool> UpdateWorkAsync(Work updatedWork)
        {
            return await _repository.UpdateWorkAsync(updatedWork);
        }

        public async Task<bool> DeleteWorkAsync(int id)
        {
            return await _repository.DeleteWorkAsync(id);
        }
    }
}
