using Microsoft.Extensions.Options;
using OpenAI;
using OpenAI.Chat;
using System.ClientModel;

namespace Pixelbadger.NewsHaiku.Infrastructure;

internal class OpenAINewsHaikuGenerator : INewsHaikuGenerator
{
    private const string SystemPrompt =
        "User input consists of a series of images of today's newspaper headlines. " +
        "Analyze the headlines and other text in these images, and generate four " +
        "haiku that capture the spirit and essence of today's news. " +
        "Do not under any circumstances include any additional text like titles or headers, but you must separate each haiku with three dashes (---).";

    private readonly ChatClient _chatClient;

    public OpenAINewsHaikuGenerator(OpenAIClient openAIClient, IOptions<OpenAIOptions> options)
    {
        _chatClient = openAIClient.GetChatClient(options.Value.Model);
    }

    public async Task<string[]> GenerateFromImages(IEnumerable<BinaryData> images)
    {
        ChatMessage systemInput = new SystemChatMessage(SystemPrompt);

        List<ChatMessageContentPart> chatParts = new();
        foreach (BinaryData image in images)
        {
            chatParts.Add(ChatMessageContentPart.CreateImageMessageContentPart(image, "image/jpeg"));
        }
        ChatMessage userInput = new UserChatMessage(chatParts);

        ClientResult<ChatCompletion> result = await _chatClient.CompleteChatAsync(systemInput, userInput);

        string chatResponse = string.Join("", result.Value.Content.Select(c => c.Text));

        return chatResponse.Split("---", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }
}
