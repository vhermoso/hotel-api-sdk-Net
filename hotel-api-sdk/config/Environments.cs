using System.Collections.Generic;
using System.Configuration;

namespace com.hotelbeds.distribution.hotel_api_sdk.config
{
    public class EnvironmentsSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(EnviornmentCollection), AddItemName = "environment")]
        public EnviornmentCollection Environments
        {
            get { return (EnviornmentCollection)this[""]; }
        }
    }

    public class EnviornmentCollection : ConfigurationElementCollection, IEnumerable<Environment>
    {
        private readonly List<Environment> environments;

        public EnviornmentCollection()
        {
            this.environments = new List<Environment>();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            var environment = new Environment();
            this.environments.Add(environment);
            return environment;
        }

        protected override object GetElementKey(ConfigurationElement environment)
        {
            return ((Environment)environment).Name;
        }

        public new IEnumerator<Environment> GetEnumerator()
        {
            return this.environments.GetEnumerator();
        }
    }

    public class Environment : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
        }

        [ConfigurationProperty("baseUrl", IsKey = false, IsRequired = true)]
        public string BaseUrl
        {
            get { return (string)this["baseUrl"]; }
        }

        [ConfigurationProperty("baseSecureUrl", IsKey = false, IsRequired = false)]
        public string BaseSecureUrl
        {
            get { return (string)this["baseSecureUrl"]; }
        }
    }
}
