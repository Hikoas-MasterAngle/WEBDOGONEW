using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WEBDOGO.Models
{
	public class xss
	{
        public static string chongxss(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            string output = Regex.Replace(input, "<script.*?>.*?</script>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);

            output = Regex.Replace(output, "<.*?>", "");

            output = HttpUtility.HtmlEncode(output);

            return output;
        }
    }
}