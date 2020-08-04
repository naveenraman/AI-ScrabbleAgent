using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleAgentAI
{
    public class WordRack
    {
        private readonly List<char> _originalCharacters;
        private readonly List<char> _availableCharacters;

        public List<char> AvailableLetters
        {
            get { return _availableCharacters; }
        }

        public WordRack(IEnumerable<char> characters)
        {
            _originalCharacters = new List<char>(this.Sanitize(characters));
            _availableCharacters = new List<char>(_originalCharacters);
        }

        private WordRack(IEnumerable<char> original, IEnumerable<char> available)
        {
            _originalCharacters = new List<char>(original);
            _availableCharacters = new List<char>(available);
        }

        private char[] Sanitize(IEnumerable<char> characters)
        {
            List<char> letters = new List<char>();
            foreach (char character in characters)
            {
                if (char.IsLetter(character))
                {
                    char temp = character;
                    if (!char.IsLower(temp))
                    {
                        temp = char.ToLower(temp);
                    }
                    letters.Add(temp);
                }
            }
            return letters.ToArray();
        }

        public void Consume(char letter)
        {
            if (_availableCharacters.Contains(letter))
            {
                _availableCharacters.Remove(letter);
            }
        }

        public WordRack Clone()
        {
            return new WordRack(_originalCharacters, _availableCharacters);
        }

        public override string ToString()
        {
            return new string(_availableCharacters.ToArray());
        }
    }
}
