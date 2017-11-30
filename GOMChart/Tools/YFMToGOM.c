/*
	LongestAskBid.exe 実行して完了。以下であることを確認済み。@ 2017.5.25

		Ask, Bid 最長 7 バイト
*/

#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\Calc.h"

//#define R_DIR "C:\\var\\dat\\YFMToGOM_Fx_20150728-20150803" // test
//#define R_DIR "C:\\var\\dat\\YFMToGOM_Fx_20150728と20150803だけ" // test
//#define R_DIR "C:\\var\\dat\\YFMToGOM_Fx_20161231-20170102" // test
#define R_DIR "C:\\var\\Fx_20150728-20170331"

#define FIRST_STAMP "20150728160514"
#define END_STAMP   "20170329165956"

#define BASEMENT 10

static char *WDir;
static char *CPair;

// ---- CPairFiles ----

typedef struct Price_st
{
	time_t TSec;
	char Ask[8];
	char Bid[8];
}
Price_t;

static autoList_t *CPairFiles;
static uint CPF_Index; // next index
static FILE *CPF_Fp;
static Price_t CurrPrice;
static Price_t NextPrice;

static int ReadPrice(Price_t *i) // ret: ? ! EOF
{
	char *line = readLine(CPF_Fp);
	autoList_t *tokens;
	uint c = 0;

	if(!line)
		return 0;

	tokens = tokenize(line, ',');

	i->TSec = compactStampToTime(getLine(tokens, c++));
	strcpy(i->Ask, getLine(tokens, c++));
	strcpy(i->Bid, getLine(tokens, c++));

	releaseDim(tokens, 1);
	memFree(line);
	return 1;
}
static void InitCPairFiles(void)
{
	autoList_t *files = lsFiles(R_DIR);
	char *file;
	uint index;

	CPairFiles = newList();

	foreach(files, file, index)
	{
		char *lFile = getLocal(file);

		errorCase(strlen(lFile) < 9);

		if(!_strnicmp(lFile + 9, CPair, 6))
			addElement(CPairFiles, (uint)strx(file));
	}
	releaseDim(files, 1);

	errorCase(getCount(CPairFiles) < 1);

	CPF_Index = 1;
	CPF_Fp = fileOpen(getLine(CPairFiles, 0), "rt");

	errorCase(!ReadPrice(&CurrPrice)); // 1 件目
	errorCase(!ReadPrice(&NextPrice)); // 2 件目
}
static void FnlzCPairFiles(void)
{
	releaseDim(CPairFiles, 1);
}

static char *Ask;
static char *Bid;

static int GetPrice(time_t tSec) // ret: ? まだあるよ。
{
	errorCase(tSec < CurrPrice.TSec); // 戻るな。

	if(NextPrice.TSec < tSec)
	{
		CurrPrice = NextPrice;

	reread:
		if(!ReadPrice(&NextPrice))
		{
			fileClose(CPF_Fp);

			if(getCount(CPairFiles) <= CPF_Index)
				return 0;

			CPF_Fp = fileOpen(getLine(CPairFiles, CPF_Index), "rt");
			CPF_Index++;

			errorCase(!ReadPrice(&NextPrice));
		}
		if(CurrPrice.TSec == NextPrice.TSec) // 少なくとも 20170101_AUDCHF.csv にある。20170101092908 が２回ある。
			goto reread;

		errorCase(NextPrice.TSec <= CurrPrice.TSec); // 読み込んだレコードの時間が進んでない。
	}
	errorCase(NextPrice.TSec < tSec); // 先行き過ぎ。

	memFree(Ask);
	memFree(Bid);

	{
		time_t n = tSec - CurrPrice.TSec;
		time_t d = NextPrice.TSec - CurrPrice.TSec;

		Ask = calcLine_cx(
			CurrPrice.Ask,
			'+',
			calcLine_xx(
				calcLine_xx(
					calcLine(
						NextPrice.Ask,
						'-',
						CurrPrice.Ask,
						10,
						0
						),
					'*',
					xcout("%I64d", n),
					10,
					0
					),
				'/',
				xcout("%I64d", d),
				10,
				BASEMENT
				),
			10,
			0
			);

		Bid = calcLine_cx(
			CurrPrice.Bid,
			'+',
			calcLine_xx(
				calcLine_xx(
					calcLine(
						NextPrice.Bid,
						'-',
						CurrPrice.Bid,
						10,
						0
						),
					'*',
					xcout("%I64d", n),
					10,
					0
					),
				'/',
				xcout("%I64d", d),
				10,
				BASEMENT
				),
			10,
			0
			);
	}

	return 1;
}

