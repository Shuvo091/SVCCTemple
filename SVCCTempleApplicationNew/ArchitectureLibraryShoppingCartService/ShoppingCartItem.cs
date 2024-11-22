using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryShoppingCartService
{
    public class ShoppingCartItem
    {
        public long ShoppingCartItemId { set; get; }
        public string ItemDesc { set; get; }
        public long ItemId { set; get; }
        public float? ItemRate { set; get; }
        public float? OrderAmount { set; get; }
        public string OrderComments { set; get; }
        public int? OrderQty { set; get; }
        public string RateTypeNameDesc { set; get; }
        public short SeqNum { set; get; }
        public long ShoppingCartId { set; get; }
        public string AddUserId { set; get; }
        public string AddUserName { set; get; }
        public string AddDateTime { set; get; }
        public string UpdUserId { set; get; }
        public string UpdUserName { set; get; }
        public string UpdDateTime { set; get; }
        public ShoppingCart ShoppingCart { set; get; }
    }
}
