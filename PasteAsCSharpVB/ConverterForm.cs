using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

// ERROR: Not supported in C#: OptionDeclaration

using System.Windows.Forms;
using System.Text;

namespace PasteAsCSharpVB
{
	public partial class ConverterForm
	{

		private IConvertCode[] converters = {
			new NRefactoryConverter()
		};

		private string mVBCode;
		public string VBCode
		{
			get { return mVBCode; }
			set { mVBCode = value; }
		}

		private bool mReformat;
		public bool Reformat
		{
			get { return mReformat; }
			set { mReformat = value; }
		}

		public ConverterForm()
		{
			InitializeComponent();
			Load += ConverterForm_Load;
		}

		private void ConverterForm_Load(System.Object sender, System.EventArgs e)
		{
			CsSourceTextBox.Text = Clipboard.GetText();

			ConverterListBox.ValueMember = "ConverterName";
			ConverterListBox.DisplayMember = "ConverterName";
			ConverterListBox.DataSource = converters;
		}

		private void Preview_Click(System.Object sender, System.EventArgs e)
		{
			Cursor oldCursor = this.Cursor;
			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

			string convertedCode = GetConvertedCode(CsSourceTextBox.Text);
			VbResultsTextBox.Text = convertedCode;
			this.Cursor = oldCursor;
		}

		private void ConvertButton_Click(System.Object sender, System.EventArgs e)
		{
			Cursor oldCursor = this.Cursor;
			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

			VBCode = GetConvertedCode(CsSourceTextBox.Text);
			this.Cursor = oldCursor;
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
		}

		private void ReformatCheckBox_CheckedChanged(System.Object sender, System.EventArgs e)
		{
			Reformat = ReformatCheckBox.Checked;
		}

		private string GetConvertedCode(string csCode)
		{
			IConvertCode converter = (IConvertCode)ConverterListBox.SelectedItem;
			string convertedCode = converter.Convert(csCode);
			if (!convertedCode.Contains(Environment.NewLine))
			{
				StringBuilder sb = new StringBuilder();
				foreach (char c in convertedCode)
				{
					if (c != (char)10)
					{
						sb.Append(c);
					}
					else
					{
						sb.Append(Environment.NewLine);
					}
				}
				convertedCode = sb.ToString();
			}
			return convertedCode;
		}

	}
}