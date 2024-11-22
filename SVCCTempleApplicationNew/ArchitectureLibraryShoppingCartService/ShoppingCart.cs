using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryShoppingCartService
{
    public class ShoppingCart
    {
        public long ShoppingCartId { set; get; }
        public long CreditCardDataId { set; get; }
        public CreditCardRequest CreditCardRequest { set; get; }
        public string EmailAddress { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string PrimaryTelephoneNum { set; get; }
        public float? ShoppingCartAmount { set; get; }
        public string ShoppingCartComments { set; get; }
        public string AddUserId { set; get; }
        public string AddUserName { set; get; }
        public string AddDateTime { set; get; }
        public string UpdUserId { set; get; }
        public string UpdUserName { set; get; }
        public string UpdDateTime { set; get; }
        public List<ShoppingCartItem> ShoppingCartItems { set; get; }
        public CrediCardData CrediCardData { set; get; }
    }
}
