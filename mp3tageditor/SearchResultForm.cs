using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mp3tageditor
{
	public partial class SearchResultForm : Form
	{
		public SearchResultForm()
		{
			InitializeComponent();
			
			listView1.Items.Add(new ListViewItem());
		}


	}
}
