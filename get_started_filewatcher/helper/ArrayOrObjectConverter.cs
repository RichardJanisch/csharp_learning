/*
* Copyright (c) 2021-2024 Siemens. All rights reserved.
* This software is the confidential and proprietary information of Siemens AG.
* This file is part of Trusted Traceability.
*/

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace PublisherEDCAdapter.Helper
{
    /// <summary>
    /// Converts a JSON array or object to a list of specified type.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the list.</typeparam>
    internal class ArrayOrObjectConverter<T> : JsonConverter
    { 
        /// <summary>
        /// Reads the JSON representation of the object and converts it to a list of specified type.
        /// </summary>
        /// <param name="reader">The <see cref="JsonReader"/> to read from.</param>
        /// <param name="objectType">The type of the object.</param>
        /// <param name="existingValue">The existing value of the object being read.</param>
        /// <param name="serializer">The <see cref="JsonSerializer"/> being used.</param>
        /// <returns>A list of specified type.</returns>
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            return token.Type == JTokenType.Array ? token.ToObject<IList<T>>() : new List<T> { token.ToObject<T>()! };
        }

        /// <summary>
        /// Determines whether this converter can convert the specified type to a list of specified type.
        /// </summary>
        /// <param name="objectType">The type of the object.</param>
        /// <returns><c>true</c> if the converter can convert the specified type; otherwise, <c>false</c>.</returns>
        public override bool CanConvert(Type objectType)
            => objectType == typeof(IList<T>);

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="JsonWriter"/> to write to.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="serializer">The <see cref="JsonSerializer"/> being used.</param>
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
            => serializer.Serialize(writer, value);
    }
}
