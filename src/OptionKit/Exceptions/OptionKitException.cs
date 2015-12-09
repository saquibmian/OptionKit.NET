using System;

namespace OptionKit.Exceptions {
	public class OptionKitException : Exception {
		public OptionKitException( string msg, params object[] objs )
			: base( string.Format( msg, objs ) ) { }

		public OptionKitException( Exception ex, string msg, params object[] objs )
			: base( string.Format( msg, objs ), ex ) { }
	}
}