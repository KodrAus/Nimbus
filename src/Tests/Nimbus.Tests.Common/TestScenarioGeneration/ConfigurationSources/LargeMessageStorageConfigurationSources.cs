using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ConfigInjector.QuickAndDirty;
using Nimbus.Configuration.LargeMessages;
using Nimbus.LargeMessages.Azure.Client;
using Nimbus.LargeMessages.Azure.Http;
using Nimbus.LargeMessages.FileSystem.Configuration;
using Nimbus.Tests.Common.Configuration;

namespace Nimbus.Tests.Common.TestScenarioGeneration.ConfigurationSources
{
    public class LargeMessageStorageConfigurationSources : IEnumerable<PartialConfigurationScenario<LargeMessageStorageConfiguration>>
    {
        public IEnumerator<PartialConfigurationScenario<LargeMessageStorageConfiguration>> GetEnumerator()
        {
            var largeMessageBodyTempPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                                        "Nimbus Integration Test Suite",
                                                        Guid.NewGuid().ToString());
            yield return new PartialConfigurationScenario<LargeMessageStorageConfiguration>(
                typeof (FileSystemStorageConfiguration).Name,
                new FileSystemStorageConfiguration()
                    .WithStorageDirectory(largeMessageBodyTempPath)
                );

            yield return new PartialConfigurationScenario<LargeMessageStorageConfiguration>(
                typeof (AzureBlobStorageLargeMessageStorageConfiguration).Name,
                new AzureBlobStorageLargeMessageStorageConfiguration()
                    .UsingStorageAccountConnectionString(DefaultSettingsReader.Get<AzureBlobStorageConnectionString>().Value)
                );

            yield return new PartialConfigurationScenario<LargeMessageStorageConfiguration>(
                typeof (AzureBlobStorageHttpLargeMessageStorageConfiguration).Name,
                new AzureBlobStorageHttpLargeMessageStorageConfiguration()
                    .UsingBlobStorageContainer(new Uri("http://fixme.example.com"), "FIXME")
                );
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}