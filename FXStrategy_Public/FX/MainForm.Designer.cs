namespace FX
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
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SQLTextBox = new System.Windows.Forms.TextBox();
            this.ResultTextBox = new System.Windows.Forms.TextBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.RestartButton = new System.Windows.Forms.Button();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PopTextBox = new System.Windows.Forms.TextBox();
            this.MaxGenTextBox = new System.Windows.Forms.TextBox();
            this.Worker = new System.ComponentModel.BackgroundWorker();
            this.groupBoxTerm = new System.Windows.Forms.GroupBox();
            this.radioButtonAMonth = new System.Windows.Forms.RadioButton();
            this.radioButtonTenAfter = new System.Windows.Forms.RadioButton();
            this.SellGroupBox = new System.Windows.Forms.GroupBox();
            this.sellSQLTextBox = new System.Windows.Forms.TextBox();
            this.sqlFileLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.sqlFileButton = new System.Windows.Forms.Button();
            this.isSellCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBoxTerm.SuspendLayout();
            this.SellGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // SQLTextBox
            // 
            this.SQLTextBox.Location = new System.Drawing.Point(12, 314);
            this.SQLTextBox.Multiline = true;
            this.SQLTextBox.Name = "SQLTextBox";
            this.SQLTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SQLTextBox.Size = new System.Drawing.Size(652, 74);
            this.SQLTextBox.TabIndex = 0;
            // 
            // ResultTextBox
            // 
            this.ResultTextBox.Location = new System.Drawing.Point(279, 12);
            this.ResultTextBox.Multiline = true;
            this.ResultTextBox.Name = "ResultTextBox";
            this.ResultTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ResultTextBox.Size = new System.Drawing.Size(385, 267);
            this.ResultTextBox.TabIndex = 1;
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(12, 243);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(83, 36);
            this.StartButton.TabIndex = 2;
            this.StartButton.Text = "開始";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(190, 243);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(83, 36);
            this.StopButton.TabIndex = 3;
            this.StopButton.Text = "中断";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // RestartButton
            // 
            this.RestartButton.Location = new System.Drawing.Point(101, 243);
            this.RestartButton.Name = "RestartButton";
            this.RestartButton.Size = new System.Drawing.Size(83, 36);
            this.RestartButton.TabIndex = 4;
            this.RestartButton.Text = "途中から";
            this.RestartButton.UseVisualStyleBackColor = true;
            this.RestartButton.Click += new System.EventHandler(this.RestartButton_Click);
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(12, 285);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(652, 23);
            this.ProgressBar.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "個体数:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "最大世代数:";
            // 
            // PopTextBox
            // 
            this.PopTextBox.Location = new System.Drawing.Point(84, 12);
            this.PopTextBox.Name = "PopTextBox";
            this.PopTextBox.Size = new System.Drawing.Size(100, 19);
            this.PopTextBox.TabIndex = 8;
            // 
            // MaxGenTextBox
            // 
            this.MaxGenTextBox.Location = new System.Drawing.Point(84, 36);
            this.MaxGenTextBox.Name = "MaxGenTextBox";
            this.MaxGenTextBox.Size = new System.Drawing.Size(100, 19);
            this.MaxGenTextBox.TabIndex = 9;
            // 
            // Worker
            // 
            this.Worker.WorkerReportsProgress = true;
            this.Worker.WorkerSupportsCancellation = true;
            this.Worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Worker_DoWork);
            this.Worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.Worker_ProgressChanged);
            this.Worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Worker_RunWorkerCompleted);
            // 
            // groupBoxTerm
            // 
            this.groupBoxTerm.Controls.Add(this.radioButtonAMonth);
            this.groupBoxTerm.Controls.Add(this.radioButtonTenAfter);
            this.groupBoxTerm.Location = new System.Drawing.Point(14, 61);
            this.groupBoxTerm.Name = "groupBoxTerm";
            this.groupBoxTerm.Size = new System.Drawing.Size(170, 44);
            this.groupBoxTerm.TabIndex = 10;
            this.groupBoxTerm.TabStop = false;
            this.groupBoxTerm.Text = "期間";
            this.groupBoxTerm.Visible = false;
            // 
            // radioButtonAMonth
            // 
            this.radioButtonAMonth.AutoSize = true;
            this.radioButtonAMonth.Location = new System.Drawing.Point(99, 18);
            this.radioButtonAMonth.Name = "radioButtonAMonth";
            this.radioButtonAMonth.Size = new System.Drawing.Size(49, 16);
            this.radioButtonAMonth.TabIndex = 1;
            this.radioButtonAMonth.TabStop = true;
            this.radioButtonAMonth.Text = "1ヶ月";
            this.radioButtonAMonth.UseVisualStyleBackColor = true;
            // 
            // radioButtonTenAfter
            // 
            this.radioButtonTenAfter.AutoSize = true;
            this.radioButtonTenAfter.Checked = true;
            this.radioButtonTenAfter.Location = new System.Drawing.Point(22, 18);
            this.radioButtonTenAfter.Name = "radioButtonTenAfter";
            this.radioButtonTenAfter.Size = new System.Drawing.Size(59, 16);
            this.radioButtonTenAfter.TabIndex = 0;
            this.radioButtonTenAfter.TabStop = true;
            this.radioButtonTenAfter.Text = "10日間";
            this.radioButtonTenAfter.UseVisualStyleBackColor = true;
            // 
            // SellGroupBox
            // 
            this.SellGroupBox.Controls.Add(this.sellSQLTextBox);
            this.SellGroupBox.Controls.Add(this.sqlFileLabel);
            this.SellGroupBox.Controls.Add(this.label3);
            this.SellGroupBox.Controls.Add(this.sqlFileButton);
            this.SellGroupBox.Enabled = false;
            this.SellGroupBox.Location = new System.Drawing.Point(14, 137);
            this.SellGroupBox.Name = "SellGroupBox";
            this.SellGroupBox.Size = new System.Drawing.Size(200, 100);
            this.SellGroupBox.TabIndex = 11;
            this.SellGroupBox.TabStop = false;
            // 
            // sellSQLTextBox
            // 
            this.sellSQLTextBox.Location = new System.Drawing.Point(6, 23);
            this.sellSQLTextBox.Multiline = true;
            this.sellSQLTextBox.Name = "sellSQLTextBox";
            this.sellSQLTextBox.Size = new System.Drawing.Size(188, 42);
            this.sellSQLTextBox.TabIndex = 4;
            // 
            // sqlFileLabel
            // 
            this.sqlFileLabel.AutoSize = true;
            this.sqlFileLabel.Location = new System.Drawing.Point(58, 23);
            this.sqlFileLabel.Name = "sqlFileLabel";
            this.sqlFileLabel.Size = new System.Drawing.Size(0, 12);
            this.sqlFileLabel.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "BuySign：";
            // 
            // sqlFileButton
            // 
            this.sqlFileButton.Location = new System.Drawing.Point(119, 71);
            this.sqlFileButton.Name = "sqlFileButton";
            this.sqlFileButton.Size = new System.Drawing.Size(75, 23);
            this.sqlFileButton.TabIndex = 1;
            this.sqlFileButton.Text = "ファイル選択";
            this.sqlFileButton.UseVisualStyleBackColor = true;
            this.sqlFileButton.Click += new System.EventHandler(this.sqlFileButton_Click);
            // 
            // isSellCheckBox
            // 
            this.isSellCheckBox.AutoSize = true;
            this.isSellCheckBox.Location = new System.Drawing.Point(14, 126);
            this.isSellCheckBox.Name = "isSellCheckBox";
            this.isSellCheckBox.Size = new System.Drawing.Size(123, 16);
            this.isSellCheckBox.TabIndex = 0;
            this.isSellCheckBox.Text = "売タイミングを探索？";
            this.isSellCheckBox.UseVisualStyleBackColor = true;
            this.isSellCheckBox.CheckedChanged += new System.EventHandler(this.isSellCheckBox_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 400);
            this.Controls.Add(this.isSellCheckBox);
            this.Controls.Add(this.SellGroupBox);
            this.Controls.Add(this.groupBoxTerm);
            this.Controls.Add(this.MaxGenTextBox);
            this.Controls.Add(this.PopTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.RestartButton);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.ResultTextBox);
            this.Controls.Add(this.SQLTextBox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "MainForm(FX)";
            this.groupBoxTerm.ResumeLayout(false);
            this.groupBoxTerm.PerformLayout();
            this.SellGroupBox.ResumeLayout(false);
            this.SellGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SQLTextBox;
        private System.Windows.Forms.TextBox ResultTextBox;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button RestartButton;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PopTextBox;
        private System.Windows.Forms.TextBox MaxGenTextBox;
        private System.ComponentModel.BackgroundWorker Worker;
        private System.Windows.Forms.GroupBox groupBoxTerm;
        private System.Windows.Forms.RadioButton radioButtonAMonth;
        private System.Windows.Forms.RadioButton radioButtonTenAfter;
        private System.Windows.Forms.GroupBox SellGroupBox;
        private System.Windows.Forms.CheckBox isSellCheckBox;
        private System.Windows.Forms.Button sqlFileButton;
        private System.Windows.Forms.TextBox sellSQLTextBox;
        private System.Windows.Forms.Label sqlFileLabel;
        private System.Windows.Forms.Label label3;
    }
}