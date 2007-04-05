//
// NativePolicyElement.cs
//
// Author:
//	Atsushi Enomoto <atsushi@ximian.com>
//
// Copyright (C) 2007 Novell, Inc.  http://www.novell.com
//
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
using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Xml;

namespace System.IdentityModel.Selectors
{
	[StructLayout (LayoutKind.Sequential)]
	class NativePolicyElement
	{
		// This field order must be fixed for win32 API interop:
		string target;
		string issuer;
		string parameters;
		string privacy_link;
		int privacy_ver;
		bool is_managed;

		public NativePolicyElement (
			XmlElement target, XmlElement issuer,
			Collection<XmlElement> parameters,
			Uri privacyNoticeLink,
			int privacyNoticeVersion,
			bool isManagedIssuer)
		{
			if (target == null)
				throw new ArgumentException ("target");
			if (issuer == null)
				throw new ArgumentException ("issuer");
			if (parameters == null)
				throw new ArgumentException ("parameters");
			this.target = target.OuterXml;
			this.issuer = issuer.OuterXml;
			foreach (XmlElement el in parameters)
				this.parameters += el.OuterXml;
			this.privacy_link = privacyNoticeLink != null ? privacyNoticeLink.ToString () : String.Empty;
			privacy_ver = privacyNoticeVersion;
			is_managed = isManagedIssuer;
		}
	}
}