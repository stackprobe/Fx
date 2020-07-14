using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte
{
	public class GrphCond
	{
		public class MaDayInfo
		{
			public int Index;
			public int Day;
		}

		// ---- 項目ここから

		public string CurrPair;
		public long DateTimeSt;
		public long DateTimeEd;
		public MaDayInfo[] MaDays;

		// ---- 項目ここまで

		public bool IsSame(GrphCond other)
		{
			return
				this.CurrPair == other.CurrPair &&
				this.DateTimeSt == other.DateTimeSt &&
				this.DateTimeEd == other.DateTimeEd &&
				ArrayTools.Comp(this.MaDays, other.MaDays, (a, b) => a.Index - b.Index) == 0;
		}
	}
}
