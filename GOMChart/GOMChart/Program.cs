﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Win32;
using System.Text;
using System.IO;
using System.Reflection;

namespace Charlotte
{
	static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			onBoot();

			Application.ThreadException += new ThreadExceptionEventHandler(applicationThreadException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(currentDomainUnhandledException);
			SystemEvents.SessionEnding += new SessionEndingEventHandler(sessionEnding);

			Mutex procMutex = new Mutex(false, APP_IDENT);

			if (procMutex.WaitOne(0) && GlobalProcMtx.Create(APP_IDENT, APP_TITLE))
			{
				checkSelfDir();
				Directory.SetCurrentDirectory(selfDir);
				checkAloneExe();
				checkLogonUser();

				// orig >

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainWin());

				// < orig

				GlobalProcMtx.Release();
				procMutex.ReleaseMutex();
			}
			procMutex.Close();
		}

		public const string APP_IDENT = "{6045ba31-5e7a-44ff-8143-2be484cc6008}";
		public const string APP_TITLE = "GOMChart";

		private static void applicationThreadException(object sender, ThreadExceptionEventArgs e)
		{
			try
			{
				MessageBox.Show(
					"[Application_ThreadException]\n" + e.Exception,
					APP_TITLE + " / Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);
			}
			catch
			{ }

			Environment.Exit(1);
		}

		private static void currentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			try
			{
				MessageBox.Show(
					"[CurrentDomain_UnhandledException]\n" + e.ExceptionObject,
					APP_TITLE + " / Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);
			}
			catch
			{ }

			Environment.Exit(2);
		}

		private static void sessionEnding(object sender, SessionEndingEventArgs e)
		{
			Environment.Exit(3);
		}

		public static string selfFile;
		public static string selfDir;

		public static void onBoot()
		{
			selfFile = Assembly.GetEntryAssembly().Location;
			selfDir = Path.GetDirectoryName(selfFile);
		}

		private static void checkSelfDir()
		{
			string dir = selfDir;
			Encoding SJIS = Encoding.GetEncoding(932);

			if (dir != SJIS.GetString(SJIS.GetBytes(dir)))
			{
				MessageBox.Show(
					"Shift_JIS に変換出来ない文字を含むパスからは実行できません。",
					APP_TITLE + " / エラー",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);

				Environment.Exit(4);
			}
			if (dir.Substring(1, 2) != ":\\")
			{
				MessageBox.Show(
					"ネットワークパスからは実行できません。",
					APP_TITLE + " / エラー",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);

				Environment.Exit(5);
			}
		}

		private static void checkAloneExe()
		{
			if (File.Exists("GOMChart.sig")) // リリースに含まれるファイル
				return;

			if (Directory.Exists(@"..\Debug")) // ? devenv
				return;

			MessageBox.Show(
				"WHY AM I ALONE ?",
				"",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error
				);

			Environment.Exit(6);
		}

		private static void checkLogonUser()
		{
			string userName = Environment.GetEnvironmentVariable("UserName");
			Encoding SJIS = Encoding.GetEncoding(932);

			if (
				userName == null ||
				userName == "" ||
				userName != SJIS.GetString(SJIS.GetBytes(userName)) ||
				userName.StartsWith(" ") ||
				userName.EndsWith(" ")
				)
			{
				MessageBox.Show(
					"Windows ログオンユーザー名に問題があります。",
					APP_TITLE + " / エラー",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);

				Environment.Exit(7);
			}
		}
	}
}
