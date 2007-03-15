//
// XmlSimpleDictionaryWriterTest.cs
//
// Author:
//	Atsushi Enomoto <atsushi@ximian.com>
//
// Copyright (C) 2005, 2007 Novell, Inc.  http://www.novell.com

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
using System.IO;
using System.Text;
using System.Xml;
using NUnit.Framework;

namespace MonoTests.System.Xml
{
	[TestFixture]
	public class XmlBinaryDictionaryWriterTest
	{
		[Test]
		public void UseCase1 ()
		{
Console.WriteLine ();

			MemoryStream ms = new MemoryStream ();
			XmlDictionaryWriter w = XmlDictionaryWriter.CreateBinaryWriter (ms, null, null);

			w.WriteStartDocument (true);
			w.WriteStartElement ("root");
			w.WriteAttributeString ("a", "");
			w.WriteComment ("");

			w.WriteWhitespace ("     ");
			w.WriteStartElement ("AAA", "urn:AAA");
			w.WriteEndElement ();
			w.WriteStartElement ("ePfix", "AAA", "urn:AAABBB");
			w.WriteEndElement ();
			w.WriteStartElement ("AAA");
			w.WriteCData ("CCC\u3005\u4E00CCC");
			w.WriteString ("AAA&AAA");
			w.WriteRaw ("DDD&DDD");
			w.WriteCharEntity ('\u4E01');
			w.WriteComment ("COMMENT");
			w.WriteEndElement ();
			w.WriteStartElement ("AAA");
			w.WriteAttributeString ("BBB", "bbb");
			// mhm, how namespace URIs are serialized then?
			w.WriteAttributeString ("pfix", "BBB", "urn:bbb", "bbbbb");
			// FIXME: They are not working
			// w.WriteAttributeString ("CCC", "urn:ccc", "ccccc");
			// w.WriteAttributeString ("DDD", "urn:ddd", "ddddd");
			// w.WriteAttributeString ("CCC", "urn:ddd", "cdcdc");

			// XmlLang
			w.WriteXmlAttribute ("lang", "ja");
			Assert.AreEqual ("ja", w.XmlLang, "XmlLang");

			// XmlSpace
			w.WriteStartAttribute ("xml", "space", "http://www.w3.org/XML/1998/namespace");
			w.WriteString ("pre");
			w.WriteString ("serve");
			w.WriteEndAttribute ();
			Assert.AreEqual (XmlSpace.Preserve, w.XmlSpace, "XmlSpace");

			w.WriteAttributeString ("xml", "base", "http://www.w3.org/XML/1998/namespace", "local:hogehoge");

			w.WriteString ("CCC");
			w.WriteBase64 (new byte [] {0x20, 0x20, 0x20, 0xFF, 0x80, 0x30}, 0, 6);
			w.WriteEndElement ();
			// this WriteEndElement() should result in one more
			// 0x3C, but .net does not output it.
			w.WriteEndElement ();
			w.WriteEndDocument ();

			w.Close ();

			Assert.AreEqual (usecase1_result, ms.ToArray ());
		}

