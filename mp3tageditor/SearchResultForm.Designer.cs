namespace mp3tageditor
{
	partial class SearchResultForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.listView1 = new System.Windows.Forms.ListView();
			this.Product_image_columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Product_name_columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Product_release_date_columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Select_button = new System.Windows.Forms.Button();
			this.Cancel_button = new System.Windows.Forms.Button();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Product_image_columnHeader,
            this.Product_name_columnHeader,
            this.Product_release_date_columnHeader});
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(13, 13);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(661, 265);
			this.listView1.TabIndex = 0;
			this.listView1.TabStop = false;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// Product_image_columnHeader
			// 
			this.Product_image_columnHeader.Text = "商品画像";
			this.Product_image_columnHeader.Width = 116;
			// 
			// Product_name_columnHeader
			// 
			this.Product_name_columnHeader.Text = "商品名";
			this.Product_name_columnHeader.Width = 422;
			// 
			// Product_release_date_columnHeader
			// 
			this.Product_release_date_columnHeader.Text = "発売日";
			this.Product_release_date_columnHeader.Width = 113;
			// 
			// Select_button
			// 
			this.Select_button.Location = new System.Drawing.Point(13, 285);
			this.Select_button.Name = "Select_button";
			this.Select_button.Size = new System.Drawing.Size(209, 66);
			this.Select_button.TabIndex = 1;
			this.Select_button.Text = "選択";
			this.Select_button.UseVisualStyleBackColor = true;
			// 
			// Cancel_button
			// 
			this.Cancel_button.Location = new System.Drawing.Point(465, 285);
			this.Cancel_button.Name = "Cancel_button";
			this.Cancel_button.Size = new System.Drawing.Size(209, 66);
			this.Cancel_button.TabIndex = 2;
			this.Cancel_button.Text = "キャンセル";
			this.Cancel_button.UseVisualStyleBackColor = true;
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// SearchResultForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(686, 363);
			this.Controls.Add(this.Cancel_button);
			this.Controls.Add(this.Select_button);
			this.Controls.Add(this.listView1);
			this.Name = "SearchResultForm";
			this.Text = "SearchResultForm";
			this.Load += new System.EventHandler(this.SearchResultForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader Product_image_columnHeader;
		private System.Windows.Forms.ColumnHeader Product_name_columnHeader;
		private System.Windows.Forms.ColumnHeader Product_release_date_columnHeader;
		private System.Windows.Forms.Button Select_button;
		private System.Windows.Forms.Button Cancel_button;
		private System.Windows.Forms.ImageList imageList1;
	}
}