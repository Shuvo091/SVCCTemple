using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryShoppingCartService
{
    public class CreditCardDataBO
    {
        public long? CreditCardDataId { set; get; }
        public string CreditCardAmount { set; get; }
        public string CreditCardCVV { set; get; }
        public string CreditCardExpiry { set; get; }
        public string CreditCardExpiryMM { set; get; }
        public string CreditCardExpiryYYYY { set; get; }
        public string CreditCardHolderName { set; get; }
        public string CreditCardNumber { set; get; }
        public string Currency { set; get; }
        public string CreditCardProcessMessage { set; get; }
    }
}
