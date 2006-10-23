//
// TypedMessageConverterTest.cs
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
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Xml;
using NUnit.Framework;

namespace MonoTests.System.ServiceModel.Description
{
	[TestFixture]
	public class TypedMessageConverterTest
	{
		[Test]
		[ExpectedException (typeof (ArgumentException))]
		public void InvalidArgumentType ()
		{
			TypedMessageConverter.Create (
				typeof (int), "http://tempuri.org/MyTest");
		}

		[Test]
		// It is imported from samples/typed-message-converter.
		public void StandardToMessage ()
		{
			TypedMessageConverter c = TypedMessageConverter.Create (
				typeof (Test1), "http://tempuri.org/MyTest");
			Message msg = c.ToMessage (new Test1 ());
			StringWriter sw = new StringWriter ();
			using (XmlWriter w = XmlWriter.Create (sw)) {
				msg.WriteMessage (w);
			}

			XmlDocument doc = new XmlDocument ();
			doc.LoadXml (sw.ToString ());

			XmlNamespaceManager nss = new XmlNamespaceManager (doc.NameTable);
			nss.AddNamespace ("s", "http://www.w3.org/2003/05/soap-envelope");
			nss.AddNamespace ("t", "http://tempuri.org/");
			XmlElement el = doc.SelectSingleNode ("/s:Envelope/s:Body/t:Test1", nss) as XmlElement;
			Assert.IsNotNull (el, "#1");
			Assert.IsNotNull (el.SelectSingleNode ("t:body2", nss), "#2");
			Assert.IsNotNull (el.SelectSingleNode ("t:echo", nss), "#3");
		}

		[Test]
		[Category ("NotWorking")]
		public void StandardRoundtrip ()
		{
			TypedMessageConverter c = TypedMessageConverter.Create (
				typeof (Test1), "http://tempuri.org/MyTest");
			Message msg = c.ToMessage (new Test1 ());
			Test1 t1 = (Test1) c.FromMessage (msg);
		}
	}

	[MessageContract]
	public class Test1
	{
		[MessageBodyMember]
		public Echo echo = new Echo ();

		[MessageBodyMember]
		public string body2 = "TEST body";
	}

	[DataContract]
	public class Echo
	{
		[DataMember]
		public string msg = "default";
	}
}
