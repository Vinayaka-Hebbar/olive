using System;
using System.IO;

public class GenAnimationTypes {

	enum AnimatableType {
		Boolean,
		Byte,
		Char,
		//Color,
		Decimal,
		Double,
		Int16,
		Int32,
		Int64,
		Matrix,
		Object,
		//Point3D,
		Point,
		//Quaternion,
		Rect,
		//Rotation3D,
		Single,
		Size,
		String,
		//Vector3D,
		Vector
	};

	enum AnimationClassType {
		/*Int32*/Animation,
		/*Int32*/AnimationBase,
		/*Int32*/AnimationUsingKeyFrames,
		/*Int32*/KeyFrame,
		/*Int32*/KeyFrameCollection
	};

	enum KeyFrameType {
		Discrete/*Int32KeyFrame*/,
		Linear/*Int32KeyFrame*/,
		Spline/*Int32KeyFrame*/
	};

	static bool istypecontinuous (AnimatableType t)
	{
		switch (t) {
		case AnimatableType.Boolean:
		case AnimatableType.Char:
		case AnimatableType.Object:
		case AnimatableType.String:
		case AnimatableType.Matrix:
			return false;

		//case AnimatableType.Color:
		//case AnimatableType.Point3D:
		//case AnimatableType.Quaternion:
		//case AnimatableType.Rotation3D:
		//case AnimatableType.Vector3D:

		default:
			return true;
		}
	}

	static string gettype (AnimatableType t)
	{
		switch (t) {
		case AnimatableType.Boolean: return "bool";
		case AnimatableType.Byte: return "byte";
		case AnimatableType.Char: return "char";
		case AnimatableType.Object: return "object";
		case AnimatableType.Single: return "float";
		case AnimatableType.Double: return "double";
		case AnimatableType.Int16: return "short";
		case AnimatableType.Int32: return "int";
		case AnimatableType.Int64: return "long";
		case AnimatableType.Decimal:
			//case AnimatableType.Color:
		case AnimatableType.Matrix:
			//case AnimatableType.Point3D:
		case AnimatableType.Point:
			//case AnimatableType.Quaternion:
		case AnimatableType.Rect:
			//case AnimatableType.Rotation3D:
		case AnimatableType.Size:
		case AnimatableType.String:
			//case AnimatableType.Vector3D:
		case AnimatableType.Vector:
			return t.ToString();
		}

		return null;
	}

	AnimatableType type;

	GenAnimationTypes (AnimatableType type)
	{
		this.type = type;
	}

