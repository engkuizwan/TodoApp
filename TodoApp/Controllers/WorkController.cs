using Microsoft.AspNetCore.Mvc;
using TodoApp.Model;
using TodoApp.Services;

namespace TodoApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WorkController : ControllerBase
    {
        private readonly IWorkService _workService;

        public WorkController(IWorkService workService)
        {
            _workService = workService;
        }

        [HttpPost]
        public async Task<IActionResult> AddWork(Work work)
        {
            if (work == null)
                return BadRequest("Work data cannot be null.");

            await _workService.AddWorkAsync(work);
                return Ok("New work inserted successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWorks()
        {
            var works = await _workService.GetAllWorksAsync();
            return Ok(works);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkById(int id)
        {
            var work = await _workService.GetWorkByIdAsync(id);

            if (work == null)
                return NotFound("Work not found.");

            return Ok(work);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWork(Work updatedWork)
        {
            if (updatedWork == null)
                return BadRequest("Work data cannot be null.");

            var result = await _workService.UpdateWorkAsync(updatedWork);

            if (result)
                return Ok("Work updated successfully.");
            else
                return NotFound("Work not found.");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWork(int id)
        {
            var result = await _workService.DeleteWorkAsync(id);

            if (result)
                return Ok("Work deleted successfully.");
            else
                return NotFound("Work not found.");
        }
    }
}
