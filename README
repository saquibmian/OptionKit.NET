# OptionKit.NET

This is a small, lightweight library to parse command line options. It supports flags, standard options, and trailing arguments. Command modes are also supported. 

An example usage might look like:

```
program [mode] [options] [arguments]
```

`getopt`-style option parse disabling also exists. For example, 

```
machine create --type Windows --memory 512 -- machine1
```

would produce the result:

```
{
	"Operations":["create"],
	"ExtractedOptions" : {
		"type":["Windows"],
		"memory":["512"]
	}
	"Arguments":["machine1"]
}
```

while 


```
machine create --type Windows --memory 512 --memory machine1
```

would produce the result:

```
{
	"Operations":["create"],
	"ExtractedOptions" : {
		"type":["Windows"],
		"memory":["512", "machine1"]
	}
	"Arguments":[]
}
```


## Mapping to C# objects

Plain-old C# objects can also be mapped via attribute or via expression trees. Here is an example:

```
internal class CliModel {

	[Option(
		ShortKey = "h",
		LongKey = "help",
		DefaultValue = false,
		Description = "Show Help."
	)]
	public bool ShouldDisplayHelp { get; set; }

	[Option(
		ShortKey = "v",
		LongKey = "verbose",
		DefaultValue = false,
		Description = "Enable verbose logging to console."
	)]
	public bool ShouldLogVerbose { get; set; }

	[Option(
		ShortKey = "d",
		LongKey = "doSomething",
		DefaultValue = false,
		Description = "Set this to true to do something."
	)]
	public bool ShouldDoSomething { get; set; }

}

public static void Main(string[] args) {
	try {
		CliModel options = m_optionManager.Parse<CliModel>( args );
	} catch( Exception e ) {
		Logger.ErrorFormat( "Unable to extract arguments; error was: {0}", e.Message );
		Console.Error.WriteLine( m_optionManager.Usage<CliModel>() );
		Environment.Exit(-1);
	}
}
```

