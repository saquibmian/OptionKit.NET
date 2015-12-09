using System.Collections.Generic;
using OptionKit.Exceptions;

namespace OptionKit {

	public interface IOptionExtractor {

		Options ExtractOptions( string[] args );

	}
	
	public sealed class OptionExtractor : IOptionExtractor {

		public static readonly string OptionParsingDisableKey = "--";
		public static readonly string[] OptionKeyPrefixes = { "--", "-" };

		private enum State {

			JustStartedParsing,
			InTheMiddleOfParsing,
			ParsingDisabled,
			ParsingImplicitlyDisabled,
			Done

		}

		private enum OptionType {
			Flag, Value
		}
		
		public Options ExtractOptions( string[] args ) {
			var toReturn = new Options();
			var parsedOptions = toReturn.ExtractedOptions;

			var optionTypes = new Dictionary<string, OptionType>();
			
			var numberOfArgs = args.Length;
			var processedArgs = 0;
			var state = State.JustStartedParsing;

			while( state != State.Done && args.Any() ) {
				var arg = args[processedArgs];

				switch( state ) {
					case State.ParsingImplicitlyDisabled:
						if( arg.IsParsingDisableKey() ) {
							state = State.ParsingDisabled;
							++processedArgs;
							break;
						}
						if( arg.IsOptionKey() ) {
							throw new UnexpectedOptionException( arg );
						}						
						toReturn.Trailers.Add( arg );
						++processedArgs;
						break;

					case State.ParsingDisabled:
						toReturn.Trailers.Add( arg );
						++processedArgs;
						break;

					case State.JustStartedParsing:
						if( !arg.IsOptionKey() ) {
							throw new UnexpectedOperationException( arg );
						}
						state = State.InTheMiddleOfParsing;
						break;
					
					case State.InTheMiddleOfParsing:
						if( arg.IsParsingDisableKey() ) {
							state = State.ParsingDisabled;
							++processedArgs;
							break;
						}

						if( !arg.IsOptionKey() ) {
							state = State.ParsingImplicitlyDisabled;
							break;
						}

						var option = arg;

						// if we're at the last option or the value is an option, then we have a flag (unary) option
						if( processedArgs == numberOfArgs - 1 || args[processedArgs + 1].IsOptionKey() ) {
							if( optionTypes.ContainsKey( option ) && optionTypes[option] == OptionType.Value ) {
								throw new AmbiguousOptionException( option );
							}
							parsedOptions[option] = new List<string> { "true" };
							optionTypes[option] = OptionType.Flag;
						
						} else if( !parsedOptions.ContainsKey( option ) ) {
							var value = args[processedArgs + 1]; 
							parsedOptions[option] = new List<string> { value };
							optionTypes[option] = OptionType.Value;
							++processedArgs;
						
						} else {
							// if option was previously provided as a flag
							if( optionTypes.ContainsKey( option ) && optionTypes[option] == OptionType.Flag ) {
								throw new AmbiguousOptionException( option );
							}

							var value = args[processedArgs + 1]; 
							parsedOptions[option].Add( value );
							++processedArgs;
						}
						++processedArgs;
						break;
				}

				if( processedArgs == numberOfArgs ) {
					state = State.Done;
				}
			}

			return toReturn;
		}

    }


}
