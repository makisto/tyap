using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TYAP_Lab3
{
	public partial class Form1 : Form
	{
        private readonly DMPA dmpa;
        public Form1()
        {
            InitializeComponent();

            dmpa = new DMPA();

            dmpa_history_data_grid_view.ColumnCount = 1;
            transitions_data_grid_view.Columns.Add("q0", "q0");
            transitions_data_grid_view.Columns.Add("w0", "w0");
            transitions_data_grid_view.Columns.Add("z0", "z0");
            transitions_data_grid_view.Columns.Add("q1", "q1");
            transitions_data_grid_view.Columns.Add("z1", "z1");

            states_text_box.TextChanged += InputTextChanged;
            symbols_text_box.TextChanged += InputTextChanged;
            final_states_text_box.TextChanged += InputTextChanged;
            stack_symbols_text_box.TextChanged += InputTextChanged;
            keyword_text_box.TextChanged += InputTextChanged;

            Execute_button.Click += CalcBtn_Click;
        }
        private void ValidateCalc()
        {
            Execute_button.Enabled = states_text_box.Text.Length > 0 && symbols_text_box.Text.Length > 0 && final_states_text_box.Text.Length > 0 && stack_symbols_text_box.Text.Length > 0;
        }
        private void InputTextChanged(object sender, EventArgs e)
        {
            ValidateCalc();
        }
        void CalcBtn_Click(object sender, EventArgs e)
        {
            try
            {
                dmpa.Clear();
                dmpa.states = states_text_box.Text;
                dmpa.symbols = symbols_text_box.Text;
                dmpa.final_states = final_states_text_box.Text;
                dmpa.stack_symbols = stack_symbols_text_box.Text;
                dmpa.keyword = keyword_text_box.Text;

                for (int i = 0; i < transitions_data_grid_view.RowCount; i++)
                {
                    dmpa.Add_transition_to_transition_dictionary(
                        GetGrdiVal(i, 0),
                        GetGrdiVal(i, 1),
                        GetGrdiVal(i, 2),
                        GetGrdiVal(i, 3),
                        GetGrdiVal(i, 4)
                        );
                }

                if (keyword_text_box.Text == "")
                {
                    dmpa.list_of_tacts.Add("(" + dmpa.states[0] + ", " + " " + ", " + dmpa.stack_symbols[0] + ")");
                    throw new Exception("Не существует правила перехода для прочтения пустой цепочки");
                }

                dmpa.Exec();

                dmpa_status.ForeColor = Color.DarkGreen;
                dmpa_status.Text = "Цепочка принадлежит заданному языку";
            }
            catch (Exception exc)
            {
                dmpa_status.ForeColor = Color.DarkRed;
                dmpa_status.Text = exc.Message;
            }

            dmpa_history_data_grid_view.Rows.Clear();
            foreach (string str in dmpa.list_of_tacts)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dmpa_history_data_grid_view, str);
                dmpa_history_data_grid_view.Rows.Add(row);
            }
        }

        private string GetGrdiVal(int row, int col)
        {
            object gridVal = transitions_data_grid_view.Rows[row].Cells[col].Value;
            return (gridVal == null || (string)gridVal == "") ? "" : (string)gridVal;
        }
	}
}
