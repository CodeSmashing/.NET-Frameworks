# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project (tries) to adhere to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added

- New [Conditions and Iterations](Assignments/conditions-and-iterations) assignment.
- New [Stack with methods](Assignments/stack-with-methods) assignment.
- New [Administration app](Assignments/administration-app) assignment.

## [0.6.0] - 2025-10-09

### Added

- Initial commit for the "use-nullables" assignment. Includes a C# console application that demonstrates the use of nullable types and null-coalescing operators, along with supporting project and solution files. This includes:
	- A [Program.cs](Assignments/use-nullables/Program.cs) class to contain the Main method of the assignment and handle info displaying.
	- A [Worker.cs](Assignments/use-nullables/Worker.cs) class to represent a worker with properties like Name, YearsInSales, ... and methods related to and once for calculating their bonus based on years in service.

## [0.5.2] - 2025-10-09

### Changed

- In the [Conditions and Iterations](Assignments/conditions-and-iterations) assignment, refactored the Program class to:
	- Explicitly add the namespace "conditions_and_iterations".
	- Explicitly add the private static void Main() method.
	- Use the GetInput() method from [InputHelper.cs](Assignments/Utilities/InputHelper.cs) to replace the previous getInput() method.

## [0.5.1] - 2025-10-09

### Changed

- In the [Working with arrays](Assignments/working-with-arrays) assignment, refactored the Program class to:
	- Explicitly add the namespace "working_with_arrays".
	- Explicitly add the private static void Main() method.
	- Use the GetInput() method from [InputHelper.cs](Assignments/Utilities/InputHelper.cs) to replace the previous getInput() method.

## [0.5.0] - 2025-10-09

### Added

- Initial commit for the "classes-and-events" assignment. Includes a C# console application that demonstrates the use of classes and events, along with supporting project and solution files. This includes:
	- A [Book.cs](Assignments/classes-and-events/Book.cs) class to represent a book with properties like title, author, and ISBN.
	- A [Magazine.cs](Assignments/classes-and-events/Magazine.cs) class to represent a magazine with the [Book.cs](Assignments/classes-and-events/Book.cs) properties inherited with an additional Period property to indicate the publication frequency.
	- A [PublicationPeriod.cs](Assignments/classes-and-events/PublicationPeriod.cs) enum to define possible publication periods (e.g., Daily, Weekly, Monthly).
	- A [Order.cs](Assignments/classes-and-events/Order.cs) class to represent an order with properties like Item, DateOrder, Quantity, ... and an event OrderPlaced that is triggered when an order is placed.
	- A [Program.cs](Assignments/classes-and-events/Program.cs) class to contain the Main method of the assignment and handle user interaction.

## [0.4.0] - 2025-10-01

### Added

- A new [Utilities](Assignments/Utilities/) class library to contain helper classes and methods to reduce code duplication.
- A new [DotNetFrameworks.sln](DotNetFrameworks/DotNetFrameworks.sln) solution to allow cross project to project use.
- In the [Using Delegates](Assignments/using-delegates/) assignment, added a [Program.cs](Assignments/using-delegates/Program.cs) class to contain the Main method of the assignment.
- In the [Utilities](Assignments/Utilities/) library a [InputHelper.cs](Assignments/Utilities/InputHelper.cs) class which holds:
	- A generic version of the GetInput() method.
	- A ChangeType() method for converting an object's type.
	- A WriteReadClear() method for performing a frequently used set of console method calls.

### Changed

- In [AlarmActions.cs](Assignments/using-delegates/AlarmActions.cs):
	- Formatted file using my personal perference coming from JavaScript.
