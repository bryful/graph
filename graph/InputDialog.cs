﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace graph
{
	public partial class InputDialog : Form
	{
		public string Title
		{
			get { return base.Text; }
			set { base.Text = value; }
		}
		public new string Text
		{
			get { return textBox1.Text; }
			set { textBox1.Text = value; }
		}
		public InputDialog()
		{
			InitializeComponent();
		}
	}
}
