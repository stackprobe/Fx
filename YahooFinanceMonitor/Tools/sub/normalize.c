#include "C:\Factory\Common\all.h"

#define R_DIR "C:\\tmp\\Fx"
#define PERIOD_SEC 2

static char *TsuukaPtn = "USDJPY";

static autoList_t *RFiles;
static uint RFIndex;
static FILE *RFp;
static FILE *WFp;

static char *GetNextRFile(void)
{
	while(RFIndex < getCount(RFiles))
	{
		char *file = getLine(RFiles, RFIndex);

		RFIndex++;

		if(mbs_stristr(getLocal(file), TsuukaPtn))
			return file;
	}
	return NULL;
}
static char *ReadLine(void)
{
restart:
	if(RFp)
	{
		char *line = readLine(RFp);

		if(line)
			return line;

		fileClose(RFp);
	}

	{
		char *file = GetNextRFile();

		if(!file)
			return NULL;

		cout("FILE: %s\n", file);

		RFp = fileOpen(file, "rt");
	}
	goto restart;
}

static char *Line;
static char *Stamp;
static time_t ITime;
static char *Data;

static void ParseLine_x(char *line)
{
	char *p;

	memFree(Line);
	memFree(Stamp);
	memFree(Data);

	Line = strx(line);
	p = strchr(line, ',');

	errorCase(!p);

	*p = '\0';
	Stamp = strx(line);
	ITime = compactStampToTime(Stamp);
	Data = strx(p + 1);

	memFree(line);
}

static time_t WITime;
static char *LastData;

static void DoMerge(void)
{
	char *line = ReadLine();

	errorCase(!line);
	ParseLine_x(line);
	WITime = ITime;

	writeLine_x(WFp, xcout("0,%s", Line));

	WITime += PERIOD_SEC;
	LastData = strx(Data);

	for(; ; )
	{
		line = ReadLine();

		if(!line)
			break;

		ParseLine_x(line);

		while(WITime < ITime) // ITime に追い付かない。-> ロストしたところ！
		{
			char *stamp = makeCompactStamp(getStampDataTime(WITime));

			writeLine_x(WFp, xcout("1,%s,%s", stamp, LastData));
			memFree(stamp);

			WITime += PERIOD_SEC;
		}
		errorCase(ITime < WITime); // ITime を通り過ぎた。-> 不正な ITime

		writeLine_x(WFp, xcout("0,%s", Line));

		WITime += PERIOD_SEC;
		memFree(LastData);
		LastData = strx(Data);
	}
}
static void DoNormalize(char *outFile)
{
	RFiles = lsFiles(R_DIR);
	rapidSortLines(RFiles);
	RFIndex = 0;
	RFp = NULL;
	WFp = fileOpen(outFile, "wt");

	DoMerge();

	fileClose(WFp);
}
int main(int argc, char **argv)
{
	DoNormalize(nextArg());
}
