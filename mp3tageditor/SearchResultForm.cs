using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.IO;

namespace mp3tageditor
{
	public partial class SearchResultForm : Form
	{
		public SearchResultForm()
		{
			InitializeComponent();
		}

		private async void SearchResultForm_Load(object sender, EventArgs e)
		{
			HttpClient hc = new HttpClient();
			for(int i = 0; i < datashareclass.Product_imageuris.ToArray().Length; i++)
			{
				using(Stream imgstr = await hc.GetStreamAsync(datashareclass.Product_imageuris[i]))
				{
					Image img = Image.FromStream(imgstr);

				}
			}
		}


		private DataShareClass dsclass;
		public DataShareClass datashareclass
		{
			set { dsclass = value; }
			get { return dsclass; }
		}
	}
}
