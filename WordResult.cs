using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleAgentAI
{
    public class WordResult
    {
		private readonly string _word;
		private readonly int _points;

		public string Word
		{
			get { return _word; }
		}

		public int Points
		{
			get { return _points; }
		}

		public WordResult(string word, int points)
		{
			_word = word;
			_points = points;
		}
	}
}
