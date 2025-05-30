using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Google.Protobuf;
using Temporalio.Api.Common.V1;

namespace Temporalio.Converters
{
    /// <summary>
    /// Default implementation of a payload converter which iterates over a collection of
    /// <see cref="IEncodingConverter" />.
    /// </summary>
    /// <remarks>
    /// See <see cref="DefaultPayloadConverter()" /> for the default set of encoding converters
    /// used. To create a custom converter, a new class should extend this one.
    /// </remarks>
    public class DefaultPayloadConverter :
        IPayloadConverter,
        IWithSerializationContext<IPayloadConverter>
    {
        private readonly bool anyConvertersWithSerializationContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultPayloadConverter"/> class.
        /// </summary>
        /// <remarks>
        /// The default payload converter uses the following set of payload converters in order:
        /// <list type="bullet">
        /// <item><term><see cref="BinaryNullConverter" /></term></item>
        /// <item><term><see cref="BinaryPlainConverter" /></term></item>
        /// <item><term><see cref="JsonProtoConverter" /></term></item>
        /// <item><term><see cref="BinaryProtoConverter" /></term></item>
        /// <item><term><see cref="JsonPlainConverter" /></term></item>
        /// </list>
        /// <para>
        /// Each of those converters are tried in that order when converting to payload, stopping at
        /// the first one that can convert. This means nulls, byte arrays, and protobuf messages are
        /// all have their own encoding and anything else falls through to the JSON converter. The
        /// JSON converter will fail on anything it can't convert.
        /// </para>
        /// <para>
        /// This also means binary proto converter will never be used when converting to a payload
        /// since the JSON proto converter will accept proto objects first. It is present however
        /// for converting from payloads which may have its encoding (e.g. from another language
        /// that is using binary proto instead of JSON proto).
        /// </para>
        /// </remarks>
        public DefaultPayloadConverter()
            : this(new JsonSerializerOptions())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultPayloadConverter"/> class.
        /// </summary>
        /// <param name="jsonSerializerOptions">Custom serializer options.</param>
        /// <seealso cref="DefaultPayloadConverter()" />
        public DefaultPayloadConverter(JsonSerializerOptions jsonSerializerOptions)
            : this(
                new BinaryNullConverter(),
                new BinaryPlainConverter(),
                new JsonProtoConverter(),
                new BinaryProtoConverter(),
                new JsonPlainConverter(jsonSerializerOptions))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultPayloadConverter"/> class with a
        /// set of encoding converters.
        /// </summary>
        /// <param name="encodingConverters">
        /// Encoding converters to use. Duplicate encodings not allowed.
        /// </param>
        /// <seealso cref="DefaultPayloadConverter()" />
        public DefaultPayloadConverter(params IEncodingConverter[] encodingConverters)
        {
            EncodingConverters = Array.AsReadOnly(encodingConverters);
            IndexedEncodingConverters = encodingConverters.ToDictionary(
                x => ByteString.CopyFromUtf8(x.Encoding));
            anyConvertersWithSerializationContext = encodingConverters.Any(e =>
                e is IWithSerializationContext<IEncodingConverter>);
        }

        // KeyedCollection not worth it and OrderedDictionary not generic. So we'll expose the
        // collection and maintain an internal dictionary.

        /// <summary>
        /// Gets the encoding converters tried, in order, when converting to payload.
        /// </summary>
        public IReadOnlyCollection<IEncodingConverter> EncodingConverters { get; private init; }

        /// <summary>
        /// Gets the encoding converters by encoding looked up when converting from payload.
        /// </summary>
        protected IReadOnlyDictionary<ByteString, IEncodingConverter> IndexedEncodingConverters
        {
            get;
            private init;
        }

        /// <inheritdoc />
        public Payload ToPayload(object? value)
        {
            if (value is IEncodedRawValue)
            {
                throw new InvalidOperationException("Cannot convert IEncodedRawValue");
            }
            if (value is IRawValue rawValue)
            {
                return rawValue.Payload;
            }
            foreach (var enc in EncodingConverters)
            {
                if (enc.TryToPayload(value, out var payload))
                {
                    return payload!;
                }
            }
            var vType = value == null ? "<null>" : value.GetType().ToString();
            throw new ArgumentException($"Value of type {vType} has no known converter");
        }

        /// <inheritdoc />
        public object? ToValue(Payload payload, Type type)
        {
            if (type == typeof(IRawValue) || type == typeof(RawValue))
            {
                return new RawValue(payload);
            }
            var encoding = payload.Metadata["encoding"];
            if (IndexedEncodingConverters.TryGetValue(encoding, out var converter))
            {
                return converter.ToValue(payload, type);
            }
            throw new ArgumentException($"Unknown payload encoding {encoding.ToStringUtf8()}");
        }

        /// <inheritdoc/>
        public virtual IPayloadConverter WithSerializationContext(ISerializationContext context)
        {
            if (!anyConvertersWithSerializationContext)
            {
                return this;
            }
            return new DefaultPayloadConverter(EncodingConverters.Select(e =>
                e is IWithSerializationContext<IEncodingConverter> ew ?
                    ew.WithSerializationContext(context) : e).ToArray());
        }
    }
}
