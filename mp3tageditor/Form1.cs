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
					//ファイル名|パス|タグから読み込んだ曲名|曲の長さ
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

						pictureBox1.Image = canvas;//ImageResize(Image.FromStream(ms), 400, 400);

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
			ImageShow(null, Image.FromFile(TempImageFilePath));
		}

		/// <summary>
		/// 引数urlから画像をダウンロードし、新しいフォームに表示する。
		/// どちらかの引数は必ず指定しなければならない。どちらも指定した場合、第一引数を優先して表示する。
		/// </summary>
		/// <param name="url">画像が格納されたURLを指定。省略可。string型</param>
		/// <param name="img">画像が格納されたImageを指定。省略可。Image型</param>
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
			imageshow.StartPosition = FormStartPosition.Manual;
			imageshow.Location = new Point(this.Left + this.Width, this.Top);
			Size windowsize = new Size(400, 400);
			//画像表示のためのpictureboxを作成
			PictureBox pb = new PictureBox();
			Bitmap canvas = new Bitmap(windowsize.Width, windowsize.Height);
			////リサイズするサイズを設定
			//Rectangle destrect = new Rectangle(0, 0, windowsize.Width, windowsize.Height);
			////解像度をオリジナルの画像と同等に設定
			//canvas.SetResolution(imgdata.HorizontalResolution, imgdata.VerticalResolution);
			using(Graphics g = Graphics.FromImage(canvas))
			{
				////高画質化
				//g.CompositingMode = CompositingMode.SourceCopy;
				//g.CompositingQuality = CompositingQuality.HighQuality;
				//g.InterpolationMode = InterpolationMode.Bicubic;
				//g.SmoothingMode = SmoothingMode.HighQuality;
				//g.PixelOffsetMode = PixelOffsetMode.HighQuality;x

				//using(ImageAttributes wrapmode = new ImageAttributes())
				//{
				//	wrapmode.SetWrapMode(WrapMode.TileFlipXY);
				//	g.DrawImage(imgdata, destrect, 0, 0, windowsize.Width, windowsize.Height, GraphicsUnit.Pixel, wrapmode);
				//}
				g.DrawImage(imgdata, 0, 0, windowsize.Width, windowsize.Height);
			}
			
			//先ほどダウンロードした画像をセット or ロードした画像をセット
			pb.Image = canvas;
			pb.SizeMode = PictureBoxSizeMode.AutoSize;
			
			imageshow.Size = pb.Size + new Size(0, 28);
			//pictureboxコントロールをフォームに追加
			imageshow.Controls.Add(pb);
			imageshow.Show(this);
		}

		private Image ImageResize(Image img, int width, int height)
		{
			Console.WriteLine(width + " " + height);
			Bitmap canvas = new Bitmap(width, height);
			Graphics g = Graphics.FromImage(canvas);
			g.DrawImage(img, 0, 0, width, height);
			g.Dispose();

			return canvas;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			ImageShow("https://pbs.twimg.com/profile_images/585714416643088384/oNc3OwuT.png");
		}

		private static string tempimagefilepath = Path.GetTempFileName();
		public static string TempImageFilePath
		{
			get { return tempimagefilepath; }
		}

	}
}
