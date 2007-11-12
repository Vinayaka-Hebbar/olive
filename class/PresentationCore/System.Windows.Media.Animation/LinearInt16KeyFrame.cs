
/* this file is generated by gen-animation-types.cs.  do not modify */

using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Markup;

namespace System.Windows.Media.Animation {


public class LinearInt16KeyFrame : Int16KeyFrame
{
	short value;
	KeyTime keyTime;

	public LinearInt16KeyFrame ()
	{
	}

	public LinearInt16KeyFrame (short value)
	{
		this.value = value;
		// XXX keytime?
	}

	public LinearInt16KeyFrame (short value, KeyTime keyTime)
	{
		this.value = value;
		this.keyTime = keyTime;
	}

	protected override Freezable CreateInstanceCore ()
	{
		throw new NotImplementedException ();
	}

	protected override short InterpolateValueCore (short baseValue, double keyFrameProgress)
	{
		// standard linear interpolation
		return (short)(baseValue + (value - baseValue) * keyFrameProgress);
	}
}


}
