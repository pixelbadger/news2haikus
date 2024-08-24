using MediatR;

namespace Pixelbadger.NewsHaiku.Application.Commands;

public record GetNewsHaikuRequest() : IRequest<string[]> { }
