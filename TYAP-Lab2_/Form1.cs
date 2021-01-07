using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TYAP_Lab2_
{
	public partial class Form1 : Form
	{
        private readonly DKA dka;
        public Form1()
        {
            InitializeComponent();

            dka = new DKA();

            state_text_box.Text = "q0,q1";
            final_state_text_box.Text = "q1";
            symbols_text_box.Text = "01";
            keyword_text_box.Text = "010101";
            begin_state_combo_box.Text = "q0";

            dka_history_data_grid_view.ColumnCount = 1;

            state_text_box.TextChanged += Text_input;
            symbols_text_box.TextChanged += Text_input;
            keyword_text_box.TextChanged += Text_input;

            Execute_button.Click += Execute_button_Click;
        }
        private void Update_rules()
        {
            begin_state_combo_box.Text = "";
            begin_state_combo_box.Items.Clear();
            transitions_data_grid_view.Columns.Clear();
            if (state_text_box.Text.Length > 0 && symbols_text_box.Text.Length > 0)
            {
                string[] rows = state_text_box.Text.Split(',');
                char[] cols = symbols_text_box.Text.ToCharArray();
                foreach (char col in cols)
                {
                    transitions_data_grid_view.Columns.Add(col.ToString(), col.ToString());
                }
                foreach (string row in rows)
                {
                    DataGridViewRow viewRow = new DataGridViewRow();
                    viewRow.HeaderCell.Value = row;
                    transitions_data_grid_view.Rows.Add(viewRow);
                    begin_state_combo_box.Items.Add(row);
                }
            }
        }            
        private void Text_input(object sender, EventArgs e)
        {
            if (sender != keyword_text_box)
			{
                Update_rules();
            }
        }
        void Execute_button_Click(object sender, EventArgs e)
        {
            string[][] transitions = new string[transitions_data_grid_view.RowCount][];

            for (int i = 0; i < transitions_data_grid_view.RowCount; i++)
            {
                transitions[i] = new string[transitions_data_grid_view.ColumnCount];
                for (int j = 0; j < transitions_data_grid_view.ColumnCount; j++)
                {
                    transitions[i][j] = transitions_data_grid_view.Rows[i].Cells[j].Value == null ? "" : transitions_data_grid_view.Rows[i].Cells[j].Value.ToString();
                }
            }

            dka_history_data_grid_view.Rows.Clear();
            try
            {
                dka.states = state_text_box.Text.Split(',');
                dka.final_states = final_state_text_box.Text.Split(',');
                dka.symbols = symbols_text_box.Text.ToCharArray();
                dka.keyword = keyword_text_box.Text.ToString();
                dka.begin_state = begin_state_combo_box.Text.ToString();
                dka.transitions = transitions;
                dka.Exec();

                status.ForeColor = Color.DarkGreen;
                status.Text = "Цепочка принадлежит заданному регулярному языку.";
            }
            catch (Exception exc)
            {
                status.ForeColor = Color.DarkRed;
                status.Text = exc.Message;
            }
            foreach (string str in dka.dka_history)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dka_history_data_grid_view, str);
                dka_history_data_grid_view.Rows.Add(row);
            }
        }
    }
}
