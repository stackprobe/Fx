#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\Date2Day.h"

/*
	ある日の終値は翌日06:00のデータとして吐き出すので、出力ファイルの範囲は、以下の範囲の +1日であることに注意！
*/
#define SPAN_A_FIRST_DATE 20000101
#define SPAN_A_END_DATE   20150726

#define SPAN_B_FIRST_DATE 20170329
#define SPAN_B_END_DATE   20170513

static void Conv_CPair(char *cPair)
{
	char *file = combine_xx(combine(getSelfDir(), "dat"), xcout("200001-201705_%s.txt", cPair));
	autoList_t *lines;
	char *line;
	uint index;
	uint crFCnt = 0;
	uint outDateCnt = 0;

	cout("cPair: %s\n", cPair);

	lines = readLines(file);

	// check
	{
		line = getLine(lines, 16);

		errorCase(!lineExp("<3,AZ>//<3,AZ><>", line));

		eraseChar(line + 3);
		line[6] = '\0';

		errorCase(strcmp(line, cPair));
	}

	foreach(lines, line, index)
	{
		if(lineExp("<4,09>年<>", line))
		{
			autoList_t *tokens;
			char *p;
			char *sY;
			char *sM;
			char *sD;
			char *sOpen;
			char *sHigh;
			char *sLow;
			char *sClose;
			uint c;
			uint date;
			char *wFile;

			errorCase(!lineExp("<4,09>年<2,09>月<2,09>日\t<..09>\t<..09>\t<..09>\t<..09>\t<0,1,--><..09>%", line)); // check

			// to numeric only
			for(p = line; *p; p++)
				if(!m_isdecimal(*p) && *p != '.')
					*p = ' ';

			tokens = tokenize(line, ' ');
			trimLines(tokens);

			errorCase(getCount(tokens) != 8);

			c = 0;

			sY     = getLine(tokens, c++);
			sM     = getLine(tokens, c++);
			sD     = getLine(tokens, c++);
			sOpen  = getLine(tokens, c++);
			sHigh  = getLine(tokens, c++);
			sLow   = getLine(tokens, c++);
			sClose = getLine(tokens, c++);

			date = toValue(sY) * 10000 + toValue(sM) * 100 + toValue(sD);

			errorCase(Day2IDate(IDate2Day(date)) != date); // check

			if(
				SPAN_A_FIRST_DATE <= date && date <= SPAN_A_END_DATE ||
				SPAN_B_FIRST_DATE <= date && date <= SPAN_B_END_DATE
				)
			{
				date = Day2IDate(IDate2Day(date) + 1); // 翌日

				wFile = getOutFile_x(xcout("%u_%s.csv", date, cPair));

				if(existFile(wFile)) // ? 既に出力した日付 -> 日付の重複！
				{
					cout("日付の重複: %u\n", date);
				}
				else
				{
					writeOneLine_cx(wFile, xcout("%u060000,%s,%s,%s,%s,%s", date, sOpen, sHigh, sLow, sClose, sClose));
					crFCnt++;
				}
				memFree(wFile);
			}
			else // ? 範囲外の日付
			{
				outDateCnt++;
			}
			releaseDim(tokens, 1);
		}
	}
	memFree(file);
	releaseDim(lines, 1);

	cout("%u file(s) created!\n", crFCnt);
	cout("%u out date!\n", outDateCnt);
}
int main(int argc, char **argv)
{
	// ---- CPairs ----

	Conv_CPair("AUDCHF");
	Conv_CPair("AUDJPY");
	Conv_CPair("AUDNZD");
	Conv_CPair("AUDUSD");
	Conv_CPair("CADJPY");
	Conv_CPair("CHFJPY");
	Conv_CPair("EURAUD");
	Conv_CPair("EURCAD");
	Conv_CPair("EURCHF");
	Conv_CPair("EURGBP");
	Conv_CPair("EURJPY");
	Conv_CPair("EURNZD");
	Conv_CPair("EURUSD");
	Conv_CPair("GBPAUD");
	Conv_CPair("GBPCHF");
	Conv_CPair("GBPJPY");
	Conv_CPair("GBPNZD");
	Conv_CPair("GBPUSD");
	Conv_CPair("NZDJPY");
	Conv_CPair("NZDUSD");
	Conv_CPair("USDCAD");
	Conv_CPair("USDCHF");
	Conv_CPair("USDJPY");
	Conv_CPair("ZARJPY");

	// ----

	openOutDir();
}
