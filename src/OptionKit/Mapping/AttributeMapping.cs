using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OptionKit.Mapping {
	internal sealed class AttributeMapping : MappingBase {

		private readonly Type m_modelType;

		public AttributeMapping( Type modelType ) {
			m_modelType = modelType;
			Object = Activator.CreateInstance( m_modelType );
		}

		protected override void Process() {
			foreach( var property in m_modelType.GetProperties() ) {
				var type = property.PropertyType;
				var attribute = property.GetCustomAttribute<OptionAttribute>();

				if( attribute == null ) {
					continue;
				}

				var mapping = attribute.Mapping;
				mapping.Validate();

				Func<string[], object> convertorFunc = list => list[0].ConvertTo( type );

				if( type.IsArray ) {
					convertorFunc = list => list.Select( i => i.ConvertTo( type.GetElementType() ) ).ToArray();
				} else if( typeof( List<> ).IsAssignableFrom( type ) ) {
					convertorFunc = list => list.Select( i => i.ConvertTo( type.GetGenericArguments()[0] ) ).ToList();
				} else if( typeof( IEnumerable<> ).IsAssignableFrom( type ) ) {
					convertorFunc = list => list.Select( i => i.ConvertTo( type.GetGenericArguments()[0] ) );
				}

				Map( property, mapping, convertorFunc );
			}
		}

	}
}