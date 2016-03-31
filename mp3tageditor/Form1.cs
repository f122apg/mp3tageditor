using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Net.Http;
using System.IO;
using System.Collections.Generic;

namespace mp3tageditor
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}
		private static int counter = 0;

		private void listView1_DragEnter(object sender, DragEventArgs e)
		{
			string[] DragFiles = (string[])e.Data.GetData(DataFormats.FileDrop, false);
			//mp3ファイル以外のファイルがドロップされてないか確認する
			for(int i = 0; i < DragFiles.Length; i++)
			{
				//ドラックされたファイルに.mp3がついていなければ、mp3ファイル以外のファイルがドラックされているとして
				//e.EffectをNoneに設定し、break
				if(DragFiles[i].IndexOf(".mp3", DragFiles[i].Length - 4) != -1)
				{
					if(i == DragFiles.Length - 1)
						e.Effect = DragDropEffects.Copy;
				}
				else
				{
					e.Effect = DragDropEffects.None;
					break;
				}
			}
		}

		private void listView1_DragDrop(object sender, DragEventArgs e)
		{
			string[] Mp3Paths = (string[])e.Data.GetData(DataFormats.FileDrop);

			for(int i = 0; i < Mp3Paths.Length; i ++)
			{
				//Mp3ファイルを開く
				using(TagLib.File mp3 = TagLib.File.Create(Mp3Paths[i]))
				{
					//Mp3ファイルのTagを取得
					TagLib.Tag mp3tags = mp3.Tag;
					//Mp3ファイルのプロパティを取得
					TagLib.Properties mp3prop = mp3.Properties;

					//listviewに追加するアイテムを準備する
					//上から ファイル名|パス|タグから読み込んだ曲名|曲の長さ
					string[] items =
						{ Mp3Paths[i].Substring(Mp3Paths[i].LastIndexOf('\\') + 1),
							Mp3Paths[i],
							mp3tags.Title,
							new DateTime(0).Add(mp3prop.Duration).ToString("HH:mm:ss")
						};
					Console.WriteLine(Mp3Paths[i]);

					//listviewにアイテムを追加
					listView1.Items.Add(new ListViewItem(items));

					//ドラッグアンドドロップされた曲のアートワークを一括で取得
					MemoryStream ms;
					try
					{
						//アートワークの取得
						TagLib.IPicture Artworks = mp3tags.Pictures[0];
						ms = new MemoryStream(Artworks.Data.Data);
						ms.Seek(0, SeekOrigin.Begin);
						Image artworkimg = Image.FromStream(ms);

						Guid guid = Guid.NewGuid();
						//pictureboxに表示するために一時的に画像ファイルとして保存する
						artworkimg.Save("tmp" + guid.ToString() + ".png", ImageFormat.Png);
						TempImageFilePaths.Add(new List<string> { listView1.Items[counter].SubItems[1].Text,
							"tmp" + guid.ToString() + ".png" });
						//ファイルに属性hiddinを追加
						if(TempImageFilePaths[counter][1] != null)
							File.SetAttributes(TempImageFilePaths[counter][1], FileAttributes.Hidden);
					}
					//アートワークが設定されていない場合
					catch(IndexOutOfRangeException)
					{
						TempImageFilePaths.Add(new List<string> { listView1.Items[counter].SubItems[1].Text, null});
                    }
					counter++;
				}
			}
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(listView1.SelectedItems.Count > 0)
			{
				//listviewで選択されているmp3ファイルを開く
				using(TagLib.File mp3 = TagLib.File.Create(listView1.SelectedItems[0].SubItems[1].Text))
				{
					TagLib.Tag mp3tags = mp3.Tag;

					foreach(string artist in mp3tags.Performers)
					{
						//アーティストを取得
						Artist_textbox.Text = artist;
					}
				}

				//mp3ファイルから取得したアートワークをpictureboxに設定する
				for(int i = 0; i < TempImageFilePaths.ToArray().GetLength(0); i++)
				{
					//今選択されている曲が一時的に取得している画像と一致していた場合
					if(listView1.SelectedItems[0].SubItems[1].Text == TempImageFilePaths[i][0])
					{
						//アートワークが取得できたらpictureboxに設定する
						if(TempImageFilePaths[i][1] != null)
						{
							Image img = Image.FromFile(TempImageFilePaths[i][1]);
							pictureBox1.Image = ImageResize(img, Consts.PICTUREBOX_THUMBNAIL_SIZE);
							//BをKB(1024Byte = 1KB)で計算
							Datasize_label.Text = (new FileInfo(TempImageFilePaths[i][1]).Length / 1024f).ToString("#,### KB");
							Imagesize_label.Text = img.Width + "x" + img.Height;
							break;
						}
						//今選択されている曲が一時的に取得している画像と一致しているが
						//曲自体に画像が設定されていない場合、pictureboxには何も設定しない
						else if(TempImageFilePaths[i][1] == null)
						{
							pictureBox1.Image = null;
							Datasize_label.Text = "NA";
							Imagesize_label.Text = "NA";
							break;
						}
					}
				}
			}
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			if(listView1.SelectedItems.Count > 0)
			{
				for(int i = 0; i < TempImageFilePaths.ToArray().GetLength(0); i++)
				{
					//今選択されている曲が一時的に取得している画像と一致していたらその画像をアートワークとしてpictureboxに設定
					if(TempImageFilePaths[listView1.SelectedItems[0].Index][1] != null &&
						TempImageFilePaths[listView1.SelectedItems[0].Index][0] == TempImageFilePaths[i][0])
						ImageShow(null, Image.FromFile(TempImageFilePaths[listView1.SelectedItems[0].Index][1]));
				}
			}
		}

		/// <summary>
		/// 引数urlから画像をダウンロードし、新しいフォームに表示する。
		/// どちらかの引数は必ず指定しなければならない。どちらも指定した場合、第一引数を優先して表示する。
		/// </summary>
		/// <param name="url">画像が格納されたURLを指定。省略可。</param>
		/// <param name="img">画像が格納されたImageを指定。省略可。</param>
		private async void ImageShow(string url = null, Image img = null)
		{
			Image imgdata = null;

			if(url != null)
			{
				HttpClient hc = new HttpClient();
				//Streamダウンロード
				using(Stream imgstream = await hc.GetStreamAsync(url))
					imgdata = Image.FromStream(imgstream);
			}
			else
				imgdata = img;

			//フォーム作成
			Form imageshow = new Form();
			Size UnknownSize = new Size(16, 38);
			Size windowsize = new Size(500, 500) - UnknownSize;
			imageshow.StartPosition = FormStartPosition.Manual;
			imageshow.Location = new Point(this.Left + this.Width, this.Top);
			imageshow.BackColor = Color.Black;
			imageshow.Text = "Artwork Viewer";
			imageshow.Size = windowsize + UnknownSize;

			//画像表示のためのpictureboxを作成
			PictureBox pb = new PictureBox();
			pictureBox1.Size = windowsize;

			//先ほどダウンロードした画像をセット or ロードした画像をセット
			pb.Image = ImageResize(imgdata, windowsize);
			pb.SizeMode = PictureBoxSizeMode.AutoSize;

			//pictureboxコントロールをフォームに追加
			imageshow.Controls.Add(pb);
			imageshow.Show(this);
		}

		/// <summary>
		/// 画像をリサイズする。
		/// </summary>
		/// <param name="img">リサイズしたい画像を指定。</param>
		/// <param name="size">リサイズしたいサイズを指定。</param>
		/// <returns>指定されたサイズで調整された画像を返す。</returns>
		private Image ImageResize(Image img, Size size)
		{
			Bitmap canvas = new Bitmap(size.Width, size.Height);
			Graphics g = Graphics.FromImage(canvas);
			g.DrawImage(img, 0, 0, size.Width, size.Height);
			g.Dispose();

			return canvas;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			//ImageShow("http://ecx.images-amazon.com/images/I/61EkLn43QvL.jpg");
			for(int i = 0; i < TempImageFilePaths.ToArray().GetLength(0); i ++)
			{
				Console.WriteLine("["+i+"][0]" + TempImageFilePaths[i][0]);
				Console.WriteLine("["+i+"][1]" + TempImageFilePaths[i][1] + "\n");
			}
		}

		/// <summary>
		/// 一次元：mp3ファイルのパス。二次元要素のIDとして使用する。 二次元：mp3から読み込んだアートワークをpictureboxに表示するための画像のパス
		/// </summary>
		private static List<List<string>> tempimagefilepaths = new List<List<string>>();
		public static List<List<string>> TempImageFilePaths
		{
			get { return tempimagefilepaths; }
		}
		/// <summary>
		/// インターネットからダウンロードをキャッシュとして使用する画像のパス
		/// </summary>
		private static List<string> cacheimagefilepaths = new List<string>();
		public static List<string> CacheImageFilePaths
		{
			set { cacheimagefilepaths = value; }
			get { return cacheimagefilepaths; }
		}

	}
}

class Consts
{
	public static readonly Size PICTUREBOX_THUMBNAIL_SIZE = new Size(160, 153);
}