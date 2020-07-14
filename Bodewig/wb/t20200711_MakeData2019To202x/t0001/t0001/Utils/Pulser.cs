using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Utils
{
	public class Pulser
	{
		private int Period;
		private int Count = 0;

		public Pulser(int period = 100000)
		{
			this.Period = period;
		}

		public bool Invoke()
		{
			if (0 < this.Count)
			{
				this.Count--;
				return false;
			}
			else
			{
				this.Count = this.Period;
				return true;
			}
		}
	}
}
