using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class MovingAverage
	{
		private WeSecPrices WSP;
		private long SecInterval;
		private long SecBound;

		public MovingAverage(WeSecPrices wsp, long secInterval, long secBound)
		{
			if (wsp == null)
				throw null;

			if (secInterval < 1L)
				throw new Exception("MAインターバル=0");

			if (secBound < secInterval)
				throw new Exception("MAインターバル長過ぎ！(期間より長い！)");

			if (secBound % secInterval != 0)
				throw new Exception("MA期間はインターバルの倍数にしてね！");

			if (Consts.SAMPLING_MAX < secBound / secInterval)
				throw new Exception("MAインターバル刻み過ぎ！");

			if (Consts.SEC_BOUND_MAX < secBound)
				throw new Exception("MA期間長過ぎ！");

			//secBound /= secInterval;
			//secBound *= secInterval;

			// <-- check prms

			this.WSP = wsp;
			this.SecInterval = secInterval;
			this.SecBound = secBound;

			// ----

			this.Denom = this.SecBound / this.SecInterval;
		}

		private long CurrWeSec = -1L;
		private double Total;
		private long Denom;

		public double GetMid(long weSec)
		{
			// norm weSec {

			if (weSec < 0L)
				weSec = 0L;

			weSec /= this.SecInterval;
			weSec *= this.SecInterval;

			// }

			if (this.CurrWeSec == -1L)
			{
				this.CurrWeSec = weSec;
				this.Total = 0.0;

				for (long sec = 0L; sec < this.SecBound; sec += this.SecInterval)
					this.Total += this.WSP.GetPrice(this.CurrWeSec - sec).Mid;
			}
			while (weSec < this.CurrWeSec)
			{
				this.Total -= this.WSP.GetPrice(this.CurrWeSec).Mid;
				this.Total += this.WSP.GetPrice(this.CurrWeSec - this.SecBound).Mid;
				this.CurrWeSec -= this.SecInterval;
			}
			while (this.CurrWeSec < weSec)
			{
				this.CurrWeSec += this.SecInterval;
				this.Total += this.WSP.GetPrice(this.CurrWeSec).Mid;
				this.Total -= this.WSP.GetPrice(this.CurrWeSec - this.SecBound).Mid;
			}
			return this.Total / this.Denom;
		}
	}
}
