namespace AMSPRO
{
    partial class MterialPropertyDataForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Nametxt = new System.Windows.Forms.TextBox();
            this.MaterialTypecmb = new System.Windows.Forms.ComboBox();
            this.DirectionalSummetryTypecmb = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ShearMtxt = new System.Windows.Forms.TextBox();
            this.Thermaltxt = new System.Windows.Forms.TextBox();
            this.Poissontxt = new System.Windows.Forms.TextBox();
            this.Elastisitytxt = new System.Windows.Forms.TextBox();
            this.MperVtxt = new System.Windows.Forms.TextBox();
            this.WperVtxt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.fctxt = new System.Windows.Forms.TextBox();
            this.LweightConchc = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.EffTensileFuetxt = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.EffYeildFyetxt = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.MinTensileFutxt = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.MinYeildFytxt = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Material Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Material Type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(175, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Directional Summetry Type";
            // 
            // Nametxt
            // 
            this.Nametxt.Location = new System.Drawing.Point(254, 11);
            this.Nametxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Nametxt.Name = "Nametxt";
            this.Nametxt.Size = new System.Drawing.Size(151, 24);
            this.Nametxt.TabIndex = 9;
            // 
            // MaterialTypecmb
            // 
            this.MaterialTypecmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MaterialTypecmb.FormattingEnabled = true;
            this.MaterialTypecmb.Items.AddRange(new object[] {
            "Steel",
            "Concrete"});
            this.MaterialTypecmb.Location = new System.Drawing.Point(254, 53);
            this.MaterialTypecmb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaterialTypecmb.Name = "MaterialTypecmb";
            this.MaterialTypecmb.Size = new System.Drawing.Size(151, 24);
            this.MaterialTypecmb.TabIndex = 18;
            this.MaterialTypecmb.SelectedIndexChanged += new System.EventHandler(this.MaterialTypecmb_SelectedIndexChanged);
            // 
            // DirectionalSummetryTypecmb
            // 
            this.DirectionalSummetryTypecmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DirectionalSummetryTypecmb.FormattingEnabled = true;
            this.DirectionalSummetryTypecmb.Items.AddRange(new object[] {
            "Isotropic",
            "Orthotropic"});
            this.DirectionalSummetryTypecmb.Location = new System.Drawing.Point(254, 89);
            this.DirectionalSummetryTypecmb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DirectionalSummetryTypecmb.Name = "DirectionalSummetryTypecmb";
            this.DirectionalSummetryTypecmb.Size = new System.Drawing.Size(151, 24);
            this.DirectionalSummetryTypecmb.TabIndex = 19;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(119, 639);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 28);
            this.button1.TabIndex = 31;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(252, 639);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 28);
            this.button2.TabIndex = 32;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ShearMtxt);
            this.groupBox1.Controls.Add(this.Thermaltxt);
            this.groupBox1.Controls.Add(this.Poissontxt);
            this.groupBox1.Controls.Add(this.Elastisitytxt);
            this.groupBox1.Controls.Add(this.MperVtxt);
            this.groupBox1.Controls.Add(this.WperVtxt);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(14, 116);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(408, 220);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            // 
            // ShearMtxt
            // 
            this.ShearMtxt.Location = new System.Drawing.Point(280, 185);
            this.ShearMtxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ShearMtxt.Name = "ShearMtxt";
            this.ShearMtxt.Size = new System.Drawing.Size(116, 24);
            this.ShearMtxt.TabIndex = 29;
            this.ShearMtxt.Text = "76920000000";
            // 
            // Thermaltxt
            // 
            this.Thermaltxt.Location = new System.Drawing.Point(280, 153);
            this.Thermaltxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Thermaltxt.Name = "Thermaltxt";
            this.Thermaltxt.Size = new System.Drawing.Size(116, 24);
            this.Thermaltxt.TabIndex = 28;
            this.Thermaltxt.Text = "0.0000117";
            // 
            // Poissontxt
            // 
            this.Poissontxt.Location = new System.Drawing.Point(280, 118);
            this.Poissontxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Poissontxt.Name = "Poissontxt";
            this.Poissontxt.Size = new System.Drawing.Size(116, 24);
            this.Poissontxt.TabIndex = 27;
            this.Poissontxt.Text = "0.3";
            // 
            // Elastisitytxt
            // 
            this.Elastisitytxt.Location = new System.Drawing.Point(280, 86);
            this.Elastisitytxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Elastisitytxt.Name = "Elastisitytxt";
            this.Elastisitytxt.Size = new System.Drawing.Size(116, 24);
            this.Elastisitytxt.TabIndex = 26;
            this.Elastisitytxt.Text = "20000000000";
            // 
            // MperVtxt
            // 
            this.MperVtxt.Location = new System.Drawing.Point(280, 54);
            this.MperVtxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MperVtxt.Name = "MperVtxt";
            this.MperVtxt.Size = new System.Drawing.Size(116, 24);
            this.MperVtxt.TabIndex = 25;
            this.MperVtxt.Text = "0.80083";
            // 
            // WperVtxt
            // 
            this.WperVtxt.Location = new System.Drawing.Point(280, 22);
            this.WperVtxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.WperVtxt.Name = "WperVtxt";
            this.WperVtxt.Size = new System.Drawing.Size(116, 24);
            this.WperVtxt.TabIndex = 24;
            this.WperVtxt.Text = "7.849";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 188);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(118, 17);
            this.label9.TabIndex = 23;
            this.label9.Text = "Shear Modulus . G";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 156);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(230, 17);
            this.label8.TabIndex = 22;
            this.label8.Text = "Coeffiicient Of Thermal Expantion . A";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 122);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 17);
            this.label7.TabIndex = 21;
            this.label7.Text = "Poisson\'s Ratio . U";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(149, 17);
            this.label6.TabIndex = 20;
            this.label6.Text = "Modulus of Elastisity . E";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 17);
            this.label5.TabIndex = 19;
            this.label5.Text = "Mass per Unit Volume";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(153, 17);
            this.label4.TabIndex = 18;
            this.label4.Text = "Weight per Unit Volume";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.fctxt);
            this.groupBox2.Controls.Add(this.LweightConchc);
            this.groupBox2.Location = new System.Drawing.Point(14, 350);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(408, 87);
            this.groupBox2.TabIndex = 34;
            this.groupBox2.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(287, 17);
            this.label10.TabIndex = 25;
            this.label10.Text = "Specified Concrete Compressive Strenght . f\'c";
            // 
            // fctxt
            // 
            this.fctxt.Location = new System.Drawing.Point(280, 16);
            this.fctxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fctxt.Name = "fctxt";
            this.fctxt.Size = new System.Drawing.Size(116, 24);
            this.fctxt.TabIndex = 24;
            this.fctxt.Text = "2812.28";
            // 
            // LweightConchc
            // 
            this.LweightConchc.AutoSize = true;
            this.LweightConchc.Location = new System.Drawing.Point(14, 50);
            this.LweightConchc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.LweightConchc.Name = "LweightConchc";
            this.LweightConchc.Size = new System.Drawing.Size(160, 21);
            this.LweightConchc.TabIndex = 23;
            this.LweightConchc.Text = "Lightweight Concrete";
            this.LweightConchc.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.EffTensileFuetxt);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.EffYeildFyetxt);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.MinTensileFutxt);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.MinYeildFytxt);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Location = new System.Drawing.Point(14, 441);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(407, 182);
            this.groupBox3.TabIndex = 35;
            this.groupBox3.TabStop = false;
            // 
            // EffTensileFuetxt
            // 
            this.EffTensileFuetxt.Location = new System.Drawing.Point(281, 127);
            this.EffTensileFuetxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.EffTensileFuetxt.Name = "EffTensileFuetxt";
            this.EffTensileFuetxt.Size = new System.Drawing.Size(116, 24);
            this.EffTensileFuetxt.TabIndex = 38;
            this.EffTensileFuetxt.Text = "50269.48";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(14, 130);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(196, 17);
            this.label14.TabIndex = 37;
            this.label14.Text = "Effective Tensile Strength . Fue";
            // 
            // EffYeildFyetxt
            // 
            this.EffYeildFyetxt.Location = new System.Drawing.Point(281, 95);
            this.EffYeildFyetxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.EffYeildFyetxt.Name = "EffYeildFyetxt";
            this.EffYeildFyetxt.Size = new System.Drawing.Size(116, 24);
            this.EffYeildFyetxt.TabIndex = 36;
            this.EffYeildFyetxt.Text = "38668.83";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 98);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(165, 17);
            this.label13.TabIndex = 35;
            this.label13.Text = "Effective Yeild  Stess . Fye";
            // 
            // MinTensileFutxt
            // 
            this.MinTensileFutxt.Location = new System.Drawing.Point(281, 63);
            this.MinTensileFutxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinTensileFutxt.Name = "MinTensileFutxt";
            this.MinTensileFutxt.Size = new System.Drawing.Size(116, 24);
            this.MinTensileFutxt.TabIndex = 34;
            this.MinTensileFutxt.Text = "45699.53";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 63);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(191, 17);
            this.label12.TabIndex = 33;
            this.label12.Text = "Minimum Tensile Strength . Fu";
            // 
            // MinYeildFytxt
            // 
            this.MinYeildFytxt.Location = new System.Drawing.Point(281, 31);
            this.MinYeildFytxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinYeildFytxt.Name = "MinYeildFytxt";
            this.MinYeildFytxt.Size = new System.Drawing.Size(116, 24);
            this.MinYeildFytxt.TabIndex = 32;
            this.MinYeildFytxt.Text = "35153.48";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 31);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(156, 17);
            this.label11.TabIndex = 31;
            this.label11.Text = "Minimum Yeild Stess . Fy";
            // 
            // MterialPropertyDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 682);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.DirectionalSummetryTypecmb);
            this.Controls.Add(this.MaterialTypecmb);
            this.Controls.Add(this.Nametxt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MterialPropertyDataForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mterial Property Data";
            this.Load += new System.EventHandler(this.MterialPropertyDataForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Nametxt;
        private System.Windows.Forms.ComboBox MaterialTypecmb;
        private System.Windows.Forms.ComboBox DirectionalSummetryTypecmb;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox ShearMtxt;
        private System.Windows.Forms.TextBox Thermaltxt;
        private System.Windows.Forms.TextBox Poissontxt;
        private System.Windows.Forms.TextBox Elastisitytxt;
        private System.Windows.Forms.TextBox MperVtxt;
        private System.Windows.Forms.TextBox WperVtxt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox fctxt;
        private System.Windows.Forms.CheckBox LweightConchc;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox EffTensileFuetxt;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox EffYeildFyetxt;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox MinTensileFutxt;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox MinYeildFytxt;
        private System.Windows.Forms.Label label11;
    }
}