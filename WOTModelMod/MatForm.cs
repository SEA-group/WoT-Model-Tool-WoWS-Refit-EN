using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WOTModelMod
{
	public class MatForm : Form
	{
		public bool saved;

		public string etstr;

		private IContainer components;

		private TextBox textBox1;

		private Button button1;

		public MatForm(string ttstr)
		{
			InitializeComponent();
			textBox1.Text = ttstr;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			saved = true;
			etstr = textBox1.Text;
			base.DialogResult = DialogResult.OK;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MatForm));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MatForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Name = "MatForm";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
