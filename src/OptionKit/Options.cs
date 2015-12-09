using System.Collections.Generic;

namespace OptionKit {
	public sealed class Options {

		/// <summary>
		/// Any trailing arguments not parsed, including any after option parsing is stopped (by using --). 
		/// </summary>
		public List<string> Trailers { get; set; }

		/// <summary>
		/// The extracted options
		/// </summary>
		public IDictionary<string, List<string>> ExtractedOptions { get; set; }

		public Options() {
			ExtractedOptions = new Dictionary<string, List<string>>();
			Trailers = new List<string>();
		}

	}
}