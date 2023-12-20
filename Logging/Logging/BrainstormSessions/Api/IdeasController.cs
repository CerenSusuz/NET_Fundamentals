using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrainstormSessions.ClientModels;
using BrainstormSessions.Core.Interfaces;
using BrainstormSessions.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BrainstormSessions.Api
{
    public class IdeasController : ControllerBase
    {
        private readonly IBrainstormSessionRepository _sessionRepository;
        private readonly ILogger<IdeasController> _logger;

        public IdeasController(
            IBrainstormSessionRepository sessionRepository,
            ILogger<IdeasController> logger)
        {
            _sessionRepository = sessionRepository;
            _logger = logger;
        }

        #region snippet_ForSessionAndCreate
        [HttpGet("forsession/{sessionId}")]
        public async Task<IActionResult> ForSession(int sessionId)
        {
            _logger.LogInformation($"IDEAS - Accessing session with id = {sessionId}");

            var session = await _sessionRepository.GetByIdAsync(sessionId);

            if (session == null)
            {
                _logger.LogError($"IDEAS - Session with id = {sessionId} not found.");

                return NotFound(sessionId);
            }

            var result = session.Ideas.Select(idea => new IdeaDTO()
            {
                Id = idea.Id,
                Name = idea.Name,
                Description = idea.Description,
                DateCreated = idea.DateCreated
            }).ToList();

            _logger.LogInformation($"IDEAS - Successfully accessed session with id = {sessionId}");

            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]NewIdeaModel model)
        {
            _logger.LogInformation("IDEAS - IdeasController.Create was called, Creating new session ");

            if (!ModelState.IsValid)
            {
                _logger.LogError("IDEAS - ModelState is invalid in IdeasController.Create");

                return BadRequest(ModelState);
            }

            var session = await _sessionRepository.GetByIdAsync(model.SessionId);
            if (session == null)
            {
                _logger.LogError($"IDEAS - Session with id {model.SessionId} not found in IdeasController.Create");

                return NotFound(model.SessionId);
            }

            var idea = new Idea()
            {
                DateCreated = DateTimeOffset.Now,
                Description = model.Description,
                Name = model.Name
            };
            session.AddIdea(idea);

            _logger.LogInformation("IDEAS - Idea was added in IdeasController.Create");

            await _sessionRepository.UpdateAsync(session);

            return Ok(session);
        }
        #endregion

        #region snippet_ForSessionActionResult
        [HttpGet("forsessionactionresult/{sessionId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<List<IdeaDTO>>> ForSessionActionResult(int sessionId)
        {
            _logger.LogInformation($"IDEAS - Accessing session with id = {sessionId} in ForSessionActionResult");

            var session = await _sessionRepository.GetByIdAsync(sessionId);

            if (session == null)
            {
                _logger.LogError($"IDEAS - Session with id = {sessionId} not found in ForSessionActionResult.");

                return NotFound(sessionId);
            }

            var result = session.Ideas.Select(idea => new IdeaDTO()
            {
                Id = idea.Id,
                Name = idea.Name,
                Description = idea.Description,
                DateCreated = idea.DateCreated
            }).ToList();

            _logger.LogInformation($"IDEAS - Successfully accessed session with id = {sessionId} in ForSessionActionResult");

            return result;
        }
        #endregion

        #region snippet_CreateActionResult
        [HttpPost("createactionresult")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<BrainstormSession>> CreateActionResult([FromBody]NewIdeaModel model)
        {
            _logger.LogInformation("IDEAS - Creating new session in CreateActionResult");

            if (!ModelState.IsValid)
            {
                _logger.LogError("IDEAS - Invalid model state in CreateActionResult");

                return BadRequest(ModelState);
            }

            var session = await _sessionRepository.GetByIdAsync(model.SessionId);

            if (session == null)
            {
                _logger.LogError($"IDEAS - Session with id = {model.SessionId} not found in CreateActionResult.");

                return NotFound(model.SessionId);
            }

            var idea = new Idea()
            {
                DateCreated = DateTimeOffset.Now,
                Description = model.Description,
                Name = model.Name
            };
            session.AddIdea(idea);

            await _sessionRepository.UpdateAsync(session);

            _logger.LogInformation("IDEAS - Successfully created session in CreateActionResult");

            return CreatedAtAction(nameof(CreateActionResult), new { id = session.Id }, session);
        }
        #endregion
    }
}
