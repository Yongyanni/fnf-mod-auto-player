using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FnfModAutoPlayer
{
    public partial class Form1 : Form
    {
        private GlobalKeyboardHook _globalHook;
        private bool _waitingForSpace = false;
        private bool _playing = false;
        private bool _ready = false;

        private List<NoteInfo> _notes;

        public Form1()
        {
            InitializeComponent();

            // 全局键盘钩子（后台也能监听空格）
            _globalHook = new GlobalKeyboardHook();
            _globalHook.KeyDownEvent += GlobalKeyDown;

            // 初始 UI 状态
            buttonStart.Enabled = false;
            buttonStop.Enabled = false;
            labelTip.Visible = false;
            labelInfo.Text = "未加载谱面";
        }

        // -----------------------------
        // 选择谱面
        // -----------------------------
        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                var dialog = new OpenFileDialog();
                dialog.Title = "选择一个 Psych Engine 谱面";
                dialog.Filter = "JSON 谱面文件|*.json";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (!File.Exists(dialog.FileName))
                        throw new FileNotFoundException("文件不存在");

                    textBoxPath.Text = dialog.FileName;

                    _notes = PsychChartParser.LoadPlayerNotes(dialog.FileName);

                    labelInfo.Text = $"玩家音符数: {_notes.Count}";
                    _ready = _notes.Count > 0;

                    buttonStart.Enabled = _ready;
                    labelTip.Visible = _ready;
                    labelTip.Text = "按空格开始自动代打";
                }
            }
            catch (Exception ex)
            {
                _ready = false;
                buttonStart.Enabled = false;
                labelTip.Visible = false;
                MessageBox.Show($"加载谱面失败：\n\n{ex}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -----------------------------
        // 点击“开始”
        // -----------------------------
        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (!_ready || _notes == null || _notes.Count == 0)
            {
                MessageBox.Show("请先加载有效谱面。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            buttonStart.Enabled = false;
            buttonBrowse.Enabled = false;
            buttonStop.Enabled = true;

            labelTip.Visible = true;
            labelTip.Text = "切回游戏，按空格开始自动代打";

            _waitingForSpace = true;
        }

        // -----------------------------
        // 点击“停止”（选项 A）
        // -----------------------------
        private void buttonStop_Click(object sender, EventArgs e)
        {
            _waitingForSpace = false;
            _playing = false;

            labelTip.Text = "已停止";

            buttonStart.Enabled = true;
            buttonBrowse.Enabled = true;
            buttonStop.Enabled = false;
        }

        // -----------------------------
        // 全局空格监听（后台也能触发）
        // -----------------------------
        private async void GlobalKeyDown(object sender, KeyEventArgs e)
        {
            if (!_waitingForSpace)
                return;

            if (e.KeyCode == Keys.Space && !_playing)
            {
                _playing = true;
                _waitingForSpace = false;

                labelTip.Text = "正在自动代打...";

                try
                {
                    await NotePlayer.PlayAsync(_notes);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"播放过程中出错：\n\n{ex}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _playing = false;

                    labelTip.Text = "按空格重新开始自动代打";
                    _waitingForSpace = true;
                }
            }
        }


    }
}
