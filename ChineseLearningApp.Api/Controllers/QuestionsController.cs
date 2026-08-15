using ChineseLearningApp.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChineseLearningApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly ChineseLearningDbContext _context;

        public QuestionsController(ChineseLearningDbContext context)
        {
            _context = context;
        }

        [HttpGet("get-lesson")]
        public async Task<IActionResult> GetLesson()
        {
            var questions = await _context.Questions
                .Include(q => q.Words)
                .ToListAsync();

            return Ok(questions);
        }
    }
}
