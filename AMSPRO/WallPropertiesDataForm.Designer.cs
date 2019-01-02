namespace AMSPRO
{
    partial class WallPropertiesDataForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label31 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Thicknesslbl = new System.Windows.Forms.Label();
            this.Thicknesstxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ProTypetxt = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Materialtxt = new System.Windows.Forms.ComboBox();
            this.Nametxt = new System.Windows.Forms.TextBox();
            this.MTypetxt = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(10, 43);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(68, 13);
            this.label31.TabIndex = 161;
            this.label31.Text = "Wall Material";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(175, 241);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(72, 23);
            this.button4.TabIndex = 168;
            this.button4.Text = "Cancel";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(79, 241);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(72, 23);
            this.button5.TabIndex = 167;
            this.button5.Text = "Ok";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Thicknesslbl);
            this.groupBox1.Controls.Add(this.Thicknesstxt);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.ProTypetxt);
            this.groupBox1.Location = new System.Drawing.Point(5, 111);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(325, 107);
            this.groupBox1.TabIndex = 166;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Property Data";
            // 
            // Thicknesslbl
            // 
            this.Thicknesslbl.AutoSize = true;
            this.Thicknesslbl.Location = new System.Drawing.Point(12, 58);
            this.Thicknesslbl.Name = "Thicknesslbl";
            this.Thicknesslbl.Size = new System.Drawing.Size(53, 13);
            this.Thicknesslbl.TabIndex = 146;
            this.Thicknesslbl.Text = "Thickness";
            // 
            // Thicknesstxt
            // 
            this.Thicknesstxt.Location = new System.Drawing.Point(140, 51);
            this.Thicknesstxt.Name = "Thicknesstxt";
            this.Thicknesstxt.Size = new System.Drawing.Size(165, 20);
            this.Thicknesstxt.TabIndex = 145;
            this.Thicknesstxt.Text = "0.2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 144;
            this.label3.Text = "Type";
            // 
            // ProTypetxt
            // 
            this.ProTypetxt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ProTypetxt.FormattingEnabled = true;
            this.ProTypetxt.Items.AddRange(new object[] {
            "Specified",
            "Auto Select List"});
            this.ProTypetxt.Location = new System.Drawing.Point(140, 24);
            this.ProTypetxt.Name = "ProTypetxt";
            this.ProTypetxt.Size = new System.Drawing.Size(165, 21);
            this.ProTypetxt.TabIndex = 143;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 163;
            this.label1.Text = "Property Name";
            // 
            // Materialtxt
            // 
            this.Materialtxt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Materialtxt.FormattingEnabled = true;
            this.Materialtxt.Location = new System.Drawing.Point(144, 40);
            this.Materialtxt.Name = "Materialtxt";
            this.Materialtxt.Size = new System.Drawing.Size(165, 21);
            this.Materialtxt.TabIndex = 160;
            // 
            // Nametxt
            // 
            this.Nametxt.Location = new System.Drawing.Point(144, 12);
            this.Nametxt.Name = "Nametxt";
            this.Nametxt.Size = new System.Drawing.Size(165, 20);
            this.Nametxt.TabIndex = 162;
            this.Nametxt.Text = "Conc";
            // 
            // MTypetxt
            // 
            this.MTypetxt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MTypetxt.FormattingEnabled = true;
            this.MTypetxt.Items.AddRange(new object[] {
            "Shell-Thin",
            "Shell-thick",
            "Membrance",
            "Layered"});
            this.MTypetxt.Location = new System.Drawing.Point(144, 75);
            this.MTypetxt.Name = "MTypetxt";
            this.MTypetxt.Size = new System.Drawing.Size(165, 21);
            this.MTypetxt.TabIndex = 164;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 165;
            this.label2.Text = "Modeling Type";
            // 
            // WallPropertiesDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 276);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Materialtxt);
            this.Controls.Add(this.Nametxt);
            this.Controls.Add(this.MTypetxt);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "WallPropertiesDataForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wall Properties Data";
            this.Load += new System.EventHandler(this.WallPropertiesDataForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label Thicknesslbl;
        private System.Windows.Forms.TextBox Thicknesstxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ProTypetxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Materialtxt;
        private System.Windows.Forms.TextBox Nametxt;
        private System.Windows.Forms.ComboBox MTypetxt;
        private System.Windows.Forms.Label label2;
    }
}