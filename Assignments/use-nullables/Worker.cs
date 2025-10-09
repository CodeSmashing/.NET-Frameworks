using System;
using System.Collections;
using System.Text;

namespace use_nullables {
	public class Worker {
		public string Name { get; set; } = string.Empty;

		public int? YearsInSales { get; set; } = null;

		public int? YearsInSupport { get; set; } = null;

		public int? YearsInAdministration { get; set; } = null;

		// Returns -1 if the department is invalid
		// Returns the years in the department otherwise
		public int? GetYearsIn(string department) {
			switch (department.ToLowerInvariant()) {
				case "sales":
					return YearsInSales;
				case "support":
					return YearsInSupport;
				case "administration":
					return YearsInAdministration;
				default:
					return -1;
			}
		}

		// Returns false if the department or years is negative
		// Returns true otherwise
		public bool SetYearsIn(string department, int years) {
			if (0 > years)
				return false;

			switch (department.ToLowerInvariant()) {
				case "sales":
					YearsInSales = years;
					return true;
				case "support":
					YearsInSupport = years;
					return true;
				case "administration":
					YearsInAdministration = years;
					return true;
				default:
					return false;
			}
		}

		public int GetTotalYears() {
			return (YearsInSales ?? 0) + (YearsInSupport ?? 0) + (YearsInAdministration ?? 0);
		}

		public int GetOwedBonusPercentage() {
			int departmentsWorkedIn = 0;

			// Count the number of departments worked in
			if (YearsInSales.HasValue && YearsInSales.Value > 0)
				departmentsWorkedIn++;
			if (YearsInSupport.HasValue && YearsInSupport.Value > 0)
				departmentsWorkedIn++;
			if (YearsInAdministration.HasValue && YearsInAdministration.Value > 0)
				departmentsWorkedIn++;

			// If worked in less than 2 departments, no bonus.
			// Too bad!
			if (departmentsWorkedIn < 2)
				return 0;
			return GetTotalYears() * 2;
		}

		public override string ToString() {
			return $"Worker {Name} has worked {YearsInSales ?? 0} years in Sales, {YearsInSupport ?? 0} years in Support and {YearsInAdministration ?? 0} years in Administration.\nThey are owed a bonus of {GetOwedBonusPercentage()}%.";
		}
	}
}
