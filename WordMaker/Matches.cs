using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble
{
    public class Matches
    {
        public IList<string> GetMatches(string sourceLetters, string[] wordList)
        {
            sourceLetters = sourceLetters.ToLower();

            IList<string> matches = new List<string>();

            foreach (string word in wordList)
            {
                if (WordCanBeBuiltFromSourceLetters(word, sourceLetters))
                    matches.Add(word);
            }
            return matches;
        }


        bool WordCanBeBuiltFromSourceLetters(string targetWord, string sourceLetters)
        {
            string builtWord = "";

            foreach (char letter in targetWord)
            {
                int pos = sourceLetters.IndexOf(letter);
                if (pos >= 0)
                {
                    builtWord += letter;
                    sourceLetters = sourceLetters.Remove(pos, 1);
                    continue;
                }
                
                // check for wildcard
                pos = sourceLetters.IndexOf("*");
                if (pos >= 0)
                {
                    builtWord += letter;
                    sourceLetters = sourceLetters.Remove(pos, 1);
                }
            }
            return string.Equals(builtWord, targetWord);

        }
    }
}
