using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Utilities.InputHelper;

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

namespace using_delegates {
	internal class Program {
		private static void Main(string[] args) {
			Alarm alarm = Alarm.Instance;

			Console.WriteLine("Welcome to the Alarm application!");
			Console.WriteLine("This application allows you to set an alarm with various options.");
			Console.WriteLine("You can set the alarm time, snooze duration, and choose how you want to be alerted.");
			Console.WriteLine("You can also stop the alarm or snooze it when it goes off.");
			WriteReadClear(" | ");

			alarm.AlarmSetup();
			alarm.Run();
		}
	}
}
