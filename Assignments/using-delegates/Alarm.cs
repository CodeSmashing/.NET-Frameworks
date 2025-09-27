using System.Reflection;
using using_delegates;

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

DateTime alarmEnd = DateTime.Now;
System.Timers.Timer timer = new() { Interval = 1000 };
double alarmSnooze; // In minutes
int wakingMethodsChoiceCount = AlarmActions.WakingMethods.Count + 1; // +1 for finishing
timer.Elapsed += OnTimedEvent;

Console.WriteLine("Welcome to the Alarm application!");
Console.WriteLine("This application allows you to set an alarm with various options.");
Console.WriteLine("You can set the alarm time, snooze duration, and choose how you want to be alerted.");
Console.WriteLine("You can also stop the alarm or snooze it when it goes off.");
Console.Write(" | ");
Console.ReadKey();
Console.Clear();

alarmSetup();

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
	Console.WriteLine("Enter your choice (1-7)");
	Console.Write(" > ");
	_ = int.TryParse(Console.ReadLine(), out int choice);
	Console.Clear();

	switch (choice)
	{
		case 1:
			timer.Enabled = false;
			updateEndTime();
			break;
		case 2:
			timer.Enabled = false;
			updateSnoozeDuration();
			break;
		case 3:
			timer.Enabled = false;
			updatePrefferedMethods();
			break;
		case 4:
			if (timer.Enabled)
			{
				Console.WriteLine("Alarm is already running.");
			}
			else
			{
				timer.Enabled = true;
				Console.WriteLine($"Alarm started. It will go off at {alarmEnd} (HH:MM).");
			}
			Console.Write(" | ");
			Console.ReadKey();
			Console.Clear();
			break;
		case 5:
			if (timer.Enabled)
			{
				timer.Enabled = false;
				Console.WriteLine("Alarm stopped.");
			}
			else
			{
				Console.WriteLine("Alarm is not running.");
			}
			Console.Write(" | ");
			Console.ReadKey();
			Console.Clear();
			break;
		case 6:
			if (timer.Enabled)
			{
				alarmEnd = alarmEnd.AddMinutes(alarmSnooze);
				Console.WriteLine($"Alarm snoozed. New alarm time is {alarmEnd} (HH:MM).");
			}
			else
			{
				Console.WriteLine("Alarm is not running.");
			}
			Console.Write(" | ");
			Console.ReadKey();
			Console.Clear();
			break;
		case 7:
			timer.Enabled = false;
			Console.WriteLine("Exiting the application. Goodbye!");
			Console.Write(" | ");
			Console.ReadKey();
			Console.Clear();
			return;
		default:
			Console.WriteLine("Invalid choice. Please enter a number between 1 and 7.");
			Console.Write(" | ");
			Console.ReadKey();
			Console.Clear();
			break;
	}
}

void alarmSetup()
{
	updateEndTime();

	updateSnoozeDuration();

	updatePrefferedMethods();

	timer.Start();
	Console.WriteLine($"Alarm setup complete! The alarm will go off at {alarmEnd} time.");
	Console.Write(" | ");
	Console.ReadKey();
	Console.Clear();
}

void updateEndTime()
{
	alarmEnd = DateTime.Parse(getInput("alarm-end", "At what time do you want the alarm to go off? (HH:MM, 24-hour format)"));
	Console.WriteLine($"\nAlarm time set to {alarmEnd:HH:mm}.");
	Console.Write(" | ");
	Console.ReadKey();
	Console.Clear();
}

void updateSnoozeDuration()
{
	alarmSnooze = double.Parse(getInput("alarm-snooze", "Enter the snooze duration in minutes (from 0-1440)"));
	Console.WriteLine($"\nSnooze duration set to {alarmSnooze} minutes.");
	Console.Write(" | ");
	Console.ReadKey();
	Console.Clear();
}