// ----

static char *Open;
static char *High;
static char *Low;

// ---- WrPrice ----

static char *WP_Date;
static FILE *WP_Fp;

static void WrPrice(char *stamp)
{
	if(!WP_Date || !startsWith(stamp, WP_Date))
	{
		char *file;

		memFree(WP_Date);
		WP_Date = strxl(stamp, 8);

		file = combine_cx(WDir, xcout("%s_%s.csv", WP_Date, CPair));

		cout("WrPrice_file: %s\n", file);

		if(WP_Fp)
			fileClose(WP_Fp);

		WP_Fp = fileOpen(file, "wt");
		memFree(file);
	}
	writeLine_x(WP_Fp, xcout("%s,%s,%s,%s,%s,%s", stamp, Open, High, Low, Ask, Bid));
}
static void WrPriceClose(void)
{
	LOGPOS();

	memFree(WP_Date);
	WP_Date = NULL;

	if(WP_Fp)
		fileClose(WP_Fp);

	WP_Fp = NULL;
}

// ----

static void Conv_CPair(char *cPair)
{
	time_t tSec;

	CPair = cPair;
	InitCPairFiles();

	Open = strx(CurrPrice.Bid);
	High = strx(CurrPrice.Bid);
	Low  = strx(CurrPrice.Bid);

	for(tSec = CurrPrice.TSec; GetPrice(tSec); tSec++)
	{
		char *stamp = makeCompactStamp(getStampDataTime(tSec));

		if(endsWith(stamp, "060000"))
		{
			memFree(Open);
			memFree(High);
			memFree(Low);

			Open = strx(Bid);
			High = strx(Bid);
			Low  = strx(Bid);
		}
		else
		{
			if(compCalcLine(High, Bid, 10) < 0)
			{
				memFree(High);
				High = strx(Bid);
			}
			if(compCalcLine(Bid, Low, 10) < 0)
			{
				memFree(Low);
				Low = strx(Bid);
			}
		}
		if(endsWith(stamp, "00"))
			WrPrice(stamp);

		memFree(stamp);
	}
	WrPriceClose();

	FnlzCPairFiles();
	CPair = NULL;
}
static void ConvMain(void)
{
	WDir = getOutFile("Fx");
	createDir(WDir);

	// ---- CPairs ----

	Conv_CPair("AUDCHF");
	Conv_CPair("AUDJPY");
	Conv_CPair("AUDUSD");
	Conv_CPair("CADCHF");
	Conv_CPair("CADJPY");
	Conv_CPair("CHFJPY");
	Conv_CPair("CNHJPY");
	Conv_CPair("EURAUD");
	Conv_CPair("EURCHF");
	Conv_CPair("EURGBP");
	Conv_CPair("EURJPY");
	Conv_CPair("EURUSD");
	Conv_CPair("GBPCHF");
	Conv_CPair("GBPJPY");
	Conv_CPair("GBPUSD");
	Conv_CPair("HKDJPY");
	Conv_CPair("NZDJPY");
	Conv_CPair("NZDUSD");
	Conv_CPair("USDCHF");
	Conv_CPair("USDHKD");
	Conv_CPair("USDJPY");
	Conv_CPair("ZARJPY");

	// ----

	openOutDir();
}
int main(int argc, char **argv)
{
	ConvMain();
	termination(0);
}
