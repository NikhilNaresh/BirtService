using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirtService
{
    public interface IBirtReportService
    {
        /// <summary>
        /// Returns the Base64 string for the Report 
        /// </summary>
        string GetBirtReport(string birtServiceUrl, string reportName, string format, string xmlData,
            string additionalInfo = null);

    }
}
