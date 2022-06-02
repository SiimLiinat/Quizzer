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
    /// API Controller for QuizzerAnswers.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class QuizzerAnswersController : ControllerBase
    {
        private readonly IAppBLL _bll;
        
        /// <summary>
        /// API Controller for QuizzerAnswers.
        /// </summary>
        /// <param name="bll">Business logic layer</param>
        public QuizzerAnswersController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/QuizzerAnswers
        /// <summary>
        /// Get all QuizzerAnswers.
        /// </summary>
        /// <returns>List of QuizzerAnswers</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<QuizzerAnswer>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<QuizzerAnswer>>> GetQuizzerAnswers()
        {
            var quizzerAnswers = await _bll.QuizzerAnswers.GetAllAsync();
            return quizzerAnswers.Select(QuizzerAnswerMapper.MapToApi).ToList();
        }

        // GET: api/QuizzerAnswers/5
        /// <summary>
        /// Get one quizzerAnswer. Based on parameter: Id
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>QuizzerAnswer entity from database</returns>
        [HttpGet("{id:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(QuizzerAnswer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<QuizzerAnswer>> GetQuizzerAnswer(Guid id)
        {
            var quizzerAnswer = await _bll.QuizzerAnswers.FirstOrDefaultAsync(id);

            if (quizzerAnswer == null)
            {
                return NotFound();
            }

            return QuizzerAnswerMapper.MapToApi(quizzerAnswer);
        }

        // PUT: api/QuizzerAnswers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Put one quizzerAnswer into database.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <param name="quizzerAnswer">QuizzerAnswer entity</param>
        /// <returns>No content</returns>
        [HttpPut("{id:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutQuizzerAnswer(Guid id, QuizzerAnswer quizzerAnswer)
        {
            if (id != quizzerAnswer.Id)
            {
                return BadRequest();
            }

            _bll.QuizzerAnswers.Update(QuizzerAnswerMapper.MapToBll(quizzerAnswer));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/QuizzerAnswers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post one quizzer.
        /// </summary>
        /// <param name="quizzerAnswer">Quizzer entity</param>
        /// <returns>CreatedAtAction</returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(QuizzerAnswer), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<QuizzerAnswer>> PostQuizzerAnswer(QuizzerAnswerAdd quizzerAnswer)
        {
            var bllEntity = QuizzerAnswerMapper.MapToBll(quizzerAnswer);
            var addedEntity = _bll.QuizzerAnswers.Add(bllEntity);
            await _bll.SaveChangesAsync();
            var returnEntity = QuizzerAnswerMapper.MapToApi(_bll.QuizzerAnswers.FirstOrDefaultAsync(addedEntity.Id).Result!);

            return CreatedAtAction("GetQuizzerAnswer", 
                new { id = returnEntity.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" }, quizzerAnswer);
        }

        // DELETE: api/QuizzerAnswers/5
        /// <summary>
        /// Delete one quizzer's answer. Based on parameter: Id.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteQuizzerAnswer(Guid id)
        {
            var quizzerAnswer = await _bll.QuizzerAnswers.FirstOrDefaultAsync(id);
            if (quizzerAnswer == null)
            {
                return NotFound();
            }

            _bll.QuizzerAnswers.Remove(quizzerAnswer);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
