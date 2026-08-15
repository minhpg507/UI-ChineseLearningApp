using ChineseLearningApp.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChineseLearningApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordListsController : ControllerBase
    {
        private readonly ChineseLearningDbContext _context;

        public WordListsController(ChineseLearningDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WordList>>> GetWordLists()
        {
            return await _context.WordLists.OrderByDescending(x => x.CreatedAt).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<WordList>> CreateWordList(WordList newList)
        {
            _context.WordLists.Add(newList);
            await _context.Set<WordList>().AddAsync(newList);
            await _context.SaveChangesAsync();
            return Ok(newList);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWordList(int id)
        {
            var wordList = await _context.WordLists.FindAsync(id);
            if (wordList == null) return NotFound();

            _context.WordLists.Remove(wordList);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPost("{listId}/flashcards/single")]
        public async Task<IActionResult> AddSingleFlashcard(int listId, Flashcard flashcard)
        {
            flashcard.WordListId = listId; 
            _context.Flashcards.Add(flashcard);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("{listId}/flashcards/bulk")]
        public async Task<IActionResult> AddBulkFlashcards(int listId, List<Flashcard> flashcards)
        {
            foreach (var item in flashcards)
            {
                item.WordListId = listId;
            }
            _context.Flashcards.AddRange(flashcards);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("{listId}/flashcards")]
        public async Task<ActionResult<IEnumerable<Flashcard>>> GetFlashcards(int listId)
        {
            var cards = await _context.Flashcards
                                      .Where(f => f.WordListId == listId)
                                      .ToListAsync();
            return Ok(cards);
        }
        [HttpPut("{listId}/flashcards/{flashcardId}")]
        public async Task<IActionResult> UpdateFlashcard(int listId, int flashcardId, Flashcard updatedCard)
        {
            var card = await _context.Flashcards.FindAsync(flashcardId);
            if (card == null) return NotFound();

            card.FrontText = updatedCard.FrontText;
            card.BackText = updatedCard.BackText;
            card.Pinyin = updatedCard.Pinyin;

            card.WordType = updatedCard.WordType;
            card.Example = updatedCard.Example;
            card.Note = updatedCard.Note;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{listId}/flashcards/{flashcardId}")]
        public async Task<IActionResult> DeleteFlashcard(int listId, int flashcardId)
        {
            var card = await _context.Flashcards.FindAsync(flashcardId);
            if (card == null) return NotFound();

            _context.Flashcards.Remove(card);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
