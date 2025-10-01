using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Utilities.InputHelper;

namespace using_delegates {
	internal sealed class Alarm {
		private static Alarm? instance = null;
		private static DateTime alarmEnd;
		private static System.Timers.Timer timer = new() { Interval = 1000 };
		private static double alarmSnooze; // In minutes
		private static int wakingMethodsChoiceCount = AlarmActions.WakingMethods.Count + 1; // +1 for finishing
		private static StringBuilder menuBuilder = new();
		private delegate void Action();
		private delegate void GetInputAction<T>(string prompt);

		// Private constructor
		private Alarm() {
		}

		// Property to access the Instance
		public static Alarm Instance {
			get {
				// Check if the Instance already exists
				if (instance == null)
					instance = new Alarm();

				return instance;
			}
		}

		public void Run() {
			int? choice = null;
			timer.Elapsed += OnTimedEvent;

			while (true) {
				Action action;
				menuBuilder.Clear();
				menuBuilder.AppendLine("Alarm Menu:\n1. Set Alarm Time\n2. Set Snooze Duration\n3. Toggle Waking Methods\n4. Start Alarm\n5. Stop Alarm\n6. Snooze Alarm\n7. Exit\nEnter your choice (1-7)");
				choice = GetInput<int>(menuBuilder.ToString());

				switch (choice) {
					case 1:
						timer.Enabled = false;
						UpdateEndTime();
						break;
					case 2:
						timer.Enabled = false;
						UpdateSnoozeDuration();
						break;
					case 3:
						timer.Enabled = false;
						UpdatePrefferedMethods();
						break;
					case 4:
						action = timer.Enabled switch {
							true => () => Console.WriteLine("Alarm is already running."),
							false => () => {
								timer.Enabled = true;
								Console.WriteLine($"Alarm started. It will go off at {alarmEnd} (HH:MM).");
							}
						};
						action();
						WriteReadClear(" | ");
						break;
					case 5:
						action = timer.Enabled switch {
							true => () => {
								timer.Enabled = false;
								Console.WriteLine("Alarm stopped.");
							},
							false => () => Console.WriteLine("Alarm is not running.")
						};
						action();
						WriteReadClear(" | ");
						break;
					case 6:
						action = timer.Enabled switch {
							true => () => {
								alarmEnd = alarmEnd.AddMinutes(alarmSnooze);
								Console.WriteLine($"Alarm snoozed. New alarm time is {alarmEnd} (HH:MM).");
							},
							false => () => Console.WriteLine("Alarm is not running.")
						};
						action();
						WriteReadClear(" | ");
						break;
					case 7:
						timer.Enabled = false;
						Console.WriteLine("Exiting the application. Goodbye!");
						WriteReadClear(" | ");
						return;
					default:
						Console.WriteLine("Invalid choice. Please enter a number between 1 and 7.");
						WriteReadClear(" | ");
						break;
				}
			}
		}

		public void AlarmSetup() {
			UpdateEndTime();

			UpdateSnoozeDuration();

			UpdatePrefferedMethods();

			timer.Start();
			Console.WriteLine($"Alarm setup complete! The alarm will go off at {alarmEnd} time.");
			WriteReadClear(" | ");
		}

		public static void UpdateEndTime() {
			alarmEnd = GetInput<DateTime>("At what time do you want the alarm to go off? (HH:MM, 24-hour format)");
			Console.WriteLine($"Alarm time set to {alarmEnd:HH:mm}.");
			WriteReadClear(" | ");
		}

		public static void UpdateSnoozeDuration() {
			alarmSnooze = GetInput<double>("Enter the snooze duration in minutes (from 0-1440)");
			Console.WriteLine($"Snooze duration set to {alarmSnooze} minutes.");
			WriteReadClear(" | ");
		}

		public static void UpdatePrefferedMethods() {
			int wakingMethodChoice = -1;

			// Loop until the user chooses to finish (in this case option 4)
			while (wakingMethodChoice != wakingMethodsChoiceCount) {
				int i = 1;

				menuBuilder.Clear();
				menuBuilder.AppendLine("Choose your preferred methods of waking up:");
				foreach (var method in AlarmActions.WakingMethods) {
					menuBuilder.AppendLine($"{i++}. {method.Key.ToUpper()}\t-\t{method.Value}");
				}
				menuBuilder.AppendLine($"{i++}. Done");
				menuBuilder.AppendLine("You can toggle each method by entering its number.");
				menuBuilder.AppendLine($"Enter your choice (1-{wakingMethodsChoiceCount})");
				wakingMethodChoice = GetInput<int>(menuBuilder.ToString());

				switch (wakingMethodChoice) {
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

				WriteReadClear(" | ");
			}
		}

		// Define what happens when the timer elapses
		public static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e) {
			TimeSpan timeUntil = alarmEnd - DateTime.Now;

			Action action = (timeUntil.TotalSeconds <= 0) switch {
				true => () => {
					timer.Enabled = false;
					Console.WriteLine("Alarm is going off NOW!");
					foreach (var method in AlarmActions.WakingMethods) {
						if (method.Value) {
							MethodInfo? methodInfo = typeof(AlarmActions).GetMethod(method.Key, BindingFlags.Public | BindingFlags.Static);
							methodInfo?.Invoke(null, null);
						}
					}

					// Would like some later "\nPress 'S' to stop the alarm or 'Z' to snooze."
					Console.WriteLine($"Alarm went off at {alarmEnd:HH:mm}.");
					WriteReadClear(" | ");
				},
				false => () => Console.WriteLine("Time until alarm: {0} hours, {1} minutes, {2} seconds.",
					timeUntil.Hours,
					timeUntil.Minutes,
					timeUntil.Seconds)
			};
			action();
		}
	}
}