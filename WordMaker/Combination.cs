using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble
{
    public class Combination
    {
        private string Letters;

        public Combination(string letters)
        {
            Letters = letters;
        }
        public IOrderedEnumerable<string> Results()
        {
            List<string> results = new List<string>();
            string lettersbackup = Letters;

            for (int i = 0; i <= lettersbackup.Length; i++)
            {
                while (Letters.Length > 1)
                {
                    // Add given letters
                    results.Add(Letters);

                    results.AddRange(BuildMixedArray());

                    Letters = Letters.Substring(0, Letters.Length - 1);
                }
                // restore
                Letters = lettersbackup;
                // shift
                Letters = ShiftString(Letters);
                Letters = SwapCharaters();
            }
            return results.Distinct().OrderBy(x => x);
        }
        List<string> BuildMixedArray() 
        {
            List<string> results = new List<string>();

            for (int i = 0; i < Letters.Length; i++)
            {
                // shift right
                results.AddRange(ShiftRight());

                results.AddRange(SwapCharaters(i % 2 == 0));
            }
            return results;
        }
        List<string> ShiftRight()
        {
            List<string> results = new List<string>();

            // shift right
            for (int i = 0; i < Letters.Length; i++)
            {
                Letters = ShiftString(Letters);
                results.Add(Letters);
            }
            return results;
        }
        string ShiftString(string t)
        {
            return t.Substring(1, t.Length - 1) + t.Substring(0, 1);
        }
        string SwapCharaters()
        {
            // Convert letters to a char array
            char[] lettersarray = Letters.ToCharArray();

            for (int i = 0; i < lettersarray.Length - 2; i++)
            {
                if (i % 2 == 0)
                {
                    Swap(ref lettersarray[i], ref lettersarray[i + 2]);
                }
            }
            return new string(lettersarray);
        }

        List<string> SwapCharaters(bool IsEven)
        {
            List<string> results = new List<string>();
            // Convert letters to a char array
            char[] lettersarray = Letters.ToCharArray();

            // swap every other letter
            if (IsEven)
            {
                for (int i = 0; i < lettersarray.Length - 2; i++)
                {
                    if (i % 2 == 0)
                    {
                        Swap(ref lettersarray[i], ref lettersarray[i + 2]);
                        results.Add(new string(lettersarray));
                    }
                }
            }
            else
            {
                for (int i = 0; i < lettersarray.Length - 3; i++)
                {
                    if (i % 3 == 0)
                    {
                        Swap(ref lettersarray[i], ref lettersarray[i + 3]);
                        results.Add(new string(lettersarray));
                    }
                }
            }
            //results.Add(new string(lettersarray));
            Letters = new string(lettersarray);
            return results;
        }
        static void Swap(ref char a, ref char b)
        {
            char temp = a;
            a = b;
            b = temp;
        }
    }
}
