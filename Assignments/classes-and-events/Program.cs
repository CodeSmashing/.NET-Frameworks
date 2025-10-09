using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;
using static Utilities.InputHelper;

/*
 * Opdracht 1: Klassen en events
 *	Je implementeert een eenvoudig bestellingssysteem voor een boekwinkel die boeken en tijdschriften verkoopt als console-applicatie.  Voor de tijdschriften worden ook abonnementen verkocht.
 *	
 *	Concreet:
 *	Klasse-hi�rarchie: Maak een basisklasse "Boek" met volgende eigenschappen:
 *			Isbn
 *			Naam
 *			Uitgever
 *			Prijs
 *	
 *		Vanzelfsprekend voorzie je de fundementele methoden (constructor, overloaded ToString, een basis-ingeefmethode Lees)
 *		Leid daarvan de klasse "Tijdschrift" af met volgende extra eigenschap:
 *			Verschijningsperiode:  Dagelijks, Wekelijks, Maandelijks (gebruik daarvoor een enumerable)
 *			Voorzie overloads voor de basismethoden
 *	
 *		Om te testen maak je twee boeken en tijdschriften aan (Voor extra punten mag je ook voorzien dat extra boeken/tijdschriften kunnen ingegeven worden in je hoofdmenu)
 *	
 *	Generische klasse: Maak een generische klasse Bestelling<T> waarbij T het type item is dat wordt besteld (in dit geval een boek of een tijdschrijft) met volgende eigenschappen:
 *			Id (uniek volgnummer:  Zie verder)
 *			Item T
 *			Datum (van de bestelling)
 *			Aantal (besteld)
 *			Als van toepassing:  De periode van de bestelling (tijdschriften-abonnement)
 *		alsook de methode Tuple Bestel (zie verder
 *		
 *	Gepersonaliseerde get/set: 
 *			Gebruik de getter-setter om ervoor te zorgen dat de prijs van een boek altijd tussen de 5€ en 50€ is,
 *			Gebruik de getter-setter om een uniek volgnummer toe te kennen aan de bestellings-ID
 *	
 *	Tuple: Wanneer een boek wordt besteld, retourneer een Tuple met de volgende informatie: ISBN van het boek, aantal bestelde exemplaren en totale prijs.
 *	Events: Wanneer een boek is besteld, trigger een event (met een eenvoudige boodschap die de bestelling confirmeert)
 *	
 *	Evaluatie:  Zie de bijgeleverde rubriek
 */

namespace classes_and_events {
	class Program {
		// A stack to hold our inventory of books and magazines
		public static Stack inventoryStack = new();

