using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleAgentAI
{
    class Program
    {
        static void Main(string[] args)
        {
            WordSolver solver = CreateWordSolver(args);

			while (true)
			{
				char[] input = ReadInput();
				if (input.Length == 0)
				{
					break;
				}
				// Solve for the words.
				DateTime solveStarted = DateTime.Now;
				var words = solver.Solve(input).OrderByDescending(x => x.Points);
				TimeSpan searchTime = DateTime.Now - solveStarted;
				if (words.Any())
				{
					WriteWordTable(words);
					Console.WriteLine();
					Console.WriteLine("Search took {0:f} seconds.", searchTime.TotalSeconds);
					Console.WriteLine();
				}
			}
		}

		private static char[] ReadInput()
		{
			Console.Write("Enter letters -> ");
			Console.BackgroundColor = ConsoleColor.Red;
			Console.ForegroundColor = ConsoleColor.White;
			string input = Console.ReadLine();
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.Gray;
			return input.ToCharArray();
		}

		private static WordSolver CreateWordSolver(string[] args)
		{
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.WriteLine("SCRABBLE HELPER");
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine("\nReading word list and building tree...");

			// Create the solver.
			DateTime startIndex = DateTime.Now;
			string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			string defaultFilePath = Path.Combine(path + "\\File\\", "SOWPODS_complete.txt");
			FileInfo file = new FileInfo(args.Length == 1 ? args[0] : defaultFilePath);
			WordSolver solver = new WordSolver(new WordList(file));
			TimeSpan indexTime = DateTime.Now - startIndex;

			Console.WriteLine("\n # Indexed {0} words in {1:f} seconds.", solver.IndexedWordCount, indexTime.TotalSeconds);
			Console.WriteLine(" # Limiting results to the 5 best matches.");
			Console.WriteLine(" # Exit the program by pressing ENTER.\n");
			return solver;
		}

		private static void WriteWordTable(IEnumerable<WordResult> words)
		{
			Console.BackgroundColor = ConsoleColor.Blue;
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("\n-----------------------");
			Console.WriteLine("Word       |     Points");
			Console.WriteLine("-----------------------");
			foreach (var word in words)
			{
				Console.WriteLine("{0,-10} | {1,10}", word.Word, word.Points);
			}
			Console.WriteLine("-----------------------");
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.Gray;
		}
	}
}
