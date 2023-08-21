using System.Text.RegularExpressions;

namespace ABPFramworkProject
{
    public class PhoneNumberValidator
    {
        public bool IsValid(string phoneNumber)
        {
            Regex phoneNumberExpression = new Regex(@"^([0]|\+91)?[789]\d{9}$");
            return phoneNumberExpression.IsMatch(phoneNumber);
        }
    }
}
