using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classes_and_events {
	internal class Magazine(long isbn, string name, string publisher, decimal price, int stock, PublicationPeriod period) : Book(isbn, name, publisher, price, stock) {
		public PublicationPeriod Period { get; set; } = period;

		public override string ToString() {
			return base.ToString() + $", Period: {Period}";
		}
	}
}
