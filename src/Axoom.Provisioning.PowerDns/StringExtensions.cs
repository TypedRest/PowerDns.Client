namespace Axoom.Provisioning.PowerDns
{
    internal static class StringExtensions
    {
        public static string EnsureTrailingDot(this string str) => str.EndsWith(".") ? str : str + ".";
    }
}