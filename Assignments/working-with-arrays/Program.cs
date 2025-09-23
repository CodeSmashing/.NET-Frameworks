using System.Collections;

/*
 * Oefening: Werken met arrays 
 * Maak volgende console-applicatie:
 *		Creëer een methode waarin een array met 4 elementen wordt gevuld.
 *		Probeer dan een waarde te geven aan een vijfde element. 
 *		Voeg code toe die de array "vergroot" telkens dit nodig is.
 *		Test deze code door enkele extra elementen toe te voegen.
 * Zet de GitHub-link naar je code bij de opdracht.
 */

int[] numbers = { 1, 900, 200, 69 };
int? userInput;

Console.Write("Current array: ");
printArray(numbers);
Console.ReadLine();

Console.Write("\nEnter a new number to add to the array: ");
userInput = getInput();

Console.Write("\nYou entered: " + userInput);
Console.ReadLine();

numbers = numbers.Append((int) userInput).ToArray();

Console.Write("\nThe new array: ");
printArray(numbers);
Console.ReadLine();

// Method to get and validate user input
static int getInput()
{
	string? userInput = "";
	bool validInput = false;
	ArrayList inputExceptions = [null, "", " "];
	int returnValue;

	do
	{
		userInput = Console.ReadLine();

		validInput = Int32.TryParse(userInput, out returnValue);

		if (!validInput)
		{
			Console.Write("Invalid input. Try again: ");
		}
	}
	while (!validInput);
	return returnValue;
}

// Method to print the array in a nice format
static void printArray(int[] arr)
{
	Console.Write("{ ");
	foreach (int number in arr)
	{
		if (arr.Last() == number)
		{
			Console.Write($"{number} ");
			break;
		}
		Console.Write($"{number}, ");
	}
	Console.Write('}');
}