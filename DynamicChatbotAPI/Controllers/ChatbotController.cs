using DynamicChatbotAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DynamicChatbotAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatbotController : ControllerBase
    {
        private readonly DynamicChatbotApiContext _context;

        public ChatbotController(DynamicChatbotApiContext context)
        {
            _context = context;
        }

        [HttpGet("{companyId}")]
        public async Task<IActionResult> GetChatbotData(int companyId)
        {
            var company = await _context.Companies
                .Include(c => c.Actions)
                    .ThenInclude(a => a.FollowupQuestions)
                        .ThenInclude(f => f.Options)
                .FirstOrDefaultAsync(c => c.CompanyId == companyId);

            if (company == null)
            {
                return NotFound();
            }

            var response = new
            {
                webappName = company.CompanyName,
                actions = company.Actions.Select(a => new
                {
                    action1 = a.ActionName,
                    api = a.ApiUrl,
                    followups = a.FollowupQuestions.Select(f => new
                    {
                        question = f.QuestionText,
                        type = f.QuestionType,
                        options = f.Options.Select(o => o.OptionText).ToList()
                    }).ToList()
                }).ToList()
            };

            return Ok(response);
        }
    }
}
