using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace using_delegates {
	internal static class AlarmActions {
		private static List<KeyValuePair<string, bool>> wakingMethods =
		[
			new KeyValuePair<string, bool>("Sound", false),
			new KeyValuePair<string, bool>("Message", false),
			new KeyValuePair<string, bool>("Flashing", false)
		];

		public static List<KeyValuePair<string, bool>> WakingMethods {
			get {
				return wakingMethods;
			}
		}

		// Toggle the method based on the index, return the updated method
		public static KeyValuePair<string, bool> ToggleWakingMethod(int methodIndex) {
			return wakingMethods[methodIndex] = new KeyValuePair<string, bool>(
				wakingMethods[methodIndex].Key,
				!wakingMethods[methodIndex].Value
			);
		}

		public static void Sound() {
			Console.Beep(1000, 500);
			Console.Beep(1200, 500);
			Console.Beep(1400, 500);
			Console.Beep(1600, 500);
			Console.Beep(1800, 500);
		}
		public static void Message() {
			Console.WriteLine("Wake up! It's time to start your day!");
		}
		public static void Flashing() {
			for (int i = 0; i < 5; i++) {
				Console.BackgroundColor = ConsoleColor.DarkGray;
				Console.Clear();
				System.Threading.Thread.Sleep(250);
				Console.BackgroundColor = ConsoleColor.Black;
				Console.Clear();
				System.Threading.Thread.Sleep(250);
			}
		}
	}
}
