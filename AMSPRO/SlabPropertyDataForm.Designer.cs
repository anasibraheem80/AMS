namespace AMSPRO
{
    partial class SlabPropertyDataForm
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
            this.Materialtxt = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Nametxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.MTypetxt = new System.Windows.Forms.ComboBox();
            this.ProTypetxt = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RibDirectionlbl = new System.Windows.Forms.Label();
            this.RibSpacingdlbl = new System.Windows.Forms.Label();
            this.RibSpacingdtxt = new System.Windows.Forms.TextBox();
            this.RibDirectiontxt = new System.Windows.Forms.ComboBox();
            this.RibSpacing2lbl = new System.Windows.Forms.Label();
            this.RibSpacing2txt = new System.Windows.Forms.TextBox();
            this.RibSpacing1lbl = new System.Windows.Forms.Label();
            this.RibSpacing1txt = new System.Windows.Forms.TextBox();
            this.StemWidthatBottomlbl = new System.Windows.Forms.Label();
            this.StemWidthatBottomtxt = new System.Windows.Forms.TextBox();
            this.StemWidthatToplbl = new System.Windows.Forms.Label();
            this.StemWidthatToptxt = new System.Windows.Forms.TextBox();
            this.SlabThicknesslbl = new System.Windows.Forms.Label();
            this.SlabThicknesstxt = new System.Windows.Forms.TextBox();
            this.OverallDepthlbl = new System.Windows.Forms.Label();
            this.OverallDepthtxt = new System.Windows.Forms.TextBox();
            this.Thicknesslbl = new System.Windows.Forms.Label();
            this.Thicknesstxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.OneWaytxt = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(18, 40);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(68, 13);
            this.label31.TabIndex = 138;
            this.label31.Text = "Slab Material";
            // 
            // Materialtxt
            // 
            this.Materialtxt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Materialtxt.FormattingEnabled = true;
            this.Materialtxt.Location = new System.Drawing.Point(152, 37);
            this.Materialtxt.Name = "Materialtxt";
            this.Materialtxt.Size = new System.Drawing.Size(165, 21);
            this.Materialtxt.TabIndex = 137;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 140;
            this.label1.Text = "Property Name";
            // 
            // Nametxt
            // 
            this.Nametxt.Location = new System.Drawing.Point(152, 9);
            this.Nametxt.Name = "Nametxt";
            this.Nametxt.Size = new System.Drawing.Size(165, 20);
            this.Nametxt.TabIndex = 139;
            this.Nametxt.Text = "Conc";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 142;
            this.label2.Text = "Modeling Type";
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
            this.MTypetxt.Location = new System.Drawing.Point(152, 72);
            this.MTypetxt.Name = "MTypetxt";
            this.MTypetxt.Size = new System.Drawing.Size(165, 21);
            this.MTypetxt.TabIndex = 141;
            this.MTypetxt.SelectedIndexChanged += new System.EventHandler(this.MTypetxt_SelectedIndexChanged);
            // 
            // ProTypetxt
            // 
            this.ProTypetxt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ProTypetxt.FormattingEnabled = true;
            this.ProTypetxt.Items.AddRange(new object[] {
            "Slab",
            "Drop",
            "Ribbed",
            "Waffle"});
            this.ProTypetxt.Location = new System.Drawing.Point(140, 24);
            this.ProTypetxt.Name = "ProTypetxt";
            this.ProTypetxt.Size = new System.Drawing.Size(165, 21);
            this.ProTypetxt.TabIndex = 143;
            this.ProTypetxt.SelectedIndexChanged += new System.EventHandler(this.ProTypetxt_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RibDirectionlbl);
            this.groupBox1.Controls.Add(this.RibSpacingdlbl);
            this.groupBox1.Controls.Add(this.RibSpacingdtxt);
            this.groupBox1.Controls.Add(this.RibDirectiontxt);
            this.groupBox1.Controls.Add(this.RibSpacing2lbl);
            this.groupBox1.Controls.Add(this.RibSpacing2txt);
            this.groupBox1.Controls.Add(this.RibSpacing1lbl);
            this.groupBox1.Controls.Add(this.RibSpacing1txt);
            this.groupBox1.Controls.Add(this.StemWidthatBottomlbl);
            this.groupBox1.Controls.Add(this.StemWidthatBottomtxt);
            this.groupBox1.Controls.Add(this.StemWidthatToplbl);
            this.groupBox1.Controls.Add(this.StemWidthatToptxt);
            this.groupBox1.Controls.Add(this.SlabThicknesslbl);
            this.groupBox1.Controls.Add(this.SlabThicknesstxt);
            this.groupBox1.Controls.Add(this.OverallDepthlbl);
            this.groupBox1.Controls.Add(this.OverallDepthtxt);
            this.groupBox1.Controls.Add(this.Thicknesslbl);
            this.groupBox1.Controls.Add(this.Thicknesstxt);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.ProTypetxt);
            this.groupBox1.Location = new System.Drawing.Point(12, 126);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(383, 315);
            this.groupBox1.TabIndex = 144;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Property Data";
            // 
            // RibDirectionlbl
            // 
            this.RibDirectionlbl.AutoSize = true;
            this.RibDirectionlbl.Location = new System.Drawing.Point(11, 211);
            this.RibDirectionlbl.Name = "RibDirectionlbl";
            this.RibDirectionlbl.Size = new System.Drawing.Size(67, 13);
            this.RibDirectionlbl.TabIndex = 163;
            this.RibDirectionlbl.Text = "Rib Direction";
            // 
            // RibSpacingdlbl
            // 
            this.RibSpacingdlbl.AutoSize = true;
            this.RibSpacingdlbl.Location = new System.Drawing.Point(11, 185);
            this.RibSpacingdlbl.Name = "RibSpacingdlbl";
            this.RibSpacingdlbl.Size = new System.Drawing.Size(62, 13);
            this.RibSpacingdlbl.TabIndex = 162;
            this.RibSpacingdlbl.Text = "Rib Spacing";
            // 
            // RibSpacingdtxt
            // 
            this.RibSpacingdtxt.Location = new System.Drawing.Point(140, 182);
            this.RibSpacingdtxt.Name = "RibSpacingdtxt";
            this.RibSpacingdtxt.Size = new System.Drawing.Size(165, 20);
            this.RibSpacingdtxt.TabIndex = 161;
            this.RibSpacingdtxt.Text = "0.2";
            // 
            // RibDirectiontxt
            // 
            this.RibDirectiontxt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RibDirectiontxt.FormattingEnabled = true;
            this.RibDirectiontxt.Items.AddRange(new object[] {
            "Local 1 Axis",
            "Local 2 Axis"});
            this.RibDirectiontxt.Location = new System.Drawing.Point(140, 208);
            this.RibDirectiontxt.Name = "RibDirectiontxt";
            this.RibDirectiontxt.Size = new System.Drawing.Size(165, 21);
            this.RibDirectiontxt.TabIndex = 160;
            // 
            // RibSpacing2lbl
            // 
            this.RibSpacing2lbl.AutoSize = true;
            this.RibSpacing2lbl.Location = new System.Drawing.Point(11, 264);
            this.RibSpacing2lbl.Name = "RibSpacing2lbl";
            this.RibSpacing2lbl.Size = new System.Drawing.Size(71, 13);
            this.RibSpacing2lbl.TabIndex = 158;
            this.RibSpacing2lbl.Text = "Rib Spacing 2";
            // 
            // RibSpacing2txt
            // 
            this.RibSpacing2txt.Location = new System.Drawing.Point(140, 261);
            this.RibSpacing2txt.Name = "RibSpacing2txt";
            this.RibSpacing2txt.Size = new System.Drawing.Size(165, 20);
            this.RibSpacing2txt.TabIndex = 157;
            this.RibSpacing2txt.Text = "0.2";
            // 
            // RibSpacing1lbl
            // 
            this.RibSpacing1lbl.AutoSize = true;
            this.RibSpacing1lbl.Location = new System.Drawing.Point(11, 238);
            this.RibSpacing1lbl.Name = "RibSpacing1lbl";
            this.RibSpacing1lbl.Size = new System.Drawing.Size(71, 13);
            this.RibSpacing1lbl.TabIndex = 156;
            this.RibSpacing1lbl.Text = "Rib Spacing 1";
            // 
            // RibSpacing1txt
            // 
            this.RibSpacing1txt.Location = new System.Drawing.Point(140, 235);
            this.RibSpacing1txt.Name = "RibSpacing1txt";
            this.RibSpacing1txt.Size = new System.Drawing.Size(165, 20);
            this.RibSpacing1txt.TabIndex = 155;
            this.RibSpacing1txt.Text = "0.2";
            // 
            // StemWidthatBottomlbl
            // 
            this.StemWidthatBottomlbl.AutoSize = true;
            this.StemWidthatBottomlbl.Location = new System.Drawing.Point(11, 159);
            this.StemWidthatBottomlbl.Name = "StemWidthatBottomlbl";
            this.StemWidthatBottomlbl.Size = new System.Drawing.Size(112, 13);
            this.StemWidthatBottomlbl.TabIndex = 154;
            this.StemWidthatBottomlbl.Text = "Stem Width at Bottom";
            // 
            // StemWidthatBottomtxt
            // 
            this.StemWidthatBottomtxt.Location = new System.Drawing.Point(140, 156);
            this.StemWidthatBottomtxt.Name = "StemWidthatBottomtxt";
            this.StemWidthatBottomtxt.Size = new System.Drawing.Size(165, 20);
            this.StemWidthatBottomtxt.TabIndex = 153;
            this.StemWidthatBottomtxt.Text = "0.2";
            // 
            // StemWidthatToplbl
            // 
            this.StemWidthatToplbl.AutoSize = true;
            this.StemWidthatToplbl.Location = new System.Drawing.Point(11, 133);
            this.StemWidthatToplbl.Name = "StemWidthatToplbl";
            this.StemWidthatToplbl.Size = new System.Drawing.Size(96, 13);
            this.StemWidthatToplbl.TabIndex = 152;
            this.StemWidthatToplbl.Text = "Stem Width at Top";
            // 
            // StemWidthatToptxt
            // 
            this.StemWidthatToptxt.Location = new System.Drawing.Point(140, 130);
            this.StemWidthatToptxt.Name = "StemWidthatToptxt";
            this.StemWidthatToptxt.Size = new System.Drawing.Size(165, 20);
            this.StemWidthatToptxt.TabIndex = 151;
            this.StemWidthatToptxt.Text = "0.2";
            // 
            // SlabThicknesslbl
            // 
            this.SlabThicknesslbl.AutoSize = true;
            this.SlabThicknesslbl.Location = new System.Drawing.Point(11, 106);
            this.SlabThicknesslbl.Name = "SlabThicknesslbl";
            this.SlabThicknesslbl.Size = new System.Drawing.Size(82, 13);
            this.SlabThicknesslbl.TabIndex = 150;
            this.SlabThicknesslbl.Text = "Slab Thicknessd";
            // 
            // SlabThicknesstxt
            // 
            this.SlabThicknesstxt.Location = new System.Drawing.Point(140, 103);
            this.SlabThicknesstxt.Name = "SlabThicknesstxt";
            this.SlabThicknesstxt.Size = new System.Drawing.Size(165, 20);
            this.SlabThicknesstxt.TabIndex = 149;
            this.SlabThicknesstxt.Text = "0.2";
            // 
            // OverallDepthlbl
            // 
            this.OverallDepthlbl.AutoSize = true;
            this.OverallDepthlbl.Location = new System.Drawing.Point(11, 80);
            this.OverallDepthlbl.Name = "OverallDepthlbl";
            this.OverallDepthlbl.Size = new System.Drawing.Size(79, 13);
            this.OverallDepthlbl.TabIndex = 148;
            this.OverallDepthlbl.Text = "Overall Depthd";
            // 
            // OverallDepthtxt
            // 
            this.OverallDepthtxt.Location = new System.Drawing.Point(140, 77);
            this.OverallDepthtxt.Name = "OverallDepthtxt";
            this.OverallDepthtxt.Size = new System.Drawing.Size(165, 20);
            this.OverallDepthtxt.TabIndex = 147;
            this.OverallDepthtxt.Text = "0.2";
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
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(205, 456);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(72, 23);
            this.button4.TabIndex = 155;
            this.button4.Text = "Cancel";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(109, 456);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(72, 23);
            this.button5.TabIndex = 154;
            this.button5.Text = "Ok";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // OneWaytxt
            // 
            this.OneWaytxt.AutoSize = true;
            this.OneWaytxt.Checked = true;
            this.OneWaytxt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.OneWaytxt.Location = new System.Drawing.Point(30, 103);
            this.OneWaytxt.Name = "OneWaytxt";
            this.OneWaytxt.Size = new System.Drawing.Size(182, 17);
            this.OneWaytxt.TabIndex = 159;
            this.OneWaytxt.Text = "Use Spicial One-Way Distribution";
            this.OneWaytxt.UseVisualStyleBackColor = true;
            this.OneWaytxt.Visible = false;
            // 
            // SlabPropertyDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 483);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.OneWaytxt);
            this.Controls.Add(this.MTypetxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Nametxt);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.Materialtxt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SlabPropertyDataForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SlabPropertyDataForm";
            this.Load += new System.EventHandler(this.SlabPropertyDataForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.ComboBox Materialtxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Nametxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox MTypetxt;
        private System.Windows.Forms.ComboBox ProTypetxt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Thicknesslbl;
        private System.Windows.Forms.TextBox Thicknesstxt;
        private System.Windows.Forms.Label RibSpacing2lbl;
        private System.Windows.Forms.TextBox RibSpacing2txt;
        private System.Windows.Forms.Label RibSpacing1lbl;
        private System.Windows.Forms.TextBox RibSpacing1txt;
        private System.Windows.Forms.Label StemWidthatBottomlbl;
        private System.Windows.Forms.TextBox StemWidthatBottomtxt;
        private System.Windows.Forms.Label StemWidthatToplbl;
        private System.Windows.Forms.TextBox StemWidthatToptxt;
        private System.Windows.Forms.Label SlabThicknesslbl;
        private System.Windows.Forms.TextBox SlabThicknesstxt;
        private System.Windows.Forms.Label OverallDepthlbl;
        private System.Windows.Forms.TextBox OverallDepthtxt;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.CheckBox OneWaytxt;
        private System.Windows.Forms.ComboBox RibDirectiontxt;
        private System.Windows.Forms.Label RibSpacingdlbl;
        private System.Windows.Forms.TextBox RibSpacingdtxt;
        private System.Windows.Forms.Label RibDirectionlbl;
    }
}