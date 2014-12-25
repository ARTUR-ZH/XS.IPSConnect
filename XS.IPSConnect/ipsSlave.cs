using Newtonsoft.Json;
using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace XS.IPSConnect
{
    /// <summary>
    /// IPS Connect slave application for the .NET framework.
    /// </summary>
    public class ipsSlave
    {
        /// <summary>
        /// The master application url.
        /// </summary>
        string masterUrl;
        /// <summary>
        /// The master application key.
        /// </summary>
        string masterKey;

        /// <summary>
        /// Constructs a new slave application.
        /// </summary>
        /// <param name="masterUrl">The master application url.</param>
        /// <param name="masterKey">The master application key.</param>
        public ipsSlave(string masterUrl, string masterKey = "")
        {
            this.masterUrl = masterUrl;
            this.masterKey = masterKey;

            Console.WriteLine("Created IPS Connect Slave, url: " + masterUrl + ", key: " + masterKey);
        }

        /// <summary>
        /// Attempts to log the user in.
        /// </summary>
        /// <param name="id">Either the username, email address or ID, as per idType.</param>
        /// <param name="password">The account password.</param>
        /// <param name="idType">Either "username", if providing a username, "email" if providing an email address or "id" if providing the master application's ID number for the account.</param>
        public IPSLoginResponse Login(string id, string password, IPSIDType idType = IPSIDType.username)
        {
            // Create a json string.
            string json = string.Empty;

            // Use the web client to ask IPB for a response.
            using (WebClient w = new WebClient())
            {
                json = w.DownloadString(this.masterUrl + "?act=login&idType=" + idType.ToString() + "&id=" + id + "&password=" + GetMD5(password));
            }

            // Return the response from IPB.
            return JsonConvert.DeserializeObject<IPSLoginResponse>(json);
        }

        /// <summary>
        /// Creates an user account on the master.
        /// </summary>
        /// <param name="email">The account email address.</param>
        /// <param name="username">The account username (i.e. what the user logs in with).</param>
        /// <param name="password">The account password.</param>
        public IPSRegisterResponse Register(string email, string username, string password)
        {
            // Check to see if master key is enabled.
            if (!string.IsNullOrEmpty(this.masterKey))
            {
                // Create a json string.
                string json = string.Empty;

                // Check if email is set.
                if (email == "")
                {
                    return new IPSRegisterResponse() { status = "MISSING_DATA" };
                }

                // Check if username is set.
                if (username == "")
                {
                    return new IPSRegisterResponse() { status = "MISSING_DATA" };
                }

                // Check if password is set.
                if (password == "")
                {
                    return new IPSRegisterResponse() { status = "MISSING_DATA" };
                }

                // Use the web client to ask IPB for a response.
                using (WebClient w = new WebClient())
                {
                    json = w.DownloadString(this.masterUrl + "?act=register&key=" + this.masterKey + "&username=" + username + "&displayname=" + username + "&email=" + email + "&password=" + GetMD5(password));
                }

                // Return the response from IPB.
                return JsonConvert.DeserializeObject<IPSRegisterResponse>(json);
            }

            return new IPSRegisterResponse() { status = "BAD_KEY" };
        }

        /// <summary>
        /// Changes a user's account data on the master.
        /// </summary>
        /// <param name="id">The master application's ID number for the user that is logged in.</param>
        /// <param name="password">If changing the email address, provide the new password, md5 encoded. If not provided or blank, the password will not be changed.</param>
        /// <param name="email">If changing the email address, provide the new email address. If not provided or blank, the email address will not be changed.</param>
        /// <param name="username">If changing the username, provide the new username. If not provided or blank, the username will not be changed.</param>
        /// <param name="displayname">If changing the display name, provide the new display name. If not provided or blank, the display name will not be changed.</param>
        public IPSChangeResponse Change(int id, string password = "", string email = "", string username = "", string displayname = "")
        {
            // Check to see if master key is enabled.
            if (!string.IsNullOrEmpty(this.masterKey))
            {
                // Create a json string.
                string json = string.Empty;

                // Check if password is set.
                if (password != "")
                {
                    password = GetMD5(password);
                }

                // Use the web client to ask IPB for a response.
                using (WebClient w = new WebClient())
                {
                    json = w.DownloadString(this.masterUrl + "?act=change&key=" + GetMD5(this.masterKey + id) + "&id=" + id + "&username=" + username + "&displayname=" + displayname + "&email=" + email + "&password=" + password);
                }

                // Return the response from IPB.
                return JsonConvert.DeserializeObject<IPSChangeResponse>(json);
            }

            return new IPSChangeResponse() { status = "BAD_KEY" };
        }

        /// <summary>
        /// MD5 hashing function.
        /// </summary>
        /// <param name="text">The text to hash.</param>
        public static string GetMD5(string text)
        {
            // To calculate MD5 hash from an input string
            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(text);
            byte[] hash = md5.ComputeHash(inputBytes);

            // convert byte array to hex string
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                //to make hex string use lower case instead of uppercase add parameter “X2″
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString().ToLower();
        }
    }
}
