using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using PublicApi.DTO.v1.APIs;
using AnswerMapper = PublicApi.DTO.v1.Mappers.AnswerMapper;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API Controller for Answers.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AnswersController : ControllerBase
    {
        private readonly IAppBLL _bll;
        
        /// <summary>
        /// API Controller for Answers.
        /// </summary>
        /// <param name="bll">Business logic layer</param>
        public AnswersController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Answers
        /// <summary>
        /// Get all answers.
        /// </summary>
        /// <returns>List of Answers</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Answer>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<Answer>>> GetAnswers()
        {
            var answers = await _bll.Answers.GetAllAsync();
            return answers.Select(AnswerMapper.MapToApi).ToList();
        }

        // GET: api/Answers/5
        /// <summary>
        /// Get one answer. Based on parameter: Id
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>Answer entity from database</returns>
        [HttpGet("{id:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Answer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Answer>> GetAnswer(Guid id)
        {
            var answer = await _bll.Answers.FirstOrDefaultAsync(id);

            if (answer == null)
            {
                return NotFound();
            }

            return AnswerMapper.MapToApi(answer);
        }

        // PUT: api/Answers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Put one answer into database.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <param name="answer">Answer entity</param>
        /// <returns>No content</returns>
        [HttpPut("{id:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutAnswer(Guid id, Answer answer)
        {
            if (id != answer.Id)
            {
                return BadRequest();
            }

            _bll.Answers.Update(AnswerMapper.MapToBll(answer));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Answers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post one answer.
        /// </summary>
        /// <param name="answer">Answer entity</param>
        /// <returns>CreatedAtAction</returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Answer), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Answer>> PostAnswer(AnswerAdd answer)
        {
            var bllEntity = AnswerMapper.MapToBll(answer);
            var addedEntity = _bll.Answers.Add(bllEntity);
            await _bll.SaveChangesAsync();
            var returnEntity = AnswerMapper.MapToApi(_bll.Answers.FirstOrDefaultAsync(addedEntity.Id).Result!);

            return CreatedAtAction("GetAnswer", 
                new { id = returnEntity.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" }, answer);
        }

        // DELETE: api/Answers/5
        /// <summary>
        /// Delete one answer. Based on parameter: Id.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteAnswer(Guid id)
        {
            var answer = await _bll.Answers.FirstOrDefaultAsync(id);
            if (answer == null)
            {
                return NotFound();
            }

            _bll.Answers.Remove(answer);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
