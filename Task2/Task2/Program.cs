using System;
using System.Collections.Generic;
using System.Threading;

namespace Task2 {
	class Program {
		private static List<String> phrases;
		private static List<String> history;

		static void Main(string[] args) {
			Console.WriteLine("Hi! Enter your name:");
			String name = Console.ReadLine();

			phrases = new List<String>() {
				name + ", everything is not so bad.",
				name + ", dont't give up. I believe in you:)",
				name + ", don't rush. Tomorrow will not come soon",
				name + ", i'm sure you are close."
			};

			PlayGame(name);
		}

		private static void PlayGame(String name) {
			history = new List<String>();
			Random random = new Random();

			int number = random.Next(51);
			Console.WriteLine("So, " + name + ", let's guess the number from 0 to 50");
			DateTime start = DateTime.Now;

			Console.WriteLine(number); // todo убрать

			int tryNumber = 0;

			while (true) {
				String guessStr = Console.ReadLine();

				if (guessStr.Equals("q")) {
					Console.WriteLine("Sorry. I'm leave...");
					Thread.Sleep(1500);
					return;
				}

				if (Int32.TryParse(guessStr, out int guess)) {
					tryNumber++;
					if (guess == number) {
						ProcessRightGuess(history, tryNumber, guess, start);
						return;
					} else {
						ProcessWrongGuess(guess, number, tryNumber, random);
					}
				} else {
					Console.WriteLine("Expected a number. I'll not consider this as an try");
				}
			}
		}
	
		private static void ProcessRightGuess(List<String> history, int tryNumber, int guess, DateTime start) {
			DateTime end = DateTime.Now;

			history.Add(guess + " - bingo");

			Console.WriteLine("---Indeed! You'r right. Here is your stats:---");

			TimeSpan interval = end - start;
			Console.WriteLine("*The time to finish (in minutes): {0:0.##}", interval.TotalMinutes);
			Console.WriteLine("*The number of tries: " + tryNumber);
			Console.WriteLine("*The history of tries:");
			history.ForEach(delegate (String item) {
				Console.WriteLine("   " + item);
			});

			Console.WriteLine("Press Enter to exit");
			Console.ReadLine();
		}

		private static void ProcessWrongGuess(int guess, int number, int tryNumber, Random random) {
			if (guess > number) {
				Console.WriteLine("More than needed");
				history.Add(guess + " - more than");
			} else {
				Console.WriteLine("Less than needed");
				history.Add(guess + " - less than");
			}

			if (tryNumber % 4 == 0) {
				Console.WriteLine(phrases[random.Next(phrases.Count)]);
			}
		}
	}
}

