﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Net.Http;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

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
			//ガベージコレクションを強制的に起動
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
							pictureBox1.Image = ImageResize(img, pictureBox1.Size);
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

		private async void Search_button_Click(object sender, EventArgs e)
		{
			HttpClient hc = new HttpClient();
			//URLエンコードをする
			string Searchword_urlencoded = System.Web.HttpUtility.UrlEncode(KeyWordSearch_textBox.Text);
			string rethtml;
			HtmlAgilityPack.HtmlDocument haphdoc = new HtmlAgilityPack.HtmlDocument();

			bool Waitflag = false;

			do
			{
				//www.animate-onlineshop.jpにアクセスして、特定の言葉を検索する
				//smt = 検索される文字列、spc = 検索されるカテゴリ 3は音楽、sl= 検索結果の表示件数
				rethtml = await hc.GetStringAsync(@"http://www.animate-onlineshop.jp/products/list.php?smt=" + Searchword_urlencoded + "&spc=3&sl=100");
				if(rethtml.Contains("※ただいま検索サーバが非常に混雑しております。時間を空けてお試し下さい"))
					Waitflag = true;
				else
					Waitflag = false;
				//検索がうまくいかなかった場合、処理を5秒待機させて再び
				if( Waitflag )
					await System.Threading.Tasks.Task.Delay(5000);
			} while(rethtml.Contains("※ただいま検索サーバが非常に混雑しております。時間を空けてお試し下さい"));

			//HTML形式の文字列をHTMLとして読み込み
			haphdoc.LoadHtml(rethtml);

			//ページ内の全商品を取得
			HtmlAgilityPack.HtmlNodeCollection haphncProducts = haphdoc.DocumentNode.SelectNodes("//*[@id='result']/ul");
			//商品画像
			List<HtmlAgilityPack.HtmlNode> haphnImage = new List<HtmlAgilityPack.HtmlNode>();
			//商品名
			List<HtmlAgilityPack.HtmlNode> haphnName = new List<HtmlAgilityPack.HtmlNode>();
			//発売日
			List<HtmlAgilityPack.HtmlNode> haphnRelease_date = new List<HtmlAgilityPack.HtmlNode>();

			//SearchResultFormに表示するためのデータを保持する
			DataShareClass dsc = new DataShareClass();
			//<ul class="product_horizontal_list clearfix"> この中に最大４つ商品情報が入っている
			//<ul class="product_horizontal_list clearfix"> の数だけループ
			//Listの添字として使用
			int index_counter = 0;
			for(int i = 1; i <= haphncProducts.Count; i++)
			{
				//<ul class="product_horizontal_list clearfix"> の中の <li class=""> の数を取得
				HtmlAgilityPack.HtmlNodeCollection haphncProducts_details = haphdoc.DocumentNode.SelectNodes("//*[@id='result']/ul[" + i + "]/li");

				for(int j = 1; j <= haphncProducts_details.Count; j ++)
				{
					//商品画像を取得
					haphnImage.Add(haphdoc.DocumentNode.SelectSingleNode("//*[@id='result']/ul[" + i + "]/li[" + j + "]/div/div[1]/a/img"));
					//商品名を取得
					haphnName.Add(haphdoc.DocumentNode.SelectSingleNode("//*[@id='result']/ul[" + i + "]/li[" + j + "]/a"));
					//発売日を取得
					haphnRelease_date.Add(haphdoc.DocumentNode.SelectSingleNode("//*[@id='result']/ul[" + i + "]/li[" + j + "]/div/p"));

					//URL部分だけ抜き取る
					//商品画像のURLを追加
					dsc.Product_imageuris.Add(haphnImage[index_counter].OuterHtml.Substring(10, haphnImage[index_counter].OuterHtml.IndexOf("alt") - 12));
					//商品名を追加
					dsc.Product_names.Add(haphnName[index_counter].InnerText);
					//発売日を追加
					dsc.Product_release_date.Add(haphnRelease_date[index_counter].InnerText);

					index_counter++;
				}
			}

			SearchResultForm srf = new SearchResultForm();
			srf.datashareclass = dsc;
			srf.ShowDialog(this);

			if(DataShareC.Product_imageuris != null)
			{
				Stream resstream = await hc.GetStreamAsync(DataShareC.Product_imageuris[0]);
				Image.FromStream(resstream).Save("ReplaceArtwork.png", ImageFormat.Png);

				Replace_pictureBox.Image = ImageResize(Image.FromFile("ReplaceArtwork.png"), Replace_pictureBox.Size);
			}
		}

		/// <summary>
		/// 歌詞タイムからアーティスト情報を取得
		/// </summary>
		/// <param name="songname">アーティスト情報を取得する曲名を指定</param>
		/// <returns>アーティスト名を返す</returns>
		private async Task<string> GetArtistFromWeb(string songname)
		{
			songname = System.Web.HttpUtility.UrlEncode(songname);

			Console.WriteLine("songname:" + songname);

			//Yahoo検索から歌詞タイムのURLを取得
			HttpClient hc_yahoo = new HttpClient();

			Console.WriteLine("yahoo検索を実行");
			Console.WriteLine("https://search.yahoo.co.jp/search?p=" + songname + "+site%3Akasi-time.com");

			string rethtml_yahoo = await hc_yahoo.GetStringAsync("https://search.yahoo.co.jp/search?p=" + songname + "+site%3Akasi-time.com");
			HtmlAgilityPack.HtmlDocument haphd_yahoo = new HtmlAgilityPack.HtmlDocument();
			haphd_yahoo.DetectEncodingHtml(rethtml_yahoo);

			Console.WriteLine("Node検索");

			HtmlAgilityPack.HtmlNode haphn_yahoo = haphd_yahoo.DocumentNode.SelectSingleNode("//*[@id='web']/ol/li[1]/em/text()");

			Console.WriteLine("End. URL:" + haphn_yahoo.InnerText);

			HttpClient hc_kasitime = new HttpClient();

			Console.WriteLine("歌詞タイムにアクセス");

			string rethtml_kasitime = await hc_kasitime.GetStringAsync("http://" + haphn_yahoo.InnerText);

			HtmlAgilityPack.HtmlDocument haphd = new HtmlAgilityPack.HtmlDocument();
			haphd.DetectEncodingHtml(rethtml_kasitime);

			//titleタグを取得
			HtmlAgilityPack.HtmlNode haphn = haphd.DocumentNode.SelectSingleNode("//title/text()");
			string artists = System.Web.HttpUtility.HtmlDecode(haphn.InnerText);
			//曲名と - 歌詞タイムを除外
			artists = artists.Substring(artists.IndexOf("　　") + 2, Math.Abs((artists.IndexOf("　　") + 2) - artists.LastIndexOf(" - ")));

			if(artists.Contains("("))
			{
				if(artists.Contains("/"))
				{
					// 「/」で分割
					string[] artistcvs = artists.Split(new char[] { '/' });
					artists = null;

					for(int i = 0; i < artistcvs.Length; i++)
					{
						artistcvs[i] = artistcvs[i].Replace("cv.", "CV.");
						artists += artistcvs[i] + " & ";
					}

					//最後に付いている 「 & 」を取り除く
					artists = artists.Remove(artists.Length - 3, 3);
				}
				else
				{
					//声優の情報が artists にあった場合、声優部分だけ抜き取って artistcv に入れる
					string artistcv = artists.Substring(artists.IndexOf("("), Math.Abs(artists.IndexOf("(") - artists.LastIndexOf(")")) + 1);
					//声優の情報を取り除く
					artists = artists.Replace(artistcv, "");
					artistcv = artistcv.Replace("&", " & ");
					artistcv = artistcv.Replace("cv.", "CV.");
					artists += " " + artistcv;
				}
			}

			artists = artists.Replace("▽", "♡");

			return artists;
		}

		private string GetArtistFromWeb2(string songname)
		{
			HttpClient hc = new HttpClient();
			//string rethtml = hc.GetStringAsync();

			HtmlAgilityPack.HtmlDocument haphd = new HtmlAgilityPack.HtmlDocument();
			haphd.DetectEncodingAndLoad("jlyric1.htm");

			HtmlAgilityPack.HtmlNode haphn = haphd.DocumentNode.SelectSingleNode("//*[@id='lyricList']");
			List<string> inner = new List<string>();

			TextBox tb = new TextBox();
			tb.Text = haphn.InnerHtml;

			for(int i = 0; i < tb.Lines.Length; i ++)
			{
				inner.Add(tb.Lines[i]);
			}

			inner.RemoveAt(0);
			inner.RemoveAt(0);
			inner.RemoveAt(inner.Count - 1);

			int gyou = 1;

			for(int i = 0; i < inner.Count; i ++)
			{
				if(gyou % 13 == 0)
				{
					Console.WriteLine((i - 7) + "行目：" + inner[i - 7]);
					Console.WriteLine((i + 1) + "行目：" + inner[i]);
				}
				gyou++;
			}

			//for(int i = 0; i < inner.Count; i ++)
			//{
			//	if(inner[i].Contains("("))
			//	{	
			//		Console.WriteLine(inner[i]);
			//		break;
			//	}
			//}

			return "";
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
		private static DataShareClass dsclass;
		public static DataShareClass DataShareC
		{
			set { dsclass = value; }
			get { return dsclass; }
		}

		private async void button1_Click(object sender, EventArgs e)
		{
			//HttpClient hc_yahoo = new HttpClient();
			//string songname = "HappyぱLucky";
			//songname = System.Web.HttpUtility.UrlEncode(songname);

			//Console.WriteLine("songname:" + songname);

			//string rethtml_yahoo = await hc_yahoo.GetStringAsync("https://search.yahoo.co.jp/search?p=" + songname + "+site%3Akasi-time.com");
			//HtmlAgilityPack.HtmlDocument haphd_yahoo = new HtmlAgilityPack.HtmlDocument();
			//haphd_yahoo.DetectEncodingHtml(rethtml_yahoo);

			//Console.WriteLine("Node検索");

			//HtmlAgilityPack.HtmlNode haphn_yahoo = haphd_yahoo.DocumentNode.SelectSingleNode("//*[@id='WS2m']/div[1]/div[2]/div/span[1]");
			ReplaceArtist_textBox.Text = GetArtistFromWeb2("HappyぱLucky");
		}
	}
}