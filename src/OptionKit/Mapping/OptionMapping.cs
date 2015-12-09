using System.Linq;
using OptionKit.Exceptions;

namespace OptionKit.Mapping {

	public sealed class OptionMapping {

		private string m_shortKey;
		private string m_longKey;

		public string ShortKey {
			get { return m_shortKey; }
			set { m_shortKey = "-" + value; }
		}

		public string LongKey {
			get { return m_longKey; }
			set { m_longKey = "--" + value; }
		}

		public string Description { get; set; }

		public bool Required { get; set; }
		
		public object DefaultValue { get; set; }

		internal string[] OptionKeys {
			get {
				return new[] {
					ShortKey, LongKey
				}.Where( k => !string.IsNullOrWhiteSpace( k ) )
					.ToArray();
			}
		}

		internal void Validate() {
			if( string.IsNullOrEmpty( LongKey ) && string.IsNullOrEmpty( ShortKey ) ) {
				throw new OptionKitException( "Option is missing both a long key and a short key." );
			}
		}
	}
}