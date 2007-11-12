
/* this file is generated by gen-animation-types.cs.  do not modify */

using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Markup;

namespace System.Windows.Media.Animation {


public class DiscreteObjectKeyFrame : ObjectKeyFrame
{
	object value;
	KeyTime keyTime;

	public DiscreteObjectKeyFrame ()
	{
	}

	public DiscreteObjectKeyFrame (object value)
	{
		this.value = value;
		// XXX keytime?
	}

	public DiscreteObjectKeyFrame (object value, KeyTime keyTime)
	{
		this.value = value;
		this.keyTime = keyTime;
	}

	protected override Freezable CreateInstanceCore ()
	{
		throw new NotImplementedException ();
	}

	protected override object InterpolateValueCore (object baseValue, double keyFrameProgress)
	{
		return keyFrameProgress == 1.0 ? value : baseValue;
	}
}


}
