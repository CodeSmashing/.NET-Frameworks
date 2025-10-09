using static Utilities.InputHelper;

/*
 * Oefening: Werken met arrays
 * Maak volgende console-applicatie:
 *		Creëer een methode waarin een array met 4 elementen wordt gevuld.
 *		Probeer dan een waarde te geven aan een vijfde element. 
 *		Voeg code toe die de array "vergroot" telkens dit nodig is.
 *		Test deze code door enkele extra elementen toe te voegen.
 * Zet de GitHub-link naar je code bij de opdracht.
 */

namespace working_with_arrays {
	internal class Program {
		private static void Main(string[] args) {
			int[] numbers = [1, 900, 200, 69];
			int? userInput;

			Console.Write("Current array: ");
			printArray(numbers);
			WriteReadClear(" | ");

			userInput = GetInput<int>("Enter a new number to add to the array: ");

			Console.WriteLine("\nYou entered: " + userInput);
			WriteReadClear(" | ");

			numbers = numbers.Append((int) userInput).ToArray();

			Console.Write("The new array: ");
			printArray(numbers);
			WriteReadClear("");

			// Method to print the array in a nice format
			static void printArray(int[] arr) {
				Console.Write("{ ");
				foreach (int number in arr) {
					if (arr.Last() == number) {
						Console.Write($"{number} ");
						break;
					}
					Console.Write($"{number}, ");
				}
				Console.Write('}');
			}
		}
	}
}
