using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Bison.Framework.Collections
{
    /// <summary>
    /// Represents a serializable collection of key value pairs.
    /// </summary>
    /// <typeparam name="TKey">The key type.</typeparam>
    /// <typeparam name="TValue">The value type.</typeparam>
    [XmlRoot("dictionary")]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
        #region Members

        /// <summary>
        /// The xml item tag.
        /// </summary>
        private const string ITEM_TAG = "item";

        /// <summary>
        /// The xml key tag.
        /// </summary>
        private const string KEY_TAG = "key";

        /// <summary>
        /// The xml value tag.
        /// </summary>
        private const string VALUE_TAG = "value";

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a serializable dictionary.
        /// </summary>
        public SerializableDictionary()
            :base ()
        {
        }

        /// <summary>
        /// Creates a serializable dictionary.
        /// </summary>
        /// <param name="capacity">The default capacity.</param>
        public SerializableDictionary(int capacity)
            : base(capacity)
        {
        }
        /// <summary>
        /// Creates a serializable dictionary.
        /// </summary>
        /// <param name="dictionary">The dictionary to copy its elements.</param>
        public SerializableDictionary(IDictionary<TKey, TValue> dictionary)
            : base(dictionary)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the xml schema.
        /// </summary>
        /// <returns>The xml schema.</returns>
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Reads the serialized xml dictionary from stream.
        /// </summary>
        /// <param name="reader">The xml reader stream.</param>
        public void ReadXml(XmlReader reader)
        {
            if (IsEmpty(reader))
            {
                return;
            }

            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            while (reader.NodeType != XmlNodeType.EndElement)
            {
                if (reader.NodeType == XmlNodeType.None)
                {
                    return;
                }

                ReadItem(reader, keySerializer, valueSerializer);
                reader.MoveToContent();
            }

            reader.ReadEndElement();
        }

        /// <summary>
        /// Writes the dictionary xml serialized to the stream.
        /// </summary>
        /// <param name="writer">The xml writer stream.</param>
        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            foreach (var key in this.Keys)
            {
                WriteItem(writer, keySerializer, valueSerializer, key);
            }
        }

        /// <summary>
        /// Checks whether the reader is empty.
        /// </summary>
        /// <param name="reader">The xml reader.</param>
        /// <returns></returns>
        private bool IsEmpty(XmlReader reader)
        {
            bool isEmpty = reader.IsEmptyElement;
            reader.Read();
            return isEmpty;
        }

        /// <summary>
        /// Reads the xml item.
        /// </summary>
        /// <param name="reader">The xml reader.</param>
        /// <param name="keySerializer">The key serializer.</param>
        /// <param name="valueSerializer">The value serializer.</param>
        private void ReadItem(XmlReader reader, XmlSerializer keySerializer, XmlSerializer valueSerializer)
        {
            reader.ReadStartElement(ITEM_TAG);
            this.Add(
                ReadKey(reader, keySerializer),
                ReakValue(reader, valueSerializer));
            reader.ReadEndElement();
        }

        /// <summary>
        /// Reads the xml key.
        /// </summary>
        /// <param name="reader">The xml reader.</param>
        /// <param name="keySerializer">The key serializer.</param>
        /// <returns></returns>
        private TKey ReadKey(XmlReader reader, XmlSerializer keySerializer)
        {
            reader.ReadStartElement(KEY_TAG);
            TKey key = (TKey)keySerializer.Deserialize(reader);
            reader.ReadEndElement();
            return key;
        }

        /// <summary>
        /// Reads the xml value.
        /// </summary>
        /// <param name="reader">The xml reader.</param>
        /// <param name="valueSerializer">The value serializer.</param>
        /// <returns></returns>
        private TValue ReakValue(XmlReader reader, XmlSerializer valueSerializer)
        {
            reader.ReadStartElement(VALUE_TAG);
            TValue value = (TValue)valueSerializer.Deserialize(reader);
            reader.ReadEndElement();
            return value;
        }

        /// <summary>
        /// Writes the xml item.
        /// </summary>
        /// <param name="writer">The xml writer.</param>
        /// <param name="keySerializer">The key serializer.</param>
        /// <param name="valueSerializer">The value serializer.</param>
        /// <param name="key">The key.</param>
        private void WriteItem(XmlWriter writer, XmlSerializer keySerializer, XmlSerializer valueSerializer, TKey key)
        {
            writer.WriteStartElement(ITEM_TAG);
            WriteKey(writer, keySerializer, key);
            WriteValue(writer, valueSerializer, key);
            writer.WriteEndElement();
        }

        /// <summary>
        /// Writes the xml key.
        /// </summary>
        /// <param name="writer">The xml writer.</param>
        /// <param name="keySerializer">The key serializer.</param>
        /// <param name="key">The key.</param>
        private void WriteKey(XmlWriter writer, XmlSerializer keySerializer, TKey key)
        {
            writer.WriteStartElement(KEY_TAG);
            keySerializer.Serialize(writer, key);
            writer.WriteEndElement();
        }

        /// <summary>
        /// Writes the xml value.
        /// </summary>
        /// <param name="writer">The xml writer.</param>
        /// <param name="valueSerializer">The value serializer.</param>
        /// <param name="key">The key.</param>
        private void WriteValue(XmlWriter writer, XmlSerializer valueSerializer, TKey key)
        {
            writer.WriteStartElement(VALUE_TAG);
            valueSerializer.Serialize(writer, key);
            writer.WriteEndElement();
        }

        #endregion
    }
}
