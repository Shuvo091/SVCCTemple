using ArchitectureLibraryCreditCardService;
using ArchitectureLibraryException;
using ArchitectureLibraryUtility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryShoppingCartService
{
    public class ShoppingCartBL
    {
        public bool ProcessShoppingCart(ShoppingCart shoppingCart, CreditCardDataBO creditCardDataBO, Dictionary<string, string> creditCardKVPs, string execUniqueId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlConnection sqlConnection = null;
            try
            {
                string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
                sqlConnection = new SqlConnection(databaseConnectionString);
                sqlConnection.Open();
                long shoppingCartId = ShoppingCartInsertRow(0, shoppingCart.EmailAddress, shoppingCart.FirstName, shoppingCart.LastName, shoppingCart.PrimaryTelephoneNum, (float)shoppingCart.ShoppingCartAmount, shoppingCart.ShoppingCartComments, sqlConnection, execUniqueId);
                shoppingCart.ShoppingCartId = shoppingCartId;
                ShoppingCartDataInsertRows(shoppingCartId, shoppingCart.ShoppingCartItems, sqlConnection, execUniqueId);
                bool returnValue = ProcessCreditCard(creditCardDataBO, creditCardKVPs, execUniqueId);
                ShoppingCartUpdateRow(shoppingCartId, (long)creditCardDataBO.CreditCardDataId, sqlConnection, execUniqueId);
                return returnValue;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            finally
            {
                try
                {
                    sqlConnection.Close();
                }
                catch
                {
                    ;
                }
            }
        }
        public bool ProcessShoppingCart(ShoppingCart shoppingCart, string execUniqueId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlConnection sqlConnection = null;
            try
            {
                string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
                sqlConnection = new SqlConnection(databaseConnectionString);
                sqlConnection.Open();
                long shoppingCartId = ShoppingCartInsertRow(0, shoppingCart.EmailAddress, shoppingCart.FirstName, shoppingCart.LastName, shoppingCart.PrimaryTelephoneNum, (float)shoppingCart.ShoppingCartAmount, shoppingCart.ShoppingCartComments, sqlConnection, execUniqueId);
                ShoppingCartDataInsertRows(shoppingCartId, shoppingCart.ShoppingCartItems, sqlConnection, execUniqueId);
                return true;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            finally
            {
                try
                {
                    sqlConnection.Close();
                }
                catch
                {
                    ;
                }
            }
        }
        public bool ProcessCreditCard(CreditCardDataBO creditCardDataBO, Dictionary<string, string> creditCardKVPs, string execUniqueId)
        {
            long creditCardDataId;
            string creditCardProcessMessage;
            bool returnValue = ProcessCreditCard(creditCardDataBO.CreditCardAmount, creditCardDataBO.CreditCardCVV, creditCardDataBO.CreditCardExpiryMM, creditCardDataBO.CreditCardExpiryYYYY, creditCardDataBO.CreditCardHolderName, creditCardDataBO.CreditCardNumber, creditCardDataBO.Currency, creditCardKVPs, execUniqueId, out creditCardDataId, out creditCardProcessMessage);
            creditCardDataBO.CreditCardDataId = creditCardDataId;
            creditCardDataBO.CreditCardProcessMessage = creditCardProcessMessage;
            return returnValue;
        }
        public bool ProcessCreditCard(string creditCardAmount, string creditCardCVV, string creditCardExpiryMM, string creditCardExpiryYYYY, string creditCardHolderName, string creditCardNumber, string currency, Dictionary<string, string> creditCardKVPs, string execUniqueId, out long creditCardDataId, out string creditCardProcessMessage)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            string terminalType = "2", transactionType = "7";
            string creditCardType;
            string cardExpiry = creditCardExpiryMM + creditCardExpiryYYYY.Substring(2);
            switch (creditCardNumber.Substring(0, 2))
            {
                case "37":
                    creditCardType = "AMEX";
                    break;
                case "38":
                    creditCardType = "DINERS";
                    break;
                default:
                    switch (creditCardNumber.Substring(0, 1))
                    {
                        case "4":
                            creditCardType = "VISA";
                            break;
                        case "5":
                            creditCardType = "MASTERCARD";
                            break;
                        case "6":
                            creditCardType = "DISCOVER";
                            break;
                        default:
                            creditCardType = "UNKNOWN";
                            break;
                    }
                    break;
            }
            return ProcessCreditCard(currency, creditCardAmount, creditCardNumber, creditCardType, cardExpiry, creditCardHolderName, creditCardCVV, terminalType, transactionType, creditCardKVPs, execUniqueId, out creditCardDataId, out creditCardProcessMessage);
        }
        private bool ProcessCreditCard(string currency, string amount, string cardNumber, string cardType, string cardExpiry, string cardHolderName, string cVV, string terminalType, string transactionType, Dictionary<string, string> creditCardKVPs, string execUniqueId, out long creditCardDataId, out string processMessage)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string restAPIRootUri, requestUri, terminalId, sharedSecret, cardNumberLast4, privateKey;
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Before getting values from Config");
                //privateKey
                try
                {
                    privateKey = creditCardKVPs["PrivateKey"];
                }
                catch
                {
                    privateKey = Utilities.GetApplicationValue("PrivateKey");
                }
                //"https://testpayments.nuvei.com/";
                try
                {
                    restAPIRootUri = creditCardKVPs["NuveiRestAPIRootUri"];
                }
                catch
                {
                    restAPIRootUri = Utilities.GetApplicationValue("NuveiRestAPIRootUri"); 
                }
                //"merchant/xmlpayment";
                try
                {
                    requestUri = creditCardKVPs["NuveiRequestUri"];
                }
                catch
                {
                    requestUri = Utilities.GetApplicationValue("NuveiRequestUri"); 
                }
                //"1064144";
                try
                {
                    terminalId = creditCardKVPs["NuveiTerminalId"];
                }
                catch
                {
                    terminalId = Utilities.GetApplicationValue("NuveiTerminalId"); 
                }
                //"123456789G1";
                try
                {
                    sharedSecret = creditCardKVPs["NuveiSharedSecret"];
                }
                catch
                {
                    sharedSecret = Utilities.GetApplicationValue("NuveiSharedSecret"); 
                }
                terminalId = EncryptDecrypt.DecryptDataMd5(terminalId, privateKey);
                sharedSecret = EncryptDecrypt.DecryptDataMd5(sharedSecret, privateKey);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: After getting values from Config");
                CreditCardNuvei creditCardNuvei = new CreditCardNuvei();
                cardNumberLast4 = cardNumber.Substring(cardNumber.Length - 4);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003000 :: Before CreditCardNuvei/ProcessCreditCard", "amount", amount, "cardHolderName", cardHolderName, "cardNumberLast4", cardNumberLast4);
                var returnValue = creditCardNuvei.ProcessCreditCard(restAPIRootUri, requestUri, terminalId, sharedSecret, terminalType, transactionType, currency, amount, cardHolderName, cardNumber, cardNumberLast4, cVV, cardType, cardExpiry, execUniqueId, out processMessage, out string requestXML, out string requestXML1, out string responseXML);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00004000 :: After CreditCardNuvei/ProcessCreditCard", "amount", amount, "cardHolderName", cardHolderName, "cardNumberLast4", cardNumberLast4, "returnValue", returnValue.ToString(), "processMessage", processMessage, "responseXML", responseXML);
                string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
                SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
                sqlConnection.Open();
                creditCardDataId = CreditCardDataInsRow(requestXML1, responseXML, returnValue ? "SUCCESS" : "FAILED", sqlConnection);
                sqlConnection.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return returnValue;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        private long CreditCardDataInsRow(string requestData, string responseData, string statusNameDesc, SqlConnection sqlConnection)
        {
            SqlCommand sqlCommand = new SqlCommand("INSERT ArchLib.CreditCardData(RequestData, ResponseData, StatusNameDesc) OUTPUT INSERTED.CreditCardDataId SELECT @RequestData, @ResponseData, @StatusNameDesc", sqlConnection);
            sqlCommand.Parameters.Add("@RequestData", SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@ResponseData", SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@StatusNameDesc", SqlDbType.VarChar, 50);
            sqlCommand.Parameters["@RequestData"].Value = requestData;
            sqlCommand.Parameters["@ResponseData"].Value = responseData;
            sqlCommand.Parameters["@StatusNameDesc"].Value = statusNameDesc;
            sqlCommand.CommandType = CommandType.Text;

            long creditCardDataId = Convert.ToInt64(sqlCommand.ExecuteScalar());

            return creditCardDataId;
        }

        private long ShoppingCartInsertRow(long creditCardDataId, string emailAddress, string firstName, string lastName, string primaryTelephoneNum, float shoppingCartAmount, string shoppingCartComments, SqlConnection sqlConnection, string execUniqueId = "")
        {
            string sqlStmt = "";
            sqlStmt += "INSERT ArchLib.ShoppingCart" + Environment.NewLine;
            sqlStmt += "      (" + Environment.NewLine;
            sqlStmt += "       CreditCardDataId" + Environment.NewLine;
            sqlStmt += "      ,EmailAddress" + Environment.NewLine;
            sqlStmt += "      ,FirstName" + Environment.NewLine;
            sqlStmt += "      ,LastName" + Environment.NewLine;
            sqlStmt += "      ,PrimaryTelephoneNum" + Environment.NewLine;
            sqlStmt += "      ,ShoppingCartAmount" + Environment.NewLine;
            sqlStmt += "      ,ShoppingCartComments" + Environment.NewLine;
            sqlStmt += "      )" + Environment.NewLine;
            sqlStmt += "OUTPUT INSERTED.ShoppingCartId" + Environment.NewLine;
            sqlStmt += "SELECT " + Environment.NewLine;
            sqlStmt += "       @CreditCardDataId" + Environment.NewLine;
            sqlStmt += "      ,@EmailAddress" + Environment.NewLine;
            sqlStmt += "      ,@FirstName" + Environment.NewLine;
            sqlStmt += "      ,@LastName" + Environment.NewLine;
            sqlStmt += "      ,@PrimaryTelephoneNum" + Environment.NewLine;
            sqlStmt += "      ,@ShoppingCartAmount" + Environment.NewLine;
            sqlStmt += "      ,@ShoppingCartComments" + Environment.NewLine;

            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ShoppingCartId", SqlDbType.BigInt);
            sqlCommand.Parameters["@ShoppingCartId"].Direction = ParameterDirection.ReturnValue;
            sqlCommand.Parameters.Add("@CreditCardDataId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 512);
            sqlCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar, 100);
            sqlCommand.Parameters.Add("@LastName", SqlDbType.NVarChar, 100);
            sqlCommand.Parameters.Add("@PrimaryTelephoneNum", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@ShoppingCartAmount", SqlDbType.Decimal);
            sqlCommand.Parameters["@ShoppingCartAmount"].Precision = 18;
            sqlCommand.Parameters["@ShoppingCartAmount"].Scale = 4;
            sqlCommand.Parameters.Add("@ShoppingCartComments", SqlDbType.NVarChar, 2048);

            sqlCommand.Parameters["@CreditCardDataId"].Value = creditCardDataId;
            sqlCommand.Parameters["@EmailAddress"].Value = emailAddress;
            sqlCommand.Parameters["@FirstName"].Value = firstName == null ? "" : firstName;
            sqlCommand.Parameters["@LastName"].Value = lastName == null ? "" : lastName;
            sqlCommand.Parameters["@PrimaryTelephoneNum"].Value = primaryTelephoneNum == null ? "0" : primaryTelephoneNum;
            sqlCommand.Parameters["@ShoppingCartAmount"].Value = shoppingCartAmount;
            sqlCommand.Parameters["@ShoppingCartComments"].Value = shoppingCartComments == null ? "" : shoppingCartComments;
            sqlCommand.CommandType = CommandType.Text;

            long shoppingCartId = Convert.ToInt64(sqlCommand.ExecuteScalar());

            return shoppingCartId;
        }
        private void ShoppingCartDataInsertRows(long shoppingCartId, List<ShoppingCartItem> shoppingCartItems, SqlConnection sqlConnection, string execUniqueId)
        {
            SqlCommand sqlCommand = new SqlCommand("INSERT ArchLib.ShoppingCartItem(ItemDesc, ItemId, ItemRate, OrderAmount, OrderComments, OrderQty, RateTypeNameDesc, SeqNum, ShoppingCartId) SELECT @ItemDesc, @ItemId, @ItemRate, @OrderAmount, @OrderComments, @OrderQty, @RateTypeNameDesc, @SeqNum, @ShoppingCartId", sqlConnection);
            sqlCommand.Parameters.Add("@ItemDesc", SqlDbType.NVarChar, 1024);
            sqlCommand.Parameters.Add("@ItemId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@ItemRate", SqlDbType.Decimal);
            sqlCommand.Parameters["@ItemRate"].Precision = 18;
            sqlCommand.Parameters["@ItemRate"].Scale = 4;
            sqlCommand.Parameters.Add("@OrderAmount", SqlDbType.Decimal);
            sqlCommand.Parameters["@OrderAmount"].Precision = 18;
            sqlCommand.Parameters["@OrderAmount"].Scale = 4;
            sqlCommand.Parameters.Add("@OrderComments", SqlDbType.NVarChar, 1024);
            sqlCommand.Parameters.Add("@OrderQty", SqlDbType.SmallInt);
            sqlCommand.Parameters.Add("@RateTypeNameDesc", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@SeqNum", SqlDbType.Float);
            sqlCommand.Parameters.Add("@ShoppingCartId", SqlDbType.BigInt);
            sqlCommand.Parameters["@ShoppingCartId"].Value = shoppingCartId;
            foreach (var shoppingCartItem in shoppingCartItems)
            {
                sqlCommand.Parameters["@ItemDesc"].Value = shoppingCartItem.ItemDesc;
                sqlCommand.Parameters["@ItemId"].Value = shoppingCartItem.ItemId;
                sqlCommand.Parameters["@ItemRate"].Value = shoppingCartItem.ItemRate;
                sqlCommand.Parameters["@OrderAmount"].Value = shoppingCartItem.OrderAmount;
                sqlCommand.Parameters["@OrderComments"].Value = shoppingCartItem.OrderComments;
                sqlCommand.Parameters["@OrderQty"].Value = shoppingCartItem.OrderQty;
                sqlCommand.Parameters["@RateTypeNameDesc"].Value = "PURCHASE";
                sqlCommand.Parameters["@SeqNum"].Value = shoppingCartItem.SeqNum;
                sqlCommand.ExecuteNonQuery();
            }
        }
        private bool ShoppingCartUpdateRow(long shoppingCartId, long creditCardDataId, SqlConnection sqlConnection, string execUniqueId)
        {
            string sqlStmt = "";
            sqlStmt += "UPDATE ArchLib.ShoppingCart SET CreditCardDataId = " + creditCardDataId + " WHERE ShoppingCartId = " + shoppingCartId + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ShoppingCartId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@CreditCardDataId", SqlDbType.BigInt);
            sqlCommand.Parameters["@ShoppingCartId"].Value = shoppingCartId;
            sqlCommand.Parameters["@CreditCardDataId"].Value = creditCardDataId;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.ExecuteNonQuery();
            return true;
        }
        //public bool ProcessCreditCardPayment(string creditCardCVV, string NameonCard, string CreditCardNumber, string CreditCardExpiryMonth,string CreditCardExpiryYear, string execUniqueId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        var returnValue = "";
        //        return returnValue;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
    }
}
