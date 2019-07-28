using System;
using System.Threading.Tasks;

namespace Excalibur
{
    public class LoginManager
    {
        public async Task<bool> Login(string username, string password, int sleepTimeInSeconds)
        {
            await Task.Delay(1000 * sleepTimeInSeconds).ConfigureAwait(false);
            
            if (username == "error")
            {
                throw new ArgumentException("Something went terribly wrong!");
            }

            return !string.IsNullOrWhiteSpace(username) 
                && !string.IsNullOrWhiteSpace(password) 
                && string.Equals(username, password);
        }
    }
}
