﻿using System;
using System.IO;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace TYAP_CursWork
{
    public partial class Form1 : Form
    {
        public int regular_side;
        public string begin_left_state;
        public bool is_first_launch = false;
        public bool is_changed_start = false;
        public string old_text_box = "";
        public char is_start;
        public string res;
        public int global_keys;
        public int global_counter = 0;
		readonly char[] non_terms = new char[] {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L',
            'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        public bool is_stop = false;
        private readonly ChainsGeneration generic;
        private readonly Dictionary<string, DataGridViewRow> non_term_dictionary;
        public string[][] transitions;
        public string[][] new_transitions;
        public string final_states;
        public string buf_final_states;
        public bool need_to_transform = false;
        public Dictionary<string, string[]> transform_dictionary;
        public Dictionary<string, string> buf_transform_dictionary;
        public Dictionary<string, int> states_dictionary;
        public Dictionary<string, int> symbols_dictionary;
        public Dictionary<string, int> result_dictionary;
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

            main_grammar_data_grid_view.Columns.Add("name", "Символ");
            main_grammar_data_grid_view.Columns.Add("content", "Значение");
            main_grammar_data_grid_view.Columns[0].ReadOnly = true;
            main_grammar_data_grid_view.Columns[0].Width = 50;
            main_grammar_data_grid_view.EditingControlShowing += Rules_grid_view_EditingControlShowing;

            fix_grammar_data_grid_view.Columns.Add("name", "Символ");
            fix_grammar_data_grid_view.Columns.Add("content", "Значение");
            fix_grammar_data_grid_view.Columns[0].ReadOnly = true;
            fix_grammar_data_grid_view.Columns[1].ReadOnly = true;

            dka_history_data_grid_view.ColumnCount = 1;

            generic.Init();

            dka_button.Enabled = false;
            transform_button.Enabled = false;
            check_button_text_box.Enabled = false;
            transform_button.Click += Execute_button_Click;
            dka_button.Click += Dka_button_Click;
            check_button_text_box.Click += Check_button_text_box_Click;
        }
        public void Init_transform_dictionary()
        {
            transform_dictionary = new Dictionary<string, string[]>();
            buf_transform_dictionary = new Dictionary<string, string>();
        }
        public void Init_DKA_dictionary()
        {
            result_dictionary = new Dictionary<string, int>();
            states_dictionary = new Dictionary<string, int>();
            symbols_dictionary = new Dictionary<string, int>();
        }
        private void Update_rules(bool is_changed, int is_text)
        {
            string key;
            DataGridViewRow row;

            if (is_text == 1)
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
                    main_grammar_data_grid_view.Rows.Remove(non_term_dictionary[is_start.ToString()]);
                    non_term_dictionary.Remove(is_start.ToString());

                    DataGridViewRow new_row = new DataGridViewRow();
                    new_row.CreateCells(main_grammar_data_grid_view, res, oldValue);
                    main_grammar_data_grid_view.Rows.Add(new_row);
                    non_term_dictionary[res] = new_row;
                }
                if (is_changed && is_changed_start == false)
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
                    main_grammar_data_grid_view.Rows.Remove(non_term_dictionary[key]);
                    non_term_dictionary.Remove(key);
                }
            }

            for (int i = 0; i < non_term_text_box.Text.Length; i++)
            {
                key = non_term_text_box.Text[i].ToString();
                if (!non_term_dictionary.ContainsKey(key))
                {
                    row = new DataGridViewRow();
                    row.CreateCells(main_grammar_data_grid_view, key, "");
                    main_grammar_data_grid_view.Rows.Add(row);
                    non_term_dictionary[key] = row;
                }
            }

            for (int i = 0; i < start_non_term_text_box.Text.Length; i++)
            {
                key = start_non_term_text_box.Text[i].ToString();
                if (!non_term_dictionary.ContainsKey(key))
                {
                    row = new DataGridViewRow();
                    row.CreateCells(main_grammar_data_grid_view, key, "");
                    main_grammar_data_grid_view.Rows.Add(row);
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
            transform_button.Enabled = term_text_box.Text.Length != 0 && start_non_term_text_box.Text.Length == 1;
        }
        private void Add_spaces(int count)
        {
            for (int j = 0; j < count; j++)
            {
                generic.log_buffer += " ";
            }
        }
        private void Get_grammar_description(Dictionary<string, string[]> dict)
        {
            string key;
            string rules = "";
            string non_terms = "";
            string alphabet = "";
            for(int i = 0; i < term_text_box.Text.Length; i++)
			{
                alphabet += term_text_box.Text[i].ToString();
			}
            for (int i = 0; i < dict.Keys.Count; i++)
            {
                key = dict.Keys.ElementAt(i);
                non_terms += key;
                rules += key + "->";
                foreach (string is_key in dict[key])
                {
                    rules += is_key + "|";   
                }
                rules = rules.Remove(rules.Length - 1);
                rules += "\n";
            }
            generic.log_buffer += "G=({" + alphabet + "},{" + non_terms + "},P," + start_non_term_text_box.Text[0].ToString() + "):\n";
            generic.log_buffer += rules + "\n";
        }
        private void Get_DKA_description(Dictionary<string, int> dict, string[][] trans)
        {
            string label;
            string states = "";
            string alphabet = "";
            string buf_final_states = final_states;
            string begin_state = regular_side == 0 ? start_non_term_text_box.Text[0].ToString() : begin_left_state;

            for (int i = 0; i < term_text_box.Text.Length; i++)
            {
                alphabet += term_text_box.Text[i].ToString();
            }
            for (int i = 0; i < dict.Keys.Count; i++)
            {
                states += dict.Keys.ElementAt(i) + ",";
            }

            if (states != "" && states[states.Length - 1].ToString() == ",")
            {
                states = states.Remove(states.Length - 1, 1);
            }
            if(buf_final_states != "" && buf_final_states[buf_final_states.Length - 1].ToString() == ",")
			{
                buf_final_states = buf_final_states.Remove(buf_final_states.Length - 1, 1);
            }

            for (int i = 0; i < term_text_box.Text.Length; i++)
            {
                Add_spaces(15);
                generic.log_buffer += term_text_box.Text[i].ToString();
            }
            generic.log_buffer += "\n";

            for (int i = 0; i < dict.Keys.Count; i++)
            {
                string key = dict.Keys.ElementAt(i);
                if(key != "")
				{
                    generic.log_buffer += key;
                    Add_spaces(15 - key.Length);
                    for (int j = 0; j < trans[i].Length; j++)
                    {
                        generic.log_buffer += trans[dict[key]][j];
                        Add_spaces(15 - trans[dict[key]][j].Length);
                    }
                    generic.log_buffer += "\n";
                }
            }
            generic.log_buffer += "M=({" + states + "},{" + alphabet + "},D," + start_non_term_text_box.Text[0].ToString() + "," + "{" + buf_final_states + "}):\n";

            dka_label.Text = "";
            label = "M = ({" + states + "},{" + alphabet + "},D," + begin_state + "," + "{" + buf_final_states + "})";
            if(label.Length > 80)
			{
                dka_label.Text = "Слишком длинное описание автомата. Смотрите лог";
            }
            else
			{
                dka_label.Text = label;
            }
        }
        private void Execute_button_Click(object sender, EventArgs e)
        {
            string key;
            bool is_term;
            bool is_grammar;
            bool is_correct = true;
            string fail_message = "";

            final_states = "";
            begin_left_state = "";

            generic.RemoveAllRules();

            generic.term = term_text_box.Text;
            generic.non_term = non_term_text_box.Text;
            generic.max_chain_length = (uint)max_numeric_up_down.Value;
            generic.min_chain_length = (uint)min_numeric_up_down.Value;
            generic.start_non_term = start_non_term_text_box.Text[0].ToString();

            regular_side = radioButton2.Checked == true ? 0 : 1;

            for (int i = 0; i < non_term_dictionary.Keys.Count; i++)
            {
                string value = "";
                key = non_term_dictionary.Keys.ElementAt(i);
                if (non_term_dictionary[key].Cells[1].Value != null)
                {
                    value = non_term_dictionary[key].Cells[1].Value.ToString();
                }
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
                        if ((value[er] != 'A' && value[er] != 'B'
                            && value[er] != 'C' && value[er] != 'D' && value[er] != 'E' && value[er] != 'F' && value[er] != 'G'
                            && value[er] != 'H' && value[er] != 'I' && value[er] != 'J' && value[er] != 'K' && value[er] != 'L'
                            && value[er] != 'M' && value[er] != 'N' && value[er] != 'O' && value[er] != 'P' && value[er] != 'Q'
                            && value[er] != 'R' && value[er] != 'S' && value[er] != 'T' && value[er] != 'U' && value[er] != 'V'
                            && value[er] != 'W' && value[er] != 'X' && value[er] != 'Y' && value[er] != 'Z' && value[er] != '|' && value[er] != '#'))
                        {
                            is_correct = false;
                            fail_message = "ОШИБКА!\nТЕРМИНАЛЬНОГО СИМВОЛА " + value[er] + " НЕТ В АЛФАВИТЕ";
                            break;
                        }
                    }
                }
                for (int j = 0; j < value.Length; j++)
                {
                    if (!non_term_dictionary.ContainsKey(value[j].ToString()) && (value[j] == 'A' || value[j] == 'B'
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

            generic.log_buffer += "Исходная грамматика:\n";
            Get_grammar_description(generic.rules_dictionary);
            is_grammar = Check_regular_grammar();

            if (is_correct == true && is_grammar == true)
            {
                generic.GenerateChains();
                Init_transform_dictionary();
                if(regular_side == 0)
				{
                    Transform_grammar();
                }
				else
				{
                    Transform_left_grammar();
                }
                dka_button.Enabled = true;
            }
            else
            {
                if(fail_message == "")
				{
                    fail_message = "Ошибка при проверке регулярной грамматики";
				}
                MessageBox.Show(fail_message);
            }
        }
        private void Check_dka(string checkable)
		{
            is_stop = false;
            bool is_final = false;

            string new_state;
            string current_state;

            dka_history_data_grid_view.Rows.Clear();
            List<string> dka_history = new List<string>();

            if (regular_side == 0)
			{
                current_state = start_non_term_text_box.Text.ToString();
            }
            else
			{
                current_state = begin_left_state;
            }

            generic.log_buffer += "Проверяем цепочку " + checkable + ":\n";
            generic.log_buffer += "Начальное состояние - " + current_state + "\n";
            dka_history.Add("(" + current_state + "," + checkable + ")");
            generic.log_buffer += "(" + current_state + "," + checkable + ")\n";
            for (int i = 0; i < checkable.Length; i++)
            {
                if (!symbols_dictionary.ContainsKey(checkable[i].ToString()))
                {
                    info_label.ForeColor = Color.Red;
                    info_label.Text = "В цепочке указано неизвестное значение: '" + checkable[i].ToString() + "'!";
                    generic.log_buffer += "В цепочке указано неизвестное значение: '" + checkable[i].ToString() + "'!\n\n";
                    is_stop = true;
                    break;
                }
                new_state = Transict_function(checkable[i].ToString(), current_state);
                if (is_stop == true)
                {
                    break;
                }
                dka_history.Add("(" + new_state + "," + checkable.Substring(i + 1) + ")");
                generic.log_buffer += "(" + new_state + "," + checkable.Substring(i + 1) + ")\n";
                current_state = new_state;
            }
            if(is_stop == false)
			{
                string[] final = final_states.Split(',');
                for (int i = 0; i < final.Length; i++)
                {
                    if (current_state == final[i].ToString())
                    {
                        is_final = true;
                        info_label.ForeColor = Color.Green;
                        info_label.Text = "Разпознавание прошло успешно!";
                        generic.log_buffer += "Разпознавание прошло успешно!\n\n";
                    }
                }
                if (is_final == false)
                {
                    info_label.ForeColor = Color.Red;
                    info_label.Text = "Автомат не перешел в конечное состояние!";
                    generic.log_buffer += "Автомат не перешел в конечное состояние!\n\n";
                }
            }
            foreach (string str in dka_history)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dka_history_data_grid_view, str);
                dka_history_data_grid_view.Rows.Add(row);
            }
        }
        private void Check_button_text_box_Click(object sender, EventArgs e)
		{
            Check_dka(check_text_box.Text.ToString());
            generic.Write_to_log();
        }
        private string Transict_function(string symbol, string current_state)
        {
            string new_state;
            if (need_to_transform == true)
			{
                new_state = new_transitions[states_dictionary[current_state]][symbols_dictionary[symbol]];
                generic.log_buffer += "Новое состояние - " + new_state + "\n";
            }
			else
			{
                new_state = transitions[states_dictionary[current_state]][symbols_dictionary[symbol]];
                generic.log_buffer += "Новое состояние - " + new_state + "\n";
            }
            if (new_state == "")
            {
                info_label.ForeColor = Color.Red;
                info_label.Text = "Нет состояния перехода для: '" + symbol + "' и исходного состояния: '" + current_state + "'!";
                generic.log_buffer += "Нет состояния перехода для: '" + symbol + "' и исходного состояния: '" + current_state + "'!\n\n";
                is_stop = true;
            }
            return new_state;
        }
        private void Build_left_side_grammar_to_DKA()
		{
            generic.log_buffer += "Добавляем целевой символ в конечное состояние\n";
            final_states += start_non_term_text_box.Text[0].ToString() + ",";
            for (int i = 0; i < transform_dictionary.Keys.Count; i++)
            {
                string key = transform_dictionary.Keys.ElementAt(i);
                foreach (string is_key in transform_dictionary[key])
                {
                    if (is_key.Length == 1 && !Check_non_term(is_key) && is_key != "")
                    {
                        //log_buffer += "Добавляем переход для дополнительного состояния #:\n";
                        begin_left_state += "#";
                        transitions[transform_dictionary.Keys.Count][symbols_dictionary[is_key[0].ToString()]] += key;
                    }
                    else if (is_key != "")
                    {
                        //log_buffer += "Добавляем переход для состояния " + is_key[0].ToString() + "\n";
                        transitions[states_dictionary[is_key[0].ToString()]][symbols_dictionary[is_key[1].ToString()]] += key;
                    }
                }
            }
            for (int i = 0; i < states_dictionary.Keys.Count; i++)
            {
                string key = states_dictionary.Keys.ElementAt(i);
                for (int j = 0; j < transitions[i].Length; j++)
                {
                    if (transitions[i][j] != "")
                    {
                        transitions[i][j] = Rearrange_in_order(transitions[i][j]);
                        generic.log_buffer += "Новый переход: " + key + "->" + transitions[i][j] + "(" + term_text_box.Text[j].ToString() + ")\n";
                    }
                    else
					{
                        generic.log_buffer += "Новый переход: " + key + "->" + transitions[i][j] + "(" + term_text_box.Text[j].ToString() + ")\n";
                    }
                }
            }
        }
        private void Build_right_side_grammar_to_DKA()
		{
            string key;
            string res = "";
            for (int i = 0; i < transform_dictionary.Keys.Count + 1; i++)
            {
                for (int j = 0; j < transitions[i].Length; j++)
                {
                    if (i != transform_dictionary.Keys.Count)
                    {
                        key = transform_dictionary.Keys.ElementAt(i);
                        foreach (string is_key in transform_dictionary[key])
                        {
                            if(is_key != "")
							{
                                if (is_key[0].ToString() == term_text_box.Text[j].ToString() && is_key.Length == 2)
                                {
                                    res += is_key[1].ToString();
                                }
                                else if (is_key[0].ToString() == term_text_box.Text[j].ToString() && is_key.Length == 1)
                                {
                                    res += "#";
                                    final_states += "#" + ",";
                                }
                            }
                        }
                        transitions[i][j] = res;
                        transitions[i][j] = Rearrange_in_order(transitions[i][j]);
                        generic.log_buffer += "Новый переход: " + key + "->" + transitions[i][j] + "(" + term_text_box.Text[j].ToString() + ")\n";
                        res = "";
                    }
                    else
                    {
                        transitions[i][j] = "";
                        generic.log_buffer += "Новый переход: " + "#" + "->" + transitions[i][j] + "(" + term_text_box.Text[j].ToString() + ")\n";
                    }
                }
            }
        }
        private void Except_non_entry_states(string[][] trans)
        {
            int current_count;
            int is_new_state = 1;
            Dictionary<string, int> buf_states_dictionary = new Dictionary<string, int>();
            for (int i = 0; i < states_dictionary.Keys.Count; i++)
            {
                string key = states_dictionary.Keys.ElementAt(i);
                buf_states_dictionary[key] = 0;
            }

            generic.log_buffer += "Начинаем смотреть с состояния ";
            if (regular_side == 1)
            {
                generic.log_buffer += begin_left_state + "\n";
                buf_states_dictionary[begin_left_state] = 1;
            }
            else
            {
                generic.log_buffer += start_non_term_text_box.Text[0].ToString() + "\n";
                buf_states_dictionary[start_non_term_text_box.Text[0].ToString()] = 1;
            }
            generic.Write_to_log();

            current_count = is_new_state;
            generic.log_buffer += "Осуществляем проход по состояниям:\n";
            for (; ; )
            {
                generic.log_buffer += "Следующая итерация:\n";
                for (int i = 0; i < states_dictionary.Keys.Count; i++)
                {
                    string key = states_dictionary.Keys.ElementAt(i);
                    if (buf_states_dictionary[key] == 0)
                    {
                        generic.log_buffer += "Состояние " + key + " пока не является достижимым\n";
                        continue;
                    }
                    for (int j = 0; j < term_text_box.Text.Length; j++)
                    {
                        is_new_state++;
                        buf_states_dictionary[trans[i][j]] = 1;
                        if (trans[i][j] != "")
                        {
                            generic.log_buffer += "Состояние " + trans[i][j] + " - достижимое\n";
                        }
                    }
                }
                generic.log_buffer += "Обнаружено " + is_new_state + " достижимых состояний\n";
                if (current_count == is_new_state)
                {
                    break;
                }
                current_count = is_new_state;
                is_new_state = 0;
                generic.Write_to_log();
            }

            generic.log_buffer += "Список достижимых состояний:\n";
            for (int i = 0; i < buf_states_dictionary.Keys.Count; i++)
            {
                string key = buf_states_dictionary.Keys.ElementAt(i);
                if (buf_states_dictionary[key] == 1)
                {
                    result_dictionary[key] = i;
                    generic.log_buffer += key + "\n";
                }
            }
            generic.Write_to_log();
        }
        private void Add_new_final_states(Dictionary<string, int> result_dictionary)
        {
            string[] buf = final_states.Split(',');
            for (int i = 0; i < result_dictionary.Keys.Count; i++)
            {
                string key = result_dictionary.Keys.ElementAt(i);
                if (key != "")
                {
                    for (int j = 0; j < buf.Length; j++)
                    {
                        for (int k = 0; k < key.Length; k++)
                        {
                            if (buf[j].ToString() == key[k].ToString())
                            {
                                final_states += key + ",";
                            }
                        }
                    }
                }
            }

            generic.log_buffer += "Список конечных состояний - " + final_states + "\n";
        }
        private void Dka_button_Click(object sender, EventArgs e)
        {
            need_to_transform = false;

            Init_DKA_dictionary();

            generic.log_buffer += "Построение ДКА:\n\nДобавляем исходные состояния, " +
                "соответствующие нетерминалам грамматики а также дополнительное состояние №:\n";
            for (int i = 0; i < transform_dictionary.Keys.Count; i++)
            {
                string key = transform_dictionary.Keys.ElementAt(i);
                generic.log_buffer += key + " " + i + "\n";
                states_dictionary[key] = i;
            }         
            states_dictionary["#"] = transform_dictionary.Keys.Count;
            generic.log_buffer += "#" + " " + transform_dictionary.Keys.Count + "\n";
            generic.Write_to_log();

            for (int i = 0; i < term_text_box.Text.Length; i++)
            {
                symbols_dictionary[term_text_box.Text[i].ToString()] = i;
            }

            transitions = new string[transform_dictionary.Keys.Count + 1][];
            for (int i = 0; i < transform_dictionary.Keys.Count + 1; i++)
            {
                transitions[i] = new string[term_text_box.Text.Length];
            }
            for (int i = 0; i < transform_dictionary.Keys.Count + 1; i++)
            {
                for(int j = 0; j < transitions[i].Length; j++)
				{
                    transitions[i][j] = "";
				}
            }

            if (regular_side == 1)
			{
                generic.log_buffer += "Создаем автомат по леволинейной грамматике:\n";
                Build_left_side_grammar_to_DKA();
            }
            else
			{
                generic.log_buffer += "Создаем автомат по праволинейной грамматике:\n";
                Build_right_side_grammar_to_DKA();
            }
            generic.log_buffer += "Построение первоначального автомата закончено\n";
            generic.Write_to_log();

            generic.log_buffer += "Первоначальный вид автомата:\n";
            Get_DKA_description(states_dictionary, transitions);
            generic.Write_to_log();

            for (int i = 0; i < transform_dictionary.Keys.Count + 1; i++)
            {
                if (need_to_transform == true)
                {
                    break;
                }
                for (int j = 0; j < transitions[i].Length; j++)
                {
                    if (need_to_transform == true)
                    {
                        break;
                    }
                    if (transitions[i][j].Length >= 2)
                    {
                        generic.log_buffer += "Автомат недетерминированный - нужно преобразовать\n";
                        generic.Write_to_log();
                        need_to_transform = true;
                        Transform_to_DKA();
                        break;
                    }
                }
            }

            if(!need_to_transform)
			{
                generic.log_buffer += "Автомат детерминированный, но...\n";
                if (regular_side == 1)
				{
                    generic.log_buffer += "Преобразуем множество начальных состояний для ДКА по леволинейной грамматике\n";
                    new_transitions = new string[10000][];
                    for (int i = 0; i < 10000; i++)
                    {
                        new_transitions[i] = new string[term_text_box.Text.Length];
                    }
                    for (int i = 0; i < transform_dictionary.Keys.Count + 1; i++)
                    {
                        for (int j = 0; j < transitions[i].Length; j++)
                        {
                            new_transitions[i][j] = transitions[i][j];
                        }
                    }
                    if (!Add_new_states())
					{
                        return;
					}
                    need_to_transform = true;

                    generic.log_buffer += "Исключаем недостижимые состояния в ДКА\n";
                    Except_non_entry_states(new_transitions);
                    generic.log_buffer += "Приводим в порядок список конечных состояний\n";
                    Add_new_final_states(result_dictionary);

                    Print_states_to_user(new_transitions);
                    generic.Write_to_log();
                }
                else
				{
                    generic.log_buffer += "Исключаем недостижимые состояния в новом ДКА\n";
                    Except_non_entry_states(transitions);
                    generic.log_buffer += "Приводим в порядок список конечных состояний\n";
                    Add_new_final_states(result_dictionary);

                    Print_states_to_user(transitions);
                    generic.Write_to_log();
                }

                generic.log_buffer += "Итоговый автомат:\n";
                if (need_to_transform == true)
                {
                    Get_DKA_description(states_dictionary, new_transitions);
                }
                else
                {
                    Get_DKA_description(states_dictionary, transitions);
                }
                Print_ask_window();
                generic.Write_to_log();
                check_button_text_box.Enabled = true; 
            }
        }
        private bool Transform_to_DKA()
        {
            new_transitions = new string[10000][];
            for (int i = 0; i < 10000; i++)
            {
                new_transitions[i] = new string[term_text_box.Text.Length];
            }
            for (int i = 0; i < transform_dictionary.Keys.Count + 1; i++)
            {
                for (int j = 0; j < transitions[i].Length; j++)
                {
                    new_transitions[i][j] = transitions[i][j];
                }
            }

            generic.log_buffer += "Добавляем новые состояния в автомат:\n";
            if (!Add_new_states())
            {
                return false;
            }
            generic.Write_to_log();

            generic.log_buffer += "Исключаем недостижимые состояния:\n";
            Except_non_entry_states(new_transitions);
            generic.Write_to_log();

            generic.log_buffer += "Приводим в порядок конечные состояния:\n";
            Add_new_final_states(result_dictionary);
            generic.Write_to_log();

            Print_states_to_user(new_transitions);

            generic.log_buffer += "\nПреобразованный автомат:\n";
            Get_DKA_description(result_dictionary, new_transitions);
            generic.Write_to_log();

            Print_ask_window();
            check_button_text_box.Enabled = true;

            return true;
        }
        private bool Add_new_states()
        {
            int counter = 1;
            string old_state;
            string result = "";
            generic.log_buffer += "Ищем новые состояния среди изначальных:\n";
            for (int i = 0; i < transform_dictionary.Keys.Count + 1; i++)
            {
                for (int j = 0; j < new_transitions[i].Length; j++)
                {
                    if (!states_dictionary.ContainsKey(new_transitions[i][j]) && new_transitions[i][j] != "")
                    {
                        old_state = new_transitions[i][j];
                        generic.log_buffer += "Новое состояние - " + old_state + "\n";
                        states_dictionary[old_state] = transform_dictionary.Keys.Count + counter;
                        generic.log_buffer += "Добавляем для нового состояния переходы:\n";
                        for (int h = 0; h < new_transitions[i].Length; h++)
                        {
                            for (int k = 0; k < old_state.Length; k++)
                            {
                                result += new_transitions[states_dictionary[old_state[k].ToString()]][h];
                            }
                            result = Rearrange_in_order(result);
                            new_transitions[states_dictionary[old_state]][h] = result;
                            generic.log_buffer += "Новый переход: " + old_state + "->" + new_transitions[states_dictionary[old_state]][h] + "(" + term_text_box.Text[j].ToString() + ")\n";
                            result = "";
                        }
                        counter++;
                        generic.Write_to_log();
                    }
                }
            }

            int is_new_state;
            int old_states_crt = counter - 1;
            bool is_entered_in_previous = false;
            generic.log_buffer += "Было добавлено " + old_states_crt + "новых состояний\n";
            int clue_number = states_dictionary.Keys.Count - old_states_crt;
            generic.Write_to_log();

            if (regular_side == 1)
            {
                generic.log_buffer += "Смотрим множество начальных состояний:\n";
                if (begin_left_state == "")
                {
                    dka_label.Text = "";
                    generic.log_buffer += "ОШИБКА! В исходной грамматике не обнаружено правил выхода из рекурсии!\n";
                    MessageBox.Show("Ошибка построения автомата");
                    dka_data_grid_view.Rows.Clear();
                    dka_data_grid_view.Columns.Clear();
                    check_button_text_box.Enabled = false;
                    generic.Write_to_log();
                    return false;
                }
                begin_left_state = Rearrange_in_order(begin_left_state);
                generic.log_buffer += "Начальное состояние для ДКА - " + begin_left_state + "\n";

                for(int i = 0; i < transform_dictionary.Keys.Count; i++)
				{
                    string key = transform_dictionary.Keys.ElementAt(i);
                    if (key == begin_left_state)
					{
                        is_entered_in_previous = true;
                        break;
                    }
				}

                if (is_entered_in_previous == false && begin_left_state != "#")
                {
                    generic.log_buffer += "Начальное состояние отсутствует в списке исходных\n" +
                        "Добавляем новое начальное состояние " + begin_left_state + "\n";
                    states_dictionary[begin_left_state] = transform_dictionary.Keys.Count + counter;
                    generic.log_buffer += "Добавляем для нового состояния переходы:\n";
                    for (int h = 0; h < term_text_box.Text.Length; h++)
                    {
                        for (int k = 0; k < begin_left_state.Length; k++)
                        {
                            result += new_transitions[states_dictionary[begin_left_state[k].ToString()]][h];
                        }
                        result = Rearrange_in_order(result);
                        new_transitions[states_dictionary[begin_left_state]][h] = result;
                        generic.log_buffer += "Новый переход: " + begin_left_state + "->" + new_transitions[states_dictionary[begin_left_state]][h] + "(" + term_text_box.Text[h].ToString() + ")\n";
                        result = "";
                    }
                    counter++;
                    is_new_state = old_states_crt + 1;
                }
                else
                {
                    is_new_state = old_states_crt;
                }
                generic.Write_to_log();
            }
            else
            {
                if (final_states == "")
                {
                    dka_label.Text = "";
                    generic.log_buffer += "ОШИБКА! В исходной грамматике не обнаружено правил выхода из рекурсии!\n";
                    MessageBox.Show("Ошибка построения автомата");
                    dka_data_grid_view.Rows.Clear();
                    dka_data_grid_view.Columns.Clear();
                    check_button_text_box.Enabled = false;
                    generic.Write_to_log();
                    return false;
                }
                is_new_state = old_states_crt;
            }

            generic.log_buffer += "Просматриваем новые состояния и ищем новые среди них:\n";
            while (is_new_state != 0)
            {
                generic.log_buffer += "Осталось посмотреть " + is_new_state + " состояний:\n";
                string key = states_dictionary.Keys.ElementAt(clue_number);
                for (int j = 0; j < term_text_box.Text.Length; j++)
                {
                    if (!states_dictionary.ContainsKey(new_transitions[states_dictionary[key]][j])
                        && new_transitions[states_dictionary[key]][j] != "")
                    {
                        is_new_state++;
                        old_state = new_transitions[states_dictionary[key]][j];
                        generic.log_buffer += "Новое состояние - " + old_state + "\n";
                        states_dictionary[old_state] = transform_dictionary.Keys.Count + counter;
                        generic.log_buffer += "Добавляем для нового состояния переходы:\n";
                        for (int h = 0; h < term_text_box.Text.Length; h++)
                        {
                            for (int k = 0; k < old_state.Length; k++)
                            {
                                result += new_transitions[states_dictionary[old_state[k].ToString()]][h];
                            }
                            result = Rearrange_in_order(result);
                            new_transitions[states_dictionary[old_state]][h] = result;
                            generic.log_buffer += "Новый переход: " + old_state + "->" + new_transitions[states_dictionary[old_state]][h] + "(" + term_text_box.Text[h].ToString() + ")\n";
                            result = "";
                        }
                        counter++;
                    }
                }
                clue_number++;
                is_new_state--;
                generic.Write_to_log();
            }
            generic.log_buffer += "Добавление состояний окончено\n";
            generic.Write_to_log();

            return true;
        }
        private string Rearrange_in_order(string string_to_order)
        {
            string_to_order = new string(string_to_order.ToCharArray().Distinct().ToArray());
            string_to_order = string.Concat(string_to_order.OrderBy(x => x).ToArray());
            if (string_to_order.Contains("#"))
            {
                string_to_order = string_to_order.Replace("#", "");
                string_to_order += "#";
            }
            if (string_to_order.Contains(start_non_term_text_box.Text[0].ToString()))
            {
                string_to_order = string_to_order.Replace(start_non_term_text_box.Text[0].ToString(), "");
                string_to_order = start_non_term_text_box.Text[0].ToString() + string_to_order;
            }

            return string_to_order;
        }
        private void Print_states_to_user(string [][]trans)
		{
            DataGridViewRow viewRow;

            dka_data_grid_view.Rows.Clear();
            dka_data_grid_view.Columns.Clear();

            for (int j = 0; j < term_text_box.Text.Length; j++)
            {
                dka_data_grid_view.Columns.Add(term_text_box.Text[j].ToString(), term_text_box.Text[j].ToString());
            }

            for (int i = 0; i < result_dictionary.Keys.Count; i++)
            {
                string key = result_dictionary.Keys.ElementAt(i);
                if (key != "")
                {
                    viewRow = new DataGridViewRow();
                    viewRow.HeaderCell.Value = key;
                    dka_data_grid_view.Rows.Add(viewRow);
                    for (int j = 0; j < trans[i].Length; j++)
                    {
                        dka_data_grid_view.Rows[i].Cells[j].Value = trans[result_dictionary[key]][j];
                    }
                }
            }

            final_states = final_states.Remove(final_states.Length - 1, 1);
            string[] final = final_states.Split(',');
            final = final.Distinct().ToArray();
            final_states = "";
            for (int i = 0; i < final.Length; i++)
            {
                if (result_dictionary.ContainsKey(final[i].ToString()))
                {
                    final_states += final[i].ToString() + ",";
                }
            };

            for (int i = 0; i < result_dictionary.Keys.Count; i++)
            {
                string key = result_dictionary.Keys.ElementAt(i);
                if (key != "")
                {
                    for (int j = 0; j < trans[i].Length; j++)
                    {
                        dka_data_grid_view.Rows[i].Cells[j].Value = trans[result_dictionary[key]][j];
                    }
                }
            }
        }
        private void Print_ask_window()
		{
            var result = MessageBox.Show("Хотите просмотреть распознавание сгенерированных цепочек?",
                 "Вопрос",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                generic.log_buffer += "\n\n\nПроверяем сгенерированные цепочки:\n";
                foreach (string chain in generic.chains_list)
                {
                    Check_dka(chain);
                    MessageBox.Show(chain);
                }
            }
            generic.Write_to_log();
        }
        private bool Check_non_term(string key)
		{
            if (key == start_non_term_text_box.Text[0].ToString())
			{
                return true;
			}
            for (int j = 0; j < non_term_text_box.Text.Length; j++)
            {
                if (key == non_term_text_box.Text[j].ToString())
                {
                    return true;
                }
            }
            return false;
        }
        private bool Check_regular_grammar()
        {
            string key;
            bool is_non_term;
            bool is_first_term;
            string type_grammar = regular_side == 0 ? "праволинейная" : "леволинейная";
            generic.log_buffer += "Проверка грамматики на корректность:\nТип грамматики - " + type_grammar + "\n";
            for (int i = 0; i < generic.rules_dictionary.Keys.Count; i++)
            {
                key = generic.rules_dictionary.Keys.ElementAt(i);
                generic.log_buffer += "Проверяем правила для нетерминала " + key + ":\n";
                foreach (string is_key in generic.rules_dictionary[key])
                {
                    generic.log_buffer += "Правило " + is_key + ":\n";
                    if (is_key.Length == 1)
					{
                        if(Check_non_term(is_key[0].ToString()))
						{
                            generic.log_buffer += "ОШИБКА! Правило состоит из одного нетерминала\n\n";
                            return false;
						}
                    }
                    if (is_key.Length >= 2)
                    {
                        generic.log_buffer += "Смотрим символ " + is_key[0].ToString() + ":\n";
                        if (Check_non_term(is_key[0].ToString()))
                        {
                            generic.log_buffer += "Символ " + is_key[0].ToString() + " - нетерминальный\n";
                            is_non_term = true;
                            is_first_term = false;
                            if (regular_side == 0)
                            {
                                generic.log_buffer += "ОШИБКА! Правило не имеет вид праволинейной грамматики\n\n";
                                return false;
                            }
                        }
                        else
						{
                            generic.log_buffer += "Символ " + is_key[0].ToString() + " - терминальный\n";
                            is_non_term = false;
                            is_first_term = true;
                        }
                        for (int j = 1; j < is_key.Length; j++)
                        {
                            string help = is_key[j].ToString();
                            generic.log_buffer += "Смотрим символ " + help + ":\n";
                            if (Check_non_term(help))
                            {
                                if(is_first_term == true && regular_side == 1)
                                {
                                    generic.log_buffer += "ОШИБКА! Правило не имеет вид леволинейной грамматики\n\n";
                                    return false;
                                }
                                if (is_non_term == true)
                                {
                                    generic.log_buffer += "ОШИБКА! В правиле содержится больше одного нетерминала\n\n";
                                    return false;
                                }
                                is_non_term = true;
                                generic.log_buffer += "Символ " + help + " - нетерминальный\n";
                            }
                            else
                            {
                                if (is_non_term == true && is_first_term == true)
                                {
                                    generic.log_buffer += "ОШИБКА! Правило имеет некорректный вид\n\n";
                                    return false;
                                }
                                generic.log_buffer += "Символ " + help + " - терминальный\n";
                            }
                        }
                    }
                    is_non_term = false;
                }
            }
            generic.log_buffer += "Проверка завершена успешно!\n\n";
            return true;
        }
        private string Add_new_key()
		{
            string key = "";
            for (int i = 0; i < 26; i++)
            {
                if (!generic.rules_dictionary.ContainsKey(non_terms[i].ToString())
                    && !buf_transform_dictionary.ContainsKey(non_terms[i].ToString()))
                {
                    key = non_terms[i].ToString();
                    break;
                }
            }
            return key;
        }
        private bool Transform_grammar()
		{
            int length;
            int new_nonterms;

            string key;
            string buf_rule;
            string result_rules = "";

            bool is_last_term = false;

            DataGridViewRow row;

            final_states = "";
            transform_dictionary.Clear();
            buf_transform_dictionary.Clear();
			fix_grammar_data_grid_view.Rows.Clear();
            generic.log_buffer += "Преобразование грамматики:\n";
            for (int i = 0; i < generic.rules_dictionary.Keys.Count; i++)
            {
                key = generic.rules_dictionary.Keys.ElementAt(i);
                generic.log_buffer += "Преобразуем правила нетерминала " + key + ":\n\n";
                foreach (string is_key in generic.rules_dictionary[key])
                {
                    generic.log_buffer += "Правило " + is_key + ":\n";
                    length = is_key.Length - 1;
                    if (is_key == "")
                    {
                        generic.log_buffer += "Правило не нуждается в преобразовании\n\n";
                        final_states += key + ",";
                        result_rules += "|";
                        continue;
                    }
                    if (is_key.Length == 2 && (Check_non_term(is_key[length].ToString())))
                    {
                        generic.log_buffer += "Правило не нуждается в преобразовании\n\n";
                        result_rules += is_key + "|";
                        continue;
                    }
                    if (is_key.Length >= 2)
					{
                        if (Check_non_term(is_key[length].ToString()))
						{
                            new_nonterms = length - 1;
                            is_last_term = true;
						}
                        else
						{
                            new_nonterms = length;
						}
                        if (!Check_non_term(is_key[length].ToString()) && is_key.Length == 2)
                        {
                            new_nonterms = 1;
                        }
                        generic.log_buffer += "Для преобразования нужно " + new_nonterms + " новых нетерминалов\n\n";

                        generic.log_buffer += "Преобразуем:\n";
                        string new_key = Add_new_key();
                        generic.log_buffer += "Добавляем новый нетерминал " + new_key + "\n";
                        result_rules += is_key[0].ToString() + new_key + "|";
                        generic.log_buffer += "Новое правило для нетерминала " + key + " - " + is_key[0].ToString() + new_key + "\n";
                        buf_rule = is_key.Remove(0, 1);
                        generic.log_buffer += "Остаток исходного правила - " + buf_rule + "\n\n";
                        generic.log_buffer += "Осталось добавить " + new_nonterms + " нетерминалов\n";
                        new_nonterms--;

                        while (new_nonterms >= 0)
                        {
							string buf_new_key;
							if (is_last_term == true)
							{
								while (buf_rule.Length > 2)
								{
									string new_rule = buf_rule[0].ToString() + new_key;
									buf_transform_dictionary[new_key] = new_rule;
                                    generic.log_buffer += "Новое правило:\n" + new_key + "->" + new_rule + "\n";
                                    buf_new_key = Add_new_key();
                                    generic.log_buffer += "Добавляем новый нетерминал " + buf_new_key + "\n";
                                    buf_transform_dictionary.Remove(new_key);
									new_rule = buf_rule[0].ToString() + buf_new_key;
									buf_transform_dictionary[new_key] = new_rule;
                                    generic.log_buffer += "Новое правило:\n" + new_key + "->" + new_rule + "\n\n";
                                    generic.log_buffer += "Осталось добавить " + new_nonterms + " нетерминалов\n";
                                    new_nonterms--;
                                    new_key = buf_new_key;
									buf_rule = buf_rule.Remove(0, 1);
                                    generic.log_buffer += "Остаток исходного правила - " + buf_rule + "\n";
                                }
								buf_transform_dictionary[new_key] = buf_rule;
                                generic.log_buffer += "Новое правило:\n" + new_key + "->" + buf_rule + "\n\n";
                                generic.log_buffer += "Осталось добавить " + new_nonterms + " нетерминалов\n";
                                new_nonterms--;
                            }
							else
							{
								while (buf_rule.Length > 1)
								{
									string new_rule = buf_rule[0].ToString() + new_key;
									buf_transform_dictionary[new_key] = new_rule;
                                    generic.log_buffer += "Новое правило:\n" + new_key + "->" + new_rule + "\n";
                                    buf_new_key = Add_new_key();
                                    generic.log_buffer += "Добавляем новый нетерминал " + buf_new_key + "\n";
                                    buf_transform_dictionary.Remove(new_key);
									new_rule = buf_rule[0].ToString() + buf_new_key;
									buf_transform_dictionary[new_key] = new_rule;
                                    generic.log_buffer += "Новое правило:\n" + new_key + "->" + new_rule + "\n\n";
                                    generic.log_buffer += "Осталось добавить " + new_nonterms + " нетерминалов\n";
                                    new_nonterms--;
                                    new_key = buf_new_key;
									buf_rule = buf_rule.Remove(0, 1);
                                    generic.log_buffer += "Остаток исходного правила - " + buf_rule + "\n";
                                }
								buf_transform_dictionary[new_key] = buf_rule;
                                generic.log_buffer += "Новое правило:\n" + new_key + "->" + buf_rule + "\n\n";
                                generic.log_buffer += "Осталось добавить " + new_nonterms + " нетерминалов\n";
                                new_nonterms--;
                            }
						}
                        is_last_term = false;
                    }
                    else
					{
                        result_rules += is_key + "|";
                    }
                }
                result_rules = result_rules.Remove(result_rules.Length - 1);
                generic.log_buffer += "Преобразованные правила: " + result_rules + "\n\n";

                row = new DataGridViewRow();
                row.CreateCells(fix_grammar_data_grid_view, key, result_rules);
                fix_grammar_data_grid_view.Rows.Add(row);

                transform_dictionary[key] = result_rules.Split('|');
                result_rules = "";
            }
            for (int i = 0; i < buf_transform_dictionary.Keys.Count; i++)
            {
                string buffer_key = buf_transform_dictionary.Keys.ElementAt(i);

                row = new DataGridViewRow();
                row.CreateCells(fix_grammar_data_grid_view, buffer_key, buf_transform_dictionary[buffer_key]);
                fix_grammar_data_grid_view.Rows.Add(row);

                transform_dictionary[buffer_key] = buf_transform_dictionary[buffer_key].Split('|'); ;
            }

            generic.log_buffer += "Преобразованная грамматика:\n";
            Get_grammar_description(transform_dictionary);
            generic.Write_to_log();

            return true;
        }
        private bool Transform_left_grammar()
        {
            int length;
            int new_nonterms;

            string key;
            string buf_rule;
            string result_rules = "";

            bool is_last_term = false;

            DataGridViewRow row;

            final_states = "";
            transform_dictionary.Clear();
            buf_transform_dictionary.Clear();
            fix_grammar_data_grid_view.Rows.Clear();
            generic.log_buffer += "Преобразование грамматики:\n";
            for (int i = 0; i < generic.rules_dictionary.Keys.Count; i++)
            {
                key = generic.rules_dictionary.Keys.ElementAt(i);
                generic.log_buffer += "Преобразуем правила нетерминала " + key + ":\n\n";
                foreach (string is_key in generic.rules_dictionary[key])
                {
                    generic.log_buffer += "Правило " + is_key + ":\n";
                    length = is_key.Length - 1;
                    if (is_key == "")
                    {
                        generic.log_buffer += "Правило не нуждается в преобразовании\n\n";
                        result_rules += "|";
                        begin_left_state += key;
                        continue;
                    }
                    if (is_key.Length == 2 && (Check_non_term(is_key[0].ToString())))
                    {
                        generic.log_buffer += "Правило не нуждается в преобразовании\n\n";
                        result_rules += is_key + "|";
                        continue;
                    }
                    if (is_key.Length >= 2)
                    {
                        if (Check_non_term(is_key[0].ToString()))
                        {
                            new_nonterms = length - 1;
                            is_last_term = true;
                        }
                        else
                        {
                            new_nonterms = length + 1;
                        }
                        if (!Check_non_term(is_key[0].ToString()) && is_key.Length == 2)
                        {
                            new_nonterms = 2;
                        }
                        generic.log_buffer += "Для преобразования нужно " + new_nonterms + " новых нетерминалов\n\n";

                        generic.log_buffer += "Преобразуем:\n";
                        string new_key = Add_new_key();
                        generic.log_buffer += "Добавляем новый нетерминал " + new_key + "\n";
                        result_rules += new_key + is_key[length].ToString() + "|";
                        generic.log_buffer += "Новое правило для нетерминала " + key + " - " + new_key + is_key[length].ToString() + "\n";
                        buf_rule = is_key.Remove(length, 1);
                        generic.log_buffer += "Остаток исходного правила - " + buf_rule + "\n\n";
                        generic.log_buffer += "Осталось добавить " + new_nonterms + " нетерминалов\n";
                        new_nonterms--;

                        while (new_nonterms >= 0)
                        {
                            string buf_new_key;
                            if (is_last_term == true)
                            {
                                while (buf_rule.Length > 2)
                                {
                                    string new_rule = new_key + buf_rule[buf_rule.Length - 1].ToString();
                                    buf_transform_dictionary[new_key] = new_rule;
                                    generic.log_buffer += "Новое правило:\n" + new_key + "->" + new_rule + "\n";
                                    buf_new_key = Add_new_key();
                                    generic.log_buffer += "Добавляем новый нетерминал " + buf_new_key + "\n";
                                    buf_transform_dictionary.Remove(new_key);
                                    new_rule = buf_new_key + buf_rule[buf_rule.Length - 1].ToString();
                                    buf_transform_dictionary[new_key] = new_rule;
                                    generic.log_buffer += "Новое правило:\n" + new_key + "->" + new_rule + "\n\n";
                                    generic.log_buffer += "Осталось добавить " + new_nonterms + " нетерминалов\n";
                                    new_nonterms--;
                                    new_key = buf_new_key;
                                    buf_rule = buf_rule.Remove(buf_rule.Length - 1, 1);
                                    generic.log_buffer += "Остаток исходного правила - " + buf_rule + "\n";
                                }
                                buf_transform_dictionary[new_key] = buf_rule;
                                generic.log_buffer += "Новое правило:\n" + new_key + "->" + buf_rule + "\n\n";
                                generic.log_buffer += "Осталось добавить " + new_nonterms + " нетерминалов\n";
                                new_nonterms--;
                            }
                            else
                            {
                                while (buf_rule.Length > 1)
                                {
                                    string new_rule = new_key + buf_rule[buf_rule.Length - 1].ToString();
                                    buf_transform_dictionary[new_key] = new_rule;
                                    generic.log_buffer += "Новое правило:\n" + new_key + "->" + new_rule + "\n";
                                    buf_new_key = Add_new_key();
                                    generic.log_buffer += "Добавляем новый нетерминал " + buf_new_key + "\n";
                                    buf_transform_dictionary.Remove(new_key);
                                    new_rule = buf_new_key + buf_rule[buf_rule.Length - 1].ToString();
                                    buf_transform_dictionary[new_key] = new_rule;
                                    generic.log_buffer += "Новое правило:\n" + new_key + "->" + new_rule + "\n\n";
                                    generic.log_buffer += "Осталось добавить " + new_nonterms + " нетерминалов\n";
                                    new_nonterms--;
                                    new_key = buf_new_key;
                                    buf_rule = buf_rule.Remove(buf_rule.Length - 1, 1);
                                    generic.log_buffer += "Остаток исходного правила - " + buf_rule + "\n";
                                }
                                buf_transform_dictionary[new_key] = buf_rule;
                                generic.log_buffer += "Новое правило:\n" + new_key + "->" + buf_rule + "\n\n";
                                generic.log_buffer += "Осталось добавить " + new_nonterms + " нетерминалов\n";
                                new_nonterms--;
                            }
                        }
                        is_last_term = false;
                    }
                    else
                    {
                        result_rules += is_key + "|";
                    }
                }
                result_rules = result_rules.Remove(result_rules.Length - 1);
                generic.log_buffer += "Преобразованные правила: " + result_rules + "\n\n";

                row = new DataGridViewRow();
                row.CreateCells(fix_grammar_data_grid_view, key, result_rules);
                fix_grammar_data_grid_view.Rows.Add(row);

                transform_dictionary[key] = result_rules.Split('|');
                result_rules = "";
            }
            for (int i = 0; i < buf_transform_dictionary.Keys.Count; i++)
            {
                string buffer_key = buf_transform_dictionary.Keys.ElementAt(i);

                row = new DataGridViewRow();
                row.CreateCells(fix_grammar_data_grid_view, buffer_key, buf_transform_dictionary[buffer_key]);
                fix_grammar_data_grid_view.Rows.Add(row);

                transform_dictionary[buffer_key] = buf_transform_dictionary[buffer_key].Split('|'); ;
            }

            generic.log_buffer += "Преобразованная грамматика:\n";
            Get_grammar_description(transform_dictionary);
            generic.Write_to_log();

            return true;
        }
        private void Rules_grid_view_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress += CellKeyPressed;
            e.Control.Leave += Control_Leave;
            ((TextBox)e.Control).ShortcutsEnabled = false;
            dka_button.Enabled = false;
            check_button_text_box.Enabled = false;
        }
        private void Control_Leave(object sender, EventArgs e)
        {
            ((Control)sender).KeyPress -= CellKeyPressed;
            dka_button.Enabled = false;
            check_button_text_box.Enabled = false;
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
                    if (!non_term_dictionary.ContainsKey(res) && is_first_launch == false)
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
            dka_button.Enabled = false;
            check_button_text_box.Enabled = false;
        }
        private void Non_term_text_box_TextChanged(object sender, EventArgs e)
        {
            bool buf_changed = is_changed_start;
            string new_text_box = non_term_text_box.Text;
            if (new_text_box.Length == old_text_box.Length && is_changed_start == false)
            {
                for (int i = 0; i < old_text_box.Length; i++)
                {
                    if (old_text_box[i] != new_text_box[i])
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
            dka_button.Enabled = false;
            check_button_text_box.Enabled = false;
        }
        private void Start_non_term_text_box_KeyPressed(object sender, KeyPressEventArgs e)
        {
            start_non_term_text_box.SelectionLength = 0;
            if ((start_non_term_text_box.Text.IndexOf(e.KeyChar) != -1 || e.KeyChar < 'A' || e.KeyChar > 'Z') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
            dka_button.Enabled = false;
            check_button_text_box.Enabled = false;
        }
        private void Non_term_text_box_KeyPressed(object sender, KeyPressEventArgs e)
        {
            non_term_text_box.SelectionLength = 0;
            old_text_box = non_term_text_box.Text;
            if ((start_non_term_text_box.Text.IndexOf(e.KeyChar) != -1 || non_term_text_box.Text.IndexOf(e.KeyChar) != -1 || e.KeyChar < 'A' || e.KeyChar > 'Z') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
            dka_button.Enabled = false;
            check_button_text_box.Enabled = false;
        }
        private void Term_text_box_KeyPressed(object sender, KeyPressEventArgs e)
        {
            term_text_box.SelectionLength = 0;
            if (term_text_box.Text.IndexOf(e.KeyChar) != -1 || (e.KeyChar >= 'A' && e.KeyChar <= 'Z') || e.KeyChar == '|')
            {
                e.Handled = true;
            }
            dka_button.Enabled = false;
            check_button_text_box.Enabled = false;
        }
        private void CellKeyPressed(object sender, KeyPressEventArgs e)
        {
            if (start_non_term_text_box.Text.IndexOf(e.KeyChar) == -1 && non_term_text_box.Text.IndexOf(e.KeyChar) == -1 && term_text_box.Text.IndexOf(e.KeyChar) == -1 && e.KeyChar != '|' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
            dka_button.Enabled = false;
            check_button_text_box.Enabled = false;
        }
        private void MaxChainResultLenSpinner_ValueChanged(object sender, EventArgs e)
        {
            min_numeric_up_down.Maximum = max_numeric_up_down.Value;
            dka_button.Enabled = false;
            check_button_text_box.Enabled = false;
        }
		private void Button1_Click(object sender, EventArgs e)
		{
            MessageBox.Show("Мартасов Илья, группа ИП-711\nВариант 13\nНаписать программу, которая по заданной регулярной грамматике(грамматика может быть НЕ автоматного вида!, ЛЛ или ПЛ) построит эквивалентный ДКА(представление функции переходов в виде таблицы).Программа должна сгенерировать по исходной грамматике несколько цепочек в заданном диапазоне длин и проверить их допустимость построенным автоматом. Процессы построения цепочек и проверки их выводимости отображать на экране(по требованию). Предусмотреть возможность проверки цепочки, введённой пользователем.");
		}
		private void Button3_Click(object sender, EventArgs e)
		{
            generic.log_buffer = "";
            File.Delete(@"C:\CursWorkLogFile\log.txt");
            MessageBox.Show("Лог-файл удален");
		}
		private void Button4_Click(object sender, EventArgs e)
		{
            int counter = 4;
            string path = "";
            int line_counter = 0;

            char ch;
            string key;
            string values;
            string states = "";
            bool is_fail = false;
            string alphabet = "";
            string start_non_term;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    path = openFileDialog.FileName;
                    if(path == @"C:\CursWorkLogFile\log.txt")
                    {
                        generic.log_buffer += "Нельзя загружать грамматику из лога\n";
                        generic.Write_to_log();
                        return;
					}
                }
				else
				{
                    return;
				}
            }

            generic.log_buffer += "Загрузка грамматики из файла:\n";
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    switch (line_counter)
					{
                        case 0:
                            if (!line.Contains("G=({") || !line.Contains("},{") || !line.Contains("},P,"))
                            {
                                generic.log_buffer += "НЕКОРРЕКТНОЕ НАЧАЛО!\n";
                                generic.Write_to_log();
                                is_fail = true;
                                return;
                            }
                            do
							{
                                ch = line[counter];
                                if (ch == ' ')
								{
                                    continue;
								}
                                alphabet += ch;
                                counter++;
                            } while (ch != '}');

                            counter += 2;
                            do
                            {
                                ch = line[counter];
                                if (ch == ' ')
                                {
                                    continue;
                                }
                                states += ch;
                                counter++;
                            } while (ch != '}');

                            counter += 3;
                            start_non_term = line[counter].ToString();
                            states = states.Remove(states.Length - 1, 1);
                            alphabet = alphabet.Remove(alphabet.Length - 1, 1);
                            for(int i = 0; i < states.Length; i++)
							{
                                if(states[i].ToString() == start_non_term)
								{
                                    continue;
								}
							}
                            for(int i = 0; i < states.Length; i++)
							{
                                for(int j = 0; j < alphabet.Length; j++)
								{
                                    if(states[i] == alphabet[j])
									{
                                        generic.log_buffer += "СОВПАДЕНИЕ!\n";
                                        generic.Write_to_log();
                                        is_fail = true;
                                        return;
                                    }
								}
							}
                            term_text_box.Text = alphabet;
                            start_non_term_text_box.Text = start_non_term;
                            non_term_text_box.Text = states.Replace(start_non_term, "");
                            break;
                        default:
                            for (int i = 0; i < states.Length; i++)
                            {
                                if (states[i].ToString() == line[0].ToString())
                                {
                                    break;
                                }
                            }
                            values = line.Substring(3, line.Length - 3);
                            key = line[0].ToString();
                            Set_grammar_from_file(key, values);
                            break;
					}
                    counter = 0;
                    line_counter++;
                }
            }
            if(!is_fail)
			{
                generic.log_buffer += "Грамматика успешно загружена\n";
                generic.Write_to_log();
                MessageBox.Show("Грамматика успешно загружена");
            }
        }
        private void Set_grammar_from_file(string key, string value)
        {
            non_term_dictionary[key].Cells[1].Value = value;

            Update_rules(false, 0);
            Is_button_valid();
        }
    }
}
