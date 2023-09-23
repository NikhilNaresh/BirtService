using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace BirtService
{
    public class BirtReportService : IBirtReportService
    {

        /// <inheritdoc />
        public string GetBirtReport(string birtServiceUrl, string reportName, string format, string xmlData,
            string additionalInfo)
        {
            try
            {
                //LogMessage("BirtReportService.GetBirtReport : " + reportName);

                var completeXmlData = string.Format("&__report={0}&__format={1}{2}&datasourceXML={3}", reportName,
                    format, additionalInfo ?? string.Empty, HttpUtility.UrlEncode(xmlData));

                var request = WebRequest.Create(birtServiceUrl);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                using (var dataStream = request.GetRequestStream())
                {
                    // Get the request stream.  
                    using (var stOut = new StreamWriter(dataStream, Encoding.ASCII))
                    {
                        stOut.Write(completeXmlData);
                        stOut.Close();
                    }
                }

                using (WebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    var bytes = ReadFullStream(response.GetResponseStream());
                    var message = Convert.ToBase64String(bytes);
                    return message;
                }
            }
            catch (Exception ex)
            {
                //LogMessage("BirtReportService.GetBirtReport : ", EventType.Error, ex);
                return null;
            }
        }

        private byte[] ReadFullStream(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }
    }
}
