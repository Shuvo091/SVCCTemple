using ArchitectureLibraryException;
using ArchitectureLibraryUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ArchitectureLibraryCreditCardService
{
    public class CreditCardNuvei
    {
        public bool ProcessCreditCard(string restAPIRootUri, string requestUri, string terminalId, string sharedSecret, string terminalType, string transactionType, string currency, string amount, string cardHolderName, string cardNumber, string cardNumberLast4, string cVV, string cardType, string cardExpiry, string execUniqueId, out string processMessage, out string requestXML, out string requestXML1, out string responseXML)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                var returnValue = true;
                string reasonPhrase;
                string orderId = GenerateOrderId();
                string dateTime = DateTime.Now.ToString("dd-MM-yyyy:HH:mm:ss:fff");
                string hash = GenerateHash(terminalId, orderId, amount, dateTime, sharedSecret);
                requestXML = GenerateXML(orderId, terminalId, amount, dateTime, cardNumber, cardType, cardExpiry, cardHolderName, hash, currency, terminalType, transactionType, cVV);
                requestXML1 = GenerateXML(orderId, terminalId, amount, dateTime, cardNumberLast4, cardType, cardExpiry, cardHolderName, hash, currency, terminalType, transactionType, cVV);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Before Web API", "amount", amount, "cardHolderName", cardHolderName, "cardNumberLast4", cardNumberLast4);

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                //HttpResponseMessage httpResponseMessage = CallRESTServicePost(restAPIRootUri, requestUri, "", execUniqueId, null, null, "text/xml; charset=utf-8", requestXML);
                HttpResponseMessage httpResponseMessage = CallRESTServicePost(restAPIRootUri, requestUri, execUniqueId: execUniqueId, contentType: "text/xml; charset=utf-8", contentData: requestXML);
                reasonPhrase = httpResponseMessage.ReasonPhrase;
                responseXML = httpResponseMessage.Content.ReadAsStringAsync().Result;
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: After Web API", "amount", amount, "cardHolderName", cardHolderName, "cardNumberLast4", cardNumberLast4, "StatusCode", httpResponseMessage.StatusCode.ToString(), "reasonPhrase", reasonPhrase, "responseXML", responseXML);

                XmlDocument responseDataXmlDoc = new XmlDocument();
                responseDataXmlDoc.LoadXml(responseXML);

                XmlNode nodeToFind;
                XmlElement root = responseDataXmlDoc.DocumentElement;

                // Selects all the title elements that have an attribute named lang
                nodeToFind = root.SelectSingleNode("//PAYMENTRESPONSE");
                string prefixString = "";
                if (nodeToFind != null)
                {
                    returnValue = true;
                    processMessage = "";
                    nodeToFind = root.SelectSingleNode("//PAYMENTRESPONSE/UNIQUEREF");
                    if (nodeToFind != null)
                    {
                        processMessage = nodeToFind.InnerXml;
                    }
                    nodeToFind = root.SelectSingleNode("//PAYMENTRESPONSE/BANKRESPONSECODE");
                    if (nodeToFind != null)
                    {
                        if (nodeToFind.InnerXml == "00")
                        {
                            ;
                        }
                        else
                        {
                            returnValue = false;
                            nodeToFind = root.SelectSingleNode("//PAYMENTRESPONSE/RESPONSETEXT");
                            processMessage += Environment.NewLine + nodeToFind.InnerXml;
                        }
                    }
                }
                else
                {
                    returnValue = false;
                    nodeToFind = root.SelectSingleNode("//ERROR");
                    processMessage = "";
                    foreach (XmlNode xmlNode in nodeToFind)
                    {
                        processMessage += prefixString + xmlNode.InnerXml;
                        prefixString = Environment.NewLine;
                    }
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: ProcessResult", "returnValue", returnValue.ToString(), "amount", amount, "cardHolderName", cardHolderName, "cardNumberLast4", cardNumberLast4, "StatusCode", httpResponseMessage.StatusCode.ToString(), "reasonPhrase", reasonPhrase, "responseXML", responseXML);

                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return returnValue;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        private string GenerateOrderId()
        {
            string orderId;


            orderId = DateTime.Now.ToString("yyyyMMddHHmmssffff");

            return orderId;
        }
        private string GenerateHash(string terminalId, string orderId, string amount, string dateTime, string sharedSecret)
        {
            string plainString;
            plainString = terminalId + orderId + amount + dateTime + sharedSecret;
            byte[] toBeHashedByteArray = Encoding.UTF8.GetBytes(plainString);
            MD5CryptoServiceProvider cryptHandler = new MD5CryptoServiceProvider();
            byte[] hashedByteArray = cryptHandler.ComputeHash(toBeHashedByteArray);
            string hashedString = "";
            foreach (byte hashedByte in hashedByteArray)
            {
                if (hashedByte < 16)
                {
                    hashedString += "0" + hashedByte.ToString("x");
                }
                else
                {
                    hashedString += hashedByte.ToString("x");
                }
            }
            return hashedString;
        }
        private string GenerateXML(string orderId, string terminalId, string amount, string dateTime, string cardNumber, string cardType, string cardExpiry, string cardHolderName, string hash, string currency, string terminalType, string transactionType, string cVV)
        {
            string xmlData = "";

            xmlData += "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine;
            xmlData += "<PAYMENT>" + Environment.NewLine;
            xmlData += "    <ORDERID>" + orderId + "</ORDERID>" + Environment.NewLine;
            xmlData += "    <TERMINALID>" + terminalId + "</TERMINALID>" + Environment.NewLine;
            xmlData += "    <AMOUNT>" + amount + "</AMOUNT>" + Environment.NewLine;
            xmlData += "    <DATETIME>" + dateTime + "</DATETIME>" + Environment.NewLine;
            xmlData += "    <CARDNUMBER>" + cardNumber + "</CARDNUMBER>" + Environment.NewLine;
            xmlData += "    <CARDTYPE>" + cardType + "</CARDTYPE>" + Environment.NewLine;
            xmlData += "    <CARDEXPIRY>" + cardExpiry + "</CARDEXPIRY>" + Environment.NewLine;
            xmlData += "    <CARDHOLDERNAME>" + cardHolderName + "</CARDHOLDERNAME>" + Environment.NewLine;
            xmlData += "    <HASH>" + hash + "</HASH>" + Environment.NewLine;
            xmlData += "    <CURRENCY>" + currency + "</CURRENCY>" + Environment.NewLine;
            xmlData += "    <TERMINALTYPE>" + terminalType + "</TERMINALTYPE>" + Environment.NewLine;
            xmlData += "    <TRANSACTIONTYPE>" + transactionType + "</TRANSACTIONTYPE>" + Environment.NewLine;
            xmlData += "    <CVV>" + cVV + "</CVV>" + Environment.NewLine;
            xmlData += "</PAYMENT>" + Environment.NewLine;

            return xmlData;
        }
        private HttpResponseMessage CallRESTServicePost(string restAPIRootUri, string requestUri, string queryString = "", string execUniqueId = "", string authorizationKey = null, string authorizationValue = null, string contentType = null, string contentData = null)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string webAPIURL = restAPIRootUri + requestUri + queryString;
                HttpContent content = null;
                HttpResponseMessage httpResponseMessage;
                if (contentData != null && contentData != "")
                {
                    content = new StringContent(contentData);
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", contentType);
                }
                using (HttpClient client = new HttpClient())
                {
                    if (authorizationKey != null && authorizationValue != null)
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authorizationKey, authorizationValue);
                    }
                    httpResponseMessage = client.PostAsync(webAPIURL, content).Result;
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return httpResponseMessage;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
    }
}
/*
https://www.marketwatch.com/story/how-to-decode-your-credit-card-numbers-2017-08-25
*/
