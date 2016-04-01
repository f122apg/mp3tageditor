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
			this.button1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.Artist_textbox = new System.Windows.Forms.TextBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.Datasize_label = new System.Windows.Forms.Label();
			this.Imagesize_label = new System.Windows.Forms.Label();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.Delete_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
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
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(640, 406);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 384);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(63, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "アーティスト：";
			// 
			// Artist_textbox
			// 
			this.Artist_textbox.Location = new System.Drawing.Point(81, 381);
			this.Artist_textbox.Name = "Artist_textbox";
			this.Artist_textbox.ReadOnly = true;
			this.Artist_textbox.Size = new System.Drawing.Size(637, 19);
			this.Artist_textbox.TabIndex = 3;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(81, 406);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(160, 153);
			this.pictureBox1.TabIndex = 4;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 406);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 12);
			this.label2.TabIndex = 5;
			this.label2.Text = "アートワーク：";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(247, 528);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(68, 12);
			this.label3.TabIndex = 6;
			this.label3.Text = "データサイズ：";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(275, 547);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 12);
			this.label4.TabIndex = 7;
			this.label4.Text = "大きさ：";
			// 
			// Datasize_label
			// 
			this.Datasize_label.AutoSize = true;
			this.Datasize_label.Location = new System.Drawing.Point(312, 528);
			this.Datasize_label.Name = "Datasize_label";
			this.Datasize_label.Size = new System.Drawing.Size(27, 12);
			this.Datasize_label.TabIndex = 8;
			this.Datasize_label.Text = "N/A";
			// 
			// Imagesize_label
			// 
			this.Imagesize_label.AutoSize = true;
			this.Imagesize_label.Location = new System.Drawing.Point(312, 547);
			this.Imagesize_label.Name = "Imagesize_label";
			this.Imagesize_label.Size = new System.Drawing.Size(27, 12);
			this.Imagesize_label.TabIndex = 9;
			this.Imagesize_label.Text = "N/A";
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Delete_ToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(153, 48);
			// 
			// Delete_ToolStripMenuItem
			// 
			this.Delete_ToolStripMenuItem.Name = "Delete_ToolStripMenuItem";
			this.Delete_ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.Delete_ToolStripMenuItem.Text = "削除(&D)";
			this.Delete_ToolStripMenuItem.Click += new System.EventHandler(this.Delete_ToolStripMenuItem_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(727, 571);
			this.Controls.Add(this.Imagesize_label);
			this.Controls.Add(this.Datasize_label);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.Artist_textbox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.listView1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader Filename_columnHeader;
		private System.Windows.Forms.ColumnHeader Path_columnHeader;
		private System.Windows.Forms.ColumnHeader Songname_columnHeader;
		private System.Windows.Forms.ColumnHeader Songtime_columnHeader;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox Artist_textbox;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label Datasize_label;
		private System.Windows.Forms.Label Imagesize_label;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem Delete_ToolStripMenuItem;
	}
}

