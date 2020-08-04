using ScrabbleAgentAI.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleAgentAI
{
    public class WordList : IWordList
    {
		private readonly Dictionary<char, int> _pointTable;
		private readonly FileInfo _file;

		public WordList(FileInfo file)
		{
			_file = file;
			_pointTable = new Dictionary<char, int>()
			{
				{ 'a', 1}, { 'b', 3}, { 'c', 8}, { 'd', 1}, { 'e', 1}, { 'f', 3}, { 'g', 2}, { 'h', 3},
				{ 'i', 1}, { 'j', 7}, { 'k', 3}, { 'l', 2}, { 'm', 3}, { 'n', 1}, { 'o', 2}, { 'p', 4},
				{ 'q', 0}, { 'r', 1}, { 's', 1}, { 't', 1}, { 'u', 4}, { 'v', 3}, { 'x', 8}, { 'y', 7},
				{ 'z', 8}
			};
		}

		public PrefixTree Build()
        {
			PrefixTree tree = new PrefixTree();
			string[] lines = File.ReadAllLines(_file.FullName);
			foreach (string line in lines)
			{
				if (line.StartsWith("CUSTOM") || line.StartsWith("BASEWORDS") || line.StartsWith("DEFINITION"))
				{
					continue;
				}
				else if (line.StartsWith("COMPOUND"))
				{
					string temp = line.Trim();
					tree.Insert(temp);
				}
				else
				{
					tree.Insert(line);
				}
			}
			return tree;
		}

        public int Calculate(string word)
        {
			int points = 0;
			foreach (char character in word)
			{
				if (_pointTable.ContainsKey(character))
				{
					points += _pointTable[character];
				}
			}
			return points;
		}
    }
}
