using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.MovingAverageChart.Internal;

namespace Charlotte.MovingAverageChart
{
	public class MAChart
	{
		private MACommon.GetPrice_d GetMomentPrice;
		private int Span;
		private int TTStep;

		public MAChart(MACommon.GetPrice_d getMomentPrice, int span, int ttStep)
		{
			if (getMomentPrice == null)
				throw new Exception("getMomentPrice is null");

			if (span < 1 || IntTools.IMAX < span)
				throw new Exception("Bad span: " + span);

			if (ttStep < 1 || IntTools.IMAX < ttStep)
				throw new Exception("Bad ttStep: " + ttStep);

			this.GetMomentPrice = getMomentPrice;
			this.Span = span;
			this.TTStep = ttStep;

			if (MAConsts.TT_SPAN_MAX < this.TT_SPAN)
				throw new Exception("Bad TT_SPAN: " + this.TT_SPAN);
		}

		private long TT_SPAN
		{
			get
			{
				return (long)this.Span * this.TTStep;
			}
		}

		private double CurrTotal = 0.0;
		private long CurrTTSec = -1L;

		public double GetPrice(long ttSec)
		{
			try
			{
				Validators.CheckTTSec(ttSec);

				ttSec /= this.TTStep;
				ttSec *= this.TTStep;

				if (ttSec < this.TT_SPAN)
					throw new Exception("Bad ttSec: " + ttSec);

				if (
					this.CurrTTSec != -1L &&
					this.CurrTTSec - this.TT_SPAN <= ttSec && ttSec <= this.CurrTTSec + this.TT_SPAN
					)
				{
					while (this.CurrTTSec < ttSec)
					{
						this.CurrTotal -= this.GetMomentPrice(this.CurrTTSec - this.TT_SPAN);
						this.CurrTTSec += this.TTStep;
						this.CurrTotal += this.GetMomentPrice(this.CurrTTSec);
					}
					while (ttSec < this.CurrTTSec)
					{
						this.CurrTotal -= this.GetMomentPrice(this.CurrTTSec);
						this.CurrTTSec -= this.TTStep;
						this.CurrTotal += this.GetMomentPrice(this.CurrTTSec - this.TT_SPAN);
					}
				}
				else
				{
					this.CurrTotal = 0.0;

					for (long tts = ttSec - this.TT_SPAN; tts <= ttSec; tts += this.TTStep)
					{
						this.CurrTotal += this.GetMomentPrice(tts);
					}
					this.CurrTTSec = ttSec;
				}
				return this.CurrTotal / this.Span;
			}
			catch
			{
				this.CurrTotal = 0.0;
				this.CurrTTSec = -1L;

				throw;
			}
		}
	}
}
