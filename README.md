# SRT Translator

![GitHub License](https://img.shields.io/badge/license-MIT-blue.svg)

SRT Translator is a simple and robust application that allows you to translate SRT (SubRip Subtitle) files easily and efficiently. With this tool, you can quickly convert subtitles from one language to another, making it convenient for multilingual audiences or for personal language learning.
The application utilizes the DeeplAPI for translation, taking the API key from the environment variable named "DeeplApiKey". By default, the translation is set to Polish, but you can easily change it at the start of the `Program.cs` file.

## Features

- **SRT Translation**: Translate SRT files from one language to another.
- **Preserves SRT Formatting**: The translated SRT file maintains the original formatting, ensuring the integrity of the subtitle structure.

## Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)

## Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/assasinos/SRTTranslator.git
   ```

2. Change into the project directory:

   ```bash
   cd SRTTranslator 
   ```

3. Set the Deepl API key environment variable:

   ```bash
   export DeeplApiKey=YOUR_API_KEY
   ```

4. Build the application:

   ```bash
   dotnet build
   ```

## Usage

1. Open the `Program.cs` file in the project and locate the following line:

   ```csharp
   const string TargetLanguage = "pl"; // Set the target language code here (e.g., "pl" for Polish)
   ```

2. Change the `TargetLanguage` value to the desired language code (e.g., "en" for English, "fr" for French, etc.).

3. Run the application:

   ```bash
   dotnet run --project SRTTranslator  
 ```

4. Follow the prompts on the screen to select the source for translation.

5. The application will generate a translated SRT file in the app directory called same as original file".

6. Enjoy your translated subtitles!

## Contributing

Contributions are welcome! If you find any issues or have suggestions for improvements, please open an issue or submit a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more information.

## Acknowledgements

- The SRT Translator application was inspired by the need for easy subtitle translation in the global community.
- Special thanks to the open-source community for providing libraries and tools that make this project possible.

