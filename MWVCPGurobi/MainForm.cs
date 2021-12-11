using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MWVCPGurobi
{
    public class MainForm : Form
    {
        private Label lblSelectFile;
        private Label lblFilesLocation;
        private TextBox txtProblemFolder;
        private Button btnLoadProblems;
        private Label lblInfoManual;
        private Button btnFindSolution;
        private RichTextBox rtbResultManual;
        private TabControl tabCtrlOptions;
        private TabPage tabPageForAll;
        private Label lblInfoAutomatic;
        private TabPage tabPageManual;
        private RichTextBox rtbResultAutomatic;
        private Button btnRun;
        private ComboBox cmbSelectFiles;

        private const String FILENAME_FORMAT = "graph*.txt";

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.cmbSelectFiles = new System.Windows.Forms.ComboBox();
            this.lblSelectFile = new System.Windows.Forms.Label();
            this.lblFilesLocation = new System.Windows.Forms.Label();
            this.txtProblemFolder = new System.Windows.Forms.TextBox();
            this.btnLoadProblems = new System.Windows.Forms.Button();
            this.lblInfoManual = new System.Windows.Forms.Label();
            this.btnFindSolution = new System.Windows.Forms.Button();
            this.rtbResultManual = new System.Windows.Forms.RichTextBox();
            this.tabCtrlOptions = new System.Windows.Forms.TabControl();
            this.tabPageForAll = new System.Windows.Forms.TabPage();
            this.rtbResultAutomatic = new System.Windows.Forms.RichTextBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.lblInfoAutomatic = new System.Windows.Forms.Label();
            this.tabPageManual = new System.Windows.Forms.TabPage();
            this.tabCtrlOptions.SuspendLayout();
            this.tabPageForAll.SuspendLayout();
            this.tabPageManual.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbSelectFiles
            // 
            this.cmbSelectFiles.FormattingEnabled = true;
            this.cmbSelectFiles.Location = new System.Drawing.Point(189, 76);
            this.cmbSelectFiles.Name = "cmbSelectFiles";
            this.cmbSelectFiles.Size = new System.Drawing.Size(175, 24);
            this.cmbSelectFiles.TabIndex = 0;
            // 
            // lblSelectFile
            // 
            this.lblSelectFile.AutoSize = true;
            this.lblSelectFile.Location = new System.Drawing.Point(32, 79);
            this.lblSelectFile.Name = "lblSelectFile";
            this.lblSelectFile.Size = new System.Drawing.Size(73, 17);
            this.lblSelectFile.TabIndex = 1;
            this.lblSelectFile.Text = "Select File";
            // 
            // lblFilesLocation
            // 
            this.lblFilesLocation.AutoSize = true;
            this.lblFilesLocation.Location = new System.Drawing.Point(32, 25);
            this.lblFilesLocation.Name = "lblFilesLocation";
            this.lblFilesLocation.Size = new System.Drawing.Size(91, 17);
            this.lblFilesLocation.TabIndex = 2;
            this.lblFilesLocation.Text = "Select Folder";
            // 
            // txtProblemFolder
            // 
            this.txtProblemFolder.Location = new System.Drawing.Point(189, 19);
            this.txtProblemFolder.Name = "txtProblemFolder";
            this.txtProblemFolder.Size = new System.Drawing.Size(175, 22);
            this.txtProblemFolder.TabIndex = 3;
            // 
            // btnLoadProblems
            // 
            this.btnLoadProblems.Location = new System.Drawing.Point(394, 17);
            this.btnLoadProblems.Name = "btnLoadProblems";
            this.btnLoadProblems.Size = new System.Drawing.Size(120, 23);
            this.btnLoadProblems.TabIndex = 4;
            this.btnLoadProblems.Text = "Load Problems";
            this.btnLoadProblems.UseVisualStyleBackColor = true;
            this.btnLoadProblems.Click += new System.EventHandler(this.btnLoadProblems_Click);
            // 
            // lblInfoManual
            // 
            this.lblInfoManual.AutoSize = true;
            this.lblInfoManual.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblInfoManual.Location = new System.Drawing.Point(31, 44);
            this.lblInfoManual.Name = "lblInfoManual";
            this.lblInfoManual.Size = new System.Drawing.Size(517, 17);
            this.lblInfoManual.TabIndex = 5;
            this.lblInfoManual.Text = "Leave blank if problem files in same directory with exe. Then click Load Problems" +
    "";
            // 
            // btnFindSolution
            // 
            this.btnFindSolution.Location = new System.Drawing.Point(394, 79);
            this.btnFindSolution.Name = "btnFindSolution";
            this.btnFindSolution.Size = new System.Drawing.Size(120, 23);
            this.btnFindSolution.TabIndex = 6;
            this.btnFindSolution.Text = "Find Solution";
            this.btnFindSolution.UseVisualStyleBackColor = true;
            this.btnFindSolution.Click += new System.EventHandler(this.btnFindSolution_Click);
            // 
            // rtbResultManual
            // 
            this.rtbResultManual.Font = new System.Drawing.Font("Courier New", 11F);
            this.rtbResultManual.Location = new System.Drawing.Point(35, 120);
            this.rtbResultManual.Name = "rtbResultManual";
            this.rtbResultManual.Size = new System.Drawing.Size(479, 224);
            this.rtbResultManual.TabIndex = 7;
            this.rtbResultManual.Text = "";
            // 
            // tabCtrlOptions
            // 
            this.tabCtrlOptions.Controls.Add(this.tabPageForAll);
            this.tabCtrlOptions.Controls.Add(this.tabPageManual);
            this.tabCtrlOptions.Location = new System.Drawing.Point(12, 5);
            this.tabCtrlOptions.Name = "tabCtrlOptions";
            this.tabCtrlOptions.SelectedIndex = 0;
            this.tabCtrlOptions.Size = new System.Drawing.Size(569, 391);
            this.tabCtrlOptions.TabIndex = 8;
            // 
            // tabPageForAll
            // 
            this.tabPageForAll.Controls.Add(this.rtbResultAutomatic);
            this.tabPageForAll.Controls.Add(this.btnRun);
            this.tabPageForAll.Controls.Add(this.lblInfoAutomatic);
            this.tabPageForAll.Location = new System.Drawing.Point(4, 25);
            this.tabPageForAll.Name = "tabPageForAll";
            this.tabPageForAll.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageForAll.Size = new System.Drawing.Size(561, 362);
            this.tabPageForAll.TabIndex = 0;
            this.tabPageForAll.Text = "Automatic";
            this.tabPageForAll.UseVisualStyleBackColor = true;
            // 
            // rtbResultAutomatic
            // 
            this.rtbResultAutomatic.Font = new System.Drawing.Font("Courier New", 11F);
            this.rtbResultAutomatic.Location = new System.Drawing.Point(41, 96);
            this.rtbResultAutomatic.Name = "rtbResultAutomatic";
            this.rtbResultAutomatic.Size = new System.Drawing.Size(479, 224);
            this.rtbResultAutomatic.TabIndex = 9;
            this.rtbResultAutomatic.Text = "";
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(400, 55);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(120, 23);
            this.btnRun.TabIndex = 8;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // lblInfoAutomatic
            // 
            this.lblInfoAutomatic.AutoSize = true;
            this.lblInfoAutomatic.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblInfoAutomatic.Location = new System.Drawing.Point(20, 35);
            this.lblInfoAutomatic.Name = "lblInfoAutomatic";
            this.lblInfoAutomatic.Size = new System.Drawing.Size(473, 17);
            this.lblInfoAutomatic.TabIndex = 6;
            this.lblInfoAutomatic.Text = "\"Run\" button loads all files in this directory and finds solution usign Gurobi";
            // 
            // tabPageManual
            // 
            this.tabPageManual.Controls.Add(this.rtbResultManual);
            this.tabPageManual.Controls.Add(this.cmbSelectFiles);
            this.tabPageManual.Controls.Add(this.btnFindSolution);
            this.tabPageManual.Controls.Add(this.lblSelectFile);
            this.tabPageManual.Controls.Add(this.lblInfoManual);
            this.tabPageManual.Controls.Add(this.lblFilesLocation);
            this.tabPageManual.Controls.Add(this.btnLoadProblems);
            this.tabPageManual.Controls.Add(this.txtProblemFolder);
            this.tabPageManual.Location = new System.Drawing.Point(4, 25);
            this.tabPageManual.Name = "tabPageManual";
            this.tabPageManual.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageManual.Size = new System.Drawing.Size(561, 362);
            this.tabPageManual.TabIndex = 1;
            this.tabPageManual.Text = "Manual";
            this.tabPageManual.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 400);
            this.Controls.Add(this.tabCtrlOptions);
            this.Name = "MainForm";
            this.Text = "Minimum Weighted Vertex Cover by Gurobi";
            this.tabCtrlOptions.ResumeLayout(false);
            this.tabPageForAll.ResumeLayout(false);
            this.tabPageForAll.PerformLayout();
            this.tabPageManual.ResumeLayout(false);
            this.tabPageManual.PerformLayout();
            this.ResumeLayout(false);

        }

        private void btnLoadProblems_Click(object sender, EventArgs e)
        {
            String folder = txtProblemFolder.Text;
            if(folder.Length == 0)
            {
                folder = AppDomain.CurrentDomain.BaseDirectory;
            }
            String [] files = Directory.GetFiles(folder, FILENAME_FORMAT);
            if (files.Length == 0)
            {
                rtbResultManual.Text = "No file has found in current directory. Add file having name in 'graph*.txt' format";
            }
            else
            {
                for (int i = 0; i < files.Length; i++)
                {
                    String file = files[i].Substring(files[i].LastIndexOf("graph"));
                    cmbSelectFiles.Items.Add(file);
                }
                cmbSelectFiles.SelectedIndex = 0;
            }
        }

        private void btnFindSolution_Click(object sender, EventArgs e)
        {
            if (cmbSelectFiles.Items.Count == 0)
            {
                rtbResultManual.Text = "First load problems";
            }
            else
            {
                String folder = txtProblemFolder.Text;
                if (folder.Length == 0)
                {
                    folder = AppDomain.CurrentDomain.BaseDirectory;
                }
                String fileName = cmbSelectFiles.Text;
                String fileContent = File.ReadAllText(folder + "/" + fileName);

                rtbResultManual.Text = fileContent;
                rtbResultManual.Text += "\n-----------------------------\n";
                rtbResultManual.Text += MIPSolver.solve(fileContent);
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            String folder = AppDomain.CurrentDomain.BaseDirectory;
            
            String[] files = Directory.GetFiles(folder, FILENAME_FORMAT);
            if (files.Length == 0)
            {
                rtbResultAutomatic.Text = "No file has found in current directory. Add file having name in 'graph*.txt' format";
            }
            else
            {
                rtbResultAutomatic.Text = "";
                for (int i = 0; i < files.Length; i++)
                {
                    String file = files[i].Substring(files[i].LastIndexOf("graph"));
                    String fileContent = File.ReadAllText(file);
                    rtbResultAutomatic.Text += file.Replace("_0_", "-0.") + "\n";
                    // rtbResultAutomatic.Text += fileContent;
                    rtbResultAutomatic.Text += "-----------------------------\n";
                    rtbResultAutomatic.Text += MIPSolver.solve(fileContent);
                    rtbResultAutomatic.Text += "\n****************************\n";
                }
            }
        }
    }
}
