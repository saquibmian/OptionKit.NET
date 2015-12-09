using System;
using System.IO;
using System.Reflection;
using OptionKit.Exceptions;

namespace OptionKit.Mapping {
	public abstract class MappingBase {

		protected internal object Object;
		protected internal string Usage = null;
		protected internal Options ExtractedOptions;
		protected internal readonly IOptionMappingStringFormatter OptionFormatter = new OptionMappingStringFormatter();

		protected internal ExecutionMode? Mode = null;

		public enum ExecutionMode {
			Extract,
			Usage
		}

		protected MappingBase() {
			var programName = Path.GetFileName( Assembly.GetEntryAssembly().Location );
			Usage = string.Format( "\nUsage: {0} [OPTIONS] args\n\n", programName );
		}

		internal object BuildModelFromOptions( Options extractedOptions ) {
			ExtractedOptions = extractedOptions;
			Mode = ExecutionMode.Extract;

			Process();

			return Object;
		}

		internal string Help() {
			Mode = ExecutionMode.Usage;

			Process();

			return Usage;
		}

		protected abstract void Process();

		// TODO add support for trailing
		protected internal string[] GetArgumentsForMapping( OptionMapping mapping ) {
			string[] toReturn = null;
			var extractedOptions = ExtractedOptions.ExtractedOptions;

			if( mapping.LongKey != null && extractedOptions.ContainsKey( mapping.LongKey ) ) {
				toReturn = extractedOptions[mapping.LongKey].ToArray();
			}
			if( mapping.ShortKey != null && extractedOptions.ContainsKey( mapping.ShortKey ) ) {
				toReturn = extractedOptions[mapping.ShortKey].ToArray();
			}

			if( mapping.Required && toReturn == null ) {
				throw new MissingOptionException( mapping.OptionKeys );
			}

			return toReturn;
		}

		protected void Map<TProp>( PropertyInfo property, OptionMapping mapping, Func<string[], TProp> convertorFunc ) {
			switch( Mode ) {
				case ExecutionMode.Extract:
					string[] arguments = GetArgumentsForMapping( mapping );
					var value = arguments != null ? convertorFunc( arguments ) : mapping.DefaultValue;
					property.GetSetMethod().Invoke( Object, new[] { value } );
					break;
				case ExecutionMode.Usage:
					Usage += ( OptionFormatter.Format( mapping, property.PropertyType ) + "\n" );
					break;
			}
		}
	}
}