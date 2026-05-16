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

        // Bơm (Inject) DbContext vào Controller
        public QuestionsController(ChineseLearningDbContext context)
        {
            _context = context;
        }

        // Tạo một đường dẫn (Endpoint) để lấy dữ liệu bài học
        [HttpGet("get-lesson")]
        public async Task<IActionResult> GetLesson()
        {
            // Lấy toàn bộ câu hỏi, KÈM THEO (Include) các từ vựng thuộc về câu hỏi đó
            var questions = await _context.Questions
                .Include(q => q.Words)
                .ToListAsync();

            return Ok(questions);
        }
    }
}