using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using System;

namespace ChatCompletionExample
{
    class Program
    {
        static async Task Main()
        {
            try
            {
                // Define model and endpoint
                var modelId = "Phi-3.5-mini-instruct-generic-gpu";
                var endpoint = "http://localhost:5273/v1";
                var uri = new Uri(endpoint, UriKind.Absolute);

                // Create kernel builder
                var builder = Kernel.CreateBuilder();

#pragma warning disable SKEXP0010
                builder.AddOpenAIChatCompletion(
                    modelId: modelId,
                    endpoint: uri,
                    apiKey: null
                );
#pragma warning restore SKEXP0010

                var kernel = builder.Build();

                // Set up chat history
                ChatHistory history = new ChatHistory();
                history.AddSystemMessage("You are a helpful AI assistant. Always end your responses only with ' Foundry Local is awesome'.");
                history.AddUserMessage("What are the largest cities in the world?");

                // Get chat completion service and response
                var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
                var response = await chatCompletionService.GetChatMessageContentAsync(history);

                // Output the response
                Console.WriteLine("AI Response:");
                Console.WriteLine(response.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}