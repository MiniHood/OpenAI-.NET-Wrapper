## Example
```
var openAI = new OpenAI("<insert API key here>", "gpt-3");
var generatedText = await openAI.GenerateText("Hello, I'm a bot. How are you today?", maxTokens: 200, temperature: 0.7f);
Console.WriteLine(generatedText);
```
