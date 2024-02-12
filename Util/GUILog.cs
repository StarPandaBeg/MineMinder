using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace MineMinderX.Util
{
    public static class GUILog
    {
        private static Queue<string> _logQueue = new ();
        private static TextBox _textBox;
        
        public static void Init(TextBox textBox) {
            _textBox = textBox;
        }

        public static void DoWork(object sender, DoWorkEventArgs e) {
            var worker = (BackgroundWorker)sender;
            
            while (true) {
                while (_logQueue.Count > 0) {
                    worker.ReportProgress(0, _logQueue.Dequeue());
                }
                Thread.Sleep(10);
            }
        }

        public static void Log(string s) {
            _logQueue.Enqueue($"{s}\r\n");
        }
    }
}