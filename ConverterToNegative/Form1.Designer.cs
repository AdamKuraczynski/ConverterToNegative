namespace ConverterToNegative
{
  partial class Form1
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
      this.trackBar1 = new System.Windows.Forms.TrackBar();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.label3 = new System.Windows.Forms.Label();
      this.trackBar2 = new System.Windows.Forms.TrackBar();
      this.label4 = new System.Windows.Forms.Label();
      this.radioButton1 = new System.Windows.Forms.RadioButton();
      this.radioButton2 = new System.Windows.Forms.RadioButton();
      this.label5 = new System.Windows.Forms.Label();
      this.button1 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.pictureBox2 = new System.Windows.Forms.PictureBox();
      this.label6 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.button3 = new System.Windows.Forms.Button();
      this.button4 = new System.Windows.Forms.Button();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
      this.SuspendLayout();
      // 
      // trackBar1
      // 
      this.trackBar1.Location = new System.Drawing.Point(282, 408);
      this.trackBar1.Maximum = 100;
      this.trackBar1.Name = "trackBar1";
      this.trackBar1.Size = new System.Drawing.Size(368, 56);
      this.trackBar1.TabIndex = 0;
      this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(383, 389);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(154, 16);
      this.label1.TabIndex = 1;
      this.label1.Text = "Wybór stopnia negatywu";
      this.label1.Click += new System.EventHandler(this.label1_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(451, 448);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(14, 16);
      this.label2.TabIndex = 2;
      this.label2.Text = "0";
      this.label2.Click += new System.EventHandler(this.label2_Click);
      // 
      // pictureBox1
      // 
      this.pictureBox1.Location = new System.Drawing.Point(47, 56);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(375, 239);
      this.pictureBox1.TabIndex = 3;
      this.pictureBox1.TabStop = false;
      this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(792, 389);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(82, 16);
      this.label3.TabIndex = 4;
      this.label3.Text = "Ilość wątków";
      // 
      // trackBar2
      // 
      this.trackBar2.Location = new System.Drawing.Point(736, 407);
      this.trackBar2.Maximum = 64;
      this.trackBar2.Minimum = 1;
      this.trackBar2.Name = "trackBar2";
      this.trackBar2.Size = new System.Drawing.Size(197, 56);
      this.trackBar2.TabIndex = 5;
      this.trackBar2.Value = 1;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(830, 447);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(14, 16);
      this.label4.TabIndex = 6;
      this.label4.Text = "1";
      // 
      // radioButton1
      // 
      this.radioButton1.AutoSize = true;
      this.radioButton1.Location = new System.Drawing.Point(47, 419);
      this.radioButton1.Name = "radioButton1";
      this.radioButton1.Size = new System.Drawing.Size(44, 20);
      this.radioButton1.TabIndex = 7;
      this.radioButton1.TabStop = true;
      this.radioButton1.Text = "C#";
      this.radioButton1.UseVisualStyleBackColor = true;
      // 
      // radioButton2
      // 
      this.radioButton2.AutoSize = true;
      this.radioButton2.Location = new System.Drawing.Point(47, 454);
      this.radioButton2.Name = "radioButton2";
      this.radioButton2.Size = new System.Drawing.Size(57, 20);
      this.radioButton2.TabIndex = 8;
      this.radioButton2.TabStop = true;
      this.radioButton2.Text = "ASM";
      this.radioButton2.UseVisualStyleBackColor = true;
      this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(46, 389);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(104, 16);
      this.label5.TabIndex = 9;
      this.label5.Text = "Wybór biblioteki";
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(794, 498);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(139, 38);
      this.button1.TabIndex = 10;
      this.button1.Text = "Zapisz";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(639, 498);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(139, 38);
      this.button2.TabIndex = 11;
      this.button2.Text = "Anuluj";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // pictureBox2
      // 
      this.pictureBox2.Location = new System.Drawing.Point(558, 56);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new System.Drawing.Size(375, 239);
      this.pictureBox2.TabIndex = 12;
      this.pictureBox2.TabStop = false;
      this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(185, 307);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(60, 16);
      this.label6.TabIndex = 13;
      this.label6.Text = "Oryginał";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(733, 307);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(60, 16);
      this.label7.TabIndex = 14;
      this.label7.Text = "Negatyw";
      this.label7.Click += new System.EventHandler(this.label7_Click);
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(448, 56);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(89, 16);
      this.label8.TabIndex = 15;
      this.label8.Text = "Czas operacji";
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(474, 91);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(31, 16);
      this.label9.TabIndex = 16;
      this.label9.Text = "0:00";
      this.label9.Click += new System.EventHandler(this.label9_Click);
      // 
      // button3
      // 
      this.button3.Location = new System.Drawing.Point(428, 224);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(124, 30);
      this.button3.TabIndex = 17;
      this.button3.Text = "Wybierz";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new System.EventHandler(this.button3_Click);
      // 
      // button4
      // 
      this.button4.Location = new System.Drawing.Point(428, 266);
      this.button4.Name = "button4";
      this.button4.Size = new System.Drawing.Size(124, 30);
      this.button4.TabIndex = 18;
      this.button4.Text = "Wykonaj";
      this.button4.UseVisualStyleBackColor = true;
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.FileName = "openFileDialog1";
      this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(955, 584);
      this.Controls.Add(this.button4);
      this.Controls.Add(this.button3);
      this.Controls.Add(this.label9);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.pictureBox2);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.radioButton2);
      this.Controls.Add(this.radioButton1);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.trackBar2);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.trackBar1);
      this.Name = "Form1";
      this.Text = "Form1";
      ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TrackBar trackBar1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TrackBar trackBar2;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.RadioButton radioButton1;
    private System.Windows.Forms.RadioButton radioButton2;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.PictureBox pictureBox2;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.Button button4;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
  }
}

