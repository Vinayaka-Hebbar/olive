using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Selectors;
using System.Xml;

public class Test
{
	public static void Main (string [] args)
	{
		CardSpaceSelector.Import (args [0]);
	}
}

