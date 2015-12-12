using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OptionKit.Exceptions;
using OptionKit.Mapping;

namespace OptionKit {
	public interface IOptionManager {

        bool UseAttributeMappings { get; }

        T Parse<T>( string[] args ) where T : new();

		void AddMapping<T>( Mapping<T> mapping ) where T : new();

		string Usage<T>();

	}
	
	public sealed class OptionManager : IOptionManager {
		private readonly IOptionExtractor m_parser = new OptionExtractor();
		private readonly Dictionary<Type, MappingBase> m_mappings = new Dictionary<Type, MappingBase>();

		public bool UseAttributeMappings { get; set; }

		public OptionManager() {
			UseAttributeMappings = true;
		}

		public T Parse<T>( string[] args ) where T : new() {
			var mapping = GetMapping( typeof( T ) );
			var options = m_parser.ExtractOptions( args );
			return (T)mapping.BuildModelFromOptions( options );
		}

		public string Usage<T>() {
			var mapping = GetMapping( typeof( T ) );
			return mapping.Help();
		}

		public void AddMapping<T, TMapping>() where TMapping : Mapping<T> where T : new() {
			m_mappings[typeof( T )] = Activator.CreateInstance<TMapping>();
		}

		public void AddMapping<T>( Mapping<T> mapping ) where T : new() {
			m_mappings[typeof( T )] = mapping;
		}

		private MappingBase GetMapping( Type type ) {
			if( m_mappings.ContainsKey( type ) ) {
				return m_mappings[type];
			}

			if( UseAttributeMappings &&  type.GetProperties().Any( p => p.GetCustomAttribute<OptionAttribute>() != null )) {
				return new AttributeMapping( type );
			}

			throw new MappingNotFoundException( type );
		}

	}
}