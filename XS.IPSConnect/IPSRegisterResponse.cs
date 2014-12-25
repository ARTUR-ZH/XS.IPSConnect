using Newtonsoft.Json;

namespace XS.IPSConnect
{
    public class IPSRegisterResponse
    {
        /// <summary>
        /// Returns one of the following statuses:
        /// SUCCESS - The registration was successful.
        /// BAD_KEY - The key was incorrect.
        /// EMAIL_IN_USE - The email address is in use.
        /// USERNAME_IN_USE - The username is in use.
        /// MISSING_DATA - Not enough data.
        /// FAIL - I don't know ask IPB.
        /// </summary>
        public string status;        
        /// <summary>
        /// IPS Connect master application's ID number for the user.
        /// </summary>
        public string id;

        /// <summary>
        /// Returns the response as a Json string.
        /// </summary>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
