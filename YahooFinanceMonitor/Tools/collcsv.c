#include "C:\Factory\Common\all.h"

#define R_DIR "S:\\Fx"
#define W_DIR "C:\\var\\Fx"

int main(int argc, char **argv)
{
	char *today;
	autoList_t *files;
	char *file;
	uint index;

	errorCase(!existDir(R_DIR));
	errorCase(!existDir(W_DIR));

	today = makeCompactStamp(NULL);

	{
		uint hms = toValue(today + 8);

		cout("hms: %06u\n", hms);

		if(hms < 500 || 235000 < hms) // ? 23:50:00 ` 00:05:00 ‚ÌŠÔ
		{
			cout("##################################\n");
			cout("## “ú•t•ÏX‚ª‹ß‚¢‚Ì‚Å’†Ž~‚µ‚Ü‚· ##\n");
			cout("##################################\n");
			coSleep(2000);
			termination(1);
		}
	}

	today[8] = '\0';

	files = lsFiles(R_DIR);

	foreach(files, file, index)
	{
		cout("%s\n", file);

		if(startsWith(getLocal(file), today))
		{
			cout("‚±‚ê¡“ú‚ÌI\n");
			continue;
		}
		coExecute_x(xcout("MOVE \"%s\" \"%s\"", file, W_DIR));
	}
	memFree(today);
	releaseDim(files, 1);
}
