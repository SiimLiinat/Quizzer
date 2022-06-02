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
    /// API Controller for Quizzers.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class QuizzersController : ControllerBase
    {
        private readonly IAppBLL _bll;
        
        /// <summary>
        /// API Controller for Quizzers.
        /// </summary>
        /// <param name="bll">Business logic layer</param>
        public QuizzersController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Quizzers
        /// <summary>
        /// Get all quizzers.
        /// </summary>
        /// <returns>List of Quizzers</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Quizzer>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<Quizzer>>> GetQuizzers(Guid? userId)
        {
            var quizzers = await _bll.Quizzers.GetAllQuizzers(userId);
            return quizzers.Select(QuizzerMapper.MapToApi).ToList();
        }

        // GET: api/Quizzers/5
        /// <summary>
        /// Get one quizzer. Based on parameter: Id
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>Quizzer entity from database</returns>
        [HttpGet("{id:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Quizzer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Quizzer>> GetQuizzer(Guid id)
        {
            var quizzer = await _bll.Quizzers.FirstOrDefaultAsync(id);

            if (quizzer == null)
            {
                return NotFound();
            }

            return QuizzerMapper.MapToApi(quizzer);
        }

        // PUT: api/Quizzers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Put one quizzer into database.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <param name="quizzer">Quizzer entity</param>
        /// <returns>No content</returns>
        [HttpPut("{id:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutQuizzer(Guid id, Quizzer quizzer)
        {
            if (id != quizzer.Id)
            {
                return BadRequest();
            }

            var updatedQuizzer = QuizzerMapper.MapToBll(quizzer);
            updatedQuizzer.FinishedAt ??= DateTime.Now;
            _bll.Quizzers.Update(updatedQuizzer);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Quizzers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post one quizzer.
        /// </summary>
        /// <param name="quizzer">Quizzer entity</param>
        /// <returns>CreatedAtAction</returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Quizzer), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Quizzer>> PostQuizzer(QuizzerAdd quizzer)
        {
            var bllEntity = QuizzerMapper.MapToBll(quizzer);
            var addedEntity = _bll.Quizzers.Add(bllEntity);
            await _bll.SaveChangesAsync();
            var returnEntity = QuizzerMapper.MapToApi(_bll.Quizzers.FirstOrDefaultAsync(addedEntity.Id).Result!);

            return CreatedAtAction("GetQuizzer", 
                new { id = returnEntity.Id}, returnEntity);
        }

        // DELETE: api/Quizzers/5
        /// <summary>
        /// Delete one quizzer. Based on parameter: Id.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteQuizzer(Guid id)
        {
            var quizzer = await _bll.Quizzers.FirstOrDefaultAsync(id);
            if (quizzer == null)
            {
                return NotFound();
            }

            _bll.Quizzers.Remove(quizzer);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
