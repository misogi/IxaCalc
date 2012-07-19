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
            Startup += Application_Startup;
            Exit += Application_Exit;
            UnhandledException += Application_UnhandledException;

            InitializeComponent();
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
            ViewModelLocator.Cleanup();
        }

        /// <summary>
        /// ハンドルされてない例外があった時に実行されるイベント
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">パラメータ</param>
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // If the app is running outside of the debugger then report the exception using
            // the browser's exception mechanism. On IE this will display it a yellow alert 
            // icon in the status bar and Firefox will display a script error.
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(delegate
                {
                    ReportErrorToDOM(e);
                });
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
