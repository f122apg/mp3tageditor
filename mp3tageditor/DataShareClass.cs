using System.Collections.Generic;

namespace mp3tageditor
{
	/// <summary>
	/// Form1で検索した結果をSearchResultFormへ渡すために架け橋となる
	/// </summary>
	class DataShareClass
	{
		//商品画像を保持する
		public List<System.Drawing.Image> Product_images;
		//商品名を保持する
		public List<string> Product_names;
		//発売日を保持する
		public List<string> Product_release_date;

		//コンストラクタ
		DataShareClass(List<System.Drawing.Image> images, List<string> names, List<string> release_date)
		{
			Product_images = images;
			Product_names = names;
			Product_release_date = release_date;
        }
	}
}
