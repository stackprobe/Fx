/*
	多重起動しちゃったので、補正用
	なお、多重起動防止するよう修正した。@ 2015.11.3

	> lss 20151103
	> antiDouble.exe
*/

#include "C:\Factory\Common\all.h"

static void CheckLineStamps(autoList_t *lines)
{
	char *line;
	uint index;
	uint fDate;
	uint nextSecMin;

	foreach(lines, line, index)
	{
		uint date;
		uint time;
		uint sec;

		errorCase(!lineExp("<14,09><>", line));

		date = toValue(strxl(line + 0, 8));
		time = toValue(strxl(line + 8, 6));

		if(!index)
		{
			uint y = date / 10000;
			uint m = (date / 100) % 100;
			uint d = date % 100;

			errorCase(!m_isRange(y, 2000, 3000));
			errorCase(!m_isRange(m, 1, 12));
			errorCase(!m_isRange(d, 1, 31));

			fDate = date;
			nextSecMin = 0;
		}
		errorCase(fDate != date);

		{
			uint h = time / 10000;
			uint m = (time / 100) % 100;
			uint s = time % 100;

			errorCase(!m_isRange(h, 0, 23));
			errorCase(!m_isRange(m, 0, 59));
			errorCase(!m_isRange(s, 0, 59));

			sec = h * 3600 + m * 60 + s;
		}

		errorCase(sec < nextSecMin);
		errorCase((sec - nextSecMin) % 2 != 0);

		nextSecMin = sec + 2;
	}
}
static void Conv(char *file)
{
	autoList_t *lines = readLines(file);
	char *line;
	uint index;
	char *lastStamp = NULL;

	foreach(lines, line, index)
	{
		char *stamp;

		errorCase(!lineExp("<14,09>,<1,,..09>,<1,,..09>", line));

		stamp = strxl(line, 14);

		if(lastStamp && !strcmp(stamp, lastStamp)) // ? 直前と同じ
		{
			cout("DELETE %s\n", line);
			memFree(line);
			setElement(lines, index, 0); // 削除
		}
		else
			strzp(&lastStamp, stamp);

		memFree(stamp);
	}
	removeZero(lines);

	CheckLineStamps(lines);

	semiRemovePath(file);
	writeLines(file, lines);
	releaseDim(lines, 1);
}
int main(int argc, char **argv)
{
	autoList_t *files = readLines(FOUNDLISTFILE);
	char *file;
	uint index;

	foreach(files, file, index)
	{
		cout("> %s\n", file);

		errorCase(!lineExpICase("<8,09>_<6,az>.csv", getLocal(file))); // 2bs
	}
	cout("続行？\n");

	if(getKey() == 0x1b)
		termination(0);

	cout("続行します。\n");

	foreach(files, file, index)
	{
		Conv(file);
	}
	releaseDim(files, 1);
}
