/*
	ShowCPairs > 1.txt
	TypeGroupBy /-C 1.txt
*/

#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	autoList_t *files = lsFiles("C:\\var\\Fx_20150728-20170331");
	char *file;
	uint index;

	foreach(files, file, index)
	{
		char *lFile = getLocal(file);

		if(lineExpICase("<8,09>_<6,AZ>.csv", lFile))
		{
			lFile += 9;
			lFile[6] = '\0';

			cout("%s\n", lFile);
		}
	}
	releaseDim(files, 1);
}
