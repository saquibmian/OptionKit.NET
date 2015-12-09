using System;

namespace OptionKit.Exceptions {
	public sealed class MappingNotFoundException : OptionKitException {
		public MappingNotFoundException( Type t )
			: base( string.Format( "No mapping found for type '{0}'.", t.FullName ) ) { }
	}
}