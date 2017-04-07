using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---Office Hangman---");
            Console.WriteLine("--Here's your word:--");
            Console.WriteLine();
            Game game = new Game();
            game.Start();
        }

        class Game
        {
            int wrongGuess = 0;
            bool playGame = true;
            bool win = false;
            StringBuilder resultString = new StringBuilder();
            string wordToGuess = "";
            string guessLetter = "";
            char guessLetterChar = ' ';
            //string listFile = @"WordList.txt";
            string listFile = @"..\..\..\WordList.txt";
            public const int MAXGUESSCOUNT = 5;

            public void Start()
            {                
                wordToGuess = Picker(listFile);

                for (int i = 0; i < wordToGuess.Length; i++)
                {
                    resultString.Append("_");
                }
                Console.WriteLine(resultString);

                while (this.playGame)
                {
                    Play();
                }
                GameEnd();
            }

            void Play()
            {
                Console.WriteLine(string.Format("{0}{0}Take a guess! (letter or word)", Environment.NewLine));
                //checkCompletion()<- ha a teljes szó jó, akkor átírod a resultString-et
                //checkLetter(guessLetter)
                //checkCompletion():bool <- nyertél-e a szóval vagy kifogytál a lehetőségekből
                guessLetter = Console.ReadLine();
                if (guessLetter != "")
                {
                    guessLetterChar = guessLetter[0];
                }
                else
                {
                    return;
                }               

                if ((guessLetter == wordToGuess) && wrongGuess < MAXGUESSCOUNT)
                {
                    win = true;
                    playGame = false;                    
                }

                if (wrongGuess >= MAXGUESSCOUNT)
                {
                    playGame = false;
                    return;
                }
                else
                {
                    // write already guessed letter is guessed
                    if (guessLetter.Length != 1)
                    {
                        Console.WriteLine(string.Format("{0} is not a valid guess!", guessLetter));
                        return;
                    }
                    ResultStore();
                    Console.WriteLine(resultString);
                }                   
            }

            string Picker(string listFile)
            {
                string pickedWord;
                string wordsInFile = File.ReadAllText(listFile);
                string[] wordList = wordsInFile.Split(' ');
                Random random = new Random();

                pickedWord = wordList[random.Next(0, wordList.Length)];

                return pickedWord;
            }

            bool LetterChecker()
            {
                for (int i = 0; i < wordToGuess.Length; i++)
                {
                    if (wordToGuess[i] == guessLetterChar)
                    {
                        return true;
                    }
                }
                wrongGuess++;
                return false;
            }

            void ResultStore()
            {
                if (LetterChecker())
                {
                    for (int i = 0; i < wordToGuess.Length; i++)
                    {
                        if (wordToGuess[i] == guessLetterChar)
                        {
                            resultString[i] = guessLetterChar;
                        }
                    }
                }
                return;
            }

            void GameEnd()
            {
                if (win == true)
                {
                    Console.WriteLine("Congratulations! You won.");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Sorry, you lost. The word was: {0}", wordToGuess);
                    Console.ReadLine();
                }
            }
        }
    }
}
