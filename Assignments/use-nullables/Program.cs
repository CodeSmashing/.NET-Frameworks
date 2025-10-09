/*
 * Oefening: Gebruik nullables
 * Een bedrijf met drie afdelingen (Verkoop, Ondersteuning en Administratie) geeft zijn werknemers een bonus van 2% per jaar in dienst op hun wedde, als de werknemer in minstens 2 van de drie afdelingen heeft gewerkt.
 * 
 * Voor elk jaar moet er een vol jaar gewerkt zijn (niet afronden naar boven).
 * 
 * Vraag voor een werknemer 
 *    de naam van de werknemer
 *		hoe lang hij voor de drie afdelingen heeft gewerkt (als hij niet voor een afdeling heeft gewerkt, vul je voor die afdeling geen waarde in).
 *	
 *	Bereken hoe groot zijn bonuspercentage is.
 */

namespace use_nullables {
	class Program {
		internal static void Main(string[] args) {
			Stack<Worker> workerStack = new();

			workerStack.Push(new() { Name = "Alice Mclafferty", YearsInSales = 3, YearsInAdministration = 30 });
			workerStack.Push(new() { Name = "John Doe", YearsInAdministration = 5 });
			workerStack.Push(new() { Name = "Jane Doe", YearsInSupport = 10, YearsInAdministration = 1 });

			foreach (Worker worker in workerStack) {
				Console.WriteLine(worker);
				Console.ReadKey();
				Console.Clear();
			}
		}
	}
}