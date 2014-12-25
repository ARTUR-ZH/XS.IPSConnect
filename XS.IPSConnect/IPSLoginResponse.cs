using Newtonsoft.Json;

namespace XS.IPSConnect
{
    public class IPSLoginResponse
    {
        /// <summary>
        /// Returns one of the following statuses:
        /// SUCCESS - The login was successful.
        /// WRONG_AUTH - The password was incorrect.
        /// NO_USER - Could not locate a user based on the ID given.
        /// MISSING_DATA - You did not provide all the required data.
        /// ACCOUNT_LOCKED - Account has been locked by brute-force prevention.
        /// VALIDATING - The login was successful, but the account hasn't been validated yet. Do not process login.
        /// </summary>
        public string connect_status;        
        /// <summary>
        /// IPS Connect master application's ID number for the user that is logged in.
        /// </summary>
        public string connect_id;
        /// <summary>
        /// The username of the user that is logged in.
        /// </summary>
        public string connect_username;
        /// <summary>
        /// The displayname of the user that is logged in.
        /// </summary>
        public string connect_displayname;
        /// <summary>
        /// The email address of the user that is logged in
        /// </summary>
        public string connect_email;
        /// <summary>
        /// If the account is locked, the number of seconds until it unlocks.
        /// </summary>
        public string connect_unlock;
        /// <summary>
        /// Is a URL where you can direct the user to resend the validation email.
        /// </summary>
        public string connect_revalidate_url;

        /// <summary>
        /// Returns the response as a Json string.
        /// </summary>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
