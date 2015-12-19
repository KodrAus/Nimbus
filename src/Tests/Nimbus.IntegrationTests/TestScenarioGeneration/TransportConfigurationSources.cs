using System.Collections;
using System.Collections.Generic;
using ConfigInjector.QuickAndDirty;
using Nimbus.Configuration.Transport;
using Nimbus.IntegrationTests.Configuration;
using Nimbus.Transports.InProcess;
using Nimbus.Transports.WindowsServiceBus;

namespace Nimbus.IntegrationTests.TestScenarioGeneration
{
    public class TransportConfigurationSources : IEnumerable<PartialConfigurationScenario<TransportConfiguration>>
    {
        public IEnumerator<PartialConfigurationScenario<TransportConfiguration>> GetEnumerator()
        {
            yield return new PartialConfigurationScenario<TransportConfiguration>(
                nameof(InProcessTransportConfiguration),
                new InProcessTransportConfiguration());

            foreach (var largeMessageStorage in new LargeMessageStorageConfigurationSources())
            {
                yield return new PartialConfigurationScenario<TransportConfiguration>(
                    PartialConfigurationScenario.Combine(nameof(WindowsServiceBusTransportConfiguration), largeMessageStorage.Name),
                    new WindowsServiceBusTransportConfiguration()
                        .WithConnectionString(DefaultSettingsReader.Get<AzureServiceBusConnectionString>())
                        .WithLargeMessageStorage(largeMessageStorage.Configuration));
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}