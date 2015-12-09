namespace OptionKit.Exceptions {
    // TODO: use this
	public sealed class DuplicateOptionException : OptionKitException {

        public DuplicateOptionException( string[] prefixes )
            : base( "Expected single argument, but found multiple: [{0}]", string.Join( "|", prefixes ) ) {}

    }
}
