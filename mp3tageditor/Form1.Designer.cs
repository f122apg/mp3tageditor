namespace mp3tageditor
{
	partial class Form1
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.listView1 = new System.Windows.Forms.ListView();
			this.Filename_columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Path_columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Songname_columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Songtime_columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.Delete_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.Search_button = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.Artist_textbox = new System.Windows.Forms.TextBox();
			this.mp3artwork_pictureBox = new System.Windows.Forms.PictureBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.Datasize_label = new System.Windows.Forms.Label();
			this.Imagesize_label = new System.Windows.Forms.Label();
			this.KeyWordSearch_textBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.ReplaceArtwork_pictureBox = new System.Windows.Forms.PictureBox();
			this.label6 = new System.Windows.Forms.Label();
			this.ReplaceArtist_textBox = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.changing_tag_button = new System.Windows.Forms.Button();
			this.contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mp3artwork_pictureBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ReplaceArtwork_pictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.AllowDrop = true;
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Filename_columnHeader,
            this.Path_columnHeader,
            this.Songname_columnHeader,
            this.Songtime_columnHeader});
			this.listView1.ContextMenuStrip = this.contextMenuStrip1;
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listView1.Location = new System.Drawing.Point(12, 12);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(706, 363);
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
			this.listView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.listView1_DragDrop);
			this.listView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.listView1_DragEnter);
			// 
			// Filename_columnHeader
			// 
			this.Filename_columnHeader.Text = "ファイル名";
			this.Filename_columnHeader.Width = 151;
			// 
			// Path_columnHeader
			// 
			this.Path_columnHeader.Text = "パス";
			this.Path_columnHeader.Width = 288;
			// 
			// Songname_columnHeader
			// 
			this.Songname_columnHeader.Text = "曲名";
			this.Songname_columnHeader.Width = 178;
			// 
			// Songtime_columnHeader
			// 
			this.Songtime_columnHeader.Text = "曲の長さ";
			this.Songtime_columnHeader.Width = 70;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Delete_ToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(120, 26);
			// 
			// Delete_ToolStripMenuItem
			// 
			this.Delete_ToolStripMenuItem.Name = "Delete_ToolStripMenuItem";
			this.Delete_ToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
			this.Delete_ToolStripMenuItem.Text = "削除(&D)";
			this.Delete_ToolStripMenuItem.Click += new System.EventHandler(this.Delete_ToolStripMenuItem_Click);
			// 
			// Search_button
			// 
			this.Search_button.Location = new System.Drawing.Point(442, 446);
			this.Search_button.Name = "Search_button";
			this.Search_button.Size = new System.Drawing.Size(274, 26);
			this.Search_button.TabIndex = 1;
			this.Search_button.Text = "検索";
			this.Search_button.UseVisualStyleBackColor = true;
			this.Search_button.Click += new System.EventHandler(this.Search_button_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 384);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(164, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "mp3に記載されていたアーティスト：";
			// 
			// Artist_textbox
			// 
			this.Artist_textbox.Location = new System.Drawing.Point(180, 381);
			this.Artist_textbox.Name = "Artist_textbox";
			this.Artist_textbox.ReadOnly = true;
			this.Artist_textbox.Size = new System.Drawing.Size(538, 19);
			this.Artist_textbox.TabIndex = 3;
			// 
			// mp3artwork_pictureBox
			// 
			this.mp3artwork_pictureBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.mp3artwork_pictureBox.Location = new System.Drawing.Point(12, 421);
			this.mp3artwork_pictureBox.Name = "mp3artwork_pictureBox";
			this.mp3artwork_pictureBox.Size = new System.Drawing.Size(160, 138);
			this.mp3artwork_pictureBox.TabIndex = 4;
			this.mp3artwork_pictureBox.TabStop = false;
			this.mp3artwork_pictureBox.Click += new System.EventHandler(this.mp3artwork_pictureBox_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 406);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(166, 12);
			this.label2.TabIndex = 5;
			this.label2.Text = "mp3に格納されていたアートワーク：\r\n";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(178, 529);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(35, 12);
			this.label3.TabIndex = 6;
			this.label3.Text = "容量：";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(178, 548);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 12);
			this.label4.TabIndex = 7;
			this.label4.Text = "大きさ：";
			// 
			// Datasize_label
			// 
			this.Datasize_label.AutoSize = true;
			this.Datasize_label.Location = new System.Drawing.Point(219, 529);
			this.Datasize_label.Name = "Datasize_label";
			this.Datasize_label.Size = new System.Drawing.Size(27, 12);
			this.Datasize_label.TabIndex = 8;
			this.Datasize_label.Text = "N/A";
			// 
			// Imagesize_label
			// 
			this.Imagesize_label.AutoSize = true;
			this.Imagesize_label.Location = new System.Drawing.Point(219, 548);
			this.Imagesize_label.Name = "Imagesize_label";
			this.Imagesize_label.Size = new System.Drawing.Size(27, 12);
			this.Imagesize_label.TabIndex = 9;
			this.Imagesize_label.Text = "N/A";
			// 
			// KeyWordSearch_textBox
			// 
			this.KeyWordSearch_textBox.Location = new System.Drawing.Point(442, 421);
			this.KeyWordSearch_textBox.Name = "KeyWordSearch_textBox";
			this.KeyWordSearch_textBox.Size = new System.Drawing.Size(274, 19);
			this.KeyWordSearch_textBox.TabIndex = 10;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(274, 406);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(135, 12);
			this.label5.TabIndex = 11;
			this.label5.Text = "置き換えられるアートワーク：";
			// 
			// ReplaceArtwork_pictureBox
			// 
			this.ReplaceArtwork_pictureBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.ReplaceArtwork_pictureBox.Location = new System.Drawing.Point(276, 421);
			this.ReplaceArtwork_pictureBox.Name = "ReplaceArtwork_pictureBox";
			this.ReplaceArtwork_pictureBox.Size = new System.Drawing.Size(160, 139);
			this.ReplaceArtwork_pictureBox.TabIndex = 12;
			this.ReplaceArtwork_pictureBox.TabStop = false;
			this.ReplaceArtwork_pictureBox.Click += new System.EventHandler(this.ReplaceArtwork_pictureBox_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(442, 475);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(150, 12);
			this.label6.TabIndex = 13;
			this.label6.Text = "置き換えられるアーティストタグ：";
			// 
			// ReplaceArtist_textBox
			// 
			this.ReplaceArtist_textBox.Location = new System.Drawing.Point(444, 492);
			this.ReplaceArtist_textBox.Name = "ReplaceArtist_textBox";
			this.ReplaceArtist_textBox.Size = new System.Drawing.Size(274, 19);
			this.ReplaceArtist_textBox.TabIndex = 14;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(440, 406);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(111, 12);
			this.label7.TabIndex = 15;
			this.label7.Text = "このキーワードで検索：";
			// 
			// changing_tag_button
			// 
			this.changing_tag_button.Location = new System.Drawing.Point(444, 517);
			this.changing_tag_button.Name = "changing_tag_button";
			this.changing_tag_button.Size = new System.Drawing.Size(274, 43);
			this.changing_tag_button.TabIndex = 17;
			this.changing_tag_button.Text = "タグを置き換える";
			this.changing_tag_button.UseVisualStyleBackColor = true;
			this.changing_tag_button.Click += new System.EventHandler(this.changing_tag_button_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(727, 571);
			this.Controls.Add(this.changing_tag_button);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.ReplaceArtist_textBox);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.ReplaceArtwork_pictureBox);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.KeyWordSearch_textBox);
			this.Controls.Add(this.Imagesize_label);
			this.Controls.Add(this.Datasize_label);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.mp3artwork_pictureBox);
			this.Controls.Add(this.Artist_textbox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.Search_button);
			this.Controls.Add(this.listView1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Text = "Mp3TagEditor";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mp3artwork_pictureBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ReplaceArtwork_pictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader Filename_columnHeader;
		private System.Windows.Forms.ColumnHeader Path_columnHeader;
		private System.Windows.Forms.ColumnHeader Songname_columnHeader;
		private System.Windows.Forms.ColumnHeader Songtime_columnHeader;
		private System.Windows.Forms.Button Search_button;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox Artist_textbox;
		private System.Windows.Forms.PictureBox mp3artwork_pictureBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label Datasize_label;
		private System.Windows.Forms.Label Imagesize_label;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem Delete_ToolStripMenuItem;
		private System.Windows.Forms.TextBox KeyWordSearch_textBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.PictureBox ReplaceArtwork_pictureBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox ReplaceArtist_textBox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button changing_tag_button;
	}
}

