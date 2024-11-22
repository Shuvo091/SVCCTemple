using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace SVCCTempleWeb.ClassCode
{
    public class QRCode
    {
        public void GenerateQRCode(string qRCodeText, string qrCodeFileName, string qRCodeWidth, string qRCodeHeight)
        {
            try
            {
                var url = string.Format("http://chart.apis.google.com/chart?cht=qr&chs={1}x{2}&chl={0}", qRCodeText, qRCodeWidth, qRCodeHeight);
                WebResponse response = default(WebResponse);
                Stream remoteStream = default(Stream);
                StreamReader readStream = default(StreamReader);
                WebRequest request = WebRequest.Create(url);
                response = request.GetResponse();
                remoteStream = response.GetResponseStream();
                readStream = new StreamReader(remoteStream);
                System.Drawing.Image img = System.Drawing.Image.FromStream(remoteStream);
                string fullFileName = HttpContext.Current.Server.MapPath("~/QRCode/") + qrCodeFileName;
                img.Save(fullFileName);
                response.Close();
                remoteStream.Close();
                readStream.Close();
                //txtCode.Text = string.Empty;
                //txtWidth.Text = string.Empty;
                //txtHeight.Text = string.Empty;
                //lblMsg.Text = "The QR Code generated successfully";
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

        }
    }
}
