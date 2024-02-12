using System.ComponentModel;
using MineMinderX.Util;

namespace MineMinderX.Process
{
    public class ProcessWorker
    {
        public void DoWork(object sender, DoWorkEventArgs e, string processName) {
            var worker = (BackgroundWorker)sender;
            var rect = new ProcessData.Rect();

            try {
                var hWnd = WindowsHelper.FindWindow(null, processName);
                WindowsHelper.GetWindowRect(hWnd, ref rect);

                if (hWnd.ToInt32() == 0x0) {
                    worker.ReportProgress(0);
                    return;
                }
                
                var data = new ProcessData(processName, hWnd, rect);
                worker.ReportProgress(1, data);
            }
            catch {
                worker.ReportProgress(0);
            }
        }
    }
}