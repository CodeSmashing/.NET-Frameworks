using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities {
	public static class InputHelper {
		// Function to get and validate user input based on the expected type
		public static T GetInput<T>(string prompt) {
			string? userInput;
			bool isValid = false;
			T? result = default;

			while (!isValid) {
				try {
					Console.Write(prompt + "\n > ");
					userInput = Console.ReadLine();

					ArgumentNullException.ThrowIfNull(userInput);
					ArgumentException.ThrowIfNullOrEmpty(userInput);
					ArgumentException.ThrowIfNullOrWhiteSpace(userInput);

					// Validate based on the type of input expected
					result = ChangeType<T>(userInput);
					ArgumentNullException.ThrowIfNull(result);

					// If no exception was thrown, the input is valid
					isValid = true;
				} catch (Exception e) {
					Console.WriteLine($"\nError: {e.Message}");
					WriteReadClear(" | ");
					isValid = false;
				}
			}
			return result!;
		}

		// Generic method to change type
		public static T ChangeType<T>(this object obj) {
			return (T) Convert.ChangeType(obj, typeof(T));
		}

		// Utility method to write a separator, wait for a key press, and clear the console
		public static void WriteReadClear(string separator) {
			Console.Write(separator);
			Console.ReadKey();
			Console.Clear();
		}
	}
}
