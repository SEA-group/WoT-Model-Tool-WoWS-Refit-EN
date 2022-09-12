using DXRender;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WOTModelMod
{
	public class Form1 : Form
	{
		private IContainer components;

		private ListBox listBox1;

		private Button button1;

		private TextBox textBox1;

		private Button button2;

		private Button button3;

		private CheckBox checkBox1;

		private ListBox listBox2;

		private Button button4;

		private Button button5;

		private Button button6;

		private Button button7;

		private CheckBox checkBox2;

		private RenderForm rf;

		private PRIMITIVES curPrims;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(10, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(326, 208);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(506, 197);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Export";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(344, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(509, 21);
            this.textBox1.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(425, 197);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Replace";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.Location = new System.Drawing.Point(776, 68);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Save copy";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(668, 43);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(102, 16);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Reverse faces";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // listBox2
            // 
            this.listBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new System.Drawing.Point(344, 39);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(318, 148);
            this.listBox2.TabIndex = 6;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(344, 197);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "Add";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(587, 197);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 8;
            this.button5.Text = "Delete";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Enabled = false;
            this.button6.Location = new System.Drawing.Point(776, 197);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 9;
            this.button6.Text = "Material";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(776, 39);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 10;
            this.button7.Text = "Save";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(668, 72);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(102, 16);
            this.checkBox2.TabIndex = 11;
            this.checkBox2.Text = "Vertices only";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(864, 235);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "WoT Model Mod by Lotsbiss, WoWS adaptation by SEA group";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		public Form1()
		{
			InitializeComponent();
			rf = new RenderForm();
		}

		private void SendModel(float[] vfs)
		{
			if (rf.IsDisposed)
			{
				rf = new RenderForm();
			}
			rf.UpdateDXMesh(vfs, vfs.Length / 8);
			rf.Show();
		}

		private void ProssFile(string[] fname)
		{
			foreach (string fname2 in fname)
			{
				ProssFile(fname2);
			}
		}

		private void ProssFile(string fname)
		{
			string a = Path.GetExtension(fname).ToLower();
			if (a == ".primitives")
			{
				curPrims = new PRIMITIVES(fname);
				listBox1.Items.Clear();
				listBox1.Items.AddRange(curPrims.GetChunkList());
				Text = fname;
				if (curPrims.MatIdx == -1)
				{
					button6.Enabled = false;
				}
				else
				{
					button6.Enabled = true;
				}
			}
			else if (a == ".obj")
			{
				textBox1.Text = fname;
			}
		}

		private void Form1_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.Link;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

		private void Form1_DragDrop(object sender, DragEventArgs e)
		{
			string[] fname = (string[])e.Data.GetData(DataFormats.FileDrop);
			ProssFile(fname);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (listBox1.SelectedIndex != -1 && listBox2.SelectedIndex != -1)
			{
				curPrims.ExpObj(listBox1.SelectedIndex, listBox2.SelectedIndex);
			}
			else
			{
				MessageBox.Show("Select primitive to export");
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (listBox1.Items.Count == 1 && listBox1.SelectedIndex != 0)
			{
				listBox1.SelectedIndex = 0;
			}
			if (File.Exists(textBox1.Text) && listBox1.SelectedIndex != -1 && listBox2.SelectedIndex != -1)
			{
				if (checkBox2.Checked)
				{
					curPrims.ImpVert(textBox1.Text, listBox1.SelectedIndex, listBox2.SelectedIndex, checkBox1.Checked);
				}
				else
				{
					curPrims.ImpObj(textBox1.Text, listBox1.SelectedIndex, listBox2.SelectedIndex, checkBox1.Checked);
				}
				SendModel(curPrims.GetRenderVERT(listBox1.SelectedIndex, listBox2.SelectedIndex));
			}
			else if (listBox1.Items.Count > 0 && listBox1.SelectedIndex == -1)
			{
				MessageBox.Show("Select primitive to import");
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			if (listBox1.Items.Count > 0)
			{
				curPrims.Save();
			}
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listBox1.Items.Count > 0 && listBox1.SelectedIndex != -1)
			{
				listBox2.Items.Clear();
				listBox2.Items.AddRange(curPrims.GetFChunkList(listBox1.SelectedIndex));
				if (listBox2.Items.Count >= 1)
				{
					listBox2.SelectedIndex = 0;
				}
			}
			else
			{
				listBox2.Items.Clear();
			}
		}

		private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listBox1.Items.Count > 0 && listBox1.SelectedIndex != -1 && listBox2.Items.Count > 0 && listBox2.SelectedIndex != -1)
			{
				SendModel(curPrims.GetRenderVERT(listBox1.SelectedIndex, listBox2.SelectedIndex));
			}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			if (listBox1.Items.Count == 1 && listBox1.SelectedIndex != 0)
			{
				listBox1.SelectedIndex = 0;
			}
			if (File.Exists(textBox1.Text) && listBox1.SelectedIndex != -1 && listBox2.SelectedIndex != -1)
			{
				curPrims.AddObj(textBox1.Text, listBox1.SelectedIndex, checkBox1.Checked);
				listBox1_SelectedIndexChanged(sender, e);
				SendModel(curPrims.GetRenderVERT(listBox1.SelectedIndex, listBox2.Items.Count - 1));
			}
			else if (listBox1.Items.Count > 0 && listBox1.SelectedIndex == -1)
			{
				MessageBox.Show("Select primitive to import");
			}
		}

		private void button5_Click(object sender, EventArgs e)
		{
			if (listBox2.SelectedIndex == -1)
			{
				return;
			}
			if (listBox2.Items.Count == 1)
			{
				MessageBox.Show("Cannot delete");
				return;
			}
			curPrims.RemoveObj(listBox1.SelectedIndex, listBox2.SelectedIndex);
			listBox2.Items.Clear();
			listBox2.Items.AddRange(curPrims.GetFChunkList(listBox1.SelectedIndex));
			if (listBox2.Items.Count >= 1)
			{
				listBox2.SelectedIndex = 0;
			}
		}

		private void button6_Click(object sender, EventArgs e)
		{
			MatForm matForm = new MatForm(curPrims.GetMatStr().Replace("\n", "\r\n"));
			DialogResult dialogResult = matForm.ShowDialog();
			if (dialogResult == DialogResult.OK)
			{
				curPrims.UpdataMatStr(matForm.etstr.Replace("\r\n", "\n"));
			}
			if (!matForm.IsDisposed)
			{
				matForm.Close();
			}
		}

		private void button7_Click(object sender, EventArgs e)
		{
			if (listBox1.Items.Count > 0)
			{
				curPrims.SaveUp();
			}
		}
	}
}