		// $ : kind
		// ! : length
		// FIXME: see fixmes in the test itself.
		static readonly byte [] usecase1_result = new byte [] {
			// $!root$!  a....!__  ___.!AAA  $!urn:AA  A$$!ePfi
			0x40, 0x04, 0x72, 0x6F, 0x6F, 0x74, 0x04, 0x01,
			0x61, 0xA8, 0x02, 0x00, 0x98, 0x05, 0x20, 0x20,
			0x20, 0x20, 0x20, 0x40, 0x03, 0x41, 0x41, 0x41,
			0x08, 0x07, 0x75, 0x72, 0x6E, 0x3A, 0x41, 0x41,
			0x41, 0x01, 0x41, 0x05, 0x65, 0x50, 0x66, 0x69,// 40
			// x!AAA$!e  Pfix!urn  :AAABBB$  $!AAA$!C  CC......
			0x78, 0x03, 0x41, 0x41, 0x41, 0x09, 0x05, 0x65,
			0x50, 0x66, 0x69, 0x78, 0x0A, 0x75, 0x72, 0x6E,
			0x3A, 0x41, 0x41, 0x41, 0x42, 0x42, 0x42, 0x01,
			0x40, 0x03, 0x41, 0x41, 0x41, 0x98, 0x0C, 0x43,
			0x43, 0x43, 0xE3, 0x80, 0x85, 0xE4, 0xB8, 0x80,// 80
			// AAA$!DDD  $AAA$!DD  D$DDD...  ..$!COMM  ENT$$!AA
			0x43, 0x43, 0x43, 0x98, 0x07, 0x41, 0x41, 0x41,
			0x26, 0x41, 0x41, 0x41, 0x98, 0x07, 0x44, 0x44,
			0x44, 0x26, 0x44, 0x44, 0x44, 0x98, 0x03, 0xE4,
			0xB8, 0x81, 0x02, 0x07, 0x43, 0x4F, 0x4D, 0x4D,
			0x45, 0x4E, 0x54, 0x01, 0x40, 0x03, 0x41, 0x41,// 120
			// A$!BBB$!  bbb$!pfi  x!BBB$!b  bbbb$!xm  l!lang$!
			0x41, 0x04, 0x03, 0x42, 0x42, 0x42, 0x98, 0x03,
			0x62, 0x62, 0x62, 0x05, 0x04, 0x70, 0x66, 0x69,
			0x78, 0x03, 0x42, 0x42, 0x42, 0x98, 0x05, 0x62,
			0x62, 0x62, 0x62, 0x62, 0x05, 0x03, 0x78, 0x6D,
			0x6C, 0x04, 0x6C, 0x61, 0x6E, 0x67, 0x98, 0x02,// 160
			// ja$!xml!  space$!p  reserve
			0x6A, 0x61, 0x05, 0x03, 0x78, 0x6D, 0x6C, 0x05,
			0x73, 0x70, 0x61, 0x63, 0x65, 0x98, 0x08, 0x70,
			0x72, 0x65, 0x73, 0x65, 0x72, 0x76, 0x65, 0x05,
			0x03, 0x78, 0x6D, 0x6C, 0x04, 0x62, 0x61, 0x73,
			0x65, 0x98, 0x0E, 0x6C, 0x6F, 0x63, 0x61, 0x6C,// 200
			// ..hogehog  e$!pfix!  urn:bbb$  $CCC$!BA  SE64$
			0x3A, 0x68, 0x6F, 0x67, 0x65, 0x68, 0x6F, 0x67,
			0x65, 0x09, 0x04, 0x70, 0x66, 0x69, 0x78, 0x07,
			0x75, 0x72, 0x6E, 0x3A, 0x62, 0x62, 0x62, 0x98,
			0x03, 0x43, 0x43, 0x43, 0x9F, 0x06, 0x20, 0x20,
			0x20, 0xFF, 0x80, 0x30, 0x01,
			};

		[Test]
		[ExpectedException (typeof (ArgumentException))]
		public void ProcessingInstructions ()
		{
			MemoryStream ms = new MemoryStream ();
			XmlDictionaryWriter w = XmlDictionaryWriter.CreateBinaryWriter (ms, null, null);
			w.WriteStartDocument ();
			w.WriteProcessingInstruction ("myPI", "myValue");
		}

