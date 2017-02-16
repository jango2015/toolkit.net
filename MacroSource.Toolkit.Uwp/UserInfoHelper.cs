using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.System;


namespace MacroSource.Toolkit.Uwp
{

    public static class UserInfoHelper
    {
        public static async Task<string> GetUserAccountNameAsync() =>
            await GetUserPropertyAsync(KnownUserProperties.AccountName);

        public static async Task<string> GetUserDisplayNameAsync() =>
            await GetUserPropertyAsync(KnownUserProperties.DisplayName);

        public static async Task<string> GetUserDomainNameAsync() =>
            await GetUserPropertyAsync(KnownUserProperties.DomainName);

        public static async Task<string> GetUserFirstNameAsync() =>
            await GetUserPropertyAsync(KnownUserProperties.FirstName);

        public static async Task<string> GetUserLastNameAsync() =>
            await GetUserPropertyAsync(KnownUserProperties.LastName);

        public static async Task<string> GetUserPrincipalNameAsync() =>
            await GetUserPropertyAsync(KnownUserProperties.PrincipalName);

        public static async Task<string> GetUserProviderNameAsync() =>
            await GetUserPropertyAsync(KnownUserProperties.ProviderName);

        private static async Task<string> GetUserPropertyAsync(string property)
        {
            var users = await User.FindAllAsync(UserType.LocalUser,
                UserAuthenticationStatus.LocallyAuthenticated);
            object val = await users.FirstOrDefault()?.GetPropertyAsync(
                KnownUserProperties.ProviderName);
            return val.ToString();
        }
    }
}