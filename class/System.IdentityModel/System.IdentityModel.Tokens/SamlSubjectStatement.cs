//
// SamlSubjectStatement.cs
//
// Author:
//	Atsushi Enomoto <atsushi@ximian.com>
//
// Copyright (C) 2005 Novell, Inc.  http://www.novell.com
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
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;

namespace System.IdentityModel.Tokens
{
	public abstract class SamlSubjectStatement : SamlStatement
	{
		bool is_readonly;
		SamlSubject subject;

		protected SamlSubjectStatement ()
		{
		}

		protected SamlSubjectStatement (SamlSubject samlSubject)
		{
			if (samlSubject == null)
				throw new ArgumentNullException ("samlSubject");

			subject = samlSubject;
		}

		public SamlSubject SamlSubject {
			get { return subject; }
			set {
				CheckReadOnly ();
				subject = value;
			}
		}

		public override bool IsReadOnly {
			get { return is_readonly; }
		}

		[MonoTODO]
		public override IAuthorizationPolicy CreatePolicy (
			ClaimSet issuer, SamlSecurityTokenAuthenticator samlAuthenticator)
		{
			throw new NotImplementedException ();
		}

		private void CheckReadOnly ()
		{
			if (is_readonly)
				throw new InvalidOperationException ("This SAML assertion is read-only.");
		}

		public override void MakeReadOnly ()
		{
			is_readonly = true;
		}

		protected abstract void AddClaimsToList (IList<Claim> claims);

		[MonoTODO]
		protected void SetSubject (SamlSubject samlSubject)
		{
			throw new NotImplementedException ();
		}
	}
}
