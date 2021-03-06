﻿/*
 * Created by SharpDevelop.
 * User: Ads
 * Date: 5/17/2008
 * Time: 8:50 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;

namespace MD5_Calculator
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void TextBox1TextChanged(object sender, EventArgs e)
		{
            if(btnString.Checked)
            {
			    try
			    {
                    btnFile.Checked = false;
                    txtFileName.ReadOnly = true;
                    txtFileName.Text = "";
			    // Create a new instance of the MD5CryptoServiceProvider object.
			    MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
			    SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
			    SHA256Managed sha256 = new SHA256Managed();
			    SHA384Managed sha384 = new SHA384Managed();
			    SHA512Managed sha512 = new SHA512Managed();
    			
			    // Convert the input string to a byte array and compute the hash.
			    byte[] dataMd5 = md5.ComputeHash(Encoding.Default.GetBytes(txtString.Text));
			    byte[] dataSha1 =sha1.ComputeHash(Encoding.Default.GetBytes(txtString.Text));
			    byte[] dataSha256 =sha256.ComputeHash(Encoding.Default.GetBytes(txtString.Text));
			    byte[] dataSha384 =sha384.ComputeHash(Encoding.Default.GetBytes(txtString.Text));
			    byte[] dataSha512 =sha512.ComputeHash(Encoding.Default.GetBytes(txtString.Text));

                txtMD5.Text = FileHasher.GetHash(dataMd5);
                txtSHA1.Text = FileHasher.GetHash(dataSha1);
                txtSHA256.Text = FileHasher.GetHash(dataSha256);
                txtSHA384.Text = FileHasher.GetHash(dataSha384);
                txtSHA512.Text = FileHasher.GetHash(dataSha512);
			    }
    			
			    catch(Exception)
			    {
				    MessageBox.Show("Error");
			    }
            } 
				
;		}

        private void MainForm_Load(object sender, EventArgs e)
        {
            btnFile.Checked = true;
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            if (btnFile.Checked)
            {
                if (ofdOpenFile.ShowDialog() == DialogResult.OK)
                {
                    txtString.ReadOnly = true;
                    txtString.Text = "";

                    string fileName = ofdOpenFile.FileName;
                    txtFileName.Text = fileName;
                    txtMD5.Text = FileHasher.MD5Hash(fileName);
                    txtSHA1.Text = FileHasher.SHA1Hash(fileName);
                    txtSHA256.Text = FileHasher.SHA256Hash(fileName);
                    txtSHA384.Text = FileHasher.SHA384Hash(fileName);
                    txtSHA512.Text = FileHasher.SHA512Hash(fileName);
                }
            }
        }

        private void chkUpper_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUpper.CheckState == CheckState.Checked)
            {
                txtMD5.Text = txtMD5.Text.ToUpper();
                txtSHA1.Text = txtSHA1.Text.ToUpper();
                txtSHA256.Text = txtSHA256.Text.ToUpper();
                txtSHA384.Text = txtSHA384.Text.ToUpper();
                txtSHA512.Text = txtSHA512.Text.ToUpper();
            }
            else if(chkUpper.CheckState == CheckState.Unchecked)
            {
                txtMD5.Text = txtMD5.Text.ToLower();
                txtSHA1.Text = txtSHA1.Text.ToLower();
                txtSHA256.Text = txtSHA256.Text.ToLower();
                txtSHA384.Text = txtSHA384.Text.ToLower();
                txtSHA512.Text = txtSHA512.Text.ToLower();
            }
        }

        private void btnFile_CheckedChanged(object sender, EventArgs e)
        {
            txtString.ReadOnly = true;
            txtFileName.ReadOnly = false;
            btnSelectFile.Enabled = true;
        }

        private void btnString_CheckedChanged(object sender, EventArgs e)
        {
            txtString.ReadOnly = false;
            txtFileName.ReadOnly = true;
            btnSelectFile.Enabled = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtFileName.Text = "";
            txtString.Text = "";
            txtMD5.Text = "";
            txtSHA1.Text = "";
            txtSHA256.Text = "";
            txtSHA384.Text = "";
            txtSHA512.Text = "";
        }

        private void btnCompareTool_Click(object sender, EventArgs e)
        {
            Form HashCompare = new HashCompare();
            HashCompare.Show();
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            // Check if the Dataformat of the data can be accepted
            // (we only accept file drops from Explorer, etc.)

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                
                e.Effect = DragDropEffects.Copy; // Okay
            }
            else
            {
                e.Effect = DragDropEffects.None; // Unknown data, ignore it
            }

        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            // Extract the data from the DataObject-Container into a string list
            string[]FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            btnFile.Checked = true;
            txtString.ReadOnly = true;
            txtString.Text = "";
            txtFileName.ReadOnly = false;
            btnSelectFile.Enabled = true;

            
            // Do something with the data...
            txtFileName.Text = FileList[0];
            ofdOpenFile.FileName = FileList[0]; 
            string fileName = ofdOpenFile.FileName;

            txtMD5.Text = FileHasher.MD5Hash(fileName);
            txtSHA1.Text = FileHasher.SHA1Hash(fileName);
            txtSHA256.Text = FileHasher.SHA256Hash(fileName);
            txtSHA384.Text = FileHasher.SHA384Hash(fileName);
            txtSHA512.Text = FileHasher.SHA512Hash(fileName);

            
        }


		
		void BtnAboutClick(object sender, EventArgs e)
		{
			MessageBox.Show("\t\t\t Hash Calculator 0.1 \n" + 
			                "-----------------------------------------------------------------------------------------------------\n"+
			                "   Hash Calculator can be used to create distinct crypto hash of your files and strings.\n" +
			                "It now supports crypto hash algos such as MD5, SHA1, SHA256, SHA384 and SHA512.\n\n" +
			                "It also includes to crypto comparer; which can be used to check the hash of the files \n" +
			                "you received and to verify that they are not tampered.\n"+
			                "-----------------------------------------------------------------------------------------------------\n"+
			                " For more details or bug report contact to: \n"+
			                "\t Email : waxcodes@yahoo.com or Laharrush@yahoo.com\n"+
			                "\t WebSite : http://waxcodes.vndv.com (Temporary)\n"+
			                "-----------------------------------------------------------------------------------------------------\n"+
			                "   This application comes with absolutely no warranty, guarntee or anything like that.\n"+
			                "The author is not responsible to problems caused to you, to your computer or \n" +
			                " to your business by using this software. Also I'm not responsible for whatever \n"+
			                "the end user do with this product. Thats all up to him ", "Hash Calulator 1.0");
			                
			                
			               
		}

	}
}
