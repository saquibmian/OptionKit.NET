using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace OptionKit.Mapping {
	public abstract class Mapping<T>: MappingBase where T : new() {

		protected Mapping() {
			Object = new T();
		}
		
		protected void Map<TProp>( Expression<Func<T, TProp>> propertySelector, Action<OptionMapping> config ) {
			Map( propertySelector, config, list => list[0].ConvertTo<TProp>() );
		}

		protected void Map<TProp>( Expression<Func<T, IEnumerable<TProp>>> propertySelector, Action<OptionMapping> config ) {
			Map( propertySelector, config, list => list.Select( i => i.ConvertTo<TProp>() ) );
		}

		protected void Map<TProp>( Expression<Func<T, TProp[]>> propertySelector, Action<OptionMapping> config ) {
			Map( propertySelector, config, list => list.Select( i => i.ConvertTo<TProp>() ).ToArray() );
		}		
		
		protected void Map<TProp>( Expression<Func<T, List<TProp>>> propertySelector, Action<OptionMapping> config ) {
			Map( propertySelector, config, list => list.Select( i => i.ConvertTo<TProp>() ).ToList() );
		}

		protected void Map<TProp>( Expression<Func<T, TProp>> propertySelector, Action<OptionMapping> config, Func<string[], TProp> convertorFunc ) {
			var property = GetProperty( propertySelector );

			var mapping = new OptionMapping();
			config( mapping );
			mapping.Validate();

			Map( property, mapping, convertorFunc );
		}

		private PropertyInfo GetProperty<TProp>( Expression<Func<T, TProp>> fromExpression ) {
			var memberExpression = fromExpression.Body as MemberExpression;
			if( memberExpression == null ) {
				throw new Exception( "Mapping expression is not a MemberExpression." );
			}

			var property = memberExpression.Member as PropertyInfo;
			if( property == null ) {
				throw new Exception( "Unable to get property for expression." );
			}

			return property;
		}

	}
}