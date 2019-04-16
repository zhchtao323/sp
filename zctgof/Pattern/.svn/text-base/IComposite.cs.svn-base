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

	interface IComposite<T>
	{
		/// <summary>
		/// Gets or sets the Children in the current composite.
		/// </summary>
		/// <value>The children.</value>
		IComposite<T> Children { get; set; }
	}

	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T">The type</typeparam>
	[Serializable]
	public class Composite<T> : List<T>, IComposite<T>
	{
		#region public Composite<T> Children
		private Composite<T> _children;
		/// <summary>
		/// Gets or sets the Children in the current composite.
		/// </summary>
		/// <value>The children.</value>
		public Composite<T> Children
		{
			get
			{
                if (_children == null)
                {
                    _children = new Composite<T>();
                }
				return _children;
			}
			set { _children = value; }
		}
		#endregion

		#region IComposite<T> Members

		/// <summary>
		/// Gets or sets the Children in the current composite.
		/// </summary>
		/// <value>The children.</value>
		IComposite<T>  IComposite<T>.Children
		{
			get 
			{
				return Children;
			}
			set
			{
				if (value is Composite<T>)
					Children = value as Composite<T>;
				else
					throw new ArgumentException("Expecting object of type Composite<" + typeof(T) + ">" );
			}
		}

		#endregion
	}
}
