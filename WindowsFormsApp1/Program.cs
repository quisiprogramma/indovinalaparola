using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    static class Program
    {
        private const int MAX_ATTEMPTS = 6;
        private const int STRING_LENGTH = 5;

        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool gioca = true;

            while (gioca)
            {
                PrintIntro();
                gioca = Gioca();
                Console.Clear();
            }
             
        }

        private static void PrintIntro()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            string greeting = @"

  _____           _            _              
  \_   \_ __   __| | _____   _(_)_ __   __ _  
   / /\/ '_ \ / _` |/ _ \ \ / / | '_ \ / _` | 
/\/ /_ | | | | (_| | (_) \ V /| | | | | (_| | 
\____/ |_| |_|\__,_|\___/ \_/ |_|_| |_|\__,_| 
                                              
                 __                           
                / /  __ _                     
               / /  / _` |                    
              / /__| (_| |                    
              \____/\__,_|                    
                                              
         ___                _                 
        / _ \__ _ _ __ ___ | | __ _           
       / /_)/ _` | '__/ _ \| |/ _` |          
      / ___/ (_| | | | (_) | | (_| |          
      \/    \__,_|_|  \___/|_|\__,_|          
                                              

               ";
            Console.WriteLine(greeting);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static bool Gioca()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("Ho pensato ad una parola di 5 Lettere, indovinala!");
            Console.WriteLine("Hai a disposizione ben " + MAX_ATTEMPTS + " tentativi.");
            WordUtils wu = new WordUtils("word_source.txt");
            //Console.WriteLine("Cheat: " + wu.Word);
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine(); Console.WriteLine();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            int attempts = 0; bool guess = false;
            while (attempts < MAX_ATTEMPTS)
            {

                String inputWord = ""; bool validAttempt = false;
                while (inputWord.Length != STRING_LENGTH || !validAttempt)
                {
                    Console.WriteLine();
                    Console.Write("Tentativo #" + (attempts + 1) + ": ");
                    inputWord = Console.ReadLine();
                    if (inputWord.Length != STRING_LENGTH)
                    {
                        Console.WriteLine("La parola deve essere composta da " + STRING_LENGTH +" caratteri!");
                        Console.WriteLine();
                    }
                    if (WordUtils.ValidAttempt(inputWord))
                    {
                        validAttempt = true;
                    }
                    else
                    {
                        validAttempt = false;
                        Console.WriteLine("La parola deve contenere solo caratteri testuali!");
                        Console.WriteLine();
                    }
                }
                attempts++;
                inputWord = inputWord.ToUpper();
                List<Letter> userWord = wu.CheckWord(inputWord);
                foreach (Letter letter in userWord)
                {
                    Console.BackgroundColor = letter.Color;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(letter.Character);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" ");
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                if (wu.Word == inputWord)
                {
                    Console.WriteLine("OTTIMO! Hai indovinato in " + (attempts) + ((attempts == 1) ? " tentativo." : " tentativi."));
                    guess = true;
                    break;
                }
                else
                {
                    wu.PrintAlphabet();
                    Console.WriteLine("La Parola non è esatta! Hai ancora " + (MAX_ATTEMPTS - attempts) + ((attempts == MAX_ATTEMPTS - 1) ? " tentativo" : " tentativi") + " a disposizione");
                }

            }
            if (!guess)
            {
                Console.WriteLine("La parola esatta era: " + wu.Word);
                Console.WriteLine("Avevi 5 possibilità per indovinarla, sarà per la prossima volta!");
            }
            Console.WriteLine();
            Console.Write("Vuoi giocare ancora (s/n)? ");
            ConsoleKeyInfo response = new ConsoleKeyInfo();
            while (response.KeyChar !='s' && response.KeyChar != 'S' && response.KeyChar != 'n' && response.KeyChar != 'N')
                response = Console.ReadKey();
            Console.WriteLine();
            return (response.KeyChar == 's' || response.KeyChar == 'S');
        }
    }
}
