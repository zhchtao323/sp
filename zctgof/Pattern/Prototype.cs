/* Copyright (c) 2006 by M Aamir Maniar 
 * 
 * Permission is hereby granted, free of charge, to any person obtaining 
 * a copy of this software and associated documentation files (the 
 * "Software"), to deal in the Software without restriction, including 
 * without limitation the rights to use, copy, modify, merge, publish, 
 * distribute, sublicense, and/or sell copies of the Software, and to 
 * permit persons to whom the Software is furnished to do so, subject to 
 * the following conditions:
 * 
 * The above copyright notice and this permission notice shall be 
 * included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 * 
 * */

namespace ZCT.Pattern
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using System.IO;
	using System.ComponentModel;
	using System.Runtime.Serialization.Formatters.Binary;
	using System.Runtime.Serialization;

	public class Prototype<T> : ICloneable
	{
		T _objToClone = default(T);

		public Prototype(T objectToClone)
		{
			if (objectToClone == null)
				throw new NullReferenceException();
			_objToClone = objectToClone;
		}

		#region ICloneable Members
	 	object ICloneable.Clone()
		{
			return this.Clone();
		}
		#endregion

		public T Clone()
		{
			MemoryStream buffer = new MemoryStream();
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(buffer, _objToClone);
			buffer.Position = 0;
			return (T) formatter.Deserialize(buffer);
		}

		public static T Clone(T value)
		{
			MemoryStream buffer = new MemoryStream();
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(buffer, value);
			buffer.Position = 0;
			return (T)formatter.Deserialize(buffer);
		}
	
	}
}
