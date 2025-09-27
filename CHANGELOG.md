# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project (tries) to adhere to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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

- Initial commit for the 'using-delegates' assignment. Includes a C# console application that is incomplete and requires further development.
 (In future updates, meant to demonstrate the use of delegates to encapsulate method references, along with supporting project and solution files.)

## [0.2.1] - 2025-09-23

### Changed

- In the [Conditions and Iterations](Assignments/conditions-and-iterations/Program.cs) assignment moved inputExceptions ArrayList into getInput method and updated method signature to remove it as a parameter. This simplifies the function call and encapsulates input validation logic within the method.
- Expanded changelog entry for version 0.1.0 to provide more context on the initial commit.

### Added

- Added missing changelog entry for version 0.2.0.

## [0.2.0] - 2025-09-23

### Added

- Initial commit for the 'working-with-arrays' assignment. Includes a C# console application that demonstrates dynamic array resizing by appending user input, along with supporting project and solution files.

## [0.1.0] - 2025-09-22

### Changed

- Initial commit for the 'conditions-and-iterations' assignment. Includes a C# console application that demonstrates basic input validation and conditional logic, along with supporting project and solution files.

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

[0.3.1]: https://github.com/CodeSmashing/.NET-Frameworks/releases/tag/v0.3.1
[0.3.0]: https://github.com/CodeSmashing/.NET-Frameworks/releases/tag/v0.3.0
[0.2.1]: https://github.com/CodeSmashing/.NET-Frameworks/releases/tag/v0.2.1
[0.2.0]: https://github.com/CodeSmashing/.NET-Frameworks/releases/tag/v0.2.0
[0.1.0]: https://github.com/CodeSmashing/.NET-Frameworks/releases/tag/v0.1.0
[0.0.1]: https://github.com/CodeSmashing/.NET-Frameworks/releases/tag/v0.0.1
[0.0.0]: https://github.com/CodeSmashing/.NET-Frameworks/releases/tag/v0.0.0