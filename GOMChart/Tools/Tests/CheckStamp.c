#include "C:\Factory\Common\all.h"

#define R_FILE "C:\\var\\Fx_20150728-20170331\\20170101_AUDCHF.csv"
//#define R_FILE "C:\\var\\Fx_20150728-20170331\\20170102_AUDCHF.csv"

int main(int argc, char **argv)
{
	FILE *fp = fileOpen(R_FILE, "rt");
	char *line;

	line = readLine(fp);
	errorCase(!line);

	for(; ; )
	{
		char *nLine = readLine(fp);

		if(!nLine)
			break;

		cout("1:%s\n", line);
		cout("2:%s\n", nLine);

		errorCase(0 <= strcmp(line, nLine)); // ? i‚ñ‚Å‚¢‚È‚¢B

		memFree(line);
		line = nLine;
	}
	fileClose(fp);
}
