## Simple Example
```
using System;

namespace OpenAIExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string apiKey = "your_api_key_here";
            string engine = "text-davinci-002";

            var openAI = new OpenAIWrapper.OpenAI(apiKey, engine);
            var result = openAI.GenerateText("Do you like pizza?").Result;

            Console.WriteLine(result);
        }
    }
}
```

## Advanced Example
```
using System;

namespace OpenAIExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string apiKey = "your_api_key_here";
            string engine = "text-davinci-002";

            var openAI = new OpenAIWrapper.OpenAI(apiKey, engine);

            var prompt = "What is your favorite movie?";
            var maxTokens = 50;
            var temperature = 0.5f;
            var n = 3;
            var stop = ".";
            var stream = true;
            var presencePenalty = 0.5f;
            var bestOf = 2;
            var echo = false;

            var result = openAI.GenerateText(prompt, maxTokens, temperature, n, stop, stream, presencePenalty, bestOf, echo).Result;

            Console.WriteLine(result);
        }
    }
}
```

prompt: A string that provides the starting point for the generated text.

maxTokens: An integer that specifies the maximum number of tokens (words or punctuation) to generate.

temperature: A float that controls the randomness of the generated text. A value of 0 generates text that is very deterministic, while a value of 1 generates text that is very random.

n: An integer that specifies the number of text samples to generate.

stop: A string that defines a sequence of characters that will stop the generation if encountered.

stream: A boolean that specifies whether to generate text one token at a time (streaming) or all at once.

presencePenalty: A float that specifies a penalty for generating tokens that are already present in the prompt.

bestOf: An integer that specifies the number of generated samples to keep. The best samples, as determined by the API, are kept.

echo: A boolean that specifies whether to include the prompt in the generated text.
