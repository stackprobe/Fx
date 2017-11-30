#include "C:\Factory\Common\all.h"

// ---- mtx ----

#define MTX_OUTPUT "{ecf8ac89-6725-42f5-a5d9-b9619c73f56d}" // shared_uuid

static uint GOM_Mtx;

void GOM_Lock(void)
{
	LOGPOS();

	errorCase(GOM_Mtx);
	GOM_Mtx = mutexLock(MTX_OUTPUT);

	LOGPOS();
}
void GOM_Unlock(void)
{
	LOGPOS();

	errorCase(!GOM_Mtx);
	mutexUnlock(GOM_Mtx);
	GOM_Mtx = 0;

	LOGPOS();
}

// ----

static char *GetActDataDir(void)
{
	static char *dir;

	if(!dir)
	{
		dir = "C:\\BlueFish\\BlueFish\\Fx\\data";

		if(!existDir(dir))
			dir = "C:\\temp"; // devenv
	}
	return dir;
}
static char *GetCsvDataDir(void)
{
	static char *dir;

	if(!dir)
	{
		dir = "C:\\pub\\Fx";

		if(!existDir(dir))
			dir = "C:\\temp"; // devenv
	}
	return dir;
}
int main(int argc, char **argv)
{
	autoList_t *files;
	char *file;
	uint index;

	GOM_Lock();

	files = lsFiles(GetActDataDir());

	foreach(files, file, index)
	{
		char *ext = getExt(file);

		if(!_stricmp(ext, "act"))
		{
			char *destFile = combine_cx(GetCsvDataDir(), changeExt(getLocal(file), "csv"));

			cout("< %s\n", file);
			cout("> %s\n", destFile);

			joinFile(destFile, file);
			removeFile(file);
			memFree(destFile);

			cout("join-ok\n");
		}
	}
	releaseDim(files, 1);

	GOM_Unlock();
}
