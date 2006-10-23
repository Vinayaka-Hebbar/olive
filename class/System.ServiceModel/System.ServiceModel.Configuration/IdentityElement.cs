//
// IdentityElement.cs
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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.IdentityModel.Tokens;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.MsmqIntegration;
using System.ServiceModel.PeerResolvers;
using System.ServiceModel.Security;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace System.ServiceModel.Configuration
{
	[MonoTODO]
	public sealed partial class IdentityElement
		 : ConfigurationElement
	{
		// Static Fields
		static ConfigurationPropertyCollection properties;
		static ConfigurationProperty certificate;
		static ConfigurationProperty certificate_reference;
		static ConfigurationProperty dns;
		static ConfigurationProperty rsa;
		static ConfigurationProperty service_principal_name;
		static ConfigurationProperty user_principal_name;

		static IdentityElement ()
		{
			properties = new ConfigurationPropertyCollection ();
			certificate = new ConfigurationProperty ("certificate",
				typeof (CertificateElement), null, null/* FIXME: get converter for CertificateElement*/, null,
				ConfigurationPropertyOptions.None);

			certificate_reference = new ConfigurationProperty ("certificateReference",
				typeof (CertificateReferenceElement), null, null/* FIXME: get converter for CertificateReferenceElement*/, null,
				ConfigurationPropertyOptions.None);

			dns = new ConfigurationProperty ("dns",
				typeof (DnsElement), null, null/* FIXME: get converter for DnsElement*/, null,
				ConfigurationPropertyOptions.None);

			rsa = new ConfigurationProperty ("rsa",
				typeof (RsaElement), null, null/* FIXME: get converter for RsaElement*/, null,
				ConfigurationPropertyOptions.None);

			service_principal_name = new ConfigurationProperty ("servicePrincipalName",
				typeof (ServicePrincipalNameElement), null, null/* FIXME: get converter for ServicePrincipalNameElement*/, null,
				ConfigurationPropertyOptions.None);

			user_principal_name = new ConfigurationProperty ("userPrincipalName",
				typeof (UserPrincipalNameElement), null, null/* FIXME: get converter for UserPrincipalNameElement*/, null,
				ConfigurationPropertyOptions.None);

			properties.Add (certificate);
			properties.Add (certificate_reference);
			properties.Add (dns);
			properties.Add (rsa);
			properties.Add (service_principal_name);
			properties.Add (user_principal_name);
		}

		public IdentityElement ()
		{
		}


		// Properties

		[ConfigurationProperty ("certificate",
			 Options = ConfigurationPropertyOptions.None)]
		public CertificateElement Certificate {
			get { return (CertificateElement) base [certificate]; }
		}

		[ConfigurationProperty ("certificateReference",
			 Options = ConfigurationPropertyOptions.None)]
		public CertificateReferenceElement CertificateReference {
			get { return (CertificateReferenceElement) base [certificate_reference]; }
		}

		[ConfigurationProperty ("dns",
			 Options = ConfigurationPropertyOptions.None)]
		public DnsElement Dns {
			get { return (DnsElement) base [dns]; }
		}

		protected override ConfigurationPropertyCollection Properties {
			get { return properties; }
		}

		[ConfigurationProperty ("rsa",
			 Options = ConfigurationPropertyOptions.None)]
		public RsaElement Rsa {
			get { return (RsaElement) base [rsa]; }
		}

		[ConfigurationProperty ("servicePrincipalName",
			 Options = ConfigurationPropertyOptions.None)]
		public ServicePrincipalNameElement ServicePrincipalName {
			get { return (ServicePrincipalNameElement) base [service_principal_name]; }
		}

		[ConfigurationProperty ("userPrincipalName",
			 Options = ConfigurationPropertyOptions.None)]
		public UserPrincipalNameElement UserPrincipalName {
			get { return (UserPrincipalNameElement) base [user_principal_name]; }
		}


	}

}
