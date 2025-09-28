using MS.RoadFire.Common.Resource;
using System.Text.RegularExpressions;

namespace MS.RoadFire.Common.Helpers
{
    public static class EmailValidator
    {
        public static (bool, string) IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return (Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase), MessagesResource.EmailInvalid);
        }
    }
}
