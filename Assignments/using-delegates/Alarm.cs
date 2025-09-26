using System.Collections;
using System.Reflection;

/*
 * Oefening: Gebruik van delegates 
 * Schrijf de console-applicaties  "Wekker" voor het gebruik van delegates (en "Timer") :
 * Voorzie een repetitief menu waarmee je 
 *		de tijd kan instellen waarop je wekker "afloopt"
 *		de sluimertijd kan instellen nadat je wekker is afgelopen
 *		een "stop alarm" + sluimerknop
 *		schakel de verschillende manieren van wekken in of uit
 *	
 *	Voorzie voor manier van wekken minstens
 *		geluid
 *		een boodschap
 *		een knipperlicht
 *	
 *	die apart of in combinaties gebruikt moeten worden.  Deze mogen als "tekst" op de console geschreven worden,.  Maar als "geluid" of "knipperen" echt geproduceerd kan worden, is dat natuurlijk een meerwaarde.
 *	Een absolute voorwaarde:  Op de "menu"-Switch na, mag er geen conditionele statements (if / else) gebruikt worden.  Je zal dus strikt moeten werken met delegates en Timer
*/

Timer timer = new();

Console.WriteLine("Welcome to the Alarm application!");
Console.WriteLine("This application allows you to set an alarm with various options.");
Console.WriteLine("You can set the alarm time, snooze duration, and choose how you want to be alerted.");
Console.WriteLine("You can also stop the alarm or snooze it when it goes off.");
Console.ReadKey();
Console.Clear();

timerSetup();

while (true)
{
	Console.WriteLine("Alarm Menu:");
	Console.WriteLine("1. Set Alarm Time");
	Console.WriteLine("2. Set Snooze Duration");
	Console.WriteLine("3. Toggle Waking Methods");
	Console.WriteLine("4. Start Alarm");
	Console.WriteLine("5. Stop Alarm");
	Console.WriteLine("6. Snooze Alarm");
	Console.WriteLine("7. Exit");
	Console.Write("Enter your choice (1-7)\n > ");
	_ = int.TryParse(Console.ReadLine(), out int choice);
	Console.Clear();

	switch (choice)
	{
		case 1:
			updateEndTime();
			break;
		case 2:
			updateSnoozeDuration();
			break;
		case 3:
			updatePrefferedMethods();
			break;
		case 4:
			if (timer.IsRunning)
			{
				Console.WriteLine("Alarm is already running.");
			}
			else
			{
				timer.IsRunning = true;
				Console.WriteLine($"Alarm started. It will go off at {timer.EndTimeString()} (HH:MM).");
			}
			break;
		case 5:
			if (timer.IsRunning)
			{
				timer.IsRunning = false;
				Console.WriteLine("Alarm stopped.");
			}
			else
			{
				Console.WriteLine("Alarm is not running.");
			}
			break;
		case 6:
			if (timer.IsRunning)
			{
				timer.Snooze();
				Console.WriteLine($"Alarm snoozed. New alarm time is {timer.EndTimeString()} (HH:MM).");
			}
			else
			{
				Console.WriteLine("Alarm is not running.");
			}
			break;
		case 7:
			Console.WriteLine("Exiting the application. Goodbye!");
			return;
		default:
			Console.WriteLine("Invalid choice. Please enter a number between 1 and 7.");
			break;
	}

	Console.ReadKey();
}

void timerSetup()
{
	updateEndTime();

	updateSnoozeDuration();

	updatePrefferedMethods();

	Console.WriteLine("\nAlarm setup complete! The alarm will go off at the set time.");
	timer.IsRunning = true;
	Console.ReadKey();
	Console.Clear();
}

void updateEndTime()
{
	Console.Write("\nAt what time do you want the alarm to go off? (HH:MM, 24-hour format)\n > ");
	_ = TimeOnly.TryParse(getInput("24-hour").ToString("00:00"), out TimeOnly parsedTime);
	timer.EndTime = parsedTime;
	Console.ReadKey();
	Console.Clear();
}

void updateSnoozeDuration()
{
	Console.Write("\nEnter the snooze duration in minutes (in minutes from 0-1440)\n > ");
	timer.SnoozeTime = getInput("minutes");
	Console.WriteLine($"\nSnooze duration set to {timer.SnoozeTime} minutes.");
	Console.ReadKey();
	Console.Clear();
}

