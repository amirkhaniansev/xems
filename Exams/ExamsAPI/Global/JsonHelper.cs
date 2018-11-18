using System;
using System.Text;

namespace ExamsAPI.Global
{
    public static class JsonHelper
    {
        public static string ConvertQueryToJson(string queryString)
        {
            if (string.IsNullOrEmpty(queryString))
                return string.Empty;

            var sb = new StringBuilder();

            sb.Append("{\"");

            for (var counter = 1; counter < queryString.Length; counter++)
            {
                if (queryString[counter] == '=')
                    sb.Append("\":\"");
                else if (queryString[counter] == '&')
                    sb.Append("\",\"");
                else sb.Append(queryString[counter]);                
            }

            sb.Append("\"}");

            return sb.ToString();
        }
    }
}