/*
This code is under the MIT License
Copyright 2019 Jeremiah Haven

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files 
(the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, 
publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, 
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.


Libraries:
*/
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
        // Create a Speech Synthesizer
        public SpeechSynthesizer voice = new SpeechSynthesizer();
        
        // Define a variable so that we know what file is open
        public string fileOpen;
        
        // Execute this first
        public Form1()
        {
            // Initialize the Form
            InitializeComponent();
            
            // Create a file in the Temp folder and put it in the fileOpen variable to kill a bug
            // that would appear later on
            saveFile("This is not in!@#!@the90475984321753298$#%$#!%@TextBox!#!@$!wnhkjtkjkjkjkjeywsau4rt" +
                "kdsjahfk#@&$!*%$&%(*)$#@R&$@BVCGFDBERLIAJSDA gruyeqwuydqf@", "C:\\Windows\\Temp\\TTSFile.txt");
            fileOpen = "C:\\Windows\\Temp\\TTSFile.txt";
            
            // Reset the Status Lable in the tool strip to say "Ready"
            toolStripStatusLabel1.Text = "Ready";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
        }
        
        // Create a void to call on to set up the Open File Dialog box
        public void prepOpenFileDialog()
        {
            // The initial folder will be at the base of the C drive
            openFileDialog1.InitialDirectory = @"C:\";
            
            // Set the title
            openFileDialog1.Title = "Import a Text File";
            
            // Filter for text files by default but show an option for all files
            openFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            
            // Make sure the user dosen't select anything that dosen't exist. If they did,
            // show an alert box.
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
        }
        
        // Same as the void above
        public void prepSaveFileDialog()
        {
            saveFileDialog1.InitialDirectory = @"C:\";
            saveFileDialog1.Title = "Export a Text File";
            saveFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
        }
        
        // Create a void to call on to save a file without a save file dialog
        public void saveFile(string text, string path)
        {
            // Write the text provided in the arguments
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(path))
            {
                sw.Write(text);
            }
            
            // Update the Status Label in the tool strip and set the fileOpen variable
            toolStripStatusLabel1.Text = "Saved to file " + saveFileDialog1.FileName;
            fileOpen = saveFileDialog1.FileName;
        }
        
        // Void that uses the saveFile function above but calls a save file dialog
        public void saveFileWithDialog(string text)
        {
            // Set the necessary variables for the save file dialog box using
            // the void we made above
            prepSaveFileDialog();
            
            // Only save the file if the save file dialog is successful. If not alert the user that
            // it could not be done.
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                saveFile(text, saveFileDialog1.FileName);
            }
            else
            {
                MessageBox.Show("Could not successfully save Text File");
            }
        }
        
        // A function that returns an integer 0-3 reporting if the file has been saved yet
        public int Saved()
        {
            // Is the file in the fileOpen variable?
            if (fileOpen == "" || fileOpen == "C:\\Windows\\Temp\\TTSFile.txt")
            {
                // If they answered yes, return 0. If not, return 3.
                if (MessageBox.Show("Your work isn't saved! Would you like to save it?", "Clear Work?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    return 0;
                }
                else
                {
                    return 3;
                }
            }
            
            // Does the file contain the contents of the textbox?
            if (File.ReadAllText(fileOpen) != textBox1.Text)
            {
                // If they answered yes, return 2. If not, return 3.
                if (MessageBox.Show("Your work isn't saved! Would you like to save it?", "Clear Work?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }
            // If we have gotten this far the file has been saved. Return a 1.
            return 1;
        }
        
        // When the Button is clicked. change the Status Lable a couple of times and speak the
        // contents of the text box.
        private void button1_Click_1(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Speaking...";
            voice.Speak(textBox1.Text);
            toolStripStatusLabel1.Text = "Ready";
        }
        
        // Do the following if we clicked open in the file menu
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set the variables for the open file dialog using the void we
            // made before.
            prepOpenFileDialog();
            
            // Only open the file if the open file dialog was successful.
            // If not, alert the user that it couldn't open.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Set the text box contents to what the file opened contains
                textBox1.Text = File.ReadAllText(openFileDialog1.FileName);
                
                // Set the fileOpen variable
                fileOpen = openFileDialog1.FileName;
                
                // Set the Status Lable in the Tool Strip
                toolStripStatusLabel1.Text = "Opened file " + openFileDialog1.FileName;
            }
            else
            {
                MessageBox.Show("Could not successfully open Text File");
            }
        }
        
        // Save with the File Dialog when Save As is clicked
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileWithDialog(textBox1.Text);
        }
        
        // When Save is clicked, determin whether the file is opened. If it is,
        // save it without the save file dialog. If not, save it with the
        // save file dialog.
        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if(fileOpen == "" || fileOpen == "C:\\Windows\\Temp\\TTSFile.txt")
            {
                saveFileWithDialog(textBox1.Text);
            }
            else
            {
                saveFile(textBox1.Text, fileOpen);
            }
        }
        
        // If we click new, figure out if the file is saved using the function above
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
            
            // Reset the fileOpen variable and Text Box
            fileOpen = "";
            textBox1.Text = "";
        }
        
        // If exit is clicked, check to see if the file is saved then exit.
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
