namespace TYAP_Lab1
{
	partial class Form1
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
			if (disposing && (components != null))
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
			this.label1 = new System.Windows.Forms.Label();
			this.max_numeric = new System.Windows.Forms.NumericUpDown();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.non_term_text_box = new System.Windows.Forms.TextBox();
			this.term_text_box = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.rules_grid_view = new System.Windows.Forms.DataGridView();
			this.label5 = new System.Windows.Forms.Label();
			this.execute_button = new System.Windows.Forms.Button();
			this.chains_grid_view = new System.Windows.Forms.DataGridView();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.start_non_term_text_box = new System.Windows.Forms.TextBox();
			this.min_numeric = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.max_numeric)).BeginInit();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.rules_grid_view)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.chains_grid_view)).BeginInit();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.min_numeric)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(2, 20);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(30, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Max:";
			// 
			// max_numeric
			// 
			this.max_numeric.Location = new System.Drawing.Point(36, 19);
			this.max_numeric.Margin = new System.Windows.Forms.Padding(2);
			this.max_numeric.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
			this.max_numeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.max_numeric.Name = "max_numeric";
			this.max_numeric.Size = new System.Drawing.Size(49, 20);
			this.max_numeric.TabIndex = 1;
			this.max_numeric.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.max_numeric.ValueChanged += new System.EventHandler(this.MaxChainResultLenSpinner_ValueChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.non_term_text_box);
			this.groupBox2.Controls.Add(this.term_text_box);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Location = new System.Drawing.Point(9, 78);
			this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
			this.groupBox2.Size = new System.Drawing.Size(245, 89);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Символы:";
			// 
			// non_term_text_box
			// 
			this.non_term_text_box.Location = new System.Drawing.Point(111, 50);
			this.non_term_text_box.Margin = new System.Windows.Forms.Padding(2);
			this.non_term_text_box.Name = "non_term_text_box";
			this.non_term_text_box.ShortcutsEnabled = false;
			this.non_term_text_box.Size = new System.Drawing.Size(130, 20);
			this.non_term_text_box.TabIndex = 6;
			// 
			// term_text_box
			// 
			this.term_text_box.Location = new System.Drawing.Point(111, 26);
			this.term_text_box.Margin = new System.Windows.Forms.Padding(2);
			this.term_text_box.Name = "term_text_box";
			this.term_text_box.ShortcutsEnabled = false;
			this.term_text_box.Size = new System.Drawing.Size(130, 20);
			this.term_text_box.TabIndex = 4;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(8, 53);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(99, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Нетерминальные:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 28);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(87, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Терминальные:";
			// 
			// rules_grid_view
			// 
			this.rules_grid_view.AllowUserToAddRows = false;
			this.rules_grid_view.AllowUserToDeleteRows = false;
			this.rules_grid_view.AllowUserToResizeColumns = false;
			this.rules_grid_view.AllowUserToResizeRows = false;
			this.rules_grid_view.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.rules_grid_view.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
			this.rules_grid_view.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.rules_grid_view.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.rules_grid_view.Location = new System.Drawing.Point(9, 184);
			this.rules_grid_view.Margin = new System.Windows.Forms.Padding(2);
			this.rules_grid_view.MultiSelect = false;
			this.rules_grid_view.Name = "rules_grid_view";
			this.rules_grid_view.RowHeadersVisible = false;
			this.rules_grid_view.RowTemplate.Height = 24;
			this.rules_grid_view.Size = new System.Drawing.Size(245, 213);
			this.rules_grid_view.TabIndex = 6;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 169);
			this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(54, 13);
			this.label5.TabIndex = 7;
			this.label5.Text = "Правила:";
			// 
			// execute_button
			// 
			this.execute_button.Location = new System.Drawing.Point(12, 401);
			this.execute_button.Margin = new System.Windows.Forms.Padding(2);
			this.execute_button.Name = "execute_button";
			this.execute_button.Size = new System.Drawing.Size(231, 44);
			this.execute_button.TabIndex = 8;
			this.execute_button.Text = "Выполнить";
			this.execute_button.UseVisualStyleBackColor = true;
			// 
			// chains_grid_view
			// 
			this.chains_grid_view.AllowUserToAddRows = false;
			this.chains_grid_view.AllowUserToDeleteRows = false;
			this.chains_grid_view.AllowUserToResizeColumns = false;
			this.chains_grid_view.AllowUserToResizeRows = false;
			this.chains_grid_view.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.chains_grid_view.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
			this.chains_grid_view.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.chains_grid_view.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.chains_grid_view.Location = new System.Drawing.Point(258, 8);
			this.chains_grid_view.Margin = new System.Windows.Forms.Padding(2);
			this.chains_grid_view.Name = "chains_grid_view";
			this.chains_grid_view.ReadOnly = true;
			this.chains_grid_view.RowHeadersVisible = false;
			this.chains_grid_view.RowTemplate.Height = 24;
			this.chains_grid_view.Size = new System.Drawing.Size(477, 437);
			this.chains_grid_view.TabIndex = 9;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Controls.Add(this.start_non_term_text_box);
			this.groupBox3.Controls.Add(this.min_numeric);
			this.groupBox3.Controls.Add(this.label6);
			this.groupBox3.Controls.Add(this.max_numeric);
			this.groupBox3.Controls.Add(this.label1);
			this.groupBox3.Location = new System.Drawing.Point(9, 8);
			this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
			this.groupBox3.Size = new System.Drawing.Size(245, 66);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Дипазон";
			// 
			// start_non_term_text_box
			// 
			this.start_non_term_text_box.Location = new System.Drawing.Point(111, 37);
			this.start_non_term_text_box.Margin = new System.Windows.Forms.Padding(2);
			this.start_non_term_text_box.MaxLength = 1;
			this.start_non_term_text_box.Name = "start_non_term_text_box";
			this.start_non_term_text_box.ShortcutsEnabled = false;
			this.start_non_term_text_box.Size = new System.Drawing.Size(130, 20);
			this.start_non_term_text_box.TabIndex = 5;
			// 
			// min_numeric
			// 
			this.min_numeric.Location = new System.Drawing.Point(35, 42);
			this.min_numeric.Margin = new System.Windows.Forms.Padding(2);
			this.min_numeric.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.min_numeric.Name = "min_numeric";
			this.min_numeric.Size = new System.Drawing.Size(50, 20);
			this.min_numeric.TabIndex = 3;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(4, 44);
			this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(27, 13);
			this.label6.TabIndex = 2;
			this.label6.Text = "Min:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(108, 19);
			this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(129, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "Стартовый нетерминал:";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(738, 456);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.chains_grid_view);
			this.Controls.Add(this.execute_button);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.rules_grid_view);
			this.Controls.Add(this.groupBox2);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(754, 495);
			this.MinimumSize = new System.Drawing.Size(754, 495);
			this.Name = "Form1";
			this.Text = "Генератор цепочек";
			((System.ComponentModel.ISupportInitialize)(this.max_numeric)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.rules_grid_view)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.chains_grid_view)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.min_numeric)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown max_numeric;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox non_term_text_box;
		private System.Windows.Forms.TextBox term_text_box;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DataGridView rules_grid_view;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button execute_button;
		private System.Windows.Forms.DataGridView chains_grid_view;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.NumericUpDown min_numeric;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox start_non_term_text_box;
		private System.Windows.Forms.Label label4;
	}
}

