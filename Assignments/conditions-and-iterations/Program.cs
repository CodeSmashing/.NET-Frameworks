using System.Collections;

/*
 * Oefening: Condities en iteraties 
 * Schrijf een c# console-applicatie "Omkeren" waarin je achtereenvolgens 
 *      een for-loop
 *      een while-loop
 *      een do-while-loop
 *      recursie
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

string parsedString = "";
int parsedChoice = 0;
ArrayList inputExceptions = [null, "", " "];
List<char> charList = new List<char>();

Console.Write("Please enter some text input: ");

getInput("text", inputExceptions, ref parsedString, ref parsedChoice);

Console.WriteLine($"\nYou entered: {parsedString}");
Console.WriteLine("Choose a method to reverse the string:");

Console.WriteLine("\n1. For-loop");
Console.WriteLine("2. While-loop");
Console.WriteLine("3. Do-while-loop");
Console.WriteLine("4. Recursion");

Console.Write("\nEnter your choice (1-4): ");

getInput("number", inputExceptions, ref parsedString, ref parsedChoice);

Console.WriteLine($"\nYou entered: {parsedChoice}");
Console.ReadKey();
Console.Write("Reversed string: ");

// As we always reverse the string, we can convert it to lower case first and capitalize the last letter
parsedString = parsedString.Substring(0, parsedString.Length - 1).ToLower() + parsedString[parsedString.Length - 1].ToString().ToUpper();

// A long chain of if-else statements to choose the correct method
// Not pretty, but it is good enough for such a simple program
if (parsedChoice == 1)
{
    for (int i = 0; i < parsedString.Length; i++)
    {
        charList.Add(parsedString[parsedString.Length - 1 - i]);
    }
    Console.WriteLine(new string(charList.ToArray()));
}
else if (parsedChoice == 2)
{
    while (charList.Count != parsedString.Length)
    {
        charList.Add(parsedString[parsedString.Length - 1 - charList.Count]);
    }
    Console.WriteLine(new string(charList.ToArray()));
}
else if (parsedChoice == 3)
{
    do
    {
        charList.Add(parsedString[parsedString.Length - 1 - charList.Count]);
    }
    while (charList.Count != parsedString.Length);
    Console.WriteLine(new string(charList.ToArray()));
}
else if (parsedChoice == 4)
{
    if (parsedString != null)
    {
        Console.WriteLine(ReverseRecursion(parsedString));
    }
}
else
{
    Console.WriteLine("Invalid choice. Please restart the program and choose a number between 1 and 4.");
}
Console.ReadLine();

// Recursion method to reverse a string
// Source: https://www.geeksforgeeks.org/reverse-string-using-recursion/
static string ReverseRecursion(string str)
{
    if (str.Length == 0)
    {
        return str;
    }
    return ReverseRecursion(str.Substring(1)) + str[0];
}

// Method to get and validate user input
static void getInput(string option, ArrayList inputExceptions, ref string parsedString, ref int parsedChoice)
{
    string? userInput = "";
    bool validInput = false;
    do
    {
        userInput = Console.ReadLine();

        if (option == "text")
        {
            validInput = !inputExceptions.Contains(userInput);
            if (validInput)
            {
                parsedString = userInput ?? "";
            }
        }
        else if (option == "number")
        {
            validInput = Int32.TryParse(userInput, out parsedChoice);
            validInput = validInput && (parsedChoice >= 1 && parsedChoice <= 4);
        }

        if (!validInput)
        {
            Console.Write("Invalid input. Try again: ");
        }
    }
    while (!validInput);
}