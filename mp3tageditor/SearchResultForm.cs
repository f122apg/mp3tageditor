using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Http;
using System.IO;
using System.Security.Permissions;

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
			this.Text = "検索結果 " + dsclass.SearchResultCount + "件";
			HttpClient hc = new HttpClient();

			//画像取得の処理が中断された時の画像
			imageList1.Images.Add("canceled", Properties.Resources.getimagecanceled_image);

			for(int i = 0; i < datashareclass.Product_imageuris.ToArray().Length; i++)
			{
				if(!GetImageCancel)
				{
					//URLから画像を取得
					using(Stream imgstr = await hc.GetStreamAsync(datashareclass.Product_imageuris[i]))
					{
						Image img = Image.FromStream(imgstr);

						//商品画像、商品名、発売日を追加
						imageList1.Images.Add(dsclass.Product_imageuris[i], img);
						listView1.Items.Add(new ListViewItem { ImageKey = dsclass.Product_imageuris[i] }).SubItems.Add(dsclass.Product_names[i]);
						listView1.Items[i].SubItems.Add(dsclass.Product_release_date[i]);
					}
				}
				else
				{
					imageList1.Images.Add(dsclass.Product_imageuris[i], Properties.Resources.getimagecanceled_image);
					listView1.Items.Add(new ListViewItem { ImageKey = "canceled" }).SubItems.Add(dsclass.Product_names[i]);
					listView1.Items[i].SubItems.Add(dsclass.Product_release_date[i]);
				}
			}

			OK_button.Enabled = true;
			Cancel_button.Enabled = true;
			MessageBox.Show("画像取得処理終了。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void OK_button_Click(object sender, EventArgs e)
		{
			if(listView1.SelectedItems.Count > 0)
			{
				dsclass.Product_imageuris.Clear();
				dsclass.Product_names.Clear();
				dsclass.Product_release_date.Clear();

				//ImagekeyがキャンセルだったらListViewのImagekeyから追加するのではなく
				//imagelistから追加するようにする
				if(listView1.SelectedItems[0].ImageKey == "canceled")
					dsclass.Product_imageuris.Add(imageList1.Images.Keys[listView1.SelectedItems[0].Index + 1]);
				else
					dsclass.Product_imageuris.Add(listView1.SelectedItems[0].ImageKey);
				dsclass.Product_names.Add(listView1.SelectedItems[0].SubItems[1].Text);
				dsclass.Product_release_date.Add(listView1.SelectedItems[0].SubItems[2].Text);
				Form1.DataShareC = dsclass;

				imageList1.Images.Clear();
				listView1.Clear();
				this.Close();
			}
			else
				MessageBox.Show("曲を選択してください！", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		private void Cancel_button_Click(object sender, EventArgs e)
		{
			//キャンセルボタンを押したことを示す、nullを入れる
			dsclass.Product_imageuris = null;
			Form1.DataShareC = dsclass;

			imageList1.Images.Clear();
			listView1.Clear();
			this.Close();
		}


		private void SearchStop_button_Click(object sender, EventArgs e)
		{
			GetImageCancel = true;
			SearchStop_button.Enabled = false;
		}

		//閉じるボタン無効
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.Demand,
				Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				const int CS_NOCLOSE = 0x200;
				CreateParams cp = base.CreateParams;
				cp.ClassStyle = cp.ClassStyle | CS_NOCLOSE;

				return cp;
			}
		}

		/// <summary>
		/// 画像取得処理を止めるかどうか デフォルト値はfalse
		/// </summary>
		private bool getimagecancel = false;
		public bool GetImageCancel
		{
			set { getimagecancel = value; }
			get { return getimagecancel; }
		}
		private DataShareClass dsclass;
		public DataShareClass datashareclass
		{
			set { dsclass = value; }
			get { return dsclass; }
		}
	}
}
