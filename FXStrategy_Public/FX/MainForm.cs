using FX.GenerationChangeGroup;
using FX.Trees;
using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FX
{
    public partial class MainForm : Form
    {
        private int PopulationSize = 0;
        private int MaxGen = 0;

        private Tree BestIndividual { get; set; }

        public MainForm()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            ResultTextBox.Text = "";
            SQLTextBox.Text = "";

            if (!int.TryParse(PopTextBox.Text, out PopulationSize))
                PopulationSize = 500;

            if (!int.TryParse(MaxGenTextBox.Text, out MaxGen))
                MaxGen = 100;
            if (!Worker.IsBusy)
            {
                ResultTextBox.Text += "開始" + Environment.NewLine;
                ResultTextBox.Text += "--------------" + Environment.NewLine;
                Worker.RunWorkerAsync();
            }
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {

        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            Worker.CancelAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int generation = 0;
            var system = new GenerationChange(PopulationSize, radioButtonTenAfter.Checked, sellSQLTextBox.Text);
            system.Initialization(false);
            BestIndividual = system.BestIndividual;
            Worker.ReportProgress(generation * 100 / MaxGen, generation);

            //集団ファイル削除
            var target = new DirectoryInfo(@"../../Log/");
            //ファイル消す
            foreach (var file in target.GetFiles())
            {
                file.Delete();
            }
            
            while (generation < MaxGen)
            {
                system.OneIteration();
                generation++;
                BestIndividual = system.BestIndividual;

                // キャンセルされてないか定期的にチェック
                if (Worker.CancellationPending)
                {
                    e.Cancel = true;
                    e.Result = "キャンセル";
                    return;
                }

                Worker.ReportProgress(generation * 100 / MaxGen, generation);
                system.SavePopulation(generation);
            }

            // このメソッドからの戻り値
            e.Result = "すべて完了";
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var gen = e.UserState.ToString();
            ProgressBar.Value = e.ProgressPercentage;
            ResultTextBox.Text += gen + " : " + Environment.NewLine + BestIndividual.FitnessInfo?.ToString() + Environment.NewLine;
            SQLTextBox.Text +=  isSellCheckBox.Checked ? gen + " : " + Environment.NewLine + BestIndividual.SellSQLString + Environment.NewLine + BestIndividual.SellLCSQLString + Environment.NewLine : gen + " : " + Environment.NewLine + BestIndividual.BuySQLString + Environment.NewLine;
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ResultTextBox.Text += e.Result + Environment.NewLine;
        }

        private void isSellCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SellGroupBox.Enabled = isSellCheckBox.Checked;
        }

        private void sqlFileButton_Click(object sender, EventArgs e)
        {
            //OpenFileDialogクラスのインスタンスを作成
            var ofd = new OpenFileDialog();
            //はじめのファイル名を指定する
            //はじめに「ファイル名」で表示される文字列を指定する
            ofd.FileName = "";
            //はじめに表示されるフォルダを指定する
            //指定しない（空の文字列）の時は、現在のディレクトリが表示される
            ofd.InitialDirectory = @"C:\";
            //[ファイルの種類]に表示される選択肢を指定する
            //指定しないとすべてのファイルが表示される
            ofd.Filter =
                "SQLファイル(*.sql)|*.sql";
            //[ファイルの種類]ではじめに
            //「すべてのファイル」が選択されているようにする
            ofd.FilterIndex = 1;
            //タイトルを設定する
            ofd.Title = "開くファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            ofd.RestoreDirectory = true;


            //ダイアログを表示する
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var sr = new StreamReader(ofd.FileName, new UTF8Encoding(false));
                sellSQLTextBox.Text = sr.ReadToEnd();
            }
        }
    }
}
