using System;

namespace OptionKit.Mapping {
	public interface IOptionMappingStringFormatter {

		string Format( OptionMapping mapping, Type boundProperty );

	}

	internal sealed class OptionMappingStringFormatter : IOptionMappingStringFormatter {
		public string Format( OptionMapping mapping, Type boundProperty ) {
			return string.Format(
				"\t{0,3} {1}|{2} <{3}> \t\t{4} {5}",
				mapping.Required ? "(r)" : "",
				mapping.ShortKey,
				mapping.LongKey,
				boundProperty.Name.ToLower(),
				mapping.Description,
				mapping.DefaultValue == null ? "" : string.Format( "(default: {0})", mapping.DefaultValue )
				);
		}
	}
}