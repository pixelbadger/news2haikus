
namespace Pixelbadger.NewsHaiku.Application.Components;

internal interface IHaikuCache
{
    IEnumerable<string>? Get();
    void Put(IEnumerable<string> value);
}