using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryShoppingCartService
{
    public class CrediCardData
    {
        public long CrediCardDataId { set; get; }
        public string RequestXML { set; get; }
        public string ResponseXML { set; get; }
        public string StatusNameDesc { set; get; }
        public string AddUserId { set; get; }
        public string AddUserName { set; get; }
        public string AddDateTime { set; get; }
        public string UpdUserId { set; get; }
        public string UpdUserName { set; get; }
        public string UpdDateTime { set; get; }
    }
}
