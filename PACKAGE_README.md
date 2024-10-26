# EgonsoftHU.Extensions.Logging.Serilog

## Summary

- Enrich log events with multiple properties at once using `PropertyBagEnricher`.
- Enrich log events with the name of the source member (method, property, etc.) in which the log event occurs.
  - `ILogger.Here()` extension method.
  - `PropertyBagEnricher.CreateForSourceMember()` static factory method.
  - `PropertyBagEnricher.Here()` instance method.
- Populate `Exception.Data` dictionary with the log event properties.
  - `Exception.Populate(PropertyBagEnricher)` extension method.

## SourceMember property

When you create a logger by calling `Log.Logger.ForContext<T>()` then the log events will contain a
`SourceContext` property with the type name where the log event occurs.

This project helps you add a `SourceMember` property with the member name of the type where the log event occurs.

The `SourceMember` property name can be customized.

```csharp
using EgonsoftHU.Extensions.Logging;
using Serilog;

namespace SomeCompany;

public class SomeService
{
    // Calling ForContext<SomeService>() will add the log event property below:
    // "SourceContext": "SomeCompany.SomeService"
    private static readonly ILogger logger = Log.Logger.ForContext<SomeService>();

    public void DoSomething()
    {
        logger
            .Here() // Adds the log event property: "SourceMember": "DoSomething"
            .Verbose("Something has been done.");
    }
}
```

## PropertyBagEnricher : Serilog.Core.ILogEventEnricher

Each time you add a log event property (e.g. `logger.ForContext("SomeProperty", 42)`) a new logger instance will be created.

This enricher helps you reducing creating logger instances by adding multiple log event properties at once.

```csharp
using EgonsoftHU.Extensions.Logging;
using Serilog;

IDictionary<string, object> additionalProperties = /* omitted for clarity */

var enricher =
    PropertyBagEnricher
        .CreateForSourceMember() // Equivalent to calling .Create().Here()
        .Add("Property1", 1)
        .Add("Property2", "2")
        .AddRange(additionalProperties);

Log.Logger.ForContext(enricher).Verbose("Log message with properties.");
```

## Exception.Data population

`Exception.Data` dictionary can contain additional user-defined information about the exception.

The log event properties the enricher contains can be populated into the `Exception.Data` dictionary using the `Exception.Populate(PropertyBagEnricher)` extension method.

```csharp
using EgonsoftHU.Extensions.Logging;

IDictionary<string, object> additionalProperties = /* omitted for clarity */

var enricher =
    PropertyBagEnricher
        .Create()
        .Add("Property1", 1)
        .Add("Property2", "2")
        .AddRange(additionalProperties);

// Exception.Data dictionary will contain all the properties above.
// The extension method returns the exception itself,
// hence you can use it directly in a `throw` statement or expression.
throw new InvalidOperationException("Invalid operation.").Populate(enricher);
```

## More information

Learn more at [https://github.com/gcsizmadia/EgonsoftHU.Extensions.Logging.Serilog](https://github.com/gcsizmadia/EgonsoftHU.Extensions.Logging.Serilog)
