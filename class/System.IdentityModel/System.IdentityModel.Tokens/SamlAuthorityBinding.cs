//
// SamlAuthorityBinding.cs
//
// Author:
//	Atsushi Enomoto <atsushi@ximian.com>
//
// Copyright (C) 2006 Novell, Inc.  http://www.novell.com
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
using System.Collections.Generic;
using System.Xml;
using System.Runtime.Serialization;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;

namespace System.IdentityModel.Tokens
{
	[DataContract]
	public class SamlAuthorityBinding
	{
		XmlQualifiedName kind;
		string binding, location;
		bool is_readonly;

		public SamlAuthorityBinding ()
		{
		}

		public SamlAuthorityBinding (XmlQualifiedName kind, string binding, string location)
		{
			this.kind = kind;
			this.binding = binding;
			this.location = location;
		}

		[DataMember]
		public XmlQualifiedName AuthorityKind {
			get { return kind; }
			set {
				CheckReadOnly ();
				kind = value;
			}
		}

		[DataMember]
		public string Binding {
			get { return binding; }
			set {
				CheckReadOnly ();
				binding = value;
			}
		}

		[DataMember]
		public string Location {
			get { return location; }
			set {
				CheckReadOnly ();
				location = value;
			}
		}

		public bool IsReadOnly {
			get { return is_readonly; }
		}

		void CheckReadOnly ()
		{
			if (IsReadOnly)
				throw new InvalidOperationException ("This object is read-only.");
		}

		public void MakeReadOnly ()
		{
			is_readonly = true;
		}

		[MonoTODO]
		public virtual void ReadXml (XmlDictionaryReader reader,
			SamlSerializer samlSerializer,
			SecurityTokenSerializer keyInfoSerializer,
			SecurityTokenResolver outOfBandTokenResolver)
		{
			throw new NotImplementedException ();
		}

		[MonoTODO]
		public virtual void WriteXml (
			XmlDictionaryWriter writer,
			SamlSerializer samlSerializer,
			SecurityTokenSerializer keyInfoSerializer)
		{
			throw new NotImplementedException ();
		}
	}
}
