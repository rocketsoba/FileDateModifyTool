using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileDateModifyTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.listView1.Columns.Add("ファイル", 200, HorizontalAlignment.Left);
            this.listView1.Columns.Add("更新日時", 120, HorizontalAlignment.Left);
            this.listView1.Columns.Add("作成日時", 120, HorizontalAlignment.Left);
            this.listView1.Columns.Add("アクセス日時", 120, HorizontalAlignment.Left);
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            // DataFormats.FileDropはすべてのドロップ可能なファイル、フォルダを指すっぽい
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            this.textBox1.Text = files[0];
            this.textBox2.Text = files[0];
        }

        // 入力ファイルの参照ボタン
        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = FileSystemLogic.SelectFileOrDirectory();
        }

        // 変更ファイルの参照ボタン
        private void button2_Click(object sender, EventArgs e)
        {
            this.textBox2.Text = FileSystemLogic.SelectFileOrDirectory();
        }

        // タイムスタンプの記録
        private void button3_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text == "")
            {
                return;
            }

            try
            {
                string[] info = FileSystemLogic.ReadFileSystemInfo(this.textBox1.Text);
                this.listView1.Items.Add(new ListViewItem(info));
            }
            catch (Exception)
            {
                return;
            }
        }

        // ListViewのクリア
        private void button4_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                this.listView1.Items.Remove(item);
            }
        }

        // 変更日時の変更
        private void button5_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0 || this.textBox2.Text == "")
            {
                return;
            }

            try
            {
                DateTime selectedDateTime = DateTime.Parse(listView1.SelectedItems[0].SubItems[1].Text);
                FileSystemLogic.SetTimestamp(this.textBox2.Text, selectedDateTime, "LastWriteTime");
            }
            catch (Exception)
            {
                return;
            }
        }

        // 作成日時の変更
        private void button6_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0 || this.textBox2.Text == "")
            {
                return;
            }

            try
            {
                DateTime selectedDateTime = DateTime.Parse(listView1.SelectedItems[0].SubItems[1].Text);
                FileSystemLogic.SetTimestamp(this.textBox2.Text, selectedDateTime, "CreationTime");
            }
            catch (Exception)
            {
                return;
            }
        }


        // アクセス日時の変更
        private void button7_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0 || this.textBox2.Text == "")
            {
                return;
            }

            try
            {
                DateTime selectedDateTime = DateTime.Parse(listView1.SelectedItems[0].SubItems[1].Text);
                FileSystemLogic.SetTimestamp(this.textBox2.Text, selectedDateTime, "LastAccessTime");
            }
            catch (Exception)
            {
                return;
            }
        }

        // 変更日時を作成日時に変更
        private void button8_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0 || this.textBox2.Text == "")
            {
                return;
            }

            try
            {
                string[] info = FileSystemLogic.ReadFileSystemInfo(this.textBox2.Text);
                DateTime selectedDateTime = DateTime.Parse(info[2]);
                FileSystemLogic.SetTimestamp(this.textBox2.Text, selectedDateTime, "LastWriteTime");
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
