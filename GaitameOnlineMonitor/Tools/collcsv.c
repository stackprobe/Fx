#include "C:\Factory\Common\all.h"

#define R_DIR "S:\\Fx"
#define W_DIR "C:\\var\\Fx"

int main(int argc, char **argv)
{
	autoList_t *files;
	char *file;
	uint index;

	LOGPOS();

	files = lsFiles(R_DIR);

	LOGPOS();

	foreach(files, file, index)
	{
		char *ext = getExt(file);

		if(!_stricmp(ext, "csv"))
		{
			char *destFile = combine(W_DIR, getLocal(file));

			cout("< %s\n", file);
			cout("> %s\n", destFile);

			joinFile(destFile, file);
			removeFile(file);
			memFree(destFile);

			cout("join-ok\n");
		}
	}
	releaseDim(files, 1);

	LOGPOS();
}
