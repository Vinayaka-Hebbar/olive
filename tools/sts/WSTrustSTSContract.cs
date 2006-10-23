//
// WSTrustSTSContract.cs
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

using System.Collections.ObjectModel;
using System.IdentityModel.Claims;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Net.Security;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Xml;

namespace System.ServiceModel.Description
{
	internal class WSTrustSecurityTokenServiceProxy
		: ClientBase<IWSTrustSecurityTokenService>, IWSTrustSecurityTokenService
	{
		public WSTrustSecurityTokenServiceProxy (Binding binding, EndpointAddress address)
			: base (binding, address)
		{
		}

		public Message Issue (Message request)
		{
			return Channel.Issue (request);
		}

		public Message Renew (Message request)
		{
			return Channel.Issue (request);
		}

		public Message Cancel (Message request)
		{
			return Channel.Issue (request);
		}

		public Message Validate (Message request)
		{
			return Channel.Issue (request);
		}
	}

	[ServiceContract]
	internal interface IWSTrustSecurityTokenService
	{
		[OperationContract (Action = Constants.WstIssueAction, ReplyAction = Constants.WstIssueReplyAction, ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		Message Issue (Message request);

		[OperationContract (Action = Constants.WstRenewAction, ReplyAction = Constants.WstRenewReplyAction, ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		Message Renew (Message request);

		[OperationContract (Action = Constants.WstCancelAction, ReplyAction = Constants.WstCancelReplyAction, ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		Message Cancel (Message request);

		[OperationContract (Action = Constants.WstValidateAction, ReplyAction = Constants.WstValidateReplyAction, ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		Message Validate (Message request);

		// FIXME: do we need KET here?
	}

	class WstRequestSecurityToken
	{
		public string RequestType;
		public WspAppliesTo AppliesTo;

		public SecurityToken Entropy;

		public int KeySize;
		public string KeyType;
		public string ComputedKeyAlgorithm;
	}

	class WstEntropy
	{
		public object Token;
	}

	class WspAppliesTo
	{
		public WsaEndpointReference EndpointReference;
	}

	class WsaEndpointReference
	{
		public string Address;
	}

	class WstBinarySecret
	{
		public string Id;

		public string Type;

		public string Value;
	}

	class WstRequestSecurityTokenResponse
	{
		public string TokenType;

		public SecurityToken RequestedSecurityToken;

		public string RequestedAttachedReference;
	}
}
