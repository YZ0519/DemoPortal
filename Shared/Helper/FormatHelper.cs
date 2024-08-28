using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helper
{
    public class FormatHelper
    {
        public static string FormatTimeSpan(TimeSpan duration)
        {
            int totalDays = (int)duration.TotalDays;

            int years = totalDays / 365;
            int remainingDays = totalDays % 365;

            int months = remainingDays / 30;
            int days = remainingDays % 30;

            string formattedDuration = string.Empty;
            // Construct the formatted string
            if (years > 0)
            {
                formattedDuration = $"{years} years ";
            }
            if (months > 0)
            {
                formattedDuration += $"{months} months ";
            }
            if (days > 0)
            {
                formattedDuration += $"{days} days ";
            }
            
            return formattedDuration;
        }
    }
}
