using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TYAP_Lab1
{
    public partial class Form1 : Form
    {
        public bool is_first_launch = false;
        public bool is_changed_start = false;
        public string old_text_box;
        public string res;
        public char is_start;
        private readonly ChainsGeneration generic;
        private readonly Dictionary<string, DataGridViewRow> non_term_dictionary;
        public Form1()
        {
            InitializeComponent();

            generic = new ChainsGeneration();
            non_term_dictionary = new Dictionary<string, DataGridViewRow>();

            term_text_box.KeyPress += Term_text_box_KeyPressed;
            term_text_box.TextChanged += Term_text_box_TextChanged;
            non_term_text_box.KeyPress += Non_term_text_box_KeyPressed;
            non_term_text_box.TextChanged += Non_term_text_box_TextChanged;
            start_non_term_text_box.KeyPress += Start_non_term_text_box_KeyPressed;
            start_non_term_text_box.TextChanged += Start_non_term_text_box_TextChanged;

            rules_grid_view.Columns.Add("name", "Символ");
            rules_grid_view.Columns.Add("content", "Значение");
            rules_grid_view.Columns[0].ReadOnly = true;
            rules_grid_view.Columns[0].Width = 50;
            rules_grid_view.EditingControlShowing += Rules_grid_view_EditingControlShowing;

            chains_grid_view.Columns.Add("result", "Результат");
            chains_grid_view.Columns[0].MinimumWidth = chains_grid_view.Width;

            generic.Init();

            execute_button.Enabled = false;
            execute_button.Click += Execute_button_Click;
        }
        private void Update_rules(bool is_changed, int is_text)
        {
            string key;
            DataGridViewRow row;

            if(is_text == 1)
			{
                string total_res;
                string total_count = "";
                for (int i = 0; i < non_term_dictionary.Keys.Count; i++)
                {
                    key = non_term_dictionary.Keys.ElementAt(i);
                    total_res = non_term_dictionary[key].Cells[1].Value.ToString();
                    for (int j = 0; j < total_res.Length; j++)
                    {
                        if (total_res[j].ToString() == res)
                        {
                            total_count += is_start;
                        }
                        else if (total_res[j] == is_start)
                        {
                            total_count += res;
                        }
                        else
                        {
                            total_count += total_res[j];
                        }
                    }
                    non_term_dictionary[key].Cells[1].Value = total_count;
                    total_count = "";
                }
            }
            if (is_changed)
            {
                if (!non_term_dictionary.ContainsKey(res))
                {
                    //MessageBox.Show("CHANGED");
                    string oldValue = non_term_dictionary[is_start.ToString()].Cells[1].Value.ToString();
                    rules_grid_view.Rows.Remove(non_term_dictionary[is_start.ToString()]);
                    non_term_dictionary.Remove(is_start.ToString());

                    DataGridViewRow new_row = new DataGridViewRow();
                    new_row.CreateCells(rules_grid_view, res, oldValue);
                    rules_grid_view.Rows.Add(new_row);
                    non_term_dictionary[res] = new_row;
                }
                if(is_changed && is_changed_start == false)
				{
                    //MessageBox.Show("CHANGED 2");
                    string total_res;
                    string total_count = "";
                    for (int i = 0; i < non_term_dictionary.Keys.Count; i++)
                    {
                        key = non_term_dictionary.Keys.ElementAt(i);
                        total_res = non_term_dictionary[key].Cells[1].Value.ToString();
                        for (int j = 0; j < total_res.Length; j++)
                        {
                            if (total_res[j].ToString() == res)
                            {
                                total_count += is_start;
                            }
                            else if (total_res[j] == is_start)
                            {
                                total_count += res;
                            }
                            else
                            {
                                total_count += total_res[j];
                            }
                        }
                        non_term_dictionary[key].Cells[1].Value = total_count;
                        total_count = "";
                    }
                }
				/*else
				{
                    temp = non_term_dictionary[is_start.ToString()].Cells[1].Value.ToString();
                    non_term_dictionary[is_start.ToString()].Cells[1].Value = non_term_dictionary[res].Cells[1].Value.ToString();
                    non_term_dictionary[res].Cells[1].Value = temp;
                }*/
            }

            for (int i = 0; i < non_term_dictionary.Keys.Count; i++)
            {
                key = non_term_dictionary.Keys.ElementAt(i);
                if (non_term_text_box.Text.IndexOf(key) == -1 && start_non_term_text_box.Text.IndexOf(key) == -1)
                {
                    rules_grid_view.Rows.Remove(non_term_dictionary[key]);
                    non_term_dictionary.Remove(key);
                }
            }

            for (int i = 0; i < start_non_term_text_box.Text.Length; i++)
            {
                key = start_non_term_text_box.Text[i].ToString();
                if (!non_term_dictionary.ContainsKey(key))
                {
                    row = new DataGridViewRow();
                    row.CreateCells(rules_grid_view, key, "");
                    rules_grid_view.Rows.Add(row);
                    non_term_dictionary[key] = row;
                }
            }

            for (int i = 0; i < non_term_text_box.Text.Length; i++)
            {
                key = non_term_text_box.Text[i].ToString();
                if (!non_term_dictionary.ContainsKey(key))
                {
                    row = new DataGridViewRow();
                    row.CreateCells(rules_grid_view, key, "");
                    rules_grid_view.Rows.Add(row);
                    non_term_dictionary[key] = row;
                }
            }
        }
        private void Term_text_box_TextChanged(object sender, EventArgs e)
		{
            Is_button_valid();
		}
        private void Is_button_valid()
        {
            execute_button.Enabled = term_text_box.Text.Length != 0 && start_non_term_text_box.Text.Length == 1;
        }
        private void ShowChains()
        {
            chains_grid_view.Rows.Clear();
            foreach (string key in generic.chains_list)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(chains_grid_view, key);
                chains_grid_view.Rows.Add(row);
            }
        }
        private void Execute_button_Click(object sender, EventArgs e)
        {
            string key;
            bool is_term;
            bool is_correct = true;
            string fail_message = "";

            generic.RemoveAllRules();

            generic.term = term_text_box.Text;
            generic.non_term = non_term_text_box.Text;
            generic.start_non_term = start_non_term_text_box.Text[0].ToString();
            generic.max_chain_length = (uint)max_numeric.Value;
            generic.min_chain_length = (uint)min_numeric.Value;

            for (int i = 0; i < non_term_dictionary.Keys.Count; i++)
            {
                key = non_term_dictionary.Keys.ElementAt(i);
                string value = non_term_dictionary[key].Cells[1].Value.ToString();
                for (int er = 0; er < value.Length; er++)
                {
                    is_term = false;
                    for (int k = 0; k < term_text_box.Text.Length; k++)
                    {
                        if (value[er] == term_text_box.Text[k])
                        {
                            is_term = true;
                        }
                    }
                    if (is_term == false)
                    {
                        if((value[er] != 'A' && value[er] != 'B'
                            && value[er] != 'C' && value[er] != 'D' && value[er] != 'E' && value[er] != 'F' && value[er] != 'G'
                            && value[er] != 'H' && value[er] != 'I' && value[er] != 'J' && value[er] != 'K' && value[er] != 'L'
                            && value[er] != 'M' && value[er] != 'N' && value[er] != 'O' && value[er] != 'P' && value[er] != 'Q'
                            && value[er] != 'R' && value[er] != 'S' && value[er] != 'T' && value[er] != 'U' && value[er] != 'V'
                            && value[er] != 'W' && value[er] != 'X' && value[er] != 'Y' && value[er] != 'Z' && value[er] != '|'))
                        {
                            is_correct = false;
                            fail_message = "ОШИБКА!\nТЕРМИНАЛЬНОГО СИМВОЛА " + value[er] + " НЕТ В АЛФАВИТЕ";
                            break;
                        }
                    }
                }
                for (int j = 0; j < value.Length; j++)
				{
                    if(!non_term_dictionary.ContainsKey(value[j].ToString()) && (value[j] == 'A' || value[j] == 'B' 
                        || value[j] == 'C' || value[j] == 'D' || value[j] == 'E' || value[j] == 'F' || value[j] == 'G'
                        || value[j] == 'H' || value[j] == 'I' || value[j] == 'J' || value[j] == 'K' || value[j] == 'L'
                        || value[j] == 'M' || value[j] == 'N' || value[j] == 'O' || value[j] == 'P' || value[j] == 'Q'
                        || value[j] == 'R' || value[j] == 'S' || value[j] == 'T' || value[j] == 'U' || value[j] == 'V'
                        || value[j] == 'W' || value[j] == 'X' || value[j] == 'Y' || value[j] == 'Z'))
					{
                        is_correct = false;
                        fail_message = "ОШИБКА!\nНЕТЕРМИНАЛЬНОГО СИМВОЛА " + value[j] + " НЕТ В СПИСКЕ НЕТЕРМИНАЛОВ";
                        break;
                    }
				}
                if (non_term_dictionary[key].Cells[1].Value != null)
				{
                    generic.AddRuleDescription(key, non_term_dictionary[key].Cells[1].Value.ToString());
                }
                else
				{
                    generic.AddRuleDescription(key, "");
                }
            }

			if (is_correct == true)
			{
                generic.GenerateChains();
                ShowChains();
            }
			else
			{
                MessageBox.Show(fail_message);
			}
        }
        private void Rules_grid_view_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress += CellKeyPressed;
            e.Control.Leave += Control_Leave;
            ((TextBox)e.Control).ShortcutsEnabled = false;
        }
        private void Control_Leave(object sender, EventArgs e)
        {
            ((Control)sender).KeyPress -= CellKeyPressed;
        }
        private void Start_non_term_text_box_TextChanged(object sender, EventArgs e)
        {
            if (start_non_term_text_box.Text.Length == 0)
			{
                Update_rules(false, 0);
                Is_button_valid();
                is_first_launch = false;
            }
            else
			{
                bool is_changed = false;
                string total_res = "";
                for (int i = 0; i < non_term_text_box.Text.Length; i++)
                {
                    if (start_non_term_text_box.Text[0].Equals(non_term_text_box.Text[i]))
                    {
                        is_changed_start = true;
                        is_changed = true;
                        res = non_term_text_box.Text[i].ToString().Replace(non_term_text_box.Text[i], is_start);
                        total_res += res;
                        res = non_term_text_box.Text[i].ToString();
                    }
                    else
                    {
                        total_res += non_term_text_box.Text[i];
                    }
                }
                non_term_text_box.Text = total_res;
                old_text_box = total_res;
                if (is_changed == false)
				{
                    res = start_non_term_text_box.Text[0].ToString();
                    if(!non_term_dictionary.ContainsKey(res) && is_first_launch == false)
					{
                        is_first_launch = true;
					}
					else if (!non_term_dictionary.ContainsKey(res) && is_first_launch == true)
					{
                        is_changed = true;
                    }
                }
                Update_rules(is_changed, 0);
                is_start = start_non_term_text_box.Text[0];
                Is_button_valid();
                is_changed_start = false;
            }
        }
        private void Non_term_text_box_TextChanged(object sender, EventArgs e)
        {
            bool buf_changed = is_changed_start;
            string new_text_box = non_term_text_box.Text;
            if(new_text_box.Length == old_text_box.Length && is_changed_start == false)
			{
                for (int i = 0; i < old_text_box.Length; i++)
                {
                    if(old_text_box[i] != new_text_box[i])
					{
                        is_start = old_text_box[i];
                        res = new_text_box[i].ToString();
                        break;
                    }
                }
                is_changed_start = true;
                Update_rules(true, 1);
                is_changed_start = buf_changed;
                is_start = start_non_term_text_box.Text[0];
            }
			else
			{
                Update_rules(false, 0);
                Is_button_valid();
            }
        }
        private void Start_non_term_text_box_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if ((start_non_term_text_box.Text.IndexOf(e.KeyChar) != -1 || e.KeyChar < 'A' || e.KeyChar > 'Z') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
        private void Non_term_text_box_KeyPressed(object sender, KeyPressEventArgs e)
        {
            old_text_box = non_term_text_box.Text;
            if ((start_non_term_text_box.Text.IndexOf(e.KeyChar) != -1 || non_term_text_box.Text.IndexOf(e.KeyChar) != -1 || e.KeyChar < 'A' || e.KeyChar > 'Z') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
        private void Term_text_box_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (term_text_box.Text.IndexOf(e.KeyChar) != -1 || (e.KeyChar >= 'A' && e.KeyChar <= 'Z') || e.KeyChar == '|')
            {
                e.Handled = true;
            }
        }
        private void CellKeyPressed(object sender, KeyPressEventArgs e)
        {
            if (start_non_term_text_box.Text.IndexOf(e.KeyChar) == -1 && non_term_text_box.Text.IndexOf(e.KeyChar) == -1 && term_text_box.Text.IndexOf(e.KeyChar) == -1 && e.KeyChar != '|' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
        private void MaxChainResultLenSpinner_ValueChanged(object sender, EventArgs e)
        {
            min_numeric.Maximum = max_numeric.Value;
        }
	}
}
