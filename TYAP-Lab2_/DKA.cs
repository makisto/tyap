using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TYAP_Lab2_
{
	class DKA
	{
        public readonly List<string> dka_history;
        private readonly Dictionary<string, int> states_dictionary;
        private readonly Dictionary<string, int> symbols_dictionary;

        public char[] symbols;
        public string[] states;
        public string[] final_states;

        public string keyword = "";
        public string begin_state = "";
        public string[][] transitions;
        public DKA()
        {
            dka_history = new List<string>();
            states_dictionary = new Dictionary<string, int>();
            symbols_dictionary = new Dictionary<string, int>();
        }
        public void Exec()
        {
            dka_history.Clear();
            states_dictionary.Clear();
            symbols_dictionary.Clear();
            Check_entrance();
            Check_keyword();
        }
        private void Check_entrance()
        {
            bool is_begin_state = false;

            for (int i = 0; i < states.Length; i++)
            {
                if (begin_state.Equals(states[i]))
                {
                    is_begin_state = true;
                }
                if (states_dictionary.ContainsKey(states[i]))
                {
                    throw new Exception("Во множестве состояний обнаружен дублирующий элемент '" + states[i] + "'");
                }
                states_dictionary[states[i]] = i;
            }
            if (!is_begin_state)
            {
                throw new Exception("Начальное состояние не инициализировано");
            }

            for (int i = 0; i < symbols.Length; i++)
            {
                if (symbols_dictionary.ContainsKey(symbols[i].ToString()))
                {
                    throw new Exception("В алфавите обнаружен дублирующий элемент '" + symbols[i] + "'");
                }
                symbols_dictionary[symbols[i].ToString()] = i;
            }

            for (int i = 0; i < transitions.Length; i++)
            {
                for (int j = 0; j < transitions[i].Length; j++)
                {
                    if (transitions[i][j] != "" && !states_dictionary.ContainsKey(transitions[i][j]))
                    {
                        throw new Exception("В таблице переходов для символа: '" + symbols[j] + "' и состояния: '" + states[i] + "' указано неизвестное значение состояния: '" + transitions[i][j] + "'!");
                    }
                }
            }

            for (int i = 0; i < final_states.Length; i++)
            {
                if (!states_dictionary.ContainsKey(final_states[i]))
                {
                    throw new Exception("Неизвестное конечное состояние '" + final_states[i] + "'");
                }
            }
        }
        private void Check_keyword()
        {
            string logs = "";
            string new_state;
            string current_state = begin_state;
            for (int i = 0; i < keyword.Length; i++)
            {
                if (!symbols_dictionary.ContainsKey(keyword[i].ToString()))
                {
                    throw new Exception("В цепочке указано неизвестное значение: '" + keyword[i] + "'!");
                }
                new_state = Transict_function(keyword[i].ToString(), current_state);
                logs += keyword[i];
                dka_history.Add(current_state + "->" + new_state + ":" + logs);
                current_state = new_state;
            }
            if (!final_states.Contains(current_state))
			{
                throw new Exception("Автомат не перешел в конечное состояние!");
            }
        }
        private string Transict_function(string symbol, string current_state)
        {
            string new_state = transitions[states_dictionary[current_state]][symbols_dictionary[symbol]];
            if (new_state == "")
			{
                throw new Exception("Нет состояния перехода для: '" + symbol + "' и исходного состояния: '" + current_state + "'!");
            }              
            return new_state;
        }
    }
}
