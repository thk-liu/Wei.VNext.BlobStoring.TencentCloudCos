using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Volo.Abp;

namespace Wei.VNext.Sms
{
    public class SmsContainerConfiguration
    {
        private SmsContainerConfiguration Default => GetConfiguration<DefaultContainer>();

        private readonly Dictionary<string, SmsContainerConfiguration> _containers;

        public SmsContainerConfiguration()
        {
            _containers = new Dictionary<string, BlobContainerConSmsContainerConfigurationfiguration>
            {
                //Add default container
                [BlobContainerNameAttribute.GetContainerName<DefaultContainer>()] = new BlobContainerConfiguration()
            };
        }

        public BlobContainerConfigurations Configure<TContainer>(
            Action<SmsContainerConfiguration> configureAction)
        {
            return Configure(
                BlobContainerNameAttribute.GetContainerName<TContainer>(),
                configureAction
            );
        }

        public BlobContainerConfigurations Configure(
            [NotNull] string name,
            [NotNull] Action<SmsContainerConfiguration> configureAction)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNull(configureAction, nameof(configureAction));

            configureAction(
                _containers.GetOrAdd(
                    name,
                    () => new BlobContainerConfiguration(Default)
                )
            );

            return this;
        }

        public BlobContainerConfigurations ConfigureDefault(Action<SmsContainerConfiguration> configureAction)
        {
            configureAction(Default);
            return this;
        }

        public BlobContainerConfigurations ConfigureAll(Action<string, SmsContainerConfiguration> configureAction)
        {
            foreach (var container in _containers)
            {
                configureAction(container.Key, container.Value);
            }

            return this;
        }

        [NotNull]
        public SmsContainerConfiguration GetConfiguration<TContainer>()
        {
            return GetConfiguration(BlobContainerNameAttribute.GetContainerName<TContainer>());
        }

        [NotNull]
        public SmsContainerConfiguration GetConfiguration([NotNull] string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            return _containers.GetOrDefault(name) ??
                   Default;
        }
    }
}
