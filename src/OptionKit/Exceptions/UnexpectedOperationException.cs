namespace OptionKit.Exceptions {
	public sealed class UnexpectedOperationException : OptionKitException {
		public UnexpectedOperationException( string operation )
			: base( string.Format( "Unexpected opearation '{0}'; all operations must handled before parsing.", operation ) ) { }
	}
}