namespace OptionKit.Exceptions {
	public sealed class MissingOptionException : OptionKitException {
		public MissingOptionException( string[] prefixes ) 
			: base( "Missing argument: [{0}]", string.Join( "|", prefixes ) ) { }
	}
}