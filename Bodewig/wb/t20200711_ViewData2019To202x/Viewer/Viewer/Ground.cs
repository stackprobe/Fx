using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Utils;

namespace Charlotte
{
	public class Ground
	{
		public static Ground I;

		public string DataDir;

		public long Period_DateTimeSt;
		public long Period_DateTimeEd;

		public void LoadDataDir()
		{
			this.DataDir = Consts.DATA_DIR_01;

			if (Directory.Exists(this.DataDir) == false)
			{
				this.DataDir = Consts.DATA_DIR_02;

				if (Directory.Exists(this.DataDir) == false)
					throw new Exception("データ・ディレクトリが見つかりません。");
			}

			string periodFile = Path.Combine(this.DataDir, "_Period.txt");

			{
				string[] lines = File.ReadAllLines(periodFile);
				int c = 0;

				this.Period_DateTimeSt = long.Parse(StringUtils.KVToValue(lines[c++]));
				this.Period_DateTimeEd = long.Parse(StringUtils.KVToValue(lines[c++]));
			}

			// ここからチェック

			if (DateTimeToSecUtils.IsFairDateTime(this.Period_DateTimeSt) == false)
				throw new Exception("不正な開始日時");

			if (DateTimeToSecUtils.IsFairDateTime(this.Period_DateTimeEd) == false)
				throw new Exception("不正な開始日時");
		}
	}
}