void updatePrefferedMethods()
{
	int wakingMethodChoice = -1;

	// Loop until the user chooses to finish (in this case option 4)
	while (wakingMethodChoice != wakingMethodsChoiceCount)
	{
		int i = 1;

		Console.WriteLine("Choose your preferred methods of waking up:");
		foreach (var method in AlarmActions.WakingMethods)
		{
			Console.WriteLine($"{i++}. {method.Key.ToUpper()}\t-\t{method.Value}");
		}
		Console.WriteLine($"{i++}. Done");
		Console.WriteLine("You can toggle each method by entering its number.");
		Console.WriteLine($"Enter your choice (1-{wakingMethodsChoiceCount})");
		Console.Write(" > ");
		_ = int.TryParse(Console.ReadLine(), out wakingMethodChoice);

		switch (wakingMethodChoice)
		{
			case 1:
			case 2:
			case 3:
				KeyValuePair<string, bool> method = AlarmActions.ToggleWakingMethod(wakingMethodChoice - 1);
				Console.WriteLine($"{method.Key} is now set to\t-\t{method.Value}");
				break;
			case 4:
				Console.WriteLine("Finished setting waking methods.");
				break;
			default:
				Console.WriteLine($"Invalid choice. Please enter a number between 1 and {wakingMethodsChoiceCount}.");
				break;
		}

		Console.Write(" | ");
		Console.ReadKey();
		Console.Clear();
	}
}

// Function to get and validate user input based on the option
string getInput(string option, string prompt)
{
	string? userInput = null;
	bool isValid = false;

	while (!isValid)
	{
		try
		{
			Console.WriteLine(prompt);
			Console.Write(" > ");
			userInput = Console.ReadLine();
			ArgumentNullException.ThrowIfNull(userInput);
			ArgumentException.ThrowIfNullOrEmpty(userInput);
			ArgumentException.ThrowIfNullOrWhiteSpace(userInput);

			switch (option)
			{
				case "alarm-snooze":
					_ = double.TryParse(userInput, out double parsedAlarmSnooze);
					ArgumentOutOfRangeException.ThrowIfNegative(parsedAlarmSnooze, nameof(parsedAlarmSnooze));
					ArgumentOutOfRangeException.ThrowIfGreaterThan(parsedAlarmSnooze, 1440, nameof(parsedAlarmSnooze));

					isValid = true;
					break;
				case "alarm-end":
					_ = DateTime.TryParse(userInput, out DateTime parsedAlarmEnd);
					ArgumentOutOfRangeException.ThrowIfLessThan(parsedAlarmEnd, DateTime.Now, nameof(parsedAlarmEnd));
					ArgumentException.Equals(parsedAlarmEnd.TimeOfDay.TotalMinutes, DateTime.Now.TimeOfDay.TotalMinutes);

					isValid = true;
					break;
				default:
					Console.WriteLine("Invalid option specified.");
					Console.Write(" | ");
					Console.ReadKey();
					Console.Clear();
					break;
			}
		}
		catch (Exception e)
		{
			// Possible exception messages if I could use conditional statements:
			//"Snooze time must be a non-negative number."
			//"Invalid input. Please enter the time in HH:MM format (24-hour)."
			//"Snooze time cannot exceed 1440 minutes (24 hours)."
			Console.WriteLine($"Error: {e.Message}");
			Console.Write(" | ");
			Console.ReadKey();
			Console.Clear();
			isValid = false;
		}
	}
	return userInput!;
}

// Define what happens when the timer elapses
void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
{
	TimeSpan timeUntil = alarmEnd - DateTime.Now;

	if (timeUntil.TotalSeconds <= 0)
	{
		timer.Enabled = false;
		Console.WriteLine("\nAlarm is going off NOW!");
		foreach (var method in AlarmActions.WakingMethods)
		{
			if (method.Value)
			{
				MethodInfo? methodInfo = typeof(AlarmActions).GetMethod(method.Key, BindingFlags.Public | BindingFlags.Static);
				methodInfo?.Invoke(null, null);
			}
		}
		// Would like some later "\nPress 'S' to stop the alarm or 'Z' to snooze."
		Console.WriteLine($"\nAlarm went off at {alarmEnd:HH:mm}.");
		Console.Write(" | ");
	}
	else
	{
		Console.WriteLine($"Time until alarm: {timeUntil.Hours} hours, {timeUntil.Minutes} minutes, {timeUntil.Seconds} seconds.");
	}
}
