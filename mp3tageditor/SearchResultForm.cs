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
				//URLから画像を取得
				using(Stream imgstr = await hc.GetStreamAsync(datashareclass.Product_imageuris[i]))
				{
					Image img = Image.FromStream(imgstr);

					//商品画像、商品名、発売日を追加
					imageList1.Images.Add(dsclass.Product_imageuris[i], img);
					listView1.Items.Add(new ListViewItem { ImageKey = dsclass.Product_imageuris[i]}).SubItems.Add(dsclass.Product_names[i]);
					listView1.Items[i].SubItems.Add(dsclass.Product_release_date[i]);
				}
			}

			MessageBox.Show("検索終了。", "");
		}

		private void OK_button_Click(object sender, EventArgs e)
		{
			if(listView1.SelectedItems.Count > 0)
			{
				dsclass.Product_imageuris.Clear();
				dsclass.Product_names.Clear();
				dsclass.Product_release_date.Clear();

				dsclass.Product_imageuris.Add(listView1.SelectedItems[0].ImageKey);
				dsclass.Product_names.Add(listView1.SelectedItems[0].SubItems[1].Text);
				dsclass.Product_release_date.Add(listView1.SelectedItems[0].SubItems[2].Text);
				Form1.DataShareC = dsclass;

				imageList1.Images.Clear();
				listView1.Clear();
				this.Close();
			}
			else
				MessageBox.Show("商品を選択してください！", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		private void Cancel_button_Click(object sender, EventArgs e)
		{
			Form1.DataShareC.Product_imageuris = null;

			imageList1.Images.Clear();
			listView1.Clear();
			this.Close();
		}

		private DataShareClass dsclass;
		public DataShareClass datashareclass
		{
			set { dsclass = value; }
			get { return dsclass; }
		}
	}
}
