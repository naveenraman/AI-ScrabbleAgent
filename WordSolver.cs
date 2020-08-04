using ScrabbleAgentAI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleAgentAI
{
    public class WordSolver
    {
        private readonly PrefixTree _tree;
        private readonly IWordList _wordList;

        public int IndexedWordCount
        {
            get { return _tree.Count; }
        }

        public WordSolver(IWordList wordList)
        {
            #region Sanity Check
            if (wordList == null)
            {
                throw new ArgumentNullException("wordList", "No word list provided.");
            }
            #endregion

            _wordList = wordList;
            _tree = _wordList.Build();
        }

        public WordResult[] Solve(IEnumerable<char> characters)
        {
            // Sanitize input.
            List<WordResult> results = new List<WordResult>();
            string[] words = this.Solve(new WordRack(characters), _tree.Root);
            foreach (string word in words.Distinct())
            {
                results.Add(new WordResult(word, _wordList.Calculate(word)));
            }
            return results.OrderByDescending(x => x.Points).ToArray();
        }

        private string[] Solve(WordRack originalRack, PrefixTreeNode root)
        {
            List<string> words = new List<string>();
            foreach (char character in originalRack.AvailableLetters)
            {
                WordRack rack = originalRack.Clone();
                PrefixTreeNode node = root.Children.FirstOrDefault(x => x.Letter == character);
                if (node != null)
                {
                    rack.Consume(character);
                    if (node.IsWord)
                    {
                        words.Add(node.GetWord());
                    }

                    string[] result = this.Solve(rack, node);
                    if (result != null && result.Length > 0)
                    {
                        words.AddRange(result);
                    }
                }
            }
            return words.ToArray();
        }

    }
}
