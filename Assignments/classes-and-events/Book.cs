using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classes_and_events {
	internal class Book(long isbn, string name, string publisher, decimal price, int stock) {
		private decimal price = price;

		public long Isbn { get; set; } = isbn;

		public string Name { get; set; } = name;

		public string Publisher { get; set; } = publisher;

		public decimal Price {
			get { return this.price; }
			set {
				if (value < 5 || value > 50) {
					throw new ArgumentOutOfRangeException("Price must be between 5 and 50.");
				}
				this.price = value;
			}
		}

		public int Stock { get; set; } = stock;

		// No clue what to do with this method
		public void Read() {
		}

		public override string ToString() {
			return $"ISBN: {Isbn}, Name: {Name}, Publisher: {Publisher}, Price: {Price:C}";
		}
	}
}
