using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace MWVCPHeuristicGA
{
    public class MainForm : Form
    {
        // Obtain with your Heauristic Solution appliying Genetic Algorithms
        private System.Windows.Forms.CheckBox chkRunAllInDirectory;
        private Label lblFilesLocation;
        private Label lblInfoFilesLocation;
        private TextBox txtProblemFolder;
        private Button btnLoadProblems;
        private Label lblSelectFile;
        private ComboBox cmbSelectFiles;

        private System.Windows.Forms.Label lblNumberOfGenerations;
        private System.Windows.Forms.NumericUpDown numerNumberOfGenerations;
        private System.Windows.Forms.Label lblPopulationSize;
        private System.Windows.Forms.NumericUpDown numerPopulationSize;

        private Label lblMutationProbability;
        private Label lblCrossoverProbability;
        private NumericUpDown numerCrossoverProbability;

        private Button btnFindSolution;
        private RichTextBox rtbResult;
        private System.Windows.Forms.RadioButton rdMutationProb1PerN;
        private System.Windows.Forms.RadioButton rdMutationProbZeroDot10;
        private System.Windows.Forms.CheckBox chkRunForAllParameters;
        private const String FILENAME_FORMAT = "graph*.txt";

        public MainForm()
        {
            InitializeComponent();

            chkRunForAllParameters.Checked = true;
        }
        

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblNumberOfGenerations = new System.Windows.Forms.Label();
            this.numerNumberOfGenerations = new System.Windows.Forms.NumericUpDown();
            this.numerPopulationSize = new System.Windows.Forms.NumericUpDown();
            this.lblPopulationSize = new System.Windows.Forms.Label();
            this.chkRunAllInDirectory = new System.Windows.Forms.CheckBox();
            this.cmbSelectFiles = new System.Windows.Forms.ComboBox();
            this.btnFindSolution = new System.Windows.Forms.Button();
            this.lblSelectFile = new System.Windows.Forms.Label();
            this.lblInfoFilesLocation = new System.Windows.Forms.Label();
            this.lblFilesLocation = new System.Windows.Forms.Label();
            this.btnLoadProblems = new System.Windows.Forms.Button();
            this.txtProblemFolder = new System.Windows.Forms.TextBox();
            this.rtbResult = new System.Windows.Forms.RichTextBox();
            this.lblMutationProbability = new System.Windows.Forms.Label();
            this.numerCrossoverProbability = new System.Windows.Forms.NumericUpDown();
            this.lblCrossoverProbability = new System.Windows.Forms.Label();
            this.rdMutationProb1PerN = new System.Windows.Forms.RadioButton();
            this.rdMutationProbZeroDot10 = new System.Windows.Forms.RadioButton();
            this.chkRunForAllParameters = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numerNumberOfGenerations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numerPopulationSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numerCrossoverProbability)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNumberOfGenerations
            // 
            this.lblNumberOfGenerations.AutoSize = true;
            this.lblNumberOfGenerations.Location = new System.Drawing.Point(10, 40);
            this.lblNumberOfGenerations.Name = "lblNumberOfGenerations";
            this.lblNumberOfGenerations.Size = new System.Drawing.Size(156, 17);
            this.lblNumberOfGenerations.TabIndex = 0;
            this.lblNumberOfGenerations.Text = "Number of Generations";
            // 
            // numerNumberOfGenerations
            // 
            this.numerNumberOfGenerations.Location = new System.Drawing.Point(220, 40);
            this.numerNumberOfGenerations.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numerNumberOfGenerations.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numerNumberOfGenerations.Name = "numerNumberOfGenerations";
            this.numerNumberOfGenerations.Size = new System.Drawing.Size(120, 22);
            this.numerNumberOfGenerations.TabIndex = 1;
            this.numerNumberOfGenerations.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numerPopulationSize
            // 
            this.numerPopulationSize.Location = new System.Drawing.Point(220, 70);
            this.numerPopulationSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numerPopulationSize.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numerPopulationSize.Name = "numerPopulationSize";
            this.numerPopulationSize.Size = new System.Drawing.Size(120, 22);
            this.numerPopulationSize.TabIndex = 3;
            this.numerPopulationSize.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // lblPopulationSize
            // 
            this.lblPopulationSize.AutoSize = true;
            this.lblPopulationSize.Location = new System.Drawing.Point(10, 70);
            this.lblPopulationSize.Name = "lblPopulationSize";
            this.lblPopulationSize.Size = new System.Drawing.Size(106, 17);
            this.lblPopulationSize.TabIndex = 2;
            this.lblPopulationSize.Text = "Population Size";
            // 
            // chkRunAllInDirectory
            // 
            this.chkRunAllInDirectory.AutoSize = true;
            this.chkRunAllInDirectory.Location = new System.Drawing.Point(395, 10);
            this.chkRunAllInDirectory.Name = "chkRunAllInDirectory";
            this.chkRunAllInDirectory.Size = new System.Drawing.Size(215, 21);
            this.chkRunAllInDirectory.TabIndex = 5;
            this.chkRunAllInDirectory.Text = "Run All Files In This Directory";
            this.chkRunAllInDirectory.UseVisualStyleBackColor = true;
            this.chkRunAllInDirectory.CheckedChanged += new System.EventHandler(this.chkRunAllInDirectory_CheckedChanged);
            // 
            // cmbSelectFiles
            // 
            this.cmbSelectFiles.FormattingEnabled = true;
            this.cmbSelectFiles.Location = new System.Drawing.Point(549, 122);
            this.cmbSelectFiles.Name = "cmbSelectFiles";
            this.cmbSelectFiles.Size = new System.Drawing.Size(175, 24);
            this.cmbSelectFiles.TabIndex = 7;
            // 
            // btnFindSolution
            // 
            this.btnFindSolution.Location = new System.Drawing.Point(604, 166);
            this.btnFindSolution.Name = "btnFindSolution";
            this.btnFindSolution.Size = new System.Drawing.Size(120, 23);
            this.btnFindSolution.TabIndex = 13;
            this.btnFindSolution.Text = "Find Solution";
            this.btnFindSolution.UseVisualStyleBackColor = true;
            this.btnFindSolution.Click += new System.EventHandler(this.btnFindSolution_Click);
            // 
            // lblSelectFile
            // 
            this.lblSelectFile.AutoSize = true;
            this.lblSelectFile.Location = new System.Drawing.Point(392, 125);
            this.lblSelectFile.Name = "lblSelectFile";
            this.lblSelectFile.Size = new System.Drawing.Size(73, 17);
            this.lblSelectFile.TabIndex = 8;
            this.lblSelectFile.Text = "Select File";
            // 
            // lblInfoFilesLocation
            // 
            this.lblInfoFilesLocation.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblInfoFilesLocation.Location = new System.Drawing.Point(395, 70);
            this.lblInfoFilesLocation.Name = "lblInfoFilesLocation";
            this.lblInfoFilesLocation.Size = new System.Drawing.Size(160, 37);
            this.lblInfoFilesLocation.TabIndex = 12;
            this.lblInfoFilesLocation.Text = "Leave blank if problem files in same directory with exe. Then click Load Problems" +
    "";
            // 
            // lblFilesLocation
            // 
            this.lblFilesLocation.AutoSize = true;
            this.lblFilesLocation.Location = new System.Drawing.Point(395, 40);
            this.lblFilesLocation.Name = "lblFilesLocation";
            this.lblFilesLocation.Size = new System.Drawing.Size(91, 17);
            this.lblFilesLocation.TabIndex = 9;
            this.lblFilesLocation.Text = "Select Folder";
            // 
            // btnLoadProblems
            // 
            this.btnLoadProblems.Location = new System.Drawing.Point(604, 70);
            this.btnLoadProblems.Name = "btnLoadProblems";
            this.btnLoadProblems.Size = new System.Drawing.Size(120, 23);
            this.btnLoadProblems.TabIndex = 11;
            this.btnLoadProblems.Text = "Load Problems";
            this.btnLoadProblems.UseVisualStyleBackColor = true;
            this.btnLoadProblems.Click += new System.EventHandler(this.btnLoadProblems_Click);
            // 
            // txtProblemFolder
            // 
            this.txtProblemFolder.Location = new System.Drawing.Point(550, 40);
            this.txtProblemFolder.Name = "txtProblemFolder";
            this.txtProblemFolder.Size = new System.Drawing.Size(175, 22);
            this.txtProblemFolder.TabIndex = 10;
            // 
            // rtbResult
            // 
            this.rtbResult.Font = new System.Drawing.Font("Courier New", 11F);
            this.rtbResult.Location = new System.Drawing.Point(19, 200);
            this.rtbResult.Name = "rtbResult";
            this.rtbResult.Size = new System.Drawing.Size(720, 377);
            this.rtbResult.TabIndex = 14;
            this.rtbResult.Text = "";
            // 
            // lblMutationProbability
            // 
            this.lblMutationProbability.AutoSize = true;
            this.lblMutationProbability.Location = new System.Drawing.Point(10, 130);
            this.lblMutationProbability.Name = "lblMutationProbability";
            this.lblMutationProbability.Size = new System.Drawing.Size(132, 17);
            this.lblMutationProbability.TabIndex = 17;
            this.lblMutationProbability.Text = "Mutation Probability";
            // 
            // numerCrossoverProbability
            // 
            this.numerCrossoverProbability.DecimalPlaces = 2;
            this.numerCrossoverProbability.Location = new System.Drawing.Point(220, 100);
            this.numerCrossoverProbability.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numerCrossoverProbability.Name = "numerCrossoverProbability";
            this.numerCrossoverProbability.Size = new System.Drawing.Size(120, 22);
            this.numerCrossoverProbability.TabIndex = 16;
            this.numerCrossoverProbability.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // lblCrossoverProbability
            // 
            this.lblCrossoverProbability.AutoSize = true;
            this.lblCrossoverProbability.Location = new System.Drawing.Point(10, 100);
            this.lblCrossoverProbability.Name = "lblCrossoverProbability";
            this.lblCrossoverProbability.Size = new System.Drawing.Size(142, 17);
            this.lblCrossoverProbability.TabIndex = 15;
            this.lblCrossoverProbability.Text = "Crossover Probability";
            // 
            // rdMutationProb1PerN
            // 
            this.rdMutationProb1PerN.AutoSize = true;
            this.rdMutationProb1PerN.Checked = true;
            this.rdMutationProb1PerN.Location = new System.Drawing.Point(220, 130);
            this.rdMutationProb1PerN.Name = "rdMutationProb1PerN";
            this.rdMutationProb1PerN.Size = new System.Drawing.Size(49, 21);
            this.rdMutationProb1PerN.TabIndex = 18;
            this.rdMutationProb1PerN.TabStop = true;
            this.rdMutationProb1PerN.Text = "1/n";
            this.rdMutationProb1PerN.UseVisualStyleBackColor = true;
            // 
            // rdMutationProbZeroDot10
            // 
            this.rdMutationProbZeroDot10.AutoSize = true;
            this.rdMutationProbZeroDot10.Location = new System.Drawing.Point(290, 130);
            this.rdMutationProbZeroDot10.Name = "rdMutationProbZeroDot10";
            this.rdMutationProbZeroDot10.Size = new System.Drawing.Size(57, 21);
            this.rdMutationProbZeroDot10.TabIndex = 19;
            this.rdMutationProbZeroDot10.TabStop = true;
            this.rdMutationProbZeroDot10.Text = "0.10";
            this.rdMutationProbZeroDot10.UseVisualStyleBackColor = true;
            // 
            // chkRunForAllParameters
            // 
            this.chkRunForAllParameters.AutoSize = true;
            this.chkRunForAllParameters.Location = new System.Drawing.Point(13, 10);
            this.chkRunForAllParameters.Name = "chkRunForAllParameters";
            this.chkRunForAllParameters.Size = new System.Drawing.Size(177, 21);
            this.chkRunForAllParameters.TabIndex = 20;
            this.chkRunForAllParameters.Text = "Run For All Parameters";
            this.chkRunForAllParameters.UseVisualStyleBackColor = true;
            this.chkRunForAllParameters.CheckedChanged += new System.EventHandler(this.chkRunForAllParameters_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 589);
            this.Controls.Add(this.chkRunForAllParameters);
            this.Controls.Add(this.rdMutationProbZeroDot10);
            this.Controls.Add(this.rdMutationProb1PerN);
            this.Controls.Add(this.lblMutationProbability);
            this.Controls.Add(this.numerCrossoverProbability);
            this.Controls.Add(this.lblCrossoverProbability);
            this.Controls.Add(this.rtbResult);
            this.Controls.Add(this.cmbSelectFiles);
            this.Controls.Add(this.btnFindSolution);
            this.Controls.Add(this.lblSelectFile);
            this.Controls.Add(this.lblInfoFilesLocation);
            this.Controls.Add(this.lblFilesLocation);
            this.Controls.Add(this.btnLoadProblems);
            this.Controls.Add(this.txtProblemFolder);
            this.Controls.Add(this.chkRunAllInDirectory);
            this.Controls.Add(this.numerPopulationSize);
            this.Controls.Add(this.lblPopulationSize);
            this.Controls.Add(this.numerNumberOfGenerations);
            this.Controls.Add(this.lblNumberOfGenerations);
            this.Name = "MainForm";
            this.Text = "GA for Minimum Weight Vertex Cover";
            ((System.ComponentModel.ISupportInitialize)(this.numerNumberOfGenerations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numerPopulationSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numerCrossoverProbability)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        private void chkRunAllInDirectory_CheckedChanged(object sender, EventArgs e)
        {
            bool enable = !chkRunAllInDirectory.Checked;

            txtProblemFolder.Enabled = enable;
            cmbSelectFiles.Enabled = enable;
            btnLoadProblems.Enabled = enable;
        }

        private void btnLoadProblems_Click(object sender, EventArgs e)
        {
            String folder = txtProblemFolder.Text;
            if (folder.Length == 0)
            {
                folder = AppDomain.CurrentDomain.BaseDirectory;
            }
            String[] files = Directory.GetFiles(folder, FILENAME_FORMAT);
            if (files.Length == 0)
            {
                rtbResult.Text = "No file has found in current directory. Add file having name in 'graph*.txt' format";
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

        private String solve(String fileName)
        {
            String strResult = "";
            if (chkRunForAllParameters.Checked)
            {
                strResult += new GAHeuristicSolver(100, 100).solve(fileName, 0.5, 0);
                strResult += new GAHeuristicSolver(100, 200).solve(fileName, 0.5, 0);
                strResult += new GAHeuristicSolver(200, 100).solve(fileName, 0.5, 0);
                strResult += new GAHeuristicSolver(200, 200).solve(fileName, 0.5, 0);
                strResult += new GAHeuristicSolver(100, 100).solve(fileName, 0.5, 0.1);
                strResult += new GAHeuristicSolver(100, 200).solve(fileName, 0.5, 0.1);
                strResult += new GAHeuristicSolver(200, 100).solve(fileName, 0.5, 0.1);
                strResult += new GAHeuristicSolver(200, 200).solve(fileName, 0.5, 0.1);
                
            }
            else
            {
                double mutationProbability = 0;
                if (rdMutationProbZeroDot10.Checked)
                {
                    mutationProbability = 0.10;
                }
                int numberOfGenerations = (int)numerNumberOfGenerations.Value;
                int populationSize = (int)numerPopulationSize.Value;
                
                strResult += new GAHeuristicSolver(numberOfGenerations, populationSize).solve(fileName, 0.5, mutationProbability);
            }
            return strResult;
        }
                
        private void btnFindSolution_Click(object sender, EventArgs e)
        {
            String result = "";
            
            if (chkRunAllInDirectory.Checked)
            {
                String folder = AppDomain.CurrentDomain.BaseDirectory;

                String[] files = Directory.GetFiles(folder, FILENAME_FORMAT);
                if (files.Length == 0)
                {
                    result = "No file has found in current directory. Add file having name in 'graph*.txt' format";
                }
                else
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        String fileName = files[i].Substring(files[i].LastIndexOf("graph"));
                        result += fileName.Replace("_0_", "-0.") + "\n";
                        result += "-----------------------------\n";
                        result += solve(fileName);
                        result += "\n****************************\n";
                    }
                }
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

                result += solve(fileName);
            }
            rtbResult.Text = result;
        }

        private void chkRunForAllParameters_CheckedChanged(object sender, EventArgs e)
        {
            bool enable = !chkRunForAllParameters.Checked;

            numerNumberOfGenerations.Enabled = enable;
            numerPopulationSize.Enabled = enable;
            numerCrossoverProbability.Enabled = enable;
            rdMutationProb1PerN.Enabled = enable;
            rdMutationProbZeroDot10.Enabled = enable;
        }
    }
}
