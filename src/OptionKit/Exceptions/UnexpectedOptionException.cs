namespace OptionKit.Exceptions {
	public sealed class UnexpectedOptionException : OptionKitException {
		public UnexpectedOptionException( string arg )
			: base( string.Format( "Unexpected option '{0}'; all options must come before trailing values. If you intended to pass this in, disable option parsing using the '--' argument.", arg ) ) { }
	}
}