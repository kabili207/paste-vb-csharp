using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace PasteAsCSharpVB
{
	partial class ConverterForm : System.Windows.Forms.Form
	{

		//Form overrides dispose to clean up the component list.
		[System.Diagnostics.DebuggerNonUserCode()]
		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		//Required by the Windows Form Designer

		private System.ComponentModel.IContainer components;
		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.ConverterListBox = new System.Windows.Forms.ListBox();
			this.Label1 = new System.Windows.Forms.Label();
			this.CsSourceTextBox = new System.Windows.Forms.TextBox();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.VbResultsTextBox = new System.Windows.Forms.TextBox();
			this.ConvertButton = new System.Windows.Forms.Button();
			this.Preview = new System.Windows.Forms.Button();
			this.ReformatCheckBox = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			//
			//ConverterListBox
			//
			this.ConverterListBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.ConverterListBox.FormattingEnabled = true;
			this.ConverterListBox.Location = new System.Drawing.Point(12, 196);
			this.ConverterListBox.Name = "ConverterListBox";
			this.ConverterListBox.Size = new System.Drawing.Size(299, 56);
			this.ConverterListBox.TabIndex = 0;
			//
			//Label1
			//
			this.Label1.AutoSize = true;
			this.Label1.Location = new System.Drawing.Point(12, 180);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(56, 13);
			this.Label1.TabIndex = 1;
			this.Label1.Text = "Converter:";
			//
			//CsSourceTextBox
			//
			this.CsSourceTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.CsSourceTextBox.Location = new System.Drawing.Point(12, 29);
			this.CsSourceTextBox.Multiline = true;
			this.CsSourceTextBox.Name = "CsSourceTextBox";
			this.CsSourceTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.CsSourceTextBox.Size = new System.Drawing.Size(537, 148);
			this.CsSourceTextBox.TabIndex = 2;
			//
			//Label2
			//
			this.Label2.AutoSize = true;
			this.Label2.Location = new System.Drawing.Point(12, 13);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(58, 13);
			this.Label2.TabIndex = 3;
			this.Label2.Text = "C# Source";
			//
			//Label3
			//
			this.Label3.AutoSize = true;
			this.Label3.Location = new System.Drawing.Point(12, 255);
			this.Label3.Name = "Label3";
			this.Label3.Size = new System.Drawing.Size(133, 13);
			this.Label3.TabIndex = 4;
			this.Label3.Text = "Visual Basic .NET Preview";
			//
			//VbResultsTextBox
			//
			this.VbResultsTextBox.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.VbResultsTextBox.Location = new System.Drawing.Point(12, 271);
			this.VbResultsTextBox.Multiline = true;
			this.VbResultsTextBox.Name = "VbResultsTextBox";
			this.VbResultsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.VbResultsTextBox.Size = new System.Drawing.Size(537, 154);
			this.VbResultsTextBox.TabIndex = 5;
			//
			//ConvertButton
			//
			this.ConvertButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.ConvertButton.Location = new System.Drawing.Point(317, 225);
			this.ConvertButton.Name = "ConvertButton";
			this.ConvertButton.Size = new System.Drawing.Size(75, 23);
			this.ConvertButton.TabIndex = 6;
			this.ConvertButton.Text = "Convert";
			this.ConvertButton.UseVisualStyleBackColor = true;
			//
			//Preview
			//
			this.Preview.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.Preview.Location = new System.Drawing.Point(317, 196);
			this.Preview.Name = "Preview";
			this.Preview.Size = new System.Drawing.Size(75, 23);
			this.Preview.TabIndex = 7;
			this.Preview.Text = "Preview";
			this.Preview.UseVisualStyleBackColor = true;
			//
			//ReformatCheckBox
			//
			this.ReformatCheckBox.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.ReformatCheckBox.AutoSize = true;
			this.ReformatCheckBox.Checked = true;
			this.ReformatCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ReformatCheckBox.Location = new System.Drawing.Point(398, 229);
			this.ReformatCheckBox.Name = "ReformatCheckBox";
			this.ReformatCheckBox.Size = new System.Drawing.Size(151, 17);
			this.ReformatCheckBox.TabIndex = 8;
			this.ReformatCheckBox.Text = "Format Code After Convert";
			this.ReformatCheckBox.UseVisualStyleBackColor = true;
			//
			//ConverterForm
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(562, 440);
			this.Controls.Add(this.ReformatCheckBox);
			this.Controls.Add(this.Preview);
			this.Controls.Add(this.ConvertButton);
			this.Controls.Add(this.VbResultsTextBox);
			this.Controls.Add(this.Label3);
			this.Controls.Add(this.Label2);
			this.Controls.Add(this.CsSourceTextBox);
			this.Controls.Add(this.Label1);
			this.Controls.Add(this.ConverterListBox);
			this.Name = "ConverterForm";
			this.Text = "Paste as Visual Basic";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		internal System.Windows.Forms.ListBox ConverterListBox;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.TextBox CsSourceTextBox;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.TextBox VbResultsTextBox;
		internal System.Windows.Forms.Button ConvertButton;
		internal System.Windows.Forms.Button Preview;
		internal System.Windows.Forms.CheckBox ReformatCheckBox;
		
	}
}