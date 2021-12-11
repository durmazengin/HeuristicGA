using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestMWVCP
{
    public class MainForm : Form
    {
        // 1. Obtain with Gurobi Solution
        // 2. Obtain with your Heauristic Solution
        public MainForm()
        {
            InitializeComponent();
            
            numerNumberOfNodes.Value = 6;
            numerGraphDensity.Value = 1.5m;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if(chkHardCoded.Checked)
            {
                List<GraphData> graphs = new List<GraphData>();
                graphs.Add(GraphData.generate(50, 0.05));
                graphs.Add(GraphData.generate(50, 0.10));
                graphs.Add(GraphData.generate(50, 0.15));
                graphs.Add(GraphData.generate(50, 0.20));
                graphs.Add(GraphData.generate(50, 0.30));

                graphs.Add(GraphData.generate(75, 0.05));
                graphs.Add(GraphData.generate(75, 0.10));
                graphs.Add(GraphData.generate(75, 0.15));
                graphs.Add(GraphData.generate(75, 0.20));
                graphs.Add(GraphData.generate(75, 0.30));

                graphs.Add(GraphData.generate(100, 0.05));
                graphs.Add(GraphData.generate(100, 0.10));
                graphs.Add(GraphData.generate(100, 0.15));
                graphs.Add(GraphData.generate(100, 0.20));

                graphs.Add(GraphData.generate(125, 0.05));
                graphs.Add(GraphData.generate(125, 0.10));

                graphs.Add(GraphData.generate(150, 0.05));
                graphs.Add(GraphData.generate(150, 0.10));

                graphs.Add(GraphData.generate(175, 0.05));
                graphs.Add(GraphData.generate(200, 0.05));
            }
            else
            {
                GraphData.generate((int)numerNumberOfNodes.Value, (double)numerGraphDensity.Value);
            }
            MessageBox.Show("Graph Generated");
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblNumberOfNodes = new System.Windows.Forms.Label();
            this.numerNumberOfNodes = new System.Windows.Forms.NumericUpDown();
            this.numerGraphDensity = new System.Windows.Forms.NumericUpDown();
            this.lblGraphDensity = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.chkHardCoded = new System.Windows.Forms.CheckBox();
            this.btnMIP1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numerNumberOfNodes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numerGraphDensity)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNumberOfNodes
            // 
            this.lblNumberOfNodes.AutoSize = true;
            this.lblNumberOfNodes.Location = new System.Drawing.Point(26, 38);
            this.lblNumberOfNodes.Name = "lblNumberOfNodes";
            this.lblNumberOfNodes.Size = new System.Drawing.Size(119, 17);
            this.lblNumberOfNodes.TabIndex = 0;
            this.lblNumberOfNodes.Text = "Number of Nodes";
            // 
            // numerNumberOfNodes
            // 
            this.numerNumberOfNodes.Location = new System.Drawing.Point(235, 38);
            this.numerNumberOfNodes.Name = "numerNumberOfNodes";
            this.numerNumberOfNodes.Size = new System.Drawing.Size(120, 22);
            this.numerNumberOfNodes.TabIndex = 1;
            // 
            // numerGraphDensity
            // 
            this.numerGraphDensity.DecimalPlaces = 2;
            this.numerGraphDensity.Location = new System.Drawing.Point(235, 69);
            this.numerGraphDensity.Name = "numerGraphDensity";
            this.numerGraphDensity.Size = new System.Drawing.Size(120, 22);
            this.numerGraphDensity.TabIndex = 3;
            // 
            // lblGraphDensity
            // 
            this.lblGraphDensity.AutoSize = true;
            this.lblGraphDensity.Location = new System.Drawing.Point(26, 69);
            this.lblGraphDensity.Name = "lblGraphDensity";
            this.lblGraphDensity.Size = new System.Drawing.Size(115, 17);
            this.lblGraphDensity.TabIndex = 2;
            this.lblGraphDensity.Text = "Density of Graph";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(262, 136);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(93, 23);
            this.btnGenerate.TabIndex = 4;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // chkHardCoded
            // 
            this.chkHardCoded.AutoSize = true;
            this.chkHardCoded.Location = new System.Drawing.Point(46, 138);
            this.chkHardCoded.Name = "chkHardCoded";
            this.chkHardCoded.Size = new System.Drawing.Size(106, 21);
            this.chkHardCoded.TabIndex = 5;
            this.chkHardCoded.Text = "Hard Coded";
            this.chkHardCoded.UseVisualStyleBackColor = true;
            // 
            // btnMIP1
            // 
            this.btnMIP1.Location = new System.Drawing.Point(262, 167);
            this.btnMIP1.Name = "btnMIP1";
            this.btnMIP1.Size = new System.Drawing.Size(93, 23);
            this.btnMIP1.TabIndex = 6;
            this.btnMIP1.Text = "mip1";
            this.btnMIP1.UseVisualStyleBackColor = true;
            this.btnMIP1.Click += new System.EventHandler(this.btnMIP1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 197);
            this.Controls.Add(this.btnMIP1);
            this.Controls.Add(this.chkHardCoded);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.numerGraphDensity);
            this.Controls.Add(this.lblGraphDensity);
            this.Controls.Add(this.numerNumberOfNodes);
            this.Controls.Add(this.lblNumberOfNodes);
            this.Name = "MainForm";
            this.Text = "Minimum Weighted Vertex Cover Problem";
            ((System.ComponentModel.ISupportInitialize)(this.numerNumberOfNodes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numerGraphDensity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNumberOfNodes;
        private System.Windows.Forms.NumericUpDown numerNumberOfNodes;
        private System.Windows.Forms.NumericUpDown numerGraphDensity;
        private System.Windows.Forms.Label lblGraphDensity;
        private CheckBox chkHardCoded;
        private Button btnMIP1;
        private System.Windows.Forms.Button btnGenerate;

        private void btnMIP1_Click(object sender, EventArgs e)
        {
            mip1_cs.run();
        }
    }
}
