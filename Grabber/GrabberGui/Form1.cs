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
            string erkennung0XX5, erkennung0XX3, erkennung1XX5, erkennung1XX3;
            string vektorTag0XX5, vektorTag0XX3, vektorTag1XX5, vektorTag1XX3;
            string documentsFolderPath;
            string desktopFolderPath;
            string seqManipulatorSettingINI = @"SeqManipulatorSetting.ini";
            documentsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string iniPath = documentsFolderPath + @"\" + seqManipulatorSettingINI;
            desktopFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            if (!File.Exists(iniPath))
            {
                //create start INI
                IniFile ini = new IniFile(iniPath);
                //ini.IniWriteValue("Settings", "OutputDirectory", desktopFolderPath);
                //richTextBox3.Text = desktopFolderPath;
                ini.IniWriteValue("pIE0xx Serie", "5'-Erkennung", "GGAGGC");
                ini.IniWriteValue("pIE0xx Serie", "3'-Erkennung", "GGAGTAGTCTTC");
                ini.IniWriteValue("pIE0xx Serie", "5'-VektorTag", "CGGCGGCTCTGGAGGAGGCGGAAGC");
                ini.IniWriteValue("pIE0xx Serie", "3'-VektorTag", "GATCCGGTGGTGGCGGCAGCGGCGG");
                ini.IniWriteValue("pIE1xx Serie", "5'-Erkennung", "GGTCTCATGGG");
                ini.IniWriteValue("pIE1xx Serie", "3'-Erkennung", "GGCGTGAGACC");
                ini.IniWriteValue("pIE1xx Serie", "5'-VektorTag", "GAGTGGGGGTGGTGGTAGCGGTGGG");
                ini.IniWriteValue("pIE1xx Serie", "3'-VektorTag", "GGCGGCTCTGGAGGTGGAGGATCCG");
                erkennung0XX5 = "GGAGGC";
                erkennung0XX3 = "GGAGTAGTCTTC";
                vektorTag0XX5 = "CGGCGGCTCTGGAGGAGGCGGAAGC";
                vektorTag0XX3 = "GATCCGGTGGTGGCGGCAGCGGCGG";
                erkennung1XX5 = "GGTCTCATGGG";
                erkennung1XX3 = "GGCGTGAGACC";
                vektorTag1XX5 = "GAGTGGGGGTGGTGGTAGCGGTGGG";
                vektorTag1XX3 = "GGCGGCTCTGGAGGTGGAGGATCCG";
            }
            else
            {
                //Read in settings
                IniFile ini = new IniFile(iniPath);
                erkennung0XX5 = ini.IniReadValue("pIE0xx Serie", "5'-Erkennung");
                erkennung0XX3 = ini.IniReadValue("pIE0xx Serie", "3'-Erkennung");
                vektorTag0XX5 = ini.IniReadValue("pIE0xx Serie", "5'-VektorTag");
                vektorTag0XX3 = ini.IniReadValue("pIE0xx Serie", "3'-VektorTag");
                erkennung1XX5 = ini.IniReadValue("pIE1xx Serie", "5'-Erkennung");
                erkennung1XX3 = ini.IniReadValue("pIE1xx Serie", "3'-Erkennung");
                vektorTag1XX5 = ini.IniReadValue("pIE1xx Serie", "5'-VektorTag");
                vektorTag1XX3 = ini.IniReadValue("pIE1xx Serie", "3'-VektorTag");
            }
        }
/*
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
*/






        private void button1_Click(object sender, EventArgs e)
        // Plus Button von "From"
        {
            string desktopFolderPath;
            desktopFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            if (richTextBox1.Text=="")
            {MessageBox.Show("Please provide a name.");}
            else
            {
                try
                {
                    Grabber Grabber1 = new Grabber(richTextBox1.Text, desktopFolderPath);
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
            string desktopFolderPath;
            desktopFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            if (richTextBox1.Text == "")
            { MessageBox.Show("Please provide a name."); }
            else
            {
                try
                {
                    Grabber Grabber1 = new Grabber(richTextBox1.Text, desktopFolderPath);
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
            string desktopFolderPath;
            desktopFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            if (richTextBox2.Text == "")
            { MessageBox.Show("Please provide a name."); }
            else
            {
                try
                {
                    Grabber Grabber1 = new Grabber(richTextBox2.Text, desktopFolderPath);
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
            string desktopFolderPath;
            desktopFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            if (richTextBox2.Text == "")
            { MessageBox.Show("Please provide a name."); }
            else
            {
                try
                {
                    Grabber Grabber1 = new Grabber(richTextBox2.Text, desktopFolderPath);
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
        // Manipulate Button
        {
            string desktopFolderPath;
            desktopFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            if (richTextBox1.Text == "")
            { MessageBox.Show("Please provide a name in the 'From'-Box."); }
            else if (richTextBox2.Text == "")
            {//Only From Box filled - Single grab
                try
                {
                    
                    Grabber Grabber1 = new Grabber(richTextBox1.Text, desktopFolderPath);
                    //Grabber1.GrabIt();
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
                        
                        Grabber Grabber1 = new Grabber(richTextBox1.Text, desktopFolderPath);
                        Grabber EndPointGrabber = new Grabber(richTextBox2.Text, desktopFolderPath);
                        EndPointGrabber.AddPräfix();
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
                            else if (NameToCheck == EndPointGrabber.ToGrab.ToUpper())
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
                            Grabber Grabber2 = new Grabber(richTextBox1.Text, desktopFolderPath);
                            //Grabber2.GrabIt();
                            
                            
                            while (Grabber2.ToGrab.ToUpper() != EndPointGrabber.ToGrab.ToUpper())
                            {
                                
                                Grabber2.NextName();
                                //Grabber2.GrabIt();
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
