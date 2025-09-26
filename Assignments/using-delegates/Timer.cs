using System.Collections;

public class Timer
{
	private List<KeyValuePair<string, bool>> wakingMethods =
	[
		new KeyValuePair<string, bool>("sound", false),
		new KeyValuePair<string, bool>("message", false),
		new KeyValuePair<string, bool>("flashing", false)
	];

	public double? StartTime { get; set; }
	public TimeOnly? EndTime { get; set; }
	public double? SnoozeTime { get; set; }
	public bool IsRunning { get; set; }

	public Timer() { }

	public void Snooze()
	{
		if (this.EndTime != null && this.SnoozeTime != null)
		{
			this.EndTime = this.EndTime.Value.AddMinutes((double)this.SnoozeTime);
		}
	}

	public KeyValuePair<string, bool> GetWakingMethod(string method)
	{
		return this.wakingMethods.Find(kv => kv.Key == method);
	}

	public void ToggleWakingMethod(string method)
	{
		for (int i = 0; i < this.wakingMethods.Count; i++)
		{
			if (this.wakingMethods[i].Key == method)
			{
				this.wakingMethods[i] = new KeyValuePair<string, bool>(this.wakingMethods[i].Key, !this.wakingMethods[i].Value);
				break;
			}
		}
	}

	public string EndTimeString()
	{
		if (this.EndTime == null) return "Not set";
		return $"{this.EndTime.Value.Hour}:{this.EndTime.Value.Minute}";
	}
}