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
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.OK_button = new System.Windows.Forms.Button();
			this.Cancel_button = new System.Windows.Forms.Button();
			this.SearchStop_button = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Product_image_columnHeader,
            this.Product_name_columnHeader,
            this.Product_release_date_columnHeader});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listView1.Location = new System.Drawing.Point(13, 12);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(661, 265);
			this.listView1.SmallImageList = this.imageList1;
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
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(100, 100);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// OK_button
			// 
			this.OK_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.OK_button.Enabled = false;
			this.OK_button.Location = new System.Drawing.Point(13, 285);
			this.OK_button.Name = "OK_button";
			this.OK_button.Size = new System.Drawing.Size(209, 66);
			this.OK_button.TabIndex = 1;
			this.OK_button.Text = "OK";
			this.OK_button.UseVisualStyleBackColor = true;
			this.OK_button.Click += new System.EventHandler(this.OK_button_Click);
			// 
			// Cancel_button
			// 
			this.Cancel_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.Cancel_button.Enabled = false;
			this.Cancel_button.Location = new System.Drawing.Point(465, 285);
			this.Cancel_button.Name = "Cancel_button";
			this.Cancel_button.Size = new System.Drawing.Size(209, 66);
			this.Cancel_button.TabIndex = 2;
			this.Cancel_button.Text = "キャンセル";
			this.Cancel_button.UseVisualStyleBackColor = true;
			this.Cancel_button.Click += new System.EventHandler(this.Cancel_button_Click);
			// 
			// SearchStop_button
			// 
			this.SearchStop_button.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.SearchStop_button.Location = new System.Drawing.Point(228, 285);
			this.SearchStop_button.Name = "SearchStop_button";
			this.SearchStop_button.Size = new System.Drawing.Size(231, 66);
			this.SearchStop_button.TabIndex = 3;
			this.SearchStop_button.Text = "検索打ち止め";
			this.SearchStop_button.UseVisualStyleBackColor = true;
			this.SearchStop_button.Click += new System.EventHandler(this.SearchStop_button_Click);
			// 
			// SearchResultForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(686, 363);
			this.Controls.Add(this.SearchStop_button);
			this.Controls.Add(this.Cancel_button);
			this.Controls.Add(this.OK_button);
			this.Controls.Add(this.listView1);
			this.MinimizeBox = false;
			this.Name = "SearchResultForm";
			this.Text = "検索結果";
			this.Load += new System.EventHandler(this.SearchResultForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader Product_image_columnHeader;
		private System.Windows.Forms.ColumnHeader Product_name_columnHeader;
		private System.Windows.Forms.ColumnHeader Product_release_date_columnHeader;
		private System.Windows.Forms.Button Cancel_button;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Button OK_button;
		private System.Windows.Forms.Button SearchStop_button;
	}
}