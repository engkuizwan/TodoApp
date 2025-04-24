using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Model;

namespace TodoApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WorkController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public WorkController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        //create work
        [HttpPost]
        //[HttpPost("addWork")]
        public async Task<ActionResult<List<Work>>> addWork(Work newWork)
        {
            if (newWork != null)
            {
                appDbContext.Works.Add(newWork);
                await appDbContext.SaveChangesAsync();
                return Ok("New work inserted");

            }
            return BadRequest("Data request cannot Null");
        }

        //get all works
        [HttpGet]
        //[HttpGet("GetAllWork")]
        public async Task<ActionResult<List<Work>>> GetAllWorks()
        {
            var works = await appDbContext.Works.ToListAsync();
            return Ok(works);
        }

        //get work by id
        [HttpGet("{id:int}")]
        //[HttpGet("getWorkbyId/{id:int}")]
        public async Task<ActionResult<Work>> GetWorkbyId(int id)
        {
            var work = await appDbContext.Works.SingleOrDefaultAsync(e => e.id ==id);
            if(User != null)return Ok(work);
            return NotFound("Work not exist");
        }

        [HttpPut]
        //[HttpPut("updateWork")]
        public async Task<ActionResult<Work>> UpdateWork(Work updatedwork)
        {
                var work = await appDbContext.Works.FirstOrDefaultAsync(e => e.id == updatedwork.id);
            if(updatedwork != null)
            {
                if(work != null)
                {
                    work.name = updatedwork.name;
                    work.description = updatedwork.description;
                    work.status = updatedwork.status;
                    await appDbContext.SaveChangesAsync();
                    return Ok("Data Updated");
                }
                return NotFound("Work not exist");
            }
            return BadRequest("Data request cannot null");
        }

        [HttpDelete]
        public async Task<ActionResult<List<Work>>> DeleteWork(int id)
        {
            var work = await appDbContext.Works.FirstOrDefaultAsync(e => e.id == id);

            if (work != null)
            {
                appDbContext.Works.Remove(work);
                await appDbContext.SaveChangesAsync();
                return Ok(await appDbContext.Works.ToListAsync());
            }
            return NotFound("Work not exist");
        }


    }
}
