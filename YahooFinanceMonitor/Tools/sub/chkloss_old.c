#include "C:\Factory\Common\all.h"

#define R_DIR "C:\\tmp\\Fx"

static uint NextTime(autoList_t *lines)
{
	if(getCount(lines) == 0)
	{
		return UINTMAX;
	}

	{
		char *line = (char *)unaddElement(lines);
		uint tm;

		errorCase(strlen(line) < 14);

		line[14] = '\0';
		tm = toValue(line + 8);

		memFree(line);
		return tm;
	}
}
static uint Add2Sec(uint tm)
{
	tm += 2;

	if(tm % 100 == 60)
	{
		tm += 40;

		if(tm % 10000 == 6000)
		{
			tm += 4000;
		}
	}
	return tm;
}
static void ChkFile(char *file)
{
	autoList_t *lines = readLines(file);
	uint trueTm;
	uint currTm;
	uint renzoku = 0;

	cout("%s\n", file);

	reverseElements(lines);
	currTm = NextTime(lines);

	for(trueTm = 0; trueTm < 240000; trueTm = Add2Sec(trueTm))
	{
		if(trueTm < currTm)
		{
			renzoku++;
			cout("\t%06u %u\n", trueTm, renzoku);
		}
		else
		{
			currTm = NextTime(lines);
			renzoku = 0;
		}
	}
	releaseDim(lines, 1);
}
static void DoMain(void)
{
	autoList_t *files = lsFiles(R_DIR);
	char *file;
	uint index;

	foreach(files, file, index)
	{
		if(!_stricmp("csv", getExt(file)))
		{
			ChkFile(file);
		}
	}
	releaseDim(files, 1);
}
int main(int argc, char **argv)
{
	DoMain();
}
