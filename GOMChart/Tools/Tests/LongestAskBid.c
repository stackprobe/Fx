#include "C:\Factory\Common\all.h"

static time_t YFM_TMin;
static time_t YFM_TMax;

static int IsStamp(char *stamp, time_t tMin, time_t tMax)
{
	time_t t = compactStampToTime(stamp);
	int ret = 0;

	if(m_isRange(t, tMin, tMax))
	{
		char *tmp = makeCompactStamp(getStampDataTime(t));

		if(!strcmp(stamp, tmp))
			ret = 1;

		memFree(tmp);
	}
	return ret;
}
static int IsDoubleStr(char *str)
{
	return lineExp("<1,,09>", str) || lineExp("<1,,09>.<1,,09>", str);
}
static uint GetMax(uint a, uint b)
{
	return m_max(a, b);
}
int main(int argc, char **argv)
{
	autoList_t *files = lsFiles("C:\\var\\Fx_20150728-20170331");
	char *file;
	uint index;
	uint longest = 0;

	/*
		YFMで取得したデータは、この範囲！
	*/
	YFM_TMin = compactStampToTime("20150728000000");
	YFM_TMax = compactStampToTime("20170331000000");

	foreach(files, file, index)
	{
		char *lFile = getLocal(file);

		if(lineExpICase("<8,09>_<6,AZ>.csv", lFile))
		{
			autoList_t *lines = readLines(file);
			char *line;
			uint line_index;

			cout("< %s\n", file);

			foreach(lines, line, line_index)
			{
				autoList_t *cells;

				errorCase(!lineExp("<14,09>,<1,,09..>,<1,,09..>", line)); // check

				cells = tokenize(line, ',');

				errorCase(!IsStamp(getLine(cells, 0), YFM_TMin, YFM_TMax)); // check
				errorCase(!IsDoubleStr(getLine(cells, 1))); // check
				errorCase(!IsDoubleStr(getLine(cells, 2))); // check

				longest = GetMax(longest, strlen(getLine(cells, 1)));
				longest = GetMax(longest, strlen(getLine(cells, 2)));

				releaseDim(cells, 1);
			}
			releaseDim(lines, 1);

			cout("%u\n", longest);
		}
		else
		{
			errorCase(!lineExpICase("<8,09>_Error.txt", lFile)); // check
		}
	}
	releaseDim(files, 1);

	cout("LOGEST: %u\n", longest);
}