		[Test]
		public void UseCase2 ()
		{
			XmlDictionary dic = new XmlDictionary ();
			MemoryStream ms = new MemoryStream ();
			XmlDictionaryWriter w = XmlDictionaryWriter.CreateBinaryWriter (ms, dic, null);
			XmlDictionaryString empty = dic.Add (String.Empty);
			// empty ns
			w.WriteStartElement (dic.Add ("FOO"), empty);
			// non-dic string
			w.WriteStartElement ("BAR");
			// second time ns
			w.WriteStartElement (dic.Add ("FOO"), empty);
			// first time dic string but prior non-dic name existed
			w.WriteStartElement (dic.Add ("BAR"), empty);
			w.WriteEndElement ();
			w.WriteEndElement ();
			w.WriteEndElement ();
			// dicstr w/ ns with empty prefix
			w.WriteStartElement (dic.Add ("BAR"), dic.Add ("urn:bar"));
			// with prefix
			w.WriteStartElement ("ppp", dic.Add ("BAR"), dic.Add ("urn:bar"));
			w.WriteChars (new char [] {'x', 'y', 'z'}, 0, 3);
//			w.WriteString ("xyz"); // the same as WriteChars()
			w.WriteEndElement ();
			w.WriteString ("bbbb");
			w.WriteCData ("ccc");
			w.WriteValue (new Guid ("11112222333344445555666677778888"));
			w.WriteEndElement ();
			w.WriteStartElement ("FOO");
			w.WriteStartAttribute ("AAA");
			w.WriteValue (new Guid ("11112222333344445555666677778888"));
			w.WriteEndAttribute ();
			w.WriteStartAttribute ("BBB");
			w.WriteValue (TimeSpan.Zero);
			w.WriteEndAttribute ();
			w.WriteStartAttribute ("CC");
			w.WriteValue (new UniqueId ("uuid-00000000-0000-0000-0000-000000000000-1"));
			w.WriteEndAttribute ();
			w.WriteStartElement ("XX");
			w.WriteValue (true);
			w.WriteValue (false);
			w.WriteEndElement ();
			w.WriteStartElement ("xx", "aaa", "urn:zzz");
			w.WriteEndElement ();
			w.WriteEndElement ();

			w.Close ();

			Assert.AreEqual (usecase2_result, ms.ToArray ());
		}

		// $ : kind
		// / : especially. EndElement
		// ! : length
		// @ : dictionary index
		// ^ : missing ns decl?
		// FIXME: see fixmes in the test itself.
		static readonly byte [] usecase2_result = new byte [] {
			// $@$!BAR$  @$@///$@  ^@$!ppp!  $!ppp@$!  xyz$!bbb
			0x42, 2, 0x40, 3, 0x42, 0x41, 0x52, 0x42,
			2, 0x42, 4, 1, 1, 1, 0x42, 4,
			10, 6, 0x43, 3, 0x70, 0x70, 0x70, 4,
			11, 3, 0x70, 0x70, 0x70, 6, 0x99, 3,
			0x78, 0x79, 0x7A, 0x98, 4, 0x62, 0x62, 0x62,
			// b$!ccc$G  UIDGUIDG  UIDGUID$  !FOO$!GU  IDGUIDGU
			0x62, 0x98, 3, 0x63, 0x63, 0x63, 0xB1, 0x22,
			0x22, 0x11, 0x11, 0x33, 0x33, 0x44, 0x44, 0x55,
			0x55, 0x66, 0x66, 0x77, 0x77, 0x88, 0x88, 0x40,
			3, 0x46, 0x4F, 0x4F, 0x04, 3, 0x41, 0x41,
			0x41, 0xB0, 0x22, 0x22, 0x11, 0x11, 0x33, 0x33,
			// IDGUIDGU  ID$!BBB$T  IMESPAN  $!CC$!UN  IQUEIDUN
			0x44, 0x44, 0x55, 0x55, 0x66, 0x66, 0x77, 0x77,
			0x88, 0x88, 0x04, 3, 0x42, 0x42, 0x42, 0xAE,
			0, 0, 0, 0, 0, 0, 0, 0,
			0x04, 2, 0x43, 0x43, 0x98, 0x2B, 0x75, 0x75,
			0x69, 0x64, 0x2D, 0x30, 0x30, 0x30, 0x30, 0x30,
			// IQUEIDUN  IQUEIDUN  IQUEIDUN  IQUEID..  .$!XX$$$!
			0x30, 0x30, 0x30, 0x2D, 0x30, 0x30, 0x30, 0x30,
			0x2D, 0x30, 0x30, 0x30, 0x30, 0x2D, 0x30, 0x30,
			0x30, 0x30, 0x2D, 0x30, 0x30, 0x30, 0x30, 0x30,
			0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x2D,
			0x31, 0x40, 2, 0x58, 0x58, 0x86, 0x85, 0x41, 2,
			// xx!aaa$!x  x!urn:xxx
			0x78, 0x78, 3, 0x61, 0x61, 0x61, 0x09, 2, 0x78,
			0x78, 0x07, 0x75, 0x72, 0x6E, 0x3A, 0x7A, 0x7A,
			0x7A, 1, 1, 1
			};

