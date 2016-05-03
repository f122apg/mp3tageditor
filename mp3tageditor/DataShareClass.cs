using System.Collections.Generic;

namespace mp3tageditor
{
	/// <summary>
	/// Form1で検索した結果をSearchResultFormへ渡すために架け橋となる
	/// </summary>
	public class DataShareClass
	{
		//商品画像を保持する
		public List<string> Product_imageuris;
		//商品名を保持する
		public List<string> Product_names;
		//発売日を保持する
		public List<string> Product_release_date;
		//件数を保持する
		public int SearchResultCount;

		//コンストラクタ
		public DataShareClass()
		{
			Product_imageuris = new List<string>();
			Product_names = new List<string>();
			Product_release_date = new List<string>();
			SearchResultCount = 0;
		}
	}
}
