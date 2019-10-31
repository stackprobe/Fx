using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.ChartStorage;

namespace Charlotte.Tests.ChartStorage
{
	public class ChartStorage0001Test // -- 0001
	{
		public void Test01()
		{
			string message = "ChartStorage9999";

			if (new ChartStorage0001().Echo(message) != message)
				throw null;

			Console.WriteLine("OK!");
		}
	}
}
