#include "C:\Factory\Common\all.h"

#define MUTEX_NAME_OUT_FILE "{b560234e-3af9-41cb-a51c-81c7d0506cd7}" // shared_uuid

#define R_DIR "C:\\pub\\Fx"
#define W_DIR "C:\\pub\\Fx\\snapshot"

int main(int argc, char **argv)
{
	uint mtx;
	autoList_t *files;
	char *file;
	uint index;

	errorCase(!existDir(R_DIR));
	errorCase(!existDir(W_DIR));

	recurClearDir(W_DIR);

	mtx = mutexLock(MUTEX_NAME_OUT_FILE);
	{
		files = lsFiles(R_DIR);
	}
	mutexUnlock(mtx);

	foreach(files, file, index)
	{
		char *wFile = combine(W_DIR, getLocal(file));

		cout("< %s\n", file);
		cout("> %s\n", wFile);

		mtx = mutexLock(MUTEX_NAME_OUT_FILE);
		{
			// 運用上、アンロック中に file が削除・リネームされることは無いはず。@ 2016.3.2

			copyFile(file, wFile);
		}
		mutexUnlock(mtx);

		sleep(100); // 確実に他プロセスに制御を渡す。

		memFree(wFile);
	}
	releaseDim(files, 1);
}
