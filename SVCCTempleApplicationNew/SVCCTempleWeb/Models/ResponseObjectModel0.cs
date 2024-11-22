using SVCCTempleWeb.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class ResponseObjectModel0
    {
        private HttpStatusCode statusCode_;
        private bool isSuccessStatusCode_;
        public HttpStatusCode StatusCode
        {
            set
            {
                statusCode_ = value;
                if (StatusCode == HttpStatusCode.OK)
                    isSuccessStatusCode_ = true;
                else
                    isSuccessStatusCode_ = false;
            }
            get { return statusCode_; }
        }
        //public string ResponseTitle { set; get; }
        //public string ResponseDescription { set; get; }
        public List<KeyValuePair<string, string>> ResponseMessages { set; get; }       
        public bool IsSuccessStatusCode { get { return isSuccessStatusCode_; } }
        public string ListStyleType { set; get; }
        public int ColumnCount { get; set; }
        public ResponseTypeEnum? ResponseTypeId { set; get; }
        public string TextAlign { set; get; }
        public string TextColor { set; get; }
        public string ValidationSummaryMessage { set; get; }
    }
}
