
/* this file is generated by gen-animation-types.cs.  do not modify */

using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using NUnit.Framework;

namespace MonoTests.System.Windows.Media.Animation {


[TestFixture]
public class Int32AnimationBaseTest
{
	class Int32AnimationBasePoker : Int32AnimationBase
	{
		protected override int GetCurrentValueCore (int defaultOriginValue, int defaultDestinationValue,
							    AnimationClock animationClock)
		{
			throw new NotImplementedException ();
		}

		protected override Freezable CreateInstanceCore ()
		{
			throw new NotImplementedException ();
		}
	}

	[Test]
	public void Properties ()
	{
		Int32AnimationBasePoker poker = new Int32AnimationBasePoker ();
		Assert.AreEqual (typeof (int), poker.TargetPropertyType);
	}
}


}