using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pixelbadger.NewsHaiku.Application.Commands;
using Pixelbadger.NewsHaiku.Dtos;

namespace Pixelbadger.NewsHaiku.Controllers;
[ApiController]
[Route("[controller]")]
public class HaikuController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<HaikuController> _logger;

    public HaikuController(IMediator mediator, ILogger<HaikuController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("News", Name = "GetNewsHaiku")]
    public async Task<NewsHaikuResponseDto> GetNewsHaiku()
    {
        IEnumerable<string> haikus = await _mediator.Send(new GetNewsHaikuRequest());

        return new NewsHaikuResponseDto
        {
            Haiku = haikus
        };
    }
}