		[Test]
		public void WriteDictionaryStringWithNullDictionary ()
		{
			MemoryStream ms = new MemoryStream ();
			XmlDictionaryWriter w = XmlDictionaryWriter.CreateBinaryWriter (ms, null, null);
			XmlDictionary dic = new XmlDictionary ();
			w.WriteStartElement (dic.Add ("FOO"), XmlDictionaryString.Empty);
		}

		class MyWriterSession1 : XmlBinaryWriterSession
		{
			int count;

			public override bool TryAdd (XmlDictionaryString d, out int idx)
			{
				base.TryAdd (d, out idx);
				Assert.AreEqual (count, idx, "#x1-" + count);
				if (count++ == 0)
					Assert.AreEqual (String.Empty, d.Value, "#x2");
				else
					Assert.AreEqual ("FOO", d.Value, "#x3");
				return true;
			}
		}

		[Test]
		public void WriteDictionaryStringWithSameDictionary ()
		{
			MemoryStream ms = new MemoryStream ();
			XmlDictionary dic = new XmlDictionary ();
			XmlDictionaryWriter w = XmlDictionaryWriter.CreateBinaryWriter (ms, dic, null);
			w.WriteStartElement (dic.Add ("FOO"), XmlDictionaryString.Empty);
			w.Close ();
			Assert.AreEqual (new byte [] {0x42, 0, 1}, ms.ToArray ());
		}

		[Test]
		public void WriteDictionaryStringWithDifferentDictionary () // it actually works
		{
			MemoryStream ms = new MemoryStream ();
			XmlBinaryWriterSession session = new XmlBinaryWriterSession ();
			XmlDictionaryWriter w = XmlDictionaryWriter.CreateBinaryWriter (ms, new XmlDictionary (), session);
			XmlDictionary dic = new XmlDictionary ();
			w.WriteStartElement (dic.Add ("FOO"), XmlDictionaryString.Empty);
			w.Close ();
			Assert.AreEqual (new byte [] {0x42, 1, 1}, ms.ToArray ());
		}

		[Test]
		public void IndicesFromDictionaryAndSession ()
		{
			// So, I found out the solution for the indices puzzle.
			MemoryStream ms = new MemoryStream ();
			XmlBinaryWriterSession session = new XmlBinaryWriterSession ();
			XmlDictionary dic = new XmlDictionary ();
			XmlDictionaryWriter w = XmlDictionaryWriter.CreateBinaryWriter (ms, dic, session);
			XmlDictionary dic2 = new XmlDictionary ();
			XmlDictionary dic3 = new XmlDictionary ();
			w.WriteStartElement (dic.Add ("FOO"), XmlDictionaryString.Empty);
			w.WriteStartElement (dic2.Add ("FOO"), XmlDictionaryString.Empty);
			w.WriteStartElement (dic3.Add ("FOO"), XmlDictionaryString.Empty);
			w.WriteStartElement (dic2.Add ("BAR"), XmlDictionaryString.Empty);
			w.WriteStartElement (dic.Add ("BAR"), XmlDictionaryString.Empty);
			w.Close ();
			// ... so, looks like even indices are for dictionary, 
			// and odd indices are for session.
			byte [] bytes = new byte [] {
				0x42, 0, 0x42, 1, 0x42, 1,0x42, 3,
				0x42, 2, 1, 1, 1, 1, 1};
			Assert.AreEqual (bytes, ms.ToArray ());
		}

