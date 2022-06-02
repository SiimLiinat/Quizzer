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
    /// API Controller for Quizzes.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class QuizzesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        
        /// <summary>
        /// API Controller for Quizzes.
        /// </summary>
        /// <param name="bll">Business logic layer</param>
        public QuizzesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Quizzes
        /// <summary>
        /// Get all quizzes.
        /// </summary>
        /// <returns>List of Quizzes</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Quiz>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetQuizzes()
        {
            var quizzes = await _bll.Quizzes.GetAllAsync();
            return quizzes.Select(QuizMapper.MapToApi).ToList();
        }

        // GET: api/Quizzes/5
        /// <summary>
        /// Get one quiz. Based on parameter: Id
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>Quiz entity from database</returns>
        [HttpGet("{id:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Quiz), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<Quiz>> GetQuiz(Guid id)
        {
            var quiz = await _bll.Quizzes.FirstOrDefaultAsync(id);

            if (quiz == null)
            {
                return NotFound();
            }

            return QuizMapper.MapToApi(quiz);
        }

        // PUT: api/Quizzes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Put one quiz into database.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <param name="quiz">Quiz entity</param>
        /// <returns>No content</returns>
        [HttpPut("{id:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutQuiz(Guid id, Quiz quiz)
        {
            if (id != quiz.Id)
            {
                return BadRequest();
            }

            _bll.Quizzes.Update(QuizMapper.MapToBll(quiz));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Quizzes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post one quiz.
        /// </summary>
        /// <param name="quiz">Quiz entity</param>
        /// <returns>CreatedAtAction</returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Quiz), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Quiz>> PostQuiz(QuizAdd quiz)
        {
            var bllEntity = QuizMapper.MapToBll(quiz);
            var addedEntity = _bll.Quizzes.Add(bllEntity);
            await _bll.SaveChangesAsync();
            var returnEntity = QuizMapper.MapToApi(await _bll.Quizzes.FirstOrDefaultAsync(addedEntity.Id));

            return CreatedAtAction("GetQuiz", 
                new { id = returnEntity.Id }, addedEntity);
        }

        // DELETE: api/Quizzes/5
        /// <summary>
        /// Delete one quiz. Based on parameter: Id.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteQuiz(Guid id)
        {
            var quiz = await _bll.Quizzes.FirstOrDefaultAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }

            _bll.Quizzes.Remove(quiz);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
