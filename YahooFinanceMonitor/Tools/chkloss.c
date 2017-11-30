/*
	chkloss.exe [/A]
*/

#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\bitList.h"

#define R_DIR "C:\\var\\Fx"
#define TIME_BIT_LEN 43200

static char *EF_Line;

static void ErrorFnlz(void)
{
	if(EF_Line)
		cout("EF_Line: %s\n", EF_Line);
}
static uint TimeToIndex(char *time)
{
	uint t = toValue(time);
	uint h;
	uint m;
	uint s;

	h = t / 10000;
	m = t / 100 % 100;
	s = t % 100;

	// ? 不正な時刻
	{
		errorCase(23 < h);
		errorCase(59 < m);
		errorCase(59 < s);
		errorCase(s % 2); // ? 奇数秒
	}

	t = h * 3600 + m * 60 + s;
	t /= 2;
	errorCase(TIME_BIT_LEN <= t); // 2bs
	return t;
}
static uint IndexToTime(uint index)
{
	uint t = index * 2;
	uint h;
	uint m;
	uint s;

	h = t / 3600;
	m = t / 60 % 60;
	s = t % 60;

	return h * 10000 + m * 100 + s;
}
static void ChkLoss_File(char *date, char *currPair, FILE *wFp)
{
	char *rFile = combine_cx(R_DIR, xcout("%s_%s.csv", date, currPair));
	autoList_t *lines;
	char *line;
	uint index;
	bitList_t *timeBits = newBitList();

	if(!existFile(rFile))
	{
		writeLine_x(wFp, xcout("%s", getLocal(rFile)));
		writeLine(wFp, "\tNO-FILE");
		writeLine(wFp, "");
		goto endfunc;
	}
	lines = readLines(rFile);

	foreach(lines, line, index)
	{
		char *lDate;
		char *lTime;
		uint iLTime;
		uint timeIndex;

		EF_Line = line;

		errorCase(!lineExp("<14,09>,<>", line)); // ? 不明なフォーマット

		lDate = strxl(line, 8);
		lTime = strxl(line + 8, 6);

		errorCase(strcmp(date, lDate)); // ? 日付が違う。

		timeIndex = TimeToIndex(lTime);

		putBit(timeBits, timeIndex, 1);

		memFree(lDate);
		memFree(lTime);

		EF_Line = NULL;
	}

	{
		int wrTopFlag = 0;

		for(index = 0; index < TIME_BIT_LEN; index++)
		{
			if(!refBit(timeBits, index))
			{
				uint end;

				for(end = index; end + 1 < TIME_BIT_LEN; end++)
					if(refBit(timeBits, end + 1))
						break;

				if(!wrTopFlag)
				{
					writeLine_x(wFp, xcout("%s", getLocal(rFile)));
					wrTopFlag = 1;
				}
				writeLine_x(wFp, xcout("\t%06u - %06u : %u", IndexToTime(index), IndexToTime(end), end - index + 1));

				index = end;
			}
		}
		if(wrTopFlag)
			writeLine(wFp, "");
	}

	releaseDim(lines, 1);
	releaseBitList(timeBits);
endfunc:
	memFree(rFile);
}
static void ChkLoss(uint outDayCount, char *currPairPtn)
{
	autoList_t *dates;
	autoList_t *currPairs;
	char *outFile = getOutFile("loss.txt");
	FILE *outFp;

	dates = newList();
	currPairs = newList();
	outFp = fileOpen(outFile, "wt");

	{
		autoList_t *files = lsFiles(R_DIR);
		char *file;
		uint index;

		foreach(files, file, index)
		{
			char *lclFile = getLocal(file);

			if(lineExpICase("<8,09>_<6,AZ>.csv", lclFile))
			{
				char *date = strxl(lclFile, 8);
				char *currPair = strxl(lclFile + 9, 6);

				toUpperLine(currPair);

				addElement(dates, (uint)date);
				addElement(currPairs, (uint)currPair);
			}
		}
	}

	dates = autoDistinctLines(dates);
	currPairs = autoDistinctLines(currPairs);

	// ---- filtering ----

	while(outDayCount < getCount(dates))
		memFree((char *)desertElement(dates, 0));

	if(currPairPtn)
	{
		char *currPair;
		uint index;

		foreach(currPairs, currPair, index)
			if(!mbs_stristr(currPair, currPairPtn))
				currPair[0] = '\0';

		trimLines(currPairs);
	}

	// ----

	{
		char *date;
		char *currPair;
		uint date_index;
		uint currPair_index;

		foreach(dates, date, date_index)
		foreach(currPairs, currPair, currPair_index)
		{
			ChkLoss_File(date, currPair, outFp);
		}
	}

	fileClose(outFp);

	openOutDir();

	releaseDim(dates, 1);
	releaseDim(currPairs, 1);
	memFree(outFile);
}
int main(int argc, char **argv)
{
	addFinalizer(ErrorFnlz);

	if(argIs("/A")) // all
	{
		ChkLoss(UINTMAX, NULL);
		return;
	}
	if(argIs("/L")) // last day
	{
		ChkLoss(1, NULL);
		return;
	}
	ChkLoss(7, "USDJPY");
}
