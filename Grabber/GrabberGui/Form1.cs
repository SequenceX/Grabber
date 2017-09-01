using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GrabberGui
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            readInSettings();

        }






        private void readInSettings()
        {
            string documentsFolderPath;
            string desktopFolderPath;
            string GrabberSettingINI = @"GrabberSetting.ini";
            documentsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string iniPath = documentsFolderPath + @"\" + GrabberSettingINI;
            desktopFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            if (!File.Exists(iniPath))
            {
                //create start INI
                IniFile ini = new IniFile(iniPath);
                ini.IniWriteValue("Settings", "OutputDirectory", desktopFolderPath);
                richTextBox3.Text = desktopFolderPath;
            }
            else
            {
                //Read in settings
                IniFile ini = new IniFile(iniPath);
                richTextBox3.Text = ini.IniReadValue("Settings", "OutputDirectory");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        //Save Setting Button
        {
            //Save INI
            string documentsFolderPath;
            string GrabberSettingINI = @"GrabberSetting.ini";
            documentsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string iniPath = documentsFolderPath + @"\" + GrabberSettingINI;
            IniFile ini = new IniFile(iniPath);
            ini.IniWriteValue("Settings", "OutputDirectory", richTextBox3.Text);
        }







        private void button1_Click(object sender, EventArgs e)
        // Plus Button von "From"
        {
            if (richTextBox1.Text=="")
            {MessageBox.Show("Please provide a name.");}
            else
            {
                try
                {
                    Grabber Grabber1 = new Grabber(richTextBox1.Text, richTextBox3.Text);
                    Grabber1.NextName();
                    richTextBox1.Text = Grabber1.ToGrab;
                    Grabber1 = null;
                }
                catch (Exception exc)
                { MessageBox.Show(exc.Message); }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        // Minus Button von "From"
        {
            if (richTextBox1.Text == "")
            { MessageBox.Show("Please provide a name."); }
            else
            {
                try
                {
                    Grabber Grabber1 = new Grabber(richTextBox1.Text, richTextBox3.Text);
                    Grabber1.PreviousName();
                    richTextBox1.Text = Grabber1.ToGrab;
                    Grabber1 = null;
                }
                catch (Exception exc)
                { MessageBox.Show(exc.Message); }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        // Plus Button von "To"
        {
            if (richTextBox2.Text == "")
            { MessageBox.Show("Please provide a name."); }
            else
            {
                try
                {
                    Grabber Grabber1 = new Grabber(richTextBox2.Text, richTextBox3.Text);
                    Grabber1.NextName();
                    richTextBox2.Text = Grabber1.ToGrab;
                    Grabber1 = null;
                }
                catch (Exception exc)
                { MessageBox.Show(exc.Message); }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        // Minus Button von "To"
        {
            if (richTextBox2.Text == "")
            { MessageBox.Show("Please provide a name."); }
            else
            {
                try
                {
                    Grabber Grabber1 = new Grabber(richTextBox2.Text, richTextBox3.Text);
                    Grabber1.PreviousName();
                    richTextBox2.Text = Grabber1.ToGrab;
                    Grabber1 = null;
                }
                catch (Exception exc)
                { MessageBox.Show(exc.Message); }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        // Clear Button
        {
            richTextBox1.Text = "";
            richTextBox2.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        // Get Seq Button
        {
            if (richTextBox1.Text == "")
            { MessageBox.Show("Please provide a name in the 'From'-Box."); }
            else if (richTextBox2.Text == "")
            {//Only From Box filled - Single grab
                try
                {
                    Grabber Grabber1 = new Grabber(richTextBox1.Text, richTextBox3.Text);
                    Grabber1.GrabIt();
                    Grabber1 = null;
                }
                catch (Exception exc)
                { MessageBox.Show(exc.Message); }
            }
            else
            {//From and To Box filled - Multi grab

                try
                {
                    if (richTextBox1.Text == richTextBox2.Text)
                    {
                        MessageBox.Show("'From'-Box is equal to 'TO'-Box");
                    }
                    else
                    {
                        Grabber Grabber1 = new Grabber(richTextBox1.Text, richTextBox3.Text);
                        string NameToCheck = "";
                        int GeneCounter = 1;
                        for (int i = 0; i < 100; i++)
                        {
                            Grabber1.NextName();
                            NameToCheck = Grabber1.ToGrab.ToUpper();
                            GeneCounter++;
                            if (NameToCheck == "AZ99Z")
                            {
                                break;
                            }
                            else if (NameToCheck == "KZ99Z")
                            {
                                break;
                            }
                            else if (NameToCheck == "Z99Z")
                            {
                                break;
                            }
                            else if (NameToCheck == "ZZ99")
                            {
                                break;
                            }
                            else if (NameToCheck == "AZZ99")
                            {
                                break;
                            }
                            else if (NameToCheck == "KZZ99")
                            {
                                break;
                            }
                            else if (NameToCheck == "999Z")
                            {
                                break;
                            }
                            else if (NameToCheck == richTextBox2.Text.ToUpper())
                            {
                                break;
                            }
                        }
                        Grabber1 = null;
                        if (GeneCounter >= 100)
                        {
                            MessageBox.Show("You try to grab more than 100 sequences. Process cancelled.");
                        }
                        else
                        {
                            Grabber Grabber2 = new Grabber(richTextBox1.Text, richTextBox3.Text);
                            Grabber2.GrabIt();
                            while (Grabber2.ToGrab.ToUpper() != richTextBox2.Text.ToUpper())
                            {
                                Grabber2.NextName();
                                Grabber2.GrabIt();
                            }
                            Grabber2 = null;
                        }
                    }
                    GC.Collect();
                }
                catch (Exception exc)
                { MessageBox.Show(exc.Message); }

            }
        }

        
    }
}
