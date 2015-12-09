using System;

namespace OptionKit.Mapping {
	
	[AttributeUsage( AttributeTargets.Property, AllowMultiple = false, Inherited = true )]
    public class OptionAttribute : Attribute {

		public readonly OptionMapping Mapping = new OptionMapping();

		public bool Required {
			get {
				return Mapping.Required;
			}
			protected set {
				Mapping.Required = value;
			}
		}
		
		public string ShortKey {
			get { return Mapping.ShortKey; }
			set { Mapping.ShortKey = value; }
        }

        public string LongKey {
			get { return Mapping.LongKey; }
            set { Mapping.LongKey = value; }
        }

		public string Description {
			get {
				return Mapping.Description;	
			}
			set {
				Mapping.Description = value;
			}
		}

		public object DefaultValue {
			get {
				return Mapping.DefaultValue;
			}
			set {
				Mapping.DefaultValue = value;
			}
		}

        public OptionAttribute() {
            Required = false;
        }

    }

}
