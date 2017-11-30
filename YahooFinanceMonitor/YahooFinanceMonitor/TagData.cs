using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YahooFinanceMonitor
{
	public class TagData
	{
		public static TagData GetTagData(string text, string bgn, string end)
		{
			TagData td = new TagData();

			td.Text = text;
			td.BgnPP = GetPtnPos(text, bgn);

			if (td.BgnPP == null)
				return null;

			td.EndPP = GetPtnPos(text, end, td.BgnPP.EndPos);

			if (td.EndPP == null)
				return null;

			return td;
		}

		public string Text;
		public PtnPos BgnPP;
		public PtnPos EndPP;

		private TagData()
		{ }

		private static PtnPos GetPtnPos(string text, string ptn, int startIndex = 0)
		{
			PtnPos pp = new PtnPos();

			pp.BgnPos = text.IndexOf(ptn, startIndex);

			if (pp.BgnPos == -1)
				return null;

			pp.EndPos = pp.BgnPos + ptn.Length;
			return pp;
		}

		public class PtnPos
		{
			public int BgnPos;
			public int EndPos;
		}

		public string GetInnerString()
		{
			return this.Text.Substring(this.BgnPP.EndPos, this.EndPP.BgnPos - this.BgnPP.EndPos);
		}
	}
}
