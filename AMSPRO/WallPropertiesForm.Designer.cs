﻿namespace AMSPRO
{
    partial class WallPropertiesForm
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
            this.button10 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.PropertyNameTxt = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(249, 159);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(168, 23);
            this.button10.TabIndex = 170;
            this.button10.Text = "Make as Defaults";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(249, 188);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(168, 23);
            this.button9.TabIndex = 169;
            this.button9.Text = "Save to File";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(249, 38);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(168, 23);
            this.button8.TabIndex = 168;
            this.button8.Text = "Add Copy of Property";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(249, 12);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(168, 23);
            this.button7.TabIndex = 167;
            this.button7.Text = "Add New Property";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 166;
            this.label1.Text = "Find This Property";
            // 
            // PropertyNameTxt
            // 
            this.PropertyNameTxt.Location = new System.Drawing.Point(12, 72);
            this.PropertyNameTxt.Name = "PropertyNameTxt";
            this.PropertyNameTxt.Size = new System.Drawing.Size(166, 20);
            this.PropertyNameTxt.TabIndex = 165;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(345, 274);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(72, 23);
            this.button4.TabIndex = 164;
            this.button4.Text = "Cancel";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(249, 274);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(72, 23);
            this.button5.TabIndex = 163;
            this.button5.Text = "Ok";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(249, 114);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(168, 23);
            this.button3.TabIndex = 162;
            this.button3.Text = "Delete Property";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(249, 67);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(168, 23);
            this.button2.TabIndex = 161;
            this.button2.Text = "Modify/Show Property";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 98);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(166, 199);
            this.listBox1.TabIndex = 160;
            // 
            // WallPropertiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 324);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PropertyNameTxt);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.listBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "WallPropertiesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wall Properties";
            this.Load += new System.EventHandler(this.WallPropertiesForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PropertyNameTxt;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.ListBox listBox1;
    }
}