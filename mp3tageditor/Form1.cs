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

			//初期化処理
			string[] tmpfilepaths = Directory.GetFiles(".\\", "tmp*.png");

			//tmpファイルを全て削除する
			foreach(string filepath in tmpfilepaths)
			{
				File.Delete(filepath);
			}
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			//*********************************
			//************ 終了処理 *************
			//*********************************
			//リソースを解放
			pictureBox1.Dispose();
			//tmpファイルのロックを解除
			pictureBox1 = null;
			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();

			string[] tmpfilepaths = Directory.GetFiles(".\\", "tmp*.png");

			//tmpファイルを全て削除する
			foreach(string filepath in tmpfilepaths)
			{
				File.Delete(filepath);
			}
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
			//ドラッグアンドドロップされたmp3ファイルのパスを入れる
			string[] Mp3Paths = (string[])e.Data.GetData(DataFormats.FileDrop);
			//ドラッグアンドドロップされる前のListViewのItem数
			int ListViewBeforeItemNum = listView1.Items.Count;
			//mp3tagsの添字を参照するためのカウンタ
			int mp3tags_index_counter = 0;
			//mp3ファイルのタグを読み込んだ物を入れる
			List<TagLib.Tag> mp3tags = new List<TagLib.Tag>();


			for(int i = 0; i < Mp3Paths.Length; i++)
			{
				//現在のループをスキップするかのフラグ
				bool ForSkipflag = false;

				//ドラッグアンドドロップされたファイル郡がすでにlistviewに追加されているファイルかどうか
				for(int k = 0; k < listView1.Items.Count; k++)
				{
					//すでにlistviewに追加されていたら、現在のループを中断し、次のループへと移行する
					if(Mp3Paths[i] == listView1.Items[k].SubItems[1].Text)
						ForSkipflag = true;
				}
				
				if(ForSkipflag)
					continue;

				//Mp3ファイルを開く
				using(TagLib.File mp3 = TagLib.File.Create(Mp3Paths[i]))
				{
					//Mp3ファイルのTagを取得
					mp3tags.Add(mp3.Tag);
					//Mp3ファイルのプロパティを取得
					TagLib.Properties mp3prop = mp3.Properties;

					//listviewに追加するアイテムを準備する
					//上から ファイル名|パス|タグから読み込んだ曲名|曲の長さ
					string[] items =
						{ Mp3Paths[i].Substring(Mp3Paths[i].LastIndexOf('\\') + 1),
							Mp3Paths[i],
							mp3tags[mp3tags_index_counter].Title,
							new DateTime(0).Add(mp3prop.Duration).ToString("HH:mm:ss")
						};

					//listviewにアイテムを追加
					listView1.Items.Add(new ListViewItem(items));

					mp3tags_index_counter++;
				}
			}

			for(int j = ListViewBeforeItemNum; j < listView1.Items.Count; j++)
			{
				//ドラッグアンドドロップされた曲のアートワークを一括で取得
				MemoryStream ms;
				try
				{
					//アートワークの取得
					TagLib.IPicture Artworks = mp3tags[j - ListViewBeforeItemNum].Pictures[0];
					using(ms = new MemoryStream(Artworks.Data.Data))
					{
						ms.Seek(0, SeekOrigin.Begin);
						Image artworkimg = Image.FromStream(ms);

						Guid guid = Guid.NewGuid();
						//pictureboxに表示するために一時的に画像ファイルとして保存する
						artworkimg.Save("tmp" + guid.ToString() + ".png", ImageFormat.Png);
						TempImageFilePaths.Add(new List<string> { listView1.Items[j].SubItems[1].Text,
							"tmp" + guid.ToString() + ".png" });
						//ファイルに属性hiddinを追加
						if(TempImageFilePaths[j][1] != null)
							File.SetAttributes(TempImageFilePaths[j][1], FileAttributes.Hidden);

						artworkimg.Dispose();
					}
				}
				//アートワークが設定されていない場合
				catch(IndexOutOfRangeException)
				{
					TempImageFilePaths.Add(new List<string> { listView1.Items[j].SubItems[1].Text, null});
                }
			}

			mp3tags.Clear();
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(listView1.SelectedItems.Count > 0)
			{
				//listviewで選択されているmp3ファイルを開く
				using(TagLib.File mp3 = TagLib.File.Create(listView1.SelectedItems[0].SubItems[1].Text))
				{
					TagLib.Tag mp3tag = mp3.Tag;

					foreach(string artist in mp3tag.Performers)
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
							Datasize_label.Text = "N/A";
							Imagesize_label.Text = "N/A";
							break;
						}
					}
				}
			}
		}

		//リストから曲を削除
		private void Delete_ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if(listView1.SelectedItems.Count > 0)
			{
				if(MessageBox.Show("ファイル名：" + listView1.SelectedItems[0].SubItems[1].Text + " をリストから削除しますか？",
					"曲の削除確認",
					MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					pictureBox1.Image = null;
					Datasize_label.Text = "N/A";
					Imagesize_label.Text = "N/A";

					//削除された曲のtmpファイルの除外をするためにインクリメント
					IgnoreImageFileCounter++;
					TempImageFilePaths.RemoveAt(listView1.SelectedItems[0].Index);
					listView1.Items.RemoveAt(listView1.SelectedItems[0].Index);
				}
			}
			else
				MessageBox.Show("曲を選択してください。", "削除エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
				imgdata = (Image)img.Clone();

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
			Console.WriteLine("files:" + Directory.GetFiles(".\\", "tmp*.png").Length);
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
			get { return cacheimagefilepaths; }
		}
		/// <summary>
		/// 曲をリストから削除した時にtmpファイルを除外するためのカウンター。
		/// </summary>
		private static int ignoreimagefilecounter = 0;
		public static int IgnoreImageFileCounter
		{
			set { ignoreimagefilecounter = value; }
			get { return ignoreimagefilecounter; }
		}
	}
}

class Consts
{
	public static readonly Size PICTUREBOX_THUMBNAIL_SIZE = new Size(160, 153);
}