using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DocumentRequestAPI.DataStore
{
	// Provides a contract to perform JSON serialization of a specified set of properties for an object
	public class DynamicContractResolver : DefaultContractResolver
	{
		private readonly List<string> m_IncludedPropertyNames;

		public DynamicContractResolver(List<string> includedPropertyNames)
		{
			m_IncludedPropertyNames = includedPropertyNames;
		}

		protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
		{
			IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);

			// only serializer properties that are not named after the specified property.
			properties = properties.Where(p => m_IncludedPropertyNames.Contains(p.PropertyName)).ToList();

			return properties;
		}
	}
}