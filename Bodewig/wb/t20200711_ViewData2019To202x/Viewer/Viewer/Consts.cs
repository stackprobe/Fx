using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Charlotte
{
	public static class Consts
	{
		public const string DATA_DIR_01 = @".\Data2019To202x";
		public const string DATA_DIR_02 = @"C:\temp\Data2019To202x";

		public const int REFRESH_DELAY = 6;
		public const int REFRESH_DELAY_FAST = 2;

		public static readonly Color LABEL_FORE_COLOR = new Label().ForeColor;
		public static readonly Color LABEL_BACK_COLOR = new Label().BackColor;
	}
}
