using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;
using Lab5.Properties;
using System.ComponentModel;
using System.Resources;

namespace Lab5
{
    class Program
    {
        private static readonly FileReader _fileReader = new FileReader();
        private static readonly WordMatcher _wordMatcher = new WordMatcher();
        private static ResourceManager _resourceManager;

        static void Main(string[] args)
        {
            
            
            Console.WriteLine(Constants.LanguageOption);
            String languageOption = Console.ReadLine();

            if(languageOption.Equals(Constants.Yes, StringComparison.OrdinalIgnoreCase))
            {
                // Set the program to use French (French Canada) culture.
                Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-CA");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-CA");
                _resourceManager = new ResourceManager("Lab5.Properties.stringFR", typeof(Program).Assembly);
            }
            else
            {
                // Default to English culture.
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                _resourceManager = new ResourceManager("Lab5.Properties.stringEN", typeof(Program).Assembly);
            }

                try
                {

                    Console.WriteLine(_resourceManager.GetString("OptionRequest"));

                    String option = Console.ReadLine() ?? throw new Exception(_resourceManager.GetString("EmptyString"));
                    bool validOption = false;

                    while (!validOption)
                    {

                        if ((option.Equals(Constants.File, StringComparison.OrdinalIgnoreCase) || (option.Equals(Constants.Manual, StringComparison.OrdinalIgnoreCase))))
                        {



                            switch (option.ToUpper())
                            {
                                case Constants.File:
                                    Console.WriteLine(_resourceManager.GetString("FilePathRequest"));

                                    ExecuteScrambledWordsInFileScenario();
                                    break;
                                case Constants.Manual:
                                    Console.WriteLine(_resourceManager.GetString("WordEntryRequest"));
                                    ExecuteScrambledWordsManualEntryScenario();
                                    break;
                                default:
                                    Console.WriteLine(_resourceManager.GetString("NotValidOption"));
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine(_resourceManager.GetString("NotValidOptionLoopBack"));
                            break;
                            

                        }
                    }

                    Console.ReadLine();


                }
                catch (Exception ex)
                {
                    Console.WriteLine(_resourceManager.GetString("ProgramTermination") + ex.Message);

                }
           
        }

        private static void ExecuteScrambledWordsInFileScenario()
        {
            var filename = Console.ReadLine();
            string[] scrambledWords = _fileReader.Read(filename);
            DisplayMatchedUnscrambledWords(scrambledWords);
        }

        private static void ExecuteScrambledWordsManualEntryScenario()
        {
            bool continueScrambling = true;
            while (continueScrambling)
            {


                string input = Console.ReadLine();

                string[] scrambledWords = input.Split(',');

                DisplayMatchedUnscrambledWords(scrambledWords);

                Console.WriteLine(_resourceManager.GetString("ContinuePrompt"));
                string continueOption = Console.ReadLine();

                if (continueOption.Equals(Constants.Yes, StringComparison.OrdinalIgnoreCase))
                {
                    continueScrambling = false;
                }
            }
        }

        private static void DisplayMatchedUnscrambledWords(string[] scrambledWords)
        {
            //read the list of words from the system file. 
            string[] wordList = _fileReader.Read("C:\\Users\\airme\\Desktop\\.Net Developement\\Lab5\\Lab5\\obj\\Debug\\wordlist.txt");

            //call a word matcher method to get a list of structs of matched words.
            List<MatchedWord> matchedWords = _wordMatcher.Match(scrambledWords, wordList);

            if (matchedWords.Any())
            {
                Console.WriteLine(_resourceManager.GetString("MatchedWordsFound"));
                foreach (MatchedWord matchedWord in matchedWords)
                {
                    Console.WriteLine($"Scrambled word: {matchedWord.ScrambledWord}, Unscrambled word: {matchedWord.Word}");
                }
            }
            else
            {
                Console.WriteLine(_resourceManager.GetString("NoMatchedWordsFound"));
            }
        }
    }
}