		[Test]
		public void UseStandardSession ()
		{
			MemoryStream ms = new MemoryStream ();
			XmlBinaryWriterSession session =
				new XmlBinaryWriterSession ();
			XmlDictionary dic = new XmlDictionary ();
			XmlDictionaryWriter w = XmlDictionaryWriter.CreateBinaryWriter (ms, dic, session);
			w.WriteStartElement (dic.Add ("FOO"), XmlDictionaryString.Empty);
			w.WriteStartElement (dic.Add ("blah"), XmlDictionaryString.Empty);
			w.WriteStartElement (dic.Add ("blabla"), XmlDictionaryString.Empty);
			w.Close ();
			Assert.AreEqual (new byte [] {0x42, 0, 0x42, 2, 0x42, 4, 1, 1, 1}, ms.ToArray ());
		}

		[Test]
		public void UseStandardSession2 ()
		{
			MemoryStream ms = new MemoryStream ();
			XmlBinaryWriterSession session =
				new XmlBinaryWriterSession ();
			XmlDictionary dic = new XmlDictionary ();
			XmlDictionaryString x = dic.Add ("urn:foo");
			XmlDictionaryWriter w = XmlDictionaryWriter.CreateBinaryWriter (ms, dic, session);
			w.WriteStartElement (dic.Add ("FOO"), x);
			w.WriteStartElement (dic.Add ("blah"), x);
			w.WriteStartElement (dic.Add ("blabla"), x);
			w.Close ();
			Assert.AreEqual (new byte [] {0x42, 2, 0x0A, 0, 0x42, 4, 0x42, 6, 1, 1, 1}, ms.ToArray (), "#1");

			XmlDictionaryString ds;
			Assert.IsTrue (dic.TryLookup (0, out ds), "#2-1");
			Assert.AreEqual ("urn:foo", ds.Value, "#2-2");
			Assert.AreEqual (0, ds.Key, "#2-3");
			Assert.IsTrue (dic.TryLookup (1, out ds), "#3-1");
			Assert.AreEqual ("FOO", ds.Value, "#3-2");
			Assert.AreEqual (1, ds.Key, "#3-3");
			Assert.IsTrue (dic.TryLookup (2, out ds), "#4-1");
			Assert.AreEqual ("blah", ds.Value, "#4-2");
			Assert.AreEqual (2, ds.Key, "#4-3");
			Assert.IsTrue (dic.TryLookup (3, out ds), "#5-1");
			Assert.AreEqual ("blabla", ds.Value, "#5-2");
			Assert.AreEqual (3, ds.Key, "#5-3");
		}

		class MyWriterSession2 : XmlBinaryWriterSession
		{
			int count;

			public override bool TryAdd (XmlDictionaryString d, out int idx)
			{
				// do nothing
				idx = d.Value.Length;
				return true;
			}
		}

		[Test]
		public void UseNOPSession ()
		{
			MemoryStream ms = new MemoryStream ();
			MyWriterSession2 session = new MyWriterSession2 ();
			XmlDictionary dic = new XmlDictionary ();
			XmlDictionaryWriter w = XmlDictionaryWriter.CreateBinaryWriter (ms, dic, session);
			w.WriteStartElement (dic.Add ("FOO"), XmlDictionaryString.Empty);
			w.WriteStartElement (dic.Add ("blah"), XmlDictionaryString.Empty);
			w.WriteStartElement (dic.Add ("blabla"), XmlDictionaryString.Empty);
			w.Close ();
			Assert.AreEqual (new byte [] {0x42, 0, 0x42, 2, 0x42, 4, 1, 1, 1}, ms.ToArray ());
		}

