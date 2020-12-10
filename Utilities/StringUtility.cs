
namespace Highbrow.HiPower.Utilities
{
    public static class StringUtility
    {
        public static string GetTrimmedLowerCase(string stringToFormat)
        {
            string formattedString = GetTrimmed(stringToFormat);            
            return formattedString.ToLower();
        }

        public static string GetTrimmedUpperCase(string stringToFormat)
        {
            string formattedString = GetTrimmed(stringToFormat);
            return formattedString.ToUpper();
        }

        public static string GetTrimmed(string stringToFormat)
        {
            string formattedString = "";

            if (!string.IsNullOrEmpty(stringToFormat))            
                formattedString = stringToFormat.Trim();
            
            return formattedString;
        }
    }
}