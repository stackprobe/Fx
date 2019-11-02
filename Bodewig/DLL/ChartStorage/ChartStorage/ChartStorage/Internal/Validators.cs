using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.ChartStorage.Internal
{
	internal static class Validators
	{
		public static void CheckCurrencyPair(string currencyPair)
		{
			if (StringTools.LiteValidate(currencyPair, StringTools.ALPHA, 6, 6) == false)
				throw new Exception("Bad currencyPair: " + currencyPair);
		}

		public static void CheckDate(int date)
		{
			if (DateToDay.ToDate(DateToDay.ToDay(date)) != date)
				throw new Exception("Bad date: " + date);
		}

		public static void CheckDateTime(long dateTime)
		{
			if (DateTimeToSec.ToDateTime(DateTimeToSec.ToSec(dateTime)) != dateTime)
				throw new Exception("Bad dateTime: " + dateTime);
		}
	}
}
