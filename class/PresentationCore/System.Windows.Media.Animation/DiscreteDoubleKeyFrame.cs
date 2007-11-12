
/* this file is generated by gen-animation-types.cs.  do not modify */

using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Markup;

namespace System.Windows.Media.Animation {


public class DiscreteDoubleKeyFrame : DoubleKeyFrame
{
	double value;
	KeyTime keyTime;

	public DiscreteDoubleKeyFrame ()
	{
	}

	public DiscreteDoubleKeyFrame (double value)
	{
		this.value = value;
		// XXX keytime?
	}

	public DiscreteDoubleKeyFrame (double value, KeyTime keyTime)
	{
		this.value = value;
		this.keyTime = keyTime;
	}

	protected override Freezable CreateInstanceCore ()
	{
		throw new NotImplementedException ();
	}

	protected override double InterpolateValueCore (double baseValue, double keyFrameProgress)
	{
		return keyFrameProgress == 1.0 ? value : baseValue;
	}
}


}
