﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Converters;

namespace Structurizr.IO.Json
{
    public class JsonReader
    {

        public Workspace Read(StringReader reader)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                Converters = new List<JsonConverter> {
                    new StringEnumConverter(),
                    new IsoDateTimeConverter(),
                    new PaperSizeJsonConverter()
                },
                ObjectCreationHandling = ObjectCreationHandling.Replace
            };

            Workspace workspace = JsonConvert.DeserializeObject<Workspace>(reader.ReadToEnd(), settings);
            workspace.Hydrate();

            return workspace;
        }

    }
}
