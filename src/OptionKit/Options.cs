using System.Collections.Generic;

namespace OptionKit {
	public sealed class Options {

        /// <summary>
        /// Any leading operations not parsed. 
        /// </summary>
        public List<string> Operations { get; set; }

        /// <summary>
        /// Any trailing arguments not parsed, including any after option parsing is stopped (by using --). 
        /// </summary>
        public List<string> Arguments { get; set; }

        /// <summary>
        /// The extracted options
        /// </summary>
        public IDictionary<string, List<string>> ExtractedOptions { get; set; }

		public Options() {
			ExtractedOptions = new Dictionary<string, List<string>>();
            Arguments = new List<string>();
            Operations = new List<string>();
		}

	}
}