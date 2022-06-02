using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1.APIs;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API Controller for Questions.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class QuestionsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        
        /// <summary>
        /// API Controller for Questions.
        /// </summary>
        /// <param name="bll">Business logic layer</param>
        public QuestionsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Questions
        /// <summary>
        /// Get all questions.
        /// </summary>
        /// <returns>List of Questions</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Question>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions(Guid? quizId)
        {
            var questions = await _bll.Questions.GetAllQuestions(quizId);
            return questions.Select(QuestionMapper.MapToApi).ToList();
        }

        // GET: api/Questions/5
        /// <summary>
        /// Get one question. Based on parameter: Id
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>Question entity from database</returns>
        [HttpGet("{id:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Question), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Question>> GetQuestion(Guid id)
        {
            var question = await _bll.Questions.FirstOrDefaultAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            return QuestionMapper.MapToApi(question);
        }

        // PUT: api/Questions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Put one question into database.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <param name="question">Question entity</param>
        /// <returns>No content</returns>
        [HttpPut("{id:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutQuestion(Guid id, Question question)
        {
            if (id != question.Id)
            {
                return BadRequest();
            }

            _bll.Questions.Update(QuestionMapper.MapToBll(question));
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }

        // POST: api/Questions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post one question.
        /// </summary>
        /// <param name="question">Question entity</param>
        /// <returns>CreatedAtAction</returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Question), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Question>> PostQuestion(QuestionAdd question)
        {
            var bllEntity = QuestionMapper.MapToBll(question);
            var addedEntity = _bll.Questions.Add(bllEntity);
            await _bll.SaveChangesAsync();
            var returnEntity = QuestionMapper.MapToApi(_bll.Questions.FirstOrDefaultAsync(addedEntity.Id).Result!);

            return CreatedAtAction("GetQuestion", 
                new { id = returnEntity.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" }, question);
        }

        // DELETE: api/Questions/5
        /// <summary>
        /// Delete one question. Based on parameter: Id.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteQuestion(Guid id)
        {
            var question = await _bll.Questions.FirstOrDefaultAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            _bll.Questions.Remove(question);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
