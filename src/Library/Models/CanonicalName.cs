using System;
using Newtonsoft.Json;

namespace PowerDns.Client.Models
{
    /// <summary>
    /// Represents an canonical name which ensures a trailing dot.
    /// </summary>
    [JsonConverter(typeof(CanonicalNameJsonConverter))]
    public class CanonicalName : IEquatable<CanonicalName>
    {
        private readonly string _name;

        public CanonicalName(string name)
        {
            _name = EnsureTrailingDot(name ?? throw new ArgumentNullException(nameof(name)));
        }

        private static string EnsureTrailingDot(string str) => str.EndsWith(".") ? str : str + ".";

        public static implicit operator CanonicalName(string name) => new CanonicalName(name);

        public static implicit operator string(CanonicalName name) => name.ToString();

        public override string ToString() => _name;

        public bool Equals(CanonicalName other)
            => other != null && string.Equals(_name, other._name, StringComparison.OrdinalIgnoreCase);

        public override bool Equals(object obj) => obj is CanonicalName name && Equals(name);

        public override int GetHashCode() => StringComparer.OrdinalIgnoreCase.GetHashCode(_name);

        public static bool operator ==(CanonicalName left, CanonicalName right) => Equals(left, right);

        public static bool operator !=(CanonicalName left, CanonicalName right) => !Equals(left, right);
    }
}
