namespace TYAP_Lab2_
{
	partial class Form1
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.final_state_text_box = new System.Windows.Forms.TextBox();
			this.begin_state_combo_box = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.transitions_data_grid_view = new System.Windows.Forms.DataGridView();
			this.state_text_box = new System.Windows.Forms.TextBox();
			this.symbols_text_box = new System.Windows.Forms.TextBox();
			this.keyword_text_box = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.Execute_button = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.dka_history_data_grid_view = new System.Windows.Forms.DataGridView();
			this.label8 = new System.Windows.Forms.Label();
			this.status = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.transitions_data_grid_view)).BeginInit();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dka_history_data_grid_view)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.final_state_text_box);
			this.groupBox1.Controls.Add(this.begin_state_combo_box);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.transitions_data_grid_view);
			this.groupBox1.Controls.Add(this.state_text_box);
			this.groupBox1.Controls.Add(this.symbols_text_box);
			this.groupBox1.Controls.Add(this.keyword_text_box);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(9, 10);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
			this.groupBox1.Size = new System.Drawing.Size(393, 408);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Исходные данные:";
			// 
			// final_state_text_box
			// 
			this.final_state_text_box.Location = new System.Drawing.Point(205, 88);
			this.final_state_text_box.Name = "final_state_text_box";
			this.final_state_text_box.Size = new System.Drawing.Size(182, 20);
			this.final_state_text_box.TabIndex = 10;
			this.final_state_text_box.Text = "q2";
			// 
			// begin_state_combo_box
			// 
			this.begin_state_combo_box.FormattingEnabled = true;
			this.begin_state_combo_box.Location = new System.Drawing.Point(205, 41);
			this.begin_state_combo_box.Name = "begin_state_combo_box";
			this.begin_state_combo_box.Size = new System.Drawing.Size(182, 21);
			this.begin_state_combo_box.TabIndex = 9;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(202, 25);
			this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(121, 13);
			this.label7.TabIndex = 7;
			this.label7.Text = "Начальное состояние:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(134, 119);
			this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(124, 13);
			this.label5.TabIndex = 5;
			this.label5.Text = "Проверяемая цепочка:";
			// 
			// transitions_data_grid_view
			// 
			this.transitions_data_grid_view.AllowUserToAddRows = false;
			this.transitions_data_grid_view.AllowUserToDeleteRows = false;
			this.transitions_data_grid_view.AllowUserToResizeColumns = false;
			this.transitions_data_grid_view.AllowUserToResizeRows = false;
			this.transitions_data_grid_view.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
			this.transitions_data_grid_view.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.transitions_data_grid_view.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.transitions_data_grid_view.Location = new System.Drawing.Point(7, 171);
			this.transitions_data_grid_view.Margin = new System.Windows.Forms.Padding(2);
			this.transitions_data_grid_view.MultiSelect = false;
			this.transitions_data_grid_view.Name = "transitions_data_grid_view";
			this.transitions_data_grid_view.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
			this.transitions_data_grid_view.RowTemplate.Height = 24;
			this.transitions_data_grid_view.Size = new System.Drawing.Size(380, 232);
			this.transitions_data_grid_view.TabIndex = 1;
			// 
			// state_text_box
			// 
			this.state_text_box.Location = new System.Drawing.Point(7, 42);
			this.state_text_box.Margin = new System.Windows.Forms.Padding(2);
			this.state_text_box.Name = "state_text_box";
			this.state_text_box.ShortcutsEnabled = false;
			this.state_text_box.Size = new System.Drawing.Size(186, 20);
			this.state_text_box.TabIndex = 2;
			this.state_text_box.Text = "q0,q1,q2";
			// 
			// symbols_text_box
			// 
			this.symbols_text_box.Location = new System.Drawing.Point(7, 88);
			this.symbols_text_box.Margin = new System.Windows.Forms.Padding(2);
			this.symbols_text_box.Name = "symbols_text_box";
			this.symbols_text_box.ShortcutsEnabled = false;
			this.symbols_text_box.Size = new System.Drawing.Size(186, 20);
			this.symbols_text_box.TabIndex = 3;
			this.symbols_text_box.Text = "01abc";
			// 
			// keyword_text_box
			// 
			this.keyword_text_box.Location = new System.Drawing.Point(7, 134);
			this.keyword_text_box.Margin = new System.Windows.Forms.Padding(2);
			this.keyword_text_box.Name = "keyword_text_box";
			this.keyword_text_box.ShortcutsEnabled = false;
			this.keyword_text_box.Size = new System.Drawing.Size(382, 20);
			this.keyword_text_box.TabIndex = 4;
			this.keyword_text_box.Text = "001ab001c";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 156);
			this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(109, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "Таблица переходов:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(202, 71);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(116, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Конечные состояния:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(4, 71);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(54, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Алфавит:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 25);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(124, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Множество состояний:";
			// 
			// Execute_button
			// 
			this.Execute_button.Location = new System.Drawing.Point(16, 417);
			this.Execute_button.Margin = new System.Windows.Forms.Padding(2);
			this.Execute_button.Name = "Execute_button";
			this.Execute_button.Size = new System.Drawing.Size(380, 34);
			this.Execute_button.TabIndex = 2;
			this.Execute_button.Text = "Выполнить";
			this.Execute_button.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.dka_history_data_grid_view);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.status);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Location = new System.Drawing.Point(412, 10);
			this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
			this.groupBox2.Size = new System.Drawing.Size(322, 435);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Результаты";
			// 
			// dka_history_data_grid_view
			// 
			this.dka_history_data_grid_view.AllowUserToAddRows = false;
			this.dka_history_data_grid_view.AllowUserToDeleteRows = false;
			this.dka_history_data_grid_view.AllowUserToResizeColumns = false;
			this.dka_history_data_grid_view.AllowUserToResizeRows = false;
			this.dka_history_data_grid_view.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.dka_history_data_grid_view.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
			this.dka_history_data_grid_view.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dka_history_data_grid_view.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dka_history_data_grid_view.ColumnHeadersVisible = false;
			this.dka_history_data_grid_view.Location = new System.Drawing.Point(7, 136);
			this.dka_history_data_grid_view.Margin = new System.Windows.Forms.Padding(2);
			this.dka_history_data_grid_view.MultiSelect = false;
			this.dka_history_data_grid_view.Name = "dka_history_data_grid_view";
			this.dka_history_data_grid_view.ReadOnly = true;
			this.dka_history_data_grid_view.RowHeadersVisible = false;
			this.dka_history_data_grid_view.RowTemplate.Height = 24;
			this.dka_history_data_grid_view.Size = new System.Drawing.Size(309, 295);
			this.dka_history_data_grid_view.TabIndex = 6;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(4, 119);
			this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(115, 13);
			this.label8.TabIndex = 7;
			this.label8.Text = "Построение цепочки:";
			// 
			// status
			// 
			this.status.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.status.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.status.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.status.ForeColor = System.Drawing.Color.DarkGreen;
			this.status.Location = new System.Drawing.Point(4, 41);
			this.status.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.status.Name = "status";
			this.status.Size = new System.Drawing.Size(311, 63);
			this.status.TabIndex = 5;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(4, 25);
			this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(109, 13);
			this.label6.TabIndex = 4;
			this.label6.Text = "Статус выполнения:";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(738, 456);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.Execute_button);
			this.Controls.Add(this.groupBox1);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(754, 495);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(754, 495);
			this.Name = "Form1";
			this.Text = "ДКА";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.transitions_data_grid_view)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dka_history_data_grid_view)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView transitions_data_grid_view;
        private System.Windows.Forms.TextBox state_text_box;
        private System.Windows.Forms.TextBox symbols_text_box;
        private System.Windows.Forms.TextBox keyword_text_box;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button Execute_button;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dka_history_data_grid_view;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox begin_state_combo_box;
		private System.Windows.Forms.TextBox final_state_text_box;
	}
}

