#if NETSTANDARD2_0
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Axoom.Provisioning.PowerDns
{
    [PublicAPI]
    [ExcludeFromCodeCoverage]
    public static class PowerDnsServiceCollectionExtensions
    {
        /// <summary>
        /// Registers a <see cref="IPowerDns"/> client.
        /// </summary>
        public static IServiceCollection AddPowerDns(this IServiceCollection services)
            => services.AddSingleton<IPowerDns, PowerDns>();
    }
}
#endif