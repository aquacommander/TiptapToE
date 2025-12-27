# Tip Tap Toe&nbsp; 👨‍💻

AI powered console app built using .NET and Google Gemini.

Tip Tap Toe is a console application built for software engineers and developers who want language-specific practice for improving their typing speed. It uses Google's Gemini AI model to analyse user keystrokes, and provides tailored code sequences for users to practice their typing.

## Tech Stack 👷

- .NET
- Google Gemini [1.5 Flash model variant]

## Features 🚀

- 5 different languages to practice
- Uses Google's Gemini API to generate typing assessments
- Schemas are provided in HTTP requests to enforce structured outputs
- Various models created for JSON deserialisation and key logging
- Key strokes are logged with character, success and timestamp data
- Gemini analyses key stroke data to identify weakness and generate personalised practice
- WPM is calculated and benchmarks are provided to the user

![alt text](https://github.com/rohandrummond/tiptaptoe/blob/main/Assets/tip-tap-toe-screenshots.png?raw=true)

## Setup ⚙️

__Prerequisites__
- .NET 8
- Google AI Studio API key

__Instructions__

1. Include your Google Studio API key as a sytem environment variable using this structure ```GEMINI_API_KEY="{Your API key}"```
2. Clone the repo using ```git clone```
3. Use ```cd tiptaptoe``` to access repo
4. Run program using ```dotnet run```

.NET uses the GetEnvironmentVariable method to access your API key, you can read more about this [here](https://learn.microsoft.com/en-us/dotnet/api/system.environment.getenvironmentvariable?view=net-9.0)
   
## License 👨‍⚖️

This project is open source under the MIT License.

## Contact 📫

Check out my other projects and contact info on my [GitHub](https://github.com/rohandrummond) profile.
