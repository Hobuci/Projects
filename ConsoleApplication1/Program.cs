using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
//Author: Richard Borbely
//Cheat program for Wordshake game on Britishcouncil's website
//https://learnenglish.britishcouncil.org/en/games/wordshake
//Date: 13/09/2012
//Last Modified: 09/01/2018 'Added some comments'
namespace WordshakeCheat
{
    class Program
    {
        static void Main(string[] args)
        {
            //Letters are required to be input in one line without spaces. ex. 'DTSKWBXRWTD'
            Console.Write("Start a game and input the letters: ");

            //Get user input
            string userInput = Console.ReadLine().ToLower();
            List<string> acceptedWords = new List<string>();
            string userInputCopy = userInput;
            string removedLettersString = "";
            StreamReader sr = new StreamReader("Files/dictionary.txt");
            string line = sr.ReadLine();

            while (line != null)
            {//read in words from dictionary
                line = line.ToLower();
                for (int i = 0; i < line.Length; i++)
                {//loop through the letters of the word
                    if (userInputCopy.Contains(line[i].ToString()))
                    {//if userInput contains the letter, remove it from the string and add it to the removed letters
                        userInputCopy = userInputCopy.Remove(userInputCopy.IndexOf(line[i]), 1);
                        removedLettersString += line[i].ToString();
                        if (removedLettersString == line)
                        {//if the word builds up from the removed letters it means its a meaningful one, add it to the list of acceptable words
                            acceptedWords.Add(line.ToString());
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                //reset variables
                line = sr.ReadLine(); //get next word
                userInputCopy = userInput;
                removedLettersString = "";
            }
            sr.Close(); //close reader

            //Write the accepted words into a file
            StreamWriter sw = new StreamWriter("Files/acceptedWords.txt");
            foreach (string item in acceptedWords)
            {
                sw.WriteLine(item);
            }
            sw.Close(); //close writer

            Console.WriteLine("\nList is ready!\n" + acceptedWords.Count + " words found.\nENTER to begin Output...");
            Console.Read();
            Console.WriteLine("\nOutput begins in 5 seconds, click back to input box in-game!");
            System.Threading.Thread.Sleep(5000); //freeze for 5 secs
            Output(); //begin pushing words

            Console.WriteLine("GoodGame!");
            Console.Read();
        }

        static void Output()
        {//Push words into the game's input box, the user is expected to click back into the box
            StreamReader sr = new StreamReader("Files/acceptedWords.txt");
            string line = sr.ReadLine();
            while (line != null)
            {
                System.Threading.Thread.Sleep(25); //push words in 25ms intervals
                SendKeys.SendWait(line.ToString());
                SendKeys.SendWait("{ENTER}");
                line = sr.ReadLine(); //get next word
            }
        }
    }
}
