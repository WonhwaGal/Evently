using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Common.Domain;

namespace Evently.Modules.Ticketing.Domain.Cart;

	public static class CartErrors
	{
        public static readonly Error Empty = Error.Problem("Carts.Empty", "The cart is empty");
	}
