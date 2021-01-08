using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ConsoleApp1
{
  public static class Extensions
  {
    /// <summary>
    /// Converts an object to its serialized XML format.
    /// </summary>
    /// <typeparam name="T">The type of object we are operating on</typeparam>
    /// <param name="value">The object we are operating on</param>
    /// <param name="removeDefaultXmlNamespaces">Whether or not to remove the default XML namespaces from the output</param>
    /// <param name="omitXmlDeclaration">Whether or not to omit the XML declaration from the output</param>
    /// <param name="encoding">The character encoding to use</param>
    /// <returns>The XML string representation of the object</returns>
    public static string ToXmlString<T>(this T value, bool removeDefaultXmlNamespaces = true, bool omitXmlDeclaration = true, Encoding encoding = null) where T : class
    {
      XmlSerializerNamespaces namespaces = removeDefaultXmlNamespaces ? new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }) : null;

      var settings = new XmlWriterSettings();
      settings.Indent = true;
      settings.OmitXmlDeclaration = omitXmlDeclaration;
      settings.CheckCharacters = false;

      using (var stream = new StringWriterWithEncoding(encoding))
      using (var writer = XmlWriter.Create(stream, settings))
      {
        var serializer = new XmlSerializer(value.GetType());
        serializer.Serialize(writer, value, namespaces);
        return stream.ToString();
      }
    }

    /// <summary>
    /// Creates an object instance from the specified XML string.
    /// </summary>
    /// <typeparam name="T">The Type of the object we are operating on</typeparam>
    /// <param name="value">The object we are operating on</param>
    /// <param name="xml">The XML string to deserialize from</param>
    /// <returns>An object instance</returns>
    public static T FromXmlString<T>(this T value, string xml) where T : class
    {
      using (var reader = new StringReader(xml))
      {
        var serializer = new XmlSerializer(typeof(T));

        return (T)serializer.Deserialize(reader);
      }
    }
  }
}