void updatePrefferedMethods()
{
	int wakingMethodChoice = -1;

	KeyValuePair<string, bool> soundMethod = timer.GetWakingMethod("sound");
	KeyValuePair<string, bool> messageMethod = timer.GetWakingMethod("message");
	KeyValuePair<string, bool> flashingMethod = timer.GetWakingMethod("flashing");

	Console.WriteLine("\nChoose your preferred methods of waking up:");
	Console.WriteLine($"1. {soundMethod.Key.ToUpper()}\t-\t{soundMethod.Value}");
	Console.WriteLine($"2. {messageMethod.Key.ToUpper()}\t-\t{messageMethod.Value}");
	Console.WriteLine($"3. {flashingMethod.Key.ToUpper()}\t-\t{flashingMethod.Value}");
	Console.WriteLine("4. View current preferences");
	Console.WriteLine("5. Done");
	Console.WriteLine("You can toggle each method by entering its number.");

	while (wakingMethodChoice != 5)
	{
		Console.Write("\nEnter your choice (1-5): ");
		_ = int.TryParse(Console.ReadLine(), out wakingMethodChoice);
		switch (wakingMethodChoice)
		{
			case 1:
				timer.ToggleWakingMethod("sound");
				Console.WriteLine($"Sound is now set to\t-\t{timer.GetWakingMethod("sound").Value}");
				break;
			case 2:
				timer.ToggleWakingMethod("message");
				Console.WriteLine($"Message is now set to\t-\t{timer.GetWakingMethod("message").Value}");
				break;
			case 3:
				timer.ToggleWakingMethod("flashing");
				Console.WriteLine($"Flashing is now set to\t-\t{timer.GetWakingMethod("flashing").Value}");
				break;
			case 4:
				Console.WriteLine("\nCurrent waking method preferences:");
				Console.WriteLine($"1. {soundMethod.Key.ToUpper()}\t-\t{soundMethod.Value}");
				Console.WriteLine($"2. {messageMethod.Key.ToUpper()}\t-\t{messageMethod.Value}");
				Console.WriteLine($"3. {flashingMethod.Key.ToUpper()}\t-\t{flashingMethod.Value}");
				break;
			case 5:
				Console.WriteLine("Finished setting waking methods.");
				break;
			default:
				Console.WriteLine("Invalid choice. Please enter a number between 1 and 3.");
				break;
		}
	}

	Console.ReadKey();
	Console.Clear();
}

static double getInput(string option)
{
	string? userInput;
	double parsedTime = -1;
	bool validInput = false;

	while (!validInput)
	{
		try
		{
			userInput = Console.ReadLine();
			ArgumentNullException.ThrowIfNull(userInput);
			ArgumentException.ThrowIfNullOrEmpty(userInput);
			ArgumentException.ThrowIfNullOrWhiteSpace(userInput);

			switch (option)
			{
				case "minutes":
					_ = double.TryParse(userInput, out double minutes);
					ArgumentOutOfRangeException.ThrowIfNegative(minutes, nameof(minutes));
					ArgumentOutOfRangeException.ThrowIfGreaterThan(minutes, 1440, nameof(minutes));

					parsedTime = minutes;
					validInput = true;
					break;
				case "24-hour":
					ArgumentOutOfRangeException.ThrowIfNegative(int.Parse(userInput.Replace(":", "")), nameof(userInput));
					ArgumentOutOfRangeException.ThrowIfGreaterThan(int.Parse(userInput.Replace(":", "")), 2459, nameof(userInput));
					ArgumentOutOfRangeException.ThrowIfLessThan(userInput.Length, 5, nameof(userInput));
					ArgumentOutOfRangeException.ThrowIfNotEqual(userInput.IndexOf(':'), 2, nameof(userInput));
					_ = double.TryParse(userInput.Replace(":", ""), out double time);

					parsedTime = time;
					validInput = true;
					break;
				default:
					Console.WriteLine("Invalid option specified.");
					break;
			}
		}
		catch (Exception e)
		{
			//"Snooze time must be a non-negative number."
			//"Invalid input. Please enter the time in HH:MM format (24-hour)."
			//"Snooze time cannot exceed 1440 minutes (24 hours)."
			Console.WriteLine($"Error: {e.Message}");
			validInput = false;
		}
	}
	return parsedTime;
}