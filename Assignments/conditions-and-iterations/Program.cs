using System.Collections;
using static Utilities.InputHelper;

/*
 * Oefening: Condities en iteraties 
 * Schrijf een c# console-applicatie "Omkeren" waarin je achtereenvolgens 
 *		een for-loop
 *		een while-loop
 *		een do-while-loop
 *		recursie
 *  
 * gebruikt om een string, die je inleest vanop de console, omgekeerd naar de console schrijft.
 * 
 * Je zorgt voor een "propere" afwerking van het vragen en wegschrijven van de string, met inbegrip van een menuutje waarin je de gebruiker laat kiezen welke iteratiemethode gebruikt moet worden.
 * 
 * Het resultaat bewaar je op je GitHub.  Je zet bij deze oefening een link naar de juiste GitHub-repository.
 * 
 * 
 * Hint:  Je kan gebruikmaken van de string-methoden First() en Last()
*/

namespace conditions_and_iterations {
	class Program {
		static void Main(string[] args) {
			string inputString;
			int inputChoice;
			List<char> charList = [];

			inputString = GetInput<string>("Please enter some text input:");

			Console.WriteLine($"\nYou entered: {inputString}");
			Console.WriteLine("Choose a method to reverse the string:");

			Console.WriteLine("\n1. For-loop");
			Console.WriteLine("2. While-loop");
			Console.WriteLine("3. Do-while-loop");
			Console.WriteLine("4. Recursion");

			inputChoice = GetInput<int>("Enter your choice (1-4):");

			Console.WriteLine($"\nYou entered: {inputChoice}");
			Console.ReadKey();
			Console.Write("Reversed string: ");

			// As we always reverse the string, we can convert it to lower case first and capitalize the last letter
			inputString = inputString.Substring(0, inputString.Length - 1).ToLower() + inputString[inputString.Length - 1].ToString().ToUpper();

			// A long chain of if-else statements to choose the correct method
			// Not pretty, but it is good enough for such a simple program
			if (inputChoice == 1) {
				for (int i = 0; i < inputString.Length; i++) {
					charList.Add(inputString[inputString.Length - 1 - i]);
				}
				Console.WriteLine(new string(charList.ToArray()));
			} else if (inputChoice == 2) {
				while (charList.Count != inputString.Length) {
					charList.Add(inputString[inputString.Length - 1 - charList.Count]);
				}
				Console.WriteLine(new string(charList.ToArray()));
			} else if (inputChoice == 3) {
				do {
					charList.Add(inputString[inputString.Length - 1 - charList.Count]);
				}
				while (charList.Count != inputString.Length);
				Console.WriteLine(new string(charList.ToArray()));
			} else if (inputChoice == 4) {
				if (inputString != null) {
					Console.WriteLine(ReverseRecursion(inputString));
				}
			} else {
				Console.WriteLine("Invalid choice. Please restart the program and choose a number between 1 and 4.");
			}
			Console.ReadLine();

			// Recursion method to reverse a string
			// Source: https://www.geeksforgeeks.org/reverse-string-using-recursion/
			static string ReverseRecursion(string str) {
				if (str.Length == 0) {
					return str;
				}
				return ReverseRecursion(str.Substring(1)) + str[0];
			}
		}
	}
}