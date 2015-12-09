using System;
using System.ComponentModel;
using System.Linq;

//using System.Text.RegularExpressions;

namespace OptionKit {
	public static class StringExtensionsForOptions {


		//private static readonly Regex CliRegex = new Regex( "\"([^\"]+)\"|([^\"\\s]+)", RegexOptions.CultureInvariant | RegexOptions.Compiled );

		public static T ConvertTo<T>( this string source ) {
			return (T)source.ConvertTo( typeof( T ) );
		}

		public static object ConvertTo( this string source, Type type ) {
			if( source == null ) {
				return null;
			}

			var converter = TypeDescriptor.GetConverter( type );

			var toReturn = converter.ConvertFromString( source );

			return toReturn;
		}

		//public static string[] AsArgs( this string source ) {
		//	return CliRegex.Matches( source )
		//		.Cast<Match>()
		//		.Select( x => x.Value.Trim( '"' ) )
		//		.ToArray();
		//}
		
		public static bool HasOperation( this string[] args ) {
			return args.Length > 0 && !args[0].IsOptionKey();
		}

		public static string GetOperation( this string[] args ) {
			if( !args.HasOperation() ) {
				return null;
			}

			return args[0];
		}

		public static string[] SkipOperation( this string[] args ) {
			if( !args.HasOperation() ) {
				return args;
			}

			return args.Skip( 1 ).ToArray();
		}
		
		internal static bool IsOptionKey( this string arg ) {
			return OptionExtractor.OptionKeyPrefixes.Any( 
				prefix => arg != prefix && arg.StartsWith( prefix ) 
				);
		}

		internal static bool IsParsingDisableKey( this string arg ) {
			return arg == OptionExtractor.OptionParsingDisableKey;
		}

	}
}