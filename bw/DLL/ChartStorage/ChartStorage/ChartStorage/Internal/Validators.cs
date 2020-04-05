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
			if (date < CSConsts.DATE_TIME_MIN / 1000000 || CSConsts.DATE_TIME_MAX / 1000000 < date)
				throw new Exception("Out of range date: " + date);

			if (DateToDay.ToDate(DateToDay.ToDay(date)) != date)
				throw new Exception("Bad date: " + date);
		}

		public static void CheckDateTime(long dateTime)
		{
			if (dateTime < CSConsts.DATE_TIME_MIN || CSConsts.DATE_TIME_MAX < dateTime)
				throw new Exception("Out of range dateTime: " + dateTime);

			if (DateTimeToSec.ToDateTime(DateTimeToSec.ToSec(dateTime)) != dateTime)
				throw new Exception("Bad dateTime: " + dateTime);
		}
	}
}
