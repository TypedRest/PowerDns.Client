using System;
using Axoom.Provisioning.PowerDns.JsonConverters;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Axoom.Provisioning.PowerDns.DataTransferObjects
{
    /// <summary>
    /// Represents an canonical name which ensures a trailing dot.
    /// </summary>
    [JsonConverter(typeof(CanonicalNameJsonConverter))]
    public class CanonicalName
    {
        private readonly string _name;

        public CanonicalName([NotNull]string name) => _name = name.EnsureTrailingDot();

        public static implicit operator string(CanonicalName name) => name.ToString();
        public static implicit operator CanonicalName(string name) => new CanonicalName(name);

        public override string ToString() => _name.EnsureTrailingDot();

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((CanonicalName) obj);
        }

        public override int GetHashCode() => _name != null ? StringComparer.InvariantCultureIgnoreCase.GetHashCode(_name) : 0;

        private bool Equals(CanonicalName other) => string.Equals(_name, other._name, StringComparison.InvariantCultureIgnoreCase);
    }
}