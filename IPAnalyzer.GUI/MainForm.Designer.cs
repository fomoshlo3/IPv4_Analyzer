namespace IPAnalyzer.GUI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TBxInputIPAdress = new System.Windows.Forms.TextBox();
            this.TBxInputSubnetmask = new System.Windows.Forms.TextBox();
            this.CmdStartIPLogic = new System.Windows.Forms.Button();
            this.CmdClose = new System.Windows.Forms.Button();
            this.LblStructure = new System.Windows.Forms.Label();
            this.LblOutput = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TBxInputIPAdress
            // 
            this.TBxInputIPAdress.Location = new System.Drawing.Point(12, 12);
            this.TBxInputIPAdress.Name = "TBxInputIPAdress";
            this.TBxInputIPAdress.Size = new System.Drawing.Size(189, 23);
            this.TBxInputIPAdress.TabIndex = 0;
            // 
            // TBxInputSubnetmask
            // 
            this.TBxInputSubnetmask.Location = new System.Drawing.Point(12, 47);
            this.TBxInputSubnetmask.Name = "TBxInputSubnetmask";
            this.TBxInputSubnetmask.Size = new System.Drawing.Size(189, 23);
            this.TBxInputSubnetmask.TabIndex = 1;
            // 
            // CmdStartIPLogic
            // 
            this.CmdStartIPLogic.Location = new System.Drawing.Point(12, 76);
            this.CmdStartIPLogic.Name = "CmdStartIPLogic";
            this.CmdStartIPLogic.Size = new System.Drawing.Size(75, 23);
            this.CmdStartIPLogic.TabIndex = 2;
            this.CmdStartIPLogic.Text = "Starten";
            this.CmdStartIPLogic.UseVisualStyleBackColor = true;
            this.CmdStartIPLogic.Click += new System.EventHandler(this.CmdStartIPLogic_Click);
            // 
            // CmdClose
            // 
            this.CmdClose.Location = new System.Drawing.Point(126, 76);
            this.CmdClose.Name = "CmdClose";
            this.CmdClose.Size = new System.Drawing.Size(75, 23);
            this.CmdClose.TabIndex = 3;
            this.CmdClose.Text = "Beenden";
            this.CmdClose.UseVisualStyleBackColor = true;
            this.CmdClose.Click += new System.EventHandler(this.CmdClose_Click);
            // 
            // LblStructure
            // 
            this.LblStructure.AutoSize = true;
            this.LblStructure.Location = new System.Drawing.Point(12, 114);
            this.LblStructure.Name = "LblStructure";
            this.LblStructure.Size = new System.Drawing.Size(34, 15);
            this.LblStructure.TabIndex = 4;
            this.LblStructure.Text = "(leer)";
            // 
            // LblOutput
            // 
            this.LblOutput.AutoSize = true;
            this.LblOutput.Location = new System.Drawing.Point(126, 114);
            this.LblOutput.Name = "LblOutput";
            this.LblOutput.Size = new System.Drawing.Size(34, 15);
            this.LblOutput.TabIndex = 5;
            this.LblOutput.Text = "(leer)";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 424);
            this.Controls.Add(this.LblOutput);
            this.Controls.Add(this.LblStructure);
            this.Controls.Add(this.CmdClose);
            this.Controls.Add(this.CmdStartIPLogic);
            this.Controls.Add(this.TBxInputSubnetmask);
            this.Controls.Add(this.TBxInputIPAdress);
            this.Name = "MainForm";
            this.Text = "IPv4 Analyzer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox TBxInputIPAdress;
        private TextBox TBxInputSubnetmask;
        private Button CmdStartIPLogic;
        private Button CmdClose;
        private Label LblStructure;
        private Label LblOutput;
    }
}