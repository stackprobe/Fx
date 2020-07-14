using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Utils
{
	public static class DateTimeToSecUtils
	{
		public static bool IsFairDateTime(long dt)
		{
			return DateTimeToSec.ToDateTime(DateTimeToSec.ToSec(dt)) == dt;
		}
	}
}
