using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.IO;

namespace Text_To_Speech
{
    public partial class Form1 : Form
    {
        public SpeechSynthesizer voice = new SpeechSynthesizer();
        public string fileOpen;
        public Form1()
        {
            InitializeComponent();
            saveFile("This is not in!@#!@the90475984321753298$#%$#!%@TextBox!#!@$!wnhkjtkjkjkjkjeywsau4rt" +
                "kdsjahfk#@&$!*%$&%(*)$#@R&$@BVCGFDBERLIAJSDA gruyeqwuydqf@", "C:\\Windows\\Temp\\TTSFile.txt");
            fileOpen = "C:\\Windows\\Temp\\TTSFile.txt";
            toolStripStatusLabel1.Text = "Ready";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
        }

        public void prepOpenFileDialog()
        {
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Import a Text File";
            openFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
        }

        public void prepSaveFileDialog()
        {
            saveFileDialog1.InitialDirectory = @"C:\";
            saveFileDialog1.Title = "Export a Text File";
            saveFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
        }

        public void saveFile(string text, string path)
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(path))
            {
                sw.Write(text);
            }
            toolStripStatusLabel1.Text = "Saved to file " + saveFileDialog1.FileName;
            fileOpen = saveFileDialog1.FileName;
        }

        public void saveFileWithDialog(string text)
        {
            prepSaveFileDialog();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                saveFile(text, saveFileDialog1.FileName);
            }
            else
            {
                MessageBox.Show("Could not successfully save Text File");
            }
        }

        public int Saved()
        {
            if (fileOpen == "")
            {
                if (MessageBox.Show("Your work isn't saved! Would you like to save it?", "Clear Work?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    return 0;
                }
                else
                {
                    return 3;
                }
            }
            if (File.ReadAllText(fileOpen) != textBox1.Text)
            {
                if (MessageBox.Show("Your work isn't saved! Would you like to save it?", "Clear Work?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }
            return 1;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Speaking...";
            voice.Speak(textBox1.Text);
            toolStripStatusLabel1.Text = "Ready";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prepOpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = File.ReadAllText(openFileDialog1.FileName);
                fileOpen = openFileDialog1.FileName;
                toolStripStatusLabel1.Text = "Opened file " + openFileDialog1.FileName;
            }
            else
            {
                MessageBox.Show("Could not successfully open Text File");
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileWithDialog(textBox1.Text);
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if(fileOpen == "")
            {
                saveFileWithDialog(textBox1.Text);
            }
            else
            {
                saveFile(textBox1.Text, fileOpen);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(Saved() == 0)
            {
                saveFileWithDialog(textBox1.Text);
            }
            else if(Saved() == 2)
            {
                saveFile(textBox1.Text, fileOpen);
            }
            fileOpen = "";
            textBox1.Text = "";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Saved() == 0)
            {
                saveFileWithDialog(textBox1.Text);
            }
            else if (Saved() == 2)
            {
                saveFile(textBox1.Text, fileOpen);
            }
            Application.Exit();
        }
    }
}