- In [Alarm.cs](Assignments/using-delegates/Alarm.cs):
	- Formatted file using my personal perference coming from JavaScript.
	- Explicitly added the namespace "using_delegates".
	- Moved assignment description to [Program.cs](Assignments/using-delegates/Program.cs).
	- Moved initial welcome message to [Program.cs](Assignments/using-delegates/Program.cs).
	- Refactored the class to:
		- Use keywords like "private" and "static".
		- Use a default constructor.
		- Use a singleton pattern following [Kostas Kalafatis](https://dev.to/kalkwst/singleton-pattern-in-c-1dh0)
		- Use the GetInput() method from [InputHelper.cs](Assignments/Utilities/InputHelper.cs) to replace the previous getInput() method.
		- Use a StringBuilder for long menus.
		- Only use the GetInput() method to parse user input.
		- Encapsulate the final while loop inside a Run() method.

## [0.3.1] - 2025-09-27

### Added

- In the [Using Delegates](Assignments/using-delegates/) assignment, added [AlarmActions](Assignments/using-delegates/AlarmActions.cs) class to encapsulate actions that can be triggered by or are related to the alarm. This class includes methods for different alarm responses, such as sounding an alarm or sending a notification.
- In [Alarm.cs](Assignments/using-delegates/Alarm.cs):
	- An "alarmEnd" variable to define when the alarm should stop.
	- An "alarmSnooze" variable to set the snooze duration.
	- An "wakingMethodsChoiceCount" variable to keep track of the number of available waking methods.
	- An OnTimedEvent() method to handle the timer's elapsed event, triggering the alarm actions.

### Changed

- In [Alarm.cs](Assignments/using-delegates/Alarm.cs):
	- Replaced references to the custom Timer class with the built-in System.Timers.Timer class. This change simplifies the code and leverages the robustness of the .NET framework's built-in timer functionality.
	- Moved the "wakingMethods" variable to be a member of the AlarmActions class instead of the Timer class, as it logically pertains to the actions taken when the alarm triggers.
	- Added " > ", " | " and uses of .Clear() throughout the program to improve console output readability and user experience.
	- Renamed timerSetup() method to alarmSetup() to reflect the refactored functionality focused on alarm setup rather than generic timer setup.
	- In the final while-loop of the program, made it so the alarm stops when the user wishes to change the alarm settings.
	- Refactored the updateEndTime() and updateSnoozeDuration() methods to use the getInput() method for input validation, ensuring more consistent handling of user input across the program.
	- Refactored the updatePrefferedMethods() method:
		- To dynamically display available waking methods based on the AlarmActions class, making it easier to add or remove methods in the future.
		- By consolidating the switch-case logic for the first three cases into a single case that handles all three, reducing code duplication and improving maintainability.
	- Refactored the getInput() method to be more versatile and reusable across different parts of the program:
		- Instead of trying to parse and set specific variables directly, it now simply returns the validated input as a string. This allows calling methods to handle the parsing and assignment, making getInput() more flexible for various input scenarios.
		- Renamed the switch-cases to be more descriptive of their purpose (e.g., "alarm-snooze", "alarm-end").
		- Simplified the "alarm-end" case validation logic as per the refactoring into using System.Timers.Timer.

### Removed

- In the [Using Delegates](Assignments/using-delegates/) assignment, removed the custom Timer class and replaced its functionality with the built-in System.Timers.Timer class. This change simplifies the code and leverages the robustness of the .NET framework's built-in timer functionality.

## [0.3.0] - 2025-09-26

### Added

- Initial commit for the "using-delegates" assignment. Includes a C# console application that is incomplete and requires further development.
 (In future updates, meant to demonstrate the use of delegates to encapsulate method references, along with supporting project and solution files.)

## [0.2.1] - 2025-09-23

### Changed

- In the [Conditions and Iterations](Assignments/conditions-and-iterations/Program.cs) assignment moved inputExceptions ArrayList into getInput method and updated method signature to remove it as a parameter. This simplifies the function call and encapsulates input validation logic within the method.
- Expanded changelog entry for version 0.1.0 to provide more context on the initial commit.

### Added

- Added missing changelog entry for version 0.2.0.

## [0.2.0] - 2025-09-23

### Added

- Initial commit for the "working-with-arrays" assignment. Includes a C# console application that demonstrates dynamic array resizing by appending user input, along with supporting project and solution files.

## [0.1.0] - 2025-09-22

### Changed

- Initial commit for the "conditions-and-iterations" assignment. Includes a C# console application that demonstrates basic input validation and conditional logic, along with supporting project and solution files.

## [0.0.1] - 2025-09-22

### Added

- Initial assignments folder.
- Project files for first assignment.
- [CHANGELOG](CHANGELOG.md) for documenting changes.
- [Git Ignore](.gitignore) with default ignores for .NET projects.

## [0.0.0] - 2025-09-22

### Added

- [README](README.md) with repository title and description.
- [Git Attributes](.gitattributes).
- [MIT license](LICENSE).

[0.6.0]: https://github.com/CodeSmashing/.NET-Frameworks/releases/tag/v0.6.0
[0.5.2]: https://github.com/CodeSmashing/.NET-Frameworks/releases/tag/v0.5.2
[0.5.1]: https://github.com/CodeSmashing/.NET-Frameworks/releases/tag/v0.5.1
[0.5.0]: https://github.com/CodeSmashing/.NET-Frameworks/releases/tag/v0.5.0
[0.4.0]: https://github.com/CodeSmashing/.NET-Frameworks/releases/tag/v0.4.0
[0.3.1]: https://github.com/CodeSmashing/.NET-Frameworks/releases/tag/v0.3.1
[0.3.0]: https://github.com/CodeSmashing/.NET-Frameworks/releases/tag/v0.3.0
[0.2.1]: https://github.com/CodeSmashing/.NET-Frameworks/releases/tag/v0.2.1
[0.2.0]: https://github.com/CodeSmashing/.NET-Frameworks/releases/tag/v0.2.0
[0.1.0]: https://github.com/CodeSmashing/.NET-Frameworks/releases/tag/v0.1.0
[0.0.1]: https://github.com/CodeSmashing/.NET-Frameworks/releases/tag/v0.0.1
[0.0.0]: https://github.com/CodeSmashing/.NET-Frameworks/releases/tag/v0.0.0
