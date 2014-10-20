using System;
using System.Windows;
using GalaSoft.MvvmLight.Threading;
using IxaCalc.ViewModel;

namespace IxaCalc
{
    /// <summary>
    /// アプリケーションクラス
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class. 
        /// コンストラクタ
        /// </summary>
        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;
        }

        /// <summary>
        /// アプリケーション開始時
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">引数</param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            RootVisual = new MainPage();
            DispatcherHelper.Initialize();
        }

        /// <summary>
        /// アプリケーション終了時
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">引数</param>
        private void Application_Exit(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// ハンドルされてない例外があった時に実行されるイベント
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">パラメータ</param>
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (!System.Diagnostics.Debugger.IsAttached)
            {

                // メモ : これにより、アプリケーションは例外がスローされた後も実行され続け、例外は
                // ハンドルされません。
                // 実稼動アプリケーションでは、このエラー処理は、Web サイトにエラーを報告し、
                // アプリケーションを停止させるものに置換される必要があります。
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); });
            }
        }

        /// <summary>
        /// エラーをレポートする関数
        /// </summary>
        /// <param name="e">捕捉できなかった例外に関する引数</param>
        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }
    }
}
