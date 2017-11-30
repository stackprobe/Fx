using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte
{
	public partial class DebugWin_WeSec : Form
	{
		public DebugWin_WeSec()
		{
			InitializeComponent();
		}

		private void WeSetDebugWin_Load(object sender, EventArgs e)
		{
			List<string> dest = new List<string>();

			// ----

			long sec = DateTimeToSec.toSec(20170529070000L); // 月曜日の7:00 == 取引開始

			dest.Add("" + (sec % (86400L * 7)));

			// ----

			lblOutput.Text = string.Join("\r\n", dest);
		}
	}
}
