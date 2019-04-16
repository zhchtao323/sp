using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
//using System.Runtime.Serialization.sa;
using System.Text;

namespace CoreWebLibrary.Utility
{
   public class Serialization
   {
      /// <summary>
      ///  Binary Serializes an object
      /// </summary>
      /// <param name="obj">The object to serialize</param>
      /// <returns>byte array</returns>
      public static byte[] BinarySerialize(Object obj)
      {
         byte[] serializedObject;
         MemoryStream ms = new MemoryStream();
         BinaryFormatter b = new BinaryFormatter();
         b.Serialize(ms, obj);
         ms.Seek(0, 0);
         serializedObject = ms.ToArray();
         ms.Close();

         return serializedObject;
      }

      /// <summary>
      ///  Binary DeSerializes an object
      /// </summary>
      /// <param name="obj">The object to serialize</param>
      /// <returns>The deserialized object</returns>
      public static T BinaryDeSerialize<T>(byte[] serializedObject)
      {
         MemoryStream ms = new MemoryStream();
         ms.Write(serializedObject, 0, serializedObject.Length);
         ms.Seek(0, 0);
         BinaryFormatter b = new BinaryFormatter();
         Object obj = b.Deserialize(ms);
         ms.Close();

         return (T)obj;
      }

      /// <summary>
      /// Serialize the object to a file
      /// </summary>
      /// <param name="obj">Object to be serialized.Ensure that is Serializable !</param>
      /// <param name="filePath">File( with the entire file path) where the object will be serialized to</param>
      /// <returns>True on successful serialization.</returns>
      public static bool FileSerialize(Object obj, string filePath)
      {
         FileStream fileStream = null;

         try
         {
            fileStream = new FileStream(filePath, FileMode.Create);
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(fileStream, obj);
         }
         catch
         {
            throw;
         }
         finally
         {
            if (fileStream != null)
               fileStream.Close();
         }

         return true;
      }

      /// <summary>
      /// Deserializes a binary formatted object.
      /// </summary>
      /// <param name="filePath">Full path of the file</param>
      /// <returns>The deserialized object</returns>
      public static T FileDeSerialize<T>(string filePath)
      {
         FileStream fileStream = null;
         Object obj;
         try
         {
            if (File.Exists(filePath) == false)
               throw new FileNotFoundException("The file was not found.", filePath);

            fileStream = new FileStream(filePath, FileMode.Open);
            BinaryFormatter b = new BinaryFormatter();
            obj = b.Deserialize(fileStream);
         }
         catch
         {
            throw;
         }
         finally
         {
            if (fileStream != null)
               fileStream.Close();
         }

         return (T)obj;
      }

      /// <summary>
      /// Serializes the passed object using SOAP serialization
      /// </summary>
      /// <param name="obj">The object to serialize</param>
      /// <param name="encodingType">The encoding type to use</param>
      /// <returns>A string representing the serialized object.</returns>
      /// <remarks>encodingType is abstract: pass in a subtype of Encoding, for example instantiate: 
      /// System.Text.UTF8Encoding</remarks>
      public static string SoapMemoryStreamSerialization(object obj, Encoding encodingType)
      {
         string xmlResult;

         using (Stream stream = new MemoryStream())
         {
            try
            {
               SoapFormatter sf = new SoapFormatter();
               sf.Serialize(stream, obj);
            }
            catch
            {
               throw;
            }

            stream.Position = 0;
            byte[] b = new byte[stream.Length];
            stream.Read(b, 0, (int)stream.Length);

            xmlResult = encodingType.GetString(b, 0, b.Length);
         }

         return xmlResult;
      }

      /// <summary>
      /// Deserailizes a SOAP serialized object
      /// </summary>
      /// <param name="input">The XML string to deserialize.</param>
      /// <param name="encodingType">The encoding type to use</param>
      /// <returns>The deserialized object.</returns>
      /// <remarks>encodingType is abstract: pass in a subtype of Encoding, for example instantiate: 
      /// System.Text.UTF8Encoding</remarks>
      public T SoapDeserailization<T>(string input, System.Text.Encoding encodingType)
      {
         Object obj = null;

         using (StringReader sr = new StringReader(input))
         {
            byte[] b;
            b = encodingType.GetBytes(input);

            Stream stream = new MemoryStream(b);

            try
            {
               SoapFormatter sf = new SoapFormatter();
               obj = (object)sf.Deserialize(stream);
            }
            catch
            {
               throw;
            }
         }

         return (T)obj;
      }
   }
}