		[Test]
		public void WriteElementWithNS ()
		{
			byte [] bytes = new byte [] {
				0x42, 0, 10, 2, 0x98, 3, 0x61, 0x61,
				0x61, 0x42, 0, 0x42, 2, 1, 1, 1};
			XmlDictionaryString ds;
			MemoryStream ms = new MemoryStream ();
			XmlDictionary dic = new XmlDictionary ();
			XmlDictionaryWriter w = XmlDictionaryWriter.CreateBinaryWriter (ms, dic, null);
			w.WriteStartElement (dic.Add ("FOO"), dic.Add ("foo"));
			Assert.IsTrue (dic.TryLookup ("foo", out ds), "#1");
			Assert.AreEqual (1, ds.Key, "#2");
			w.WriteString ("aaa");
			w.WriteStartElement (dic.Add ("FOO"), dic.Add ("foo"));
			w.WriteStartElement (dic.Add ("foo"), dic.Add ("foo"));
			w.Close ();
			Assert.AreEqual (bytes, ms.ToArray (), "result");
/*
			byte [] bytes2 = new byte [] {
				0x42, 1, 10, 2, 0x98, 3, 0x61, 0x61,
				0x61, 0x42, 0, 0x42, 2, 1, 1, 1};
				
			XmlDictionaryReader dr = XmlDictionaryReader.CreateBinaryReader (new MemoryStream (bytes2), dic, new XmlDictionaryReaderQuotas ());
			try {
				dr.Read ();
				Assert.Fail ("dictionary index 1 should be regarded as invalid.");
			} catch (XmlException) {
			}
*/
		}

		[Test]
		public void Beyond128DictionaryEntries ()
		{
			XmlDictionaryString ds;
			MemoryStream ms = new MemoryStream ();
			XmlDictionary dic = new XmlDictionary ();
			for (int i = 0; i < 260; i++)
				Assert.AreEqual (i, dic.Add ("n" + i).Key, "d");
			XmlDictionary dic2 = new XmlDictionary ();
			XmlBinaryWriterSession session = new XmlBinaryWriterSession ();
			int idx;
			for (int i = 0; i < 260; i++)
				session.TryAdd (dic2.Add ("n" + i), out idx);
			XmlDictionaryWriter w = XmlDictionaryWriter.CreateBinaryWriter (ms, dic, session);
			w.WriteStartElement (dic.Add ("n128"), dic.Add ("n129"));
			w.WriteStartElement (dic2.Add ("n130"), dic2.Add ("n131"));
			w.WriteStartElement (dic.Add ("n132"), dic2.Add ("n133"));
			w.WriteStartElement (dic.Add ("n256"), dic2.Add ("n256"));
			w.Close ();

			byte [] bytes = new byte [] {
				// so, when it went beyond 128, the index
				// becomes 2 bytes, where
				// - the first byte always becomes > 80, and
				// - the second byte becomes (n / 0x80) * 2.
				0x42, 0x80, 2, 0x0A, 0x82, 2,
				0x42, 0x85, 2, 0x0A, 0x87, 2,
				0x42, 0x88, 2, 0x0A, 0x8B, 2,
				0x42, 0x80, 4, 0x0A, 0x81, 4,
				1, 1, 1, 1};
			Assert.AreEqual (bytes, ms.ToArray (), "result");
		}

