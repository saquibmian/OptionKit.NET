namespace OptionKit.Exceptions {
	public sealed class AmbiguousOptionException : OptionKitException {
		public AmbiguousOptionException( string optionName )
			: base( string.Format( "Ambiguous option '{0}' is used both as a flag and with a value.", optionName ) ) { }
	}

}