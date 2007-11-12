
/* this file is generated by gen-animation-types.cs.  do not modify */

using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Markup;

namespace System.Windows.Media.Animation {


public class SplineInt32KeyFrame : Int32KeyFrame
{

	public static readonly DependencyProperty KeySplineProperty; // XXX initialize

	int value;
	KeyTime keyTime;

	public SplineInt32KeyFrame ()
	{
	}

	public SplineInt32KeyFrame (int value)
	{
		this.value = value;
		// XX keytime?
	}

	public SplineInt32KeyFrame (int value, KeyTime keyTime)
	{
		this.value = value;
		this.keyTime = keyTime;
	}

	public SplineInt32KeyFrame (int value, KeyTime keyTime, KeySpline keySpline)
	{
		this.value = value;
		this.keyTime = keyTime;
		KeySpline = keySpline;
	}

	public KeySpline KeySpline {
		get { throw new NotImplementedException (); }
		set { throw new NotImplementedException (); }
	}

	protected override Freezable CreateInstanceCore ()
	{
		throw new NotImplementedException ();
	}

	protected override int InterpolateValueCore (int baseValue, double keyFrameProgress)
	{
		double splineProgress = KeySpline.GetSplineProgress (keyFrameProgress);

		return (int)(baseValue + (value - baseValue) * splineProgress);
	}
}


}
