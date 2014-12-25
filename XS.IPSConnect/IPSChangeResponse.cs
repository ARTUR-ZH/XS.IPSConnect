using Newtonsoft.Json;

namespace XS.IPSConnect
{
    public class IPSChangeResponse
    {
        /// <summary>
        /// Returns one of the following statuses:
        /// "BAD_KEY" - the key provided was invalid
        /// "NO_USER" - the ID number did not match any account
        /// "SUCCESS" - The information was changed successfully
        /// "EMAIL_IN_USE" - the email address provided is in use
        /// "USERNAME_IN_USE" - the username provided is in use
        /// "DISPLAYNAME_IN_USE" - the display name provided is in use
        /// "MISSING_DATA" - you did not provide anything that was necessary to update
        /// </summary>
        public string status;

        /// <summary>
        /// Returns the response as a Json string.
        /// </summary>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
