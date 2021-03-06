// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
// Copyright (c) 2008 Novell, Inc. (http://www.novell.com)
//
// Author:
//	Chris Toshok (toshok@ximian.com)
//

using System.ComponentModel;

namespace System.Windows {

	//[TypeConverter (typeof (ThicknessConverter))]
	public struct Thickness : IEquatable<Thickness> {
		double _left;
		double _top;
		double _right;
		double _bottom;
		
		public Thickness (double left, double top, double right, double bottom)
		{
			_left = left;
			_top = top;
			_right = right;
			_bottom = bottom;
		}

		public Thickness (double uniformLength)
		{
			_left =
				_top =
				_right =
				_bottom = uniformLength;
		}

		public double Left { 
			get { return _left; }
			set { _left = value; }
		}
		public double Top { 
			get { return _top; }
			set { _top = value; }
		}
		public double Right { 
			get { return _right; }
			set { _right = value; }
		}
		public double Bottom { 
			get { return _bottom; }
			set { _bottom = value; }
		}

		public bool Equals (Thickness thickness)
		{
			throw new NotImplementedException ();
		}

		public override bool Equals (object obj)
		{
			return Equals ((Thickness)obj);
		}

		public override int GetHashCode ()
		{
			throw new NotImplementedException ();
		}

		public static bool operator == (Thickness t1, Thickness t2)
		{
			throw new NotImplementedException ();
		}

		public static bool operator != (Thickness t1, Thickness t2)
		{
			throw new NotImplementedException ();
		}

		public override string ToString ()
		{
			throw new NotImplementedException ();
		}
	}

}