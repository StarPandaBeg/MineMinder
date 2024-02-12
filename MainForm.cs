
using System;
using System.ComponentModel;
using System.Windows.Forms;
using MineMinderX.AI;
using MineMinderX.GUI;
using MineMinderX.Process;
using MineMinderX.Util;

namespace MineMinderX
{
    public partial class MainForm : Form
    {
        private ProcessData _processData;
        private BoardWorker _boardWorker;
        private BoardRenderer _boardRenderer;
        private BoardAI _boardAi;

        private ProcessData ProcessData
        {
            get => _processData;
            set
            {
                _processData = value;
                grAI.Enabled = value != null;
                _boardAi.UpdateProcessData(value);
                _boardWorker.UpdateProcessData(value);
            }
        }
        
        public MainForm() {
            InitializeComponent();
            
            _boardWorker = new BoardWorker();
            _boardRenderer = new BoardRenderer(_boardWorker.Board, canvas);
            _boardAi = new BoardAI(_boardWorker.Board, null);
            ProcessData = null;
            GUILog.Init(tbLog);
            
            processWorker.DoWork += (sender, e) => new ProcessWorker().DoWork(sender, e, tbProcessName.Text);
            processWorker.ProgressChanged += ProcessWorkerOnProgress;

            boardWorker.DoWork += _boardWorker.DoWork;
            boardWorker.RunWorkerAsync();

            aiWorker.DoWork += (sender, e) => _boardAi.DoWork();
            aiWorker.RunWorkerCompleted += AiWorkerOnCompleted;

            logWorker.DoWork += GUILog.DoWork;
            logWorker.ProgressChanged += OnLog;
            logWorker.RunWorkerAsync();
        }

        private void ProcessWorkerOnProgress(object sender, ProgressChangedEventArgs e) {
            var status = e.ProgressPercentage == 1;
            
            lblProcessStatus.Text = (status) ? "НАЙДЕН" : "ОШИБКА";
            ProcessData = (status) ? (ProcessData)e.UserState : null;
            GUILog.Log((status) ? "Процесс игры обнаружен и захвачен" : "Процесс игры не обнаружен");
            
            btnProcessReload.Enabled = true;
        }

        private void ProcessReloadOnClick(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(tbProcessName.Text)) return;
            
            btnProcessReload.Enabled = false;
            processWorker.RunWorkerAsync();
        }

        private void AIStepOnClick(object sender, EventArgs e) {
            var result = _boardAi.Step();
            GUILog.Log(result ? "Шаг пройден" : "Никаких изменений");
        }

        private void OnLog(object sender, ProgressChangedEventArgs e) {
            tbLog.AppendText(e.UserState.ToString());
        }

        private void AIRunOnClick(object sender, EventArgs e) {
            btnAiRun.Enabled = false;
            aiWorker.RunWorkerAsync();
        }

        private void AiWorkerOnCompleted(object sender, RunWorkerCompletedEventArgs e) {
            btnAiRun.Enabled = true;
            GUILog.Log("ИИ закончил работу");
        }
    }
}