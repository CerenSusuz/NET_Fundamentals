using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrainstormSessions.ClientModels;
using BrainstormSessions.Core.Interfaces;
using BrainstormSessions.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BrainstormSessions.Api;

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
        _logger.LogInformation($"{nameof(IdeasController)}.{nameof(ForSession)}: Accessing session with id = {sessionId}");

        var session = await _sessionRepository.GetByIdAsync(sessionId);

        if (session == null)
        {
            _logger.LogError($"{nameof(IdeasController)}.{nameof(ForSession)}: Session with id = {sessionId} not found.");

            return NotFound(sessionId);
        }

        var result = session.Ideas.Select(idea => new IdeaDTO()
        {
            Id = idea.Id,
            Name = idea.Name,
            Description = idea.Description,
            DateCreated = idea.DateCreated
        }).ToList();

        _logger.LogInformation($"{nameof(IdeasController)}.{nameof(ForSession)}: Successfully accessed session with id = {sessionId}");

        return Ok(result);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody]NewIdeaModel model)
    {
        _logger.LogInformation($"{nameof(IdeasController)}.{nameof(Create)} was called. Creating new session.");

        if (!ModelState.IsValid)
        {
            _logger.LogError($"{nameof(IdeasController)}.{nameof(Create)}: ModelState is invalid.");

            return BadRequest(ModelState);
        }

        var session = await _sessionRepository.GetByIdAsync(model.SessionId);
        if (session == null)
        {
            _logger.LogError($"{nameof(IdeasController)}.{nameof(Create)}: Session with id {model.SessionId} not found.");

            return NotFound(model.SessionId);
        }

        var idea = new Idea()
        {
            DateCreated = DateTimeOffset.Now,
            Description = model.Description,
            Name = model.Name
        };
        session.AddIdea(idea);

        _logger.LogInformation($"{nameof(IdeasController)}.{nameof(Create)}: Idea was added.");

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
        _logger.LogInformation($"{nameof(IdeasController)}.{nameof(ForSessionActionResult)}: Accessing session with id = {sessionId}");

        var session = await _sessionRepository.GetByIdAsync(sessionId);

        if (session == null)
        {
            _logger.LogError($"{nameof(IdeasController)}.{nameof(ForSessionActionResult)}: Session with id = {sessionId} not found.");

            return NotFound(sessionId);
        }

        var result = session.Ideas.Select(idea => new IdeaDTO()
        {
            Id = idea.Id,
            Name = idea.Name,
            Description = idea.Description,
            DateCreated = idea.DateCreated
        }).ToList();

        _logger.LogInformation($"{nameof(IdeasController)}.{nameof(ForSessionActionResult)}: Successfully accessed session with id = {sessionId}");

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
        _logger.LogInformation($"{nameof(IdeasController)}.{nameof(CreateActionResult)}: Creating new session");

        if (!ModelState.IsValid)
        {
            _logger.LogError($"{nameof(IdeasController)}.{nameof(CreateActionResult)}: Invalid model state.");

            return BadRequest(ModelState);
        }

        var session = await _sessionRepository.GetByIdAsync(model.SessionId);

        if (session == null)
        {
            _logger.LogError($"{nameof(IdeasController)}.{nameof(CreateActionResult)}: Session with id = {model.SessionId} not found.");

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

        _logger.LogInformation($"{nameof(IdeasController)}.{nameof(CreateActionResult)}: Successfully created session");

        return CreatedAtAction(nameof(CreateActionResult), new { id = session.Id }, session);
    }
    #endregion
}
