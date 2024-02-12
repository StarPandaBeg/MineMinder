namespace MineMinderX
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.GroupBox groupBox3;
            this.btnProcessReload = new System.Windows.Forms.Button();
            this.lblProcessStatus = new System.Windows.Forms.Label();
            this.tbProcessName = new System.Windows.Forms.TextBox();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.grAI = new System.Windows.Forms.GroupBox();
            this.btnAiStep = new System.Windows.Forms.Button();
            this.btnAiRun = new System.Windows.Forms.Button();
            this.canvas = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.processWorker = new System.ComponentModel.BackgroundWorker();
            this.boardWorker = new System.ComponentModel.BackgroundWorker();
            this.logWorker = new System.ComponentModel.BackgroundWorker();
            this.aiWorker = new System.ComponentModel.BackgroundWorker();
            groupBox1 = new System.Windows.Forms.GroupBox();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            groupBox3 = new System.Windows.Forms.GroupBox();
            groupBox1.SuspendLayout();
            groupBox3.SuspendLayout();
            this.grAI.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.btnProcessReload);
            groupBox1.Controls.Add(this.lblProcessStatus);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(this.tbProcessName);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new System.Drawing.Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(225, 134);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Процесс";
            // 
            // btnProcessReload
            // 
            this.btnProcessReload.Location = new System.Drawing.Point(6, 94);
            this.btnProcessReload.Name = "btnProcessReload";
            this.btnProcessReload.Size = new System.Drawing.Size(209, 32);
            this.btnProcessReload.TabIndex = 4;
            this.btnProcessReload.Text = "Обновить";
            this.btnProcessReload.UseVisualStyleBackColor = true;
            this.btnProcessReload.Click += new System.EventHandler(this.ProcessReloadOnClick);
            // 
            // lblProcessStatus
            // 
            this.lblProcessStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblProcessStatus.Location = new System.Drawing.Point(70, 67);
            this.lblProcessStatus.Margin = new System.Windows.Forms.Padding(0);
            this.lblProcessStatus.Name = "lblProcessStatus";
            this.lblProcessStatus.Size = new System.Drawing.Size(145, 21);
            this.lblProcessStatus.TabIndex = 3;
            this.lblProcessStatus.Text = "ОШИБКА";
            // 
            // label2
            // 
            label2.Location = new System.Drawing.Point(6, 67);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(65, 21);
            label2.TabIndex = 2;
            label2.Text = "Статус:";
            // 
            // tbProcessName
            // 
            this.tbProcessName.Location = new System.Drawing.Point(70, 35);
            this.tbProcessName.Name = "tbProcessName";
            this.tbProcessName.Size = new System.Drawing.Size(145, 22);
            this.tbProcessName.TabIndex = 1;
            // 
            // label1
            // 
            label1.Location = new System.Drawing.Point(6, 30);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(47, 29);
            label1.TabIndex = 0;
            label1.Text = "Имя:";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(this.tbLog);
            groupBox3.Location = new System.Drawing.Point(465, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(308, 134);
            groupBox3.TabIndex = 7;
            groupBox3.TabStop = false;
            groupBox3.Text = "Лог действий";
            // 
            // tbLog
            // 
            this.tbLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbLog.Location = new System.Drawing.Point(6, 25);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ReadOnly = true;
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLog.Size = new System.Drawing.Size(296, 101);
            this.tbLog.TabIndex = 6;
            // 
            // grAI
            // 
            this.grAI.Controls.Add(this.btnAiStep);
            this.grAI.Controls.Add(this.btnAiRun);
            this.grAI.Location = new System.Drawing.Point(234, 3);
            this.grAI.Name = "grAI";
            this.grAI.Size = new System.Drawing.Size(225, 134);
            this.grAI.TabIndex = 5;
            this.grAI.TabStop = false;
            this.grAI.Text = "ИИ";
            // 
            // btnAiStep
            // 
            this.btnAiStep.Location = new System.Drawing.Point(6, 94);
            this.btnAiStep.Name = "btnAiStep";
            this.btnAiStep.Size = new System.Drawing.Size(209, 32);
            this.btnAiStep.TabIndex = 5;
            this.btnAiStep.Text = "Шаг";
            this.btnAiStep.UseVisualStyleBackColor = true;
            this.btnAiStep.Click += new System.EventHandler(this.AIStepOnClick);
            // 
            // btnAiRun
            // 
            this.btnAiRun.Location = new System.Drawing.Point(6, 25);
            this.btnAiRun.Name = "btnAiRun";
            this.btnAiRun.Size = new System.Drawing.Size(209, 63);
            this.btnAiRun.TabIndex = 4;
            this.btnAiRun.Text = "Запустить";
            this.btnAiRun.UseVisualStyleBackColor = true;
            this.btnAiRun.Click += new System.EventHandler(this.AIRunOnClick);
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.SystemColors.Window;
            this.canvas.Location = new System.Drawing.Point(12, 12);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(776, 400);
            this.canvas.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(groupBox3);
            this.panel2.Controls.Add(this.grAI);
            this.panel2.Controls.Add(groupBox1);
            this.panel2.Location = new System.Drawing.Point(12, 418);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(776, 144);
            this.panel2.TabIndex = 1;
            // 
            // processWorker
            // 
            this.processWorker.WorkerReportsProgress = true;
            // 
            // boardWorker
            // 
            this.boardWorker.WorkerReportsProgress = true;
            // 
            // logWorker
            // 
            this.logWorker.WorkerReportsProgress = true;
            // 
            // aiWorker
            // 
            this.aiWorker.WorkerReportsProgress = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 568);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.canvas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "MineMinder X";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            this.grAI.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.ComponentModel.BackgroundWorker aiWorker;

        private System.ComponentModel.BackgroundWorker logWorker;

        private System.ComponentModel.BackgroundWorker boardWorker;

        private System.Windows.Forms.GroupBox grAI;

        private System.ComponentModel.BackgroundWorker processWorker;

        private System.Windows.Forms.TextBox tbLog;

        private System.Windows.Forms.Button btnAiRun;
        private System.Windows.Forms.Button btnAiStep;

        private System.Windows.Forms.TextBox tbProcessName;
        private System.Windows.Forms.Label lblProcessStatus;
        private System.Windows.Forms.Button btnProcessReload;

        private System.Windows.Forms.Panel canvas;
        private System.Windows.Forms.Panel panel2;

        #endregion
    }
}