		static void Main(string[] args) {
			StringBuilder menuBuilder = new();
			Order<Object> itemOrder;
			Object? itemToOrder = null;
			string inputName;
			int inputQuantity;
			bool continueLoop = true;

			// Populate the stack with some books and magazines
			inventoryStack.Push(new Book(
					isbn: 9780131103627,
					name: "The C Programming Language",
					publisher: "Prentice Hall",
					price: 44.99m,
					stock: 500
			));
			inventoryStack.Push(new Book(
					isbn: 2345678987654,
					name: "The woes of a John Doe",
					publisher: "Cathrina Hall",
					price: 34,
					stock: 500
			));
			inventoryStack.Push(new Book(
					isbn: 8765432288734,
					name: "The F Programming Language",
					publisher: "Jason Cawthorne",
					price: 22,
					stock: 400
			));
			inventoryStack.Push(new Magazine(
					isbn: 23478264563,
					name: "The daily mail",
					publisher: "Rick Ashley",
					price: 12,
					stock: 200,
					period: PublicationPeriod.Daily
			));

			// A dictionary to map exception types to their corresponding factory functions
			// To make it less verbose to throw exceptions with custom messages
			Dictionary<string, Func<Exception>> exceptionFactories = new() {
				{ "inputName", () => new ArgumentNullException("inputName", "No book could be found with that name") },
				{ "lackOfStock", () => new ArgumentNullException("lackOfStock", "Not enough stock available") }
			};

			Console.Write(GetMenu("start").ToString());
			WriteReadClear("");


			// If the inventory stack is empty, exit the program early
			if (inventoryStack.Count == 0) {
				Console.WriteLine("No items available to order. Exiting.");
				WriteReadClear(" | ");
			} else {
				// Main loop to get user input and place an order
				// Even if we have a loop inside our GetInput method, we need this loop to retry if the inventoryStack search fails
				while (continueLoop && itemToOrder == null) {
					try {
						// Get the name of the item to order
						inputName = GetInput<string>(GetMenu("book").ToString());

						// Search for an item in our stack that matches the inputted name (case insensitive)
						if (inventoryStack.Count > 1) {
							itemToOrder = inventoryStack
								.ToArray()
								.FirstOrDefault(item =>
									item?.GetType().GetProperty("Name")?.GetValue(item)?.ToString()?
										.Equals(inputName, StringComparison.CurrentCultureIgnoreCase) == true);
						}

						// If no item was found, throw an exception
						_ = itemToOrder ?? throw exceptionFactories["inputName"]();
						Console.Clear();

						break;
					} catch (Exception e) {
						Console.WriteLine($"\nError: {e.Message}");
						WriteReadClear(" | ");
					}
				}

				if (itemToOrder != null) {
					// Get the quantity to order
					inputQuantity = GetInput<int>(GetMenu("quantity").ToString());
					Console.Clear();

					// Create the order
					itemOrder = new() {
						Item = itemToOrder,
						Quantity = inputQuantity,
						DateOrder = DateTime.Now,

						// If there is a Period property, set it. Otherwise, set it to null
						Period = (PublicationPeriod?) itemToOrder.GetType().GetProperty("Period")?.GetValue(itemToOrder)
					};

					// Subscribe to the OrderPlaced event
					itemOrder.OrderPlaced += (sender, args) => Console.WriteLine("Order has been placed successfully.");

					// Place the order
					Type itemType = itemToOrder.GetType();
					long? isbn = (long?) itemType.GetProperty("Isbn")?.GetValue(itemOrder.Item);
					decimal? price = (decimal?) itemType.GetProperty("Price")?.GetValue(itemOrder.Item);
					Tuple<long, int, decimal> orderDetails;

					if (isbn.HasValue && price.HasValue) {
						orderDetails = itemOrder.PlaceOrder(
							isbn: (long) isbn,
							quantity: itemOrder.Quantity,
							price: (decimal) price
						);

						// Get the order details
						Console.WriteLine("Order Details:\n\tISBN: {0};\n\tQuantity: {1};\n\tTotal Price: {2};",
							orderDetails.Item1,
							orderDetails.Item2,
							orderDetails.Item3
						);
					} else {
						// If we couldn't retrieve the ISBN or Price, show an error message
						// Logically this should never happen due to our earlier check for itemToOrder being not null
						Console.WriteLine("Error: Could not retrieve item details. How did you get here?");
					}
					WriteReadClear("");
				}
			}
		}

		// Function to generate different predefined menus based on the option provided
		public static StringBuilder GetMenu(string option) {
			StringBuilder menuBuilder = new();
			switch (option) {
				case "start":
					menuBuilder.Append(
						"\n\n\n-= Books and Magazines Order System. = -\n\n\n"
					);
					break;
				case "book":
					menuBuilder.Append(
						"To place a book order we'll need some info." +
						"\nPlease give us:" +
						"\n\t1. The name of the book (case insensitive);" +
						"\n\t2. The quantity of the book you wish to order;" +

						"\n\nAvailable books:\n"
					);

					foreach (Object item in inventoryStack) {
						string? nameInfo = (string?) item.GetType()?.GetProperty("Name")?.GetValue(item);
						if (nameInfo != null)
							menuBuilder.AppendLine($"\t{nameInfo}");
					}

					menuBuilder.Append("\nName of the book: ");
					break;
				case "quantity":
					menuBuilder.Append(
						"To place a book order we'll need some info." +
						"\nPlease give us:" +
						"\n\t1. The name of the book (case insensitive);" +
						"\n\t2. The quantity of the book you wish to order;" +
						"\nQuantity: "
					);
					break;
				default:
					break;
			}
			return menuBuilder;
		}
	}
}
