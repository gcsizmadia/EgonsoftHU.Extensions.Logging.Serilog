// Copyright © 2022 Gabor Csizmadia
// This code is licensed under MIT license (see LICENSE for details)

namespace EgonsoftHU.Extensions.Logging.Serilog
{
    partial class PropertyBagEnricher
    {
        private sealed class LogEventPropertyCreationInfo
        {
            public LogEventPropertyCreationInfo(string propertyName, object value, bool destructureObjects)
            {
                PropertyName = propertyName;
                Value = value;
                DestructureObjects = destructureObjects;
            }

            public string PropertyName { get; }
            
            public object Value { get; }
            
            public bool DestructureObjects { get; }
        }
    }
}