		[Test]
		public void GlobalAttributes ()
		{
			XmlDictionaryString ds;
			MemoryStream ms = new MemoryStream ();
			XmlDictionary dic = new XmlDictionary ();
			int idx;
			XmlDictionaryWriter w = XmlDictionaryWriter.CreateBinaryWriter (ms, dic, null);
			dic.Add ("n1");
			dic.Add ("urn:foo");
			dic.Add ("n2");
			dic.Add ("n3");
			dic.Add ("n4");
			dic.Add ("urn:bar");
			dic.Add ("n7");
			w.WriteStartElement (dic.Add ("n1"), dic.Add ("urn:foo"));
			w.WriteAttributeString (dic.Add ("n2"), dic.Add ("urn:bar"), String.Empty);
			w.WriteAttributeString (dic.Add ("n3"), dic.Add ("urn:foo"), "v");
			w.WriteAttributeString ("aaa", dic.Add ("n4"), dic.Add ("urn:bar"), String.Empty);
			w.WriteAttributeString ("bbb", "n5", "urn:foo", String.Empty);
			w.WriteAttributeString ("n6", String.Empty);
			w.WriteAttributeString (dic.Add ("n7"), XmlDictionaryString.Empty, String.Empty); // local attribute
			w.WriteAttributeString ("bbb", "n8", "urn:foo", String.Empty); // xmlns:bbb mapping already exists (n5), and written just once
			w.Close ();

			// 0x0C nameidx (value) 0x0D nameidx (value)
			// 0x07 (prefix) nameidx (value)
			// 0x05 (prefix) (name) (value)
			// 0x04...  0x06...  0x05...
			// 0x0A nsidx
			// 0x0B (prefix) nsidx
			// 0x0B...  0x0B...
			// 0x09 (prefix) (ns)
			byte [] bytes = new byte [] {
				// $@$@$$@$  !v$!aaa@
				// $@!bbb!n  5$$@$!a@
				// $!aaa!$!  bbb$urn:foo$
				0x42, 0,
				0x0C, 4, 0xA8,
				0x0D, 6, 0x98, 1, 0x76,
				0x07, 3, 0x61, 0x61, 0x61, 8, 0xA8, // 16
				0x05, 3, 0x62, 0x62, 0x62, 2, 0x6E, 0x35, 0xA8,
				0x04, 2, 0x6E, 0x36, 0xA8, // 30
				0x06, 12, 0xA8,
				0x05, 3, 0x62, 0x62, 0x62, 2, 0x6E, 0x38, 0xA8,
				0x0A, 2,
				0x0B, 1, 0x61, 10, // 48
				0x0B, 1, 0x62, 2,
				0x0B, 3, 0x61, 0x61, 0x61, 10,
				0x09, 3, 0x62, 0x62, 0x62,
				0x07, 0x75, 0x72, 0x6E, 0x3A, 0x66, 0x6F, 0x6F,
				1};
			Assert.AreEqual (bytes, ms.ToArray (), "result");
		}

		[Test]
		public void WriteAttributeXmlns ()
		{
			// equivalent to WriteXmlnsAttribute()
			XmlDictionaryString ds;
			MemoryStream ms = new MemoryStream ();
			XmlDictionary dic = new XmlDictionary ();
			XmlDictionaryWriter w = XmlDictionaryWriter.CreateBinaryWriter (ms, dic, null);
			w.WriteStartElement ("root");
			w.WriteAttributeString ("xmlns", "foo", "http://www.w3.org/2000/xmlns/", "urn:foo");
			w.WriteAttributeString (dic.Add ("xmlns"), dic.Add ("http://www.w3.org/2000/xmlns/"), "urn:bar");
			w.WriteAttributeString ("a", String.Empty);
			w.Close ();
			byte [] bytes = new byte [] {
				// 40 (root) 04 (a) A8
				// 09 (foo) (urn:foo) 08 (urn:bar)
				0x40, 4, 0x72, 0x6F, 0x6F, 0x74,
				0x04, 1, 0x61, 0xA8,
				0x09, 3, 0x66, 0x6F, 0x6F, 7, 0x75, 0x72, 0x6E, 0x3A, 0x66, 0x6F, 0x6F,
				0x08, 7, 0x75, 0x72, 0x6E, 0x3A, 0x62, 0x61, 0x72, 1
				};
			Assert.AreEqual (bytes, ms.ToArray ());
		}

		[Test]
		[ExpectedException (typeof (InvalidOperationException))]
		public void WriteAttributeWithoutElement ()
		{
			XmlDictionaryString ds;
			MemoryStream ms = new MemoryStream ();
			XmlDictionaryWriter w = XmlDictionaryWriter.CreateBinaryWriter (ms, null, null);
			w.WriteAttributeString ("foo", "value");
		}

		[Test]
		[ExpectedException (typeof (ArgumentException))]
		public void OverwriteXmlnsUri ()
		{
			XmlDictionaryString ds;
			MemoryStream ms = new MemoryStream ();
			XmlDictionaryWriter w = XmlDictionaryWriter.CreateBinaryWriter (ms, null, null);
			w.WriteStartElement ("root");
			w.WriteAttributeString ("xmlns", "foo", "urn:thisCausesError", "urn:foo");
		}
	}
}
