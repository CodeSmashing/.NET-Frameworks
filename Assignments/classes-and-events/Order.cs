using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* TODO
Allow the user to create an order that contains multiple publications (both books and magazines) instead of just a single item */

namespace classes_and_events {
	internal class Order<T> {
		public delegate void OrderPlacedEventHandler(object source, EventArgs args);
		public event OrderPlacedEventHandler? OrderPlaced = null;

		public static int Id { get; private set; } = 0;

		public T Item { get; set; } = default!; // Non-nullable but not initialized in constructor

		public DateTime DateOrder { get; set; } = DateTime.Now;

		public int Quantity { get; set; }

		public PublicationPeriod? Period { get; set; } = null;

		public Order() { }

		public Order(T item, DateTime dateOrder, int quantity, PublicationPeriod? period) {
			Id += 1;
			Item = item;
			DateOrder = dateOrder;
			Quantity = quantity;
			Period = period;
		}

		public Tuple<long, int, decimal> PlaceOrder(long isbn, int quantity, decimal price) {
			decimal totalPrice = quantity * price;

			OrderPlaced?.Invoke(this, EventArgs.Empty); // Null-safe invocation

			return Tuple.Create(isbn, quantity, totalPrice);
		}
	}
}
