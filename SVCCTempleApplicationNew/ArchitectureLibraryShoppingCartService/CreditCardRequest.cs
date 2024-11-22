using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryShoppingCartService
{
    public class CreditCardRequest
    {
        public float? CreditCardAmount { set; get; }
        public string CreditCardCVV { set; get; }
        public string CreditCardExpiryMM { set; get; }
        public string CreditCardExpiryYYYY { set; get; }
        public string CreditCardHolderName { set; get; }
        public string CreditCardNumber { set; get; }
        public string CreditCardNumberLast4 { set; get; }
        public string CreditCardType { set; get; }
        public string Currency { set; get; }
        public string TerminalType { set; get; }
        public string TransactionType { set; get; }
    }
}
