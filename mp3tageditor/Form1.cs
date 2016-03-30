using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace mp3tageditor
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

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

					//listviewに追加するアイテムを用意する
					//上から ファイル名|パス|タグから読み込んだ曲名|曲の長さ
					string[] items =
						{ Mp3Paths[i].Substring(Mp3Paths[i].LastIndexOf('\\') + 1),
							Mp3Paths[i],
							mp3tags.Title,
							new DateTime(0).Add(mp3prop.Duration).ToString("HH:mm:ss")
						};
					//listviewにアイテムを追加
					listView1.Items.Add(new ListViewItem(items));
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

					MemoryStream ms;
					try
					{
						//アートワークを取得
						TagLib.IPicture Artworks = mp3tags.Pictures[0];
						ms = new MemoryStream(Artworks.Data.Data);
						ms.Seek(0, SeekOrigin.Begin);
						//画像をリサイズ
						Bitmap canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
						Graphics g = Graphics.FromImage(canvas);
						g.DrawImage(Image.FromStream(ms), 0, 0, pictureBox1.Width, pictureBox1.Height);
						g.Dispose();
						
                        pictureBox1.Image = ImageResize(canvas, pictureBox1.Size);

						Image backupimg;
						using(Image imgsrc = Image.FromStream(ms))
							backupimg = new Bitmap(imgsrc);
						using(backupimg)
						{
							//picturebox1をクリックした時の画像表示のために一時的にアートワークを保存
							backupimg.Save(TempImageFilePath, ImageFormat.Png);
						}
					}
					//アートワークが設定されていない場合
					catch(IndexOutOfRangeException)
					{
						pictureBox1.Image = null;
					}
				}
			}
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			if(listView1.SelectedItems.Count > 0)
				ImageShow(null, Image.FromFile(TempImageFilePath));
		}

		/// <summary>
		/// 引数urlから画像をダウンロードし、新しいフォームに表示する。
		/// どちらかの引数は必ず指定しなければならない。どちらも指定した場合、第一引数を優先して表示する。
		/// </summary>
		/// <param name="url">画像が格納されたURLを指定。省略可。</param>
		/// <param name="img">画像が格納されたImageを指定。省略可。</param>
		private void ImageShow(string url = null, Image img = null)
		{
			Image imgdata = null;

			if(url != null)
			{
				HttpWebRequest hwreq = (HttpWebRequest)WebRequest.Create(new Uri(url));
				using(HttpWebResponse hwres = (HttpWebResponse)hwreq.GetResponse())
				{
					//画像をダウンロード
					Stream imgstream = hwres.GetResponseStream();
					imgdata = Image.FromStream(imgstream);
				}
			}
			else
				imgdata = img;

			//フォーム作成
			Form imageshow = new Form();
			Size windowsize = new Size(500, 500) - new Size(16, 38);
			imageshow.StartPosition = FormStartPosition.Manual;
			imageshow.Location = new Point(this.Left + this.Width, this.Top);
			imageshow.BackColor = Color.Black;
			imageshow.Text = "Artwork Viewer";
			imageshow.Size = windowsize + new Size(16, 38);
			imageshow.SizeChanged += Imageshow_SizeChanged;

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

		private void Imageshow_SizeChanged(object sender, EventArgs e)
		{
			Form test = (Form)sender;
            Console.WriteLine(test.Size);
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
			ImageShow("http://vignette3.wikia.nocookie.net/akuma-high-next-generation/images/f/fc/Nepgear_photo.jpg/revision/latest?cb=20130314231032");
		}

		private static string tempimagefilepath = Path.GetTempFileName();
		public static string TempImageFilePath
		{
			get { return tempimagefilepath; }
		}

	}
}
