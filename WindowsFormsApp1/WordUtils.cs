using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1
{
    public class WordUtils
    {
        public string Word { get; set; }
        public Dictionary<Char, ConsoleColor> lettersList;
        private const string ALPHABET = "ABCDEFGHILMNOPQRSTUVZ";

        public WordUtils(string sourcePath)
        {
            initDictionary();
            this.Word = FindWord(sourcePath).ToUpper();
        }

        private void initDictionary()
        {
            lettersList = new Dictionary<char, ConsoleColor>();
            for (int i=0; i< ALPHABET.Length; i++)
            {
                this.lettersList.Add(ALPHABET[i], ConsoleColor.Gray);
            }
        }

        public void PrintAlphabet()
        {
            for (int i = 0; i < ALPHABET.Length; i++)
            {
                Console.BackgroundColor = this.lettersList[ALPHABET[i]];
                Console.Write(ALPHABET[i]);
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
        }

        public WordUtils(string word, bool force)
        {
            this.Word = word;
        }

        public string FindWord(string filePath)
        {
            var fileName = Environment.CurrentDirectory + Path.DirectorySeparatorChar +  filePath;
            //Console.WriteLine(fileName);
            var file = File.ReadLines(fileName).ToList();
            int count = file.Count();
            Random rnd = new Random();
            int skip = rnd.Next(0, count);
            return file.Skip(skip).First();
        }

        public List<Letter> WordToLetters(string toCheck)
        {
            List<Letter> result = new List<Letter>();
            for (int i = 0; i < toCheck.Length; i++)
            {
                result.Add(item: new Letter(toCheck[i], ConsoleColor.Red));
            }
            return result;
        }

        public List<Letter> CheckWord(string paramWord)
        {
            if (paramWord.Length != Word.Length)
                throw new Exception("Dimensione sbagliata");
            List<Letter> result = WordToLetters(paramWord);
            string tempWord = Word;
            for (int i = 0; i < Word.Length; i++)
            {
                if (Word[i] == paramWord[i])
                {
                    result[i].Color = ConsoleColor.Green;
                    tempWord = replaceChar(tempWord, i, ' ');
                    this.lettersList[paramWord[i]] = ConsoleColor.Green;
                }
            }
            for (int i = 0; i < Word.Length; i++)
            {
                if (tempWord.Contains(paramWord[i]) && (result[i].Color != ConsoleColor.Green))
                {
                    result[i].Color = ConsoleColor.Yellow;
                    tempWord = tempWord.Replace(paramWord[i], ' ');
                    this.lettersList[paramWord[i]] = ConsoleColor.Yellow;
                }
                else
                {
                    if (!Word.Contains(paramWord[i]))
                        this.lettersList[paramWord[i]] = ConsoleColor.Red;
                }
            }
            for (int i = 0; i < paramWord.Length; i++)
            {
                if (Word.Contains(paramWord[i]))
                {
                    
                }
                else
                {
                    
                }
            }
            return result;
        }




        private string replaceChar(string tempWord, int index, char newChar)
        {
            StringBuilder sb = new StringBuilder(tempWord);
            sb[index] = newChar;
            return sb.ToString();
        }

        public static bool ValidAttempt(string attempt)
        {
            return Regex.IsMatch(attempt, @"^[a-zA-Z]+$");
        }


    }

    public class Letter
    {

        public Char Character { get; set; }
        public ConsoleColor Color { get; set; }

        public Letter(Char c, ConsoleColor co)
        {
            this.Character = c;
            this.Color = co;
        }

        public override string ToString()
        {
            return "[" + this.Character + " - " + this.Color + "]";
         }
    }
}