	void OutputAnimationBase (TextWriter tw)
	{
		tw.WriteLine (@"
public abstract class {0}AnimationBase : AnimationTimeline
{{
	protected {0}AnimationBase () {{ }}

	public override sealed Type TargetPropertyType {{ get {{ return typeof ({1}); }} }}

	public new {0}AnimationBase Clone ()
	{{
		throw new NotImplementedException ();
	}} 

	public override sealed object GetCurrentValue (object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
	{{
		return GetCurrentValue (({1})defaultOriginValue, ({1}) defaultDestinationValue, animationClock);
	}}

	protected abstract {1} GetCurrentValueCore ({1} defaultOriginValue, {1} defaultDestinationValue, AnimationClock animationClock);
", type, gettype(type));

		if (type != AnimatableType.Object) {
			tw.WriteLine (@"
	public {1} GetCurrentValue ({1} defaultOriginValue, {1} defaultDestinationValue, AnimationClock animationClock)
	{{
		throw new NotImplementedException ();
	}}
", type, gettype(type));
		}

		tw.WriteLine ("\n}\n");
	}


	void OutputHeader (TextWriter tw)
	{
		tw.WriteLine (@"
/* this file is generated by gen-animation-types.cs.  do not modify */

using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Markup;

namespace System.Windows.Media.Animation {
");
	}

	void OutputAnimation (TextWriter tw)
	{
		tw.WriteLine (@"
public class {0}Animation : {0}AnimationBase
{{
	public static readonly DependencyProperty ByProperty; /* XXX initialize */
	public static readonly DependencyProperty FromProperty; /* XXX initialize */
	public static readonly DependencyProperty ToProperty; /* XXX initialize */

	public {0}Animation ()
	{{
	}}

	public {0}Animation ({1} toValue, Duration duration)
	{{
	}}

	public {0}Animation ({1} toValue, Duration duration, FillBehavior fillBehavior)
	{{
	}}

	public {0}Animation ({1} fromValue, {1} toValue, Duration duration)
	{{
	}}

	public {0}Animation ({1} fromValue, {1} tovalue, Duration duration, FillBehavior fillBehavior)
	{{
	}}

	public {1}? By {{
		get {{ throw new NotImplementedException (); }}
		set {{ throw new NotImplementedException (); }}
	}}

	public {1}? From {{
		get {{ throw new NotImplementedException (); }}
		set {{ throw new NotImplementedException (); }}
	}}

	public {1}? To {{
		get {{ throw new NotImplementedException (); }}
		set {{ throw new NotImplementedException (); }}
	}}

	public bool IsAdditive {{
		get {{ throw new NotImplementedException (); }}
		set {{ throw new NotImplementedException (); }}
	}}

	public bool IsCumulative {{
		get {{ throw new NotImplementedException (); }}
		set {{ throw new NotImplementedException (); }}
	}}

	public new {0}Animation Clone ()
	{{
		throw new NotImplementedException ();
	}}

	protected override Freezable CreateInstanceCore ()
	{{
		throw new NotImplementedException ();
	}}

	protected override {1} GetCurrentValueCore ({1} defaultOriginValue, {1} defaultDestinationValue, AnimationClock animationClock)
	{{
		throw new NotImplementedException ();
	}}
}}
", type, gettype(type));
	}

	void OutputAnimationUsingKeyFrames (TextWriter tw)
	{
		tw.WriteLine (@"
[ContentProperty (""KeyFrames"")]
public class {0}AnimationUsingKeyFrames : {0}AnimationBase, IKeyFrameAnimation, IAddChild
{{
	public {0}AnimationUsingKeyFrames ()
	{{
	}}

	public {0}KeyFrameCollection KeyFrames {{
		get {{ throw new NotImplementedException (); }}
		set {{ throw new NotImplementedException (); }}
	}}

	IList IKeyFrameAnimation.KeyFrames {{
		get {{ return (IList)KeyFrames; }}
		set {{ KeyFrames = ({0}KeyFrameCollection)value; }}
	}}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void AddChild (object child)
	{{
		throw new NotImplementedException ();
	}}

	void IAddChild.AddChild (object child)
	{{
		AddChild (child);
	}}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void AddText (string childText)
	{{
		throw new NotImplementedException ();
	}}

	void IAddChild.AddText (string childText)
	{{
		AddText (childText);
	}}

	public new {0}AnimationUsingKeyFrames Clone ()
	{{
		throw new NotImplementedException ();
	}}

	protected override void CloneCore (Freezable sourceFreezable)
	{{
		throw new NotImplementedException ();
	}}

	public new {0}AnimationUsingKeyFrames CloneCurrentValue ()
	{{
		throw new NotImplementedException ();
	}}

	protected override void CloneCurrentValueCore (Freezable sourceFreezable)
	{{
		throw new NotImplementedException ();
	}}

	protected override Freezable CreateInstanceCore ()
	{{
		throw new NotImplementedException ();
	}}

	protected override bool FreezeCore (bool isChecking)
	{{
		throw new NotImplementedException ();
	}}

	protected override void GetAsFrozenCore (Freezable source)
	{{
		throw new NotImplementedException ();
	}}

	protected override void GetCurrentValueAsFrozenCore (Freezable source)
	{{
		throw new NotImplementedException ();
	}}

	protected override sealed {1} GetCurrentValueCore ({1} defaultOriginValue, {1} defaultDestinationValue, AnimationClock animationClock)
	{{
		throw new NotImplementedException ();
	}}

	protected override sealed Duration GetNaturalDurationCore (Clock clock)
	{{
		throw new NotImplementedException ();
	}}

	protected override void OnChanged ()
	{{
		throw new NotImplementedException ();
	}}

	[EditorBrowsable(EditorBrowsableState.Never)]
	public bool ShouldSerializeKeyFrames()
	{{
		throw new NotImplementedException ();
	}}

", type, gettype(type));

		if (istypecontinuous (type)) {
			tw.WriteLine (@"
	public bool IsAdditive {{
		get {{ throw new NotImplementedException (); }}
		set {{ throw new NotImplementedException (); }}
	}}

	public bool IsCumulative {{
		get {{ throw new NotImplementedException (); }}
		set {{ throw new NotImplementedException (); }}
	}}
", type, gettype(type));
		}

		tw.WriteLine ("\n}\n");
	}

	void OutputKeyFrame (TextWriter tw)
	{
		tw.WriteLine (@"
public abstract class {0}KeyFrame : Freezable, IKeyFrame
{{
	public static readonly DependencyProperty KeyTimeProperty; /* XXX initialize */
	public static readonly DependencyProperty ValueProperty; /* XXX initialize */

	protected {0}KeyFrame ()
	{{
	}}

	protected {0}KeyFrame ({1} value)
	{{
	}}

	protected {0}KeyFrame ({1} value, KeyTime keyTime)
	{{
	}}

	public KeyTime KeyTime {{
		get {{ throw new NotImplementedException (); }}
		set {{ throw new NotImplementedException (); }}
	}}

	public {1} Value {{
		get {{ throw new NotImplementedException (); }}
		set {{ throw new NotImplementedException (); }}
	}}

	object IKeyFrame.Value {{
		get {{ return Value; }}
		set {{ Value = ({1})value; }}
	}}

	public {1} InterpolateValue ({1} baseValue, double keyFrameProgress)
	{{
		throw new NotImplementedException ();
	}}

	protected abstract {1} InterpolateValueCore ({1} baseValue, double keyFrameProgress);
}}
", type, gettype(type));
	}

	void OutputFooter (TextWriter tw)
	{
		tw.WriteLine ("\n}"); /* close the namespace */
	}

	void OutputKeyFrameCollection (TextWriter tw)
	{
		tw.WriteLine (@"
public class {0}KeyFrameCollection : Freezable, IList, ICollection, IEnumerable
{{
	public {0}KeyFrameCollection ()
	{{
	}}

	public int Count {{
		get {{ throw new NotImplementedException (); }}
	}}

	public static {0}KeyFrameCollection Empty {{
		get {{ throw new NotImplementedException (); }}
	}}

	public bool IsFixedSize {{
		get {{ throw new NotImplementedException (); }}
	}}

	public bool IsReadOnly {{
		get {{ throw new NotImplementedException (); }}
	}}

	public bool IsSynchronized {{
		get {{ throw new NotImplementedException (); }}
	}}

	public object SyncRoot {{
		get {{ throw new NotImplementedException (); }}
	}}

	public {0}KeyFrame this[int index] {{
		get {{ throw new NotImplementedException (); }}
		set {{ throw new NotImplementedException (); }}
	}}

	object IList.this[int index] {{
		get {{ return this[index]; }}
		set {{ this[index] = ({0}KeyFrame)value; }}
	}}

	public int Add ({0}KeyFrame keyFrame)
	{{
		throw new NotImplementedException ();
	}}

	int IList.Add (object value)
	{{
		return Add (({0}KeyFrame) value);
	}}

	public void Clear ()
	{{
		throw new NotImplementedException ();
	}}

	public new {0}KeyFrameCollection Clone ()
	{{
		throw new NotImplementedException ();
	}}

	protected override void CloneCore (Freezable sourceFreezable)
	{{
		throw new NotImplementedException ();
	}}

	protected override void CloneCurrentValueCore (Freezable sourceFreezable)
	{{
		throw new NotImplementedException ();
	}}

	public bool Contains ({0}KeyFrame keyFrame)
	{{
		throw new NotImplementedException ();
	}}

	bool IList.Contains (object value)
	{{
		return Contains (({0}KeyFrame)value);
	}}

	public void CopyTo ({0}KeyFrame[] array, int index)
	{{
		throw new NotImplementedException ();
	}}

	void ICollection.CopyTo (Array array, int index)
	{{
		CopyTo (({0}KeyFrame[])array, index);
	}}

	protected override Freezable CreateInstanceCore ()
	{{
		throw new NotImplementedException ();
	}}

	protected override bool FreezeCore (bool isChecking)
	{{
		throw new NotImplementedException ();
	}}

	protected override void GetAsFrozenCore (Freezable sourceFreezable)
	{{
		throw new NotImplementedException ();
	}}

	protected override void GetCurrentValueAsFrozenCore (Freezable sourceFreezable)
	{{
		throw new NotImplementedException ();
	}}

	public IEnumerator GetEnumerator()
	{{
		throw new NotImplementedException ();
	}}

	public int IndexOf ({0}KeyFrame keyFrame)
	{{
		throw new NotImplementedException ();
	}}

	int IList.IndexOf (object value)
	{{
		return IndexOf (({0}KeyFrame) value);
	}}

	public void Insert (int index, {0}KeyFrame keyFrame)
	{{
		throw new NotImplementedException ();
	}}

	void IList.Insert (int index, object value)
	{{
		Insert (index, ({0}KeyFrame)value);
	}}

	public void Remove ({0}KeyFrame keyFrame)
	{{
		throw new NotImplementedException ();
	}}

	void IList.Remove (object value)
	{{
		Remove (({0}KeyFrame) value);
	}}

	public void RemoveAt (int index)
	{{
		throw new NotImplementedException ();
	}}
}}
", type, gettype(type));
	}

	void OutputBaseClassFile (AnimationClassType ac)
	{
		if (ac == AnimationClassType.Animation
 		    && !istypecontinuous (type))
			return;

		string filename = String.Format ("{0}{1}.cs", type, ac);
		Console.WriteLine ("outputting {0}", filename);

		TextWriter tw = File.CreateText (filename);
		OutputHeader (tw);
		switch (ac) {
		case AnimationClassType.Animation:
			OutputAnimation (tw);
			break;
		case AnimationClassType.AnimationBase:
			OutputAnimationBase (tw);
			break;
		case AnimationClassType.AnimationUsingKeyFrames:
			OutputAnimationUsingKeyFrames (tw);
			break;
		case AnimationClassType.KeyFrame:
			OutputKeyFrame (tw);
			break;
		case AnimationClassType.KeyFrameCollection:
			OutputKeyFrameCollection (tw);
			break;
		}
		OutputFooter (tw);
		tw.Close ();
	}

	void OutputDiscreteKeyFrame (TextWriter tw)
	{
		tw.WriteLine (@"
public class Discrete{0}KeyFrame : {0}KeyFrame
{{

	public Discrete{0}KeyFrame ()
	{{
	}}

	public Discrete{0}KeyFrame ({1} value)
	{{
	}}

	public Discrete{0}KeyFrame ({1} value, KeyTime keyTime)
	{{
	}}

	protected override Freezable CreateInstanceCore ()
	{{
		throw new NotImplementedException ();
	}}

	protected override {1} InterpolateValueCore ({1} baseValue, double keyFrameProgress)
	{{
		throw new NotImplementedException ();
	}}
}}
", type, gettype(type));
	}

	void OutputLinearKeyFrame (TextWriter tw)
	{
		tw.WriteLine (@"
public class Linear{0}KeyFrame : {0}KeyFrame
{{

	public Linear{0}KeyFrame ()
	{{
	}}

	public Linear{0}KeyFrame ({1} value)
	{{
	}}

	public Linear{0}KeyFrame ({1} value, KeyTime keyTime)
	{{
	}}

	protected override Freezable CreateInstanceCore ()
	{{
		throw new NotImplementedException ();
	}}

	protected override {1} InterpolateValueCore ({1} baseValue, double keyFrameProgress)
	{{
		throw new NotImplementedException ();
	}}
}}
", type, gettype(type));
	}

	void OutputSplineKeyFrame (TextWriter tw)
	{
		tw.WriteLine (@"
public class Spline{0}KeyFrame : {0}KeyFrame
{{

	public static readonly DependencyProperty KeySplineProperty; // XXX initialize

	public Spline{0}KeyFrame ()
	{{
	}}

	public Spline{0}KeyFrame ({1} value)
	{{
	}}

	public Spline{0}KeyFrame ({1} value, KeyTime keyTime)
	{{
	}}

	public Spline{0}KeyFrame ({1} value, KeyTime keyTime, KeySpline keySpline)
	{{
	}}

	public KeySpline KeySpline {{
		get {{ throw new NotImplementedException (); }}
		set {{ throw new NotImplementedException (); }}
	}}

	protected override Freezable CreateInstanceCore ()
	{{
		throw new NotImplementedException ();
	}}

	protected override {1} InterpolateValueCore ({1} baseValue, double keyFrameProgress)
	{{
		throw new NotImplementedException ();
	}}
}}
", type, gettype(type));
	}

	void OutputKeyFrameFile (KeyFrameType kt)
	{
		if (!istypecontinuous (type) &&
		    (kt == KeyFrameType.Linear || kt == KeyFrameType.Spline))
			return;

		string filename = String.Format ("{0}{1}KeyFrame.cs", kt, type);
		Console.WriteLine ("outputting {0}", filename);

		TextWriter tw = File.CreateText (filename);
		OutputHeader (tw);

		switch (kt) {
		case KeyFrameType.Discrete:
			OutputDiscreteKeyFrame (tw);
			break;
		case KeyFrameType.Linear:
			OutputLinearKeyFrame (tw);
			break;
		case KeyFrameType.Spline:
			OutputSplineKeyFrame (tw);
			break;
		}

		OutputFooter (tw);
		tw.Close ();
	}

	public static void Main (string[] args)
	{
		foreach (AnimatableType at in Enum.GetValues(typeof (AnimatableType))) {
			GenAnimationTypes ga = new GenAnimationTypes(at);

			/* output the base class types */
			foreach (AnimationClassType ac in Enum.GetValues (typeof (AnimationClassType)))
				ga.OutputBaseClassFile (ac);

			/* output the keyframe types */
			foreach (KeyFrameType kt in Enum.GetValues (typeof (KeyFrameType)))
				ga.OutputKeyFrameFile (kt);
		}
	}
}
