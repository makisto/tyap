using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TYAP_Lab3
{
    class Transition
    {
        public string q0 = "";
        public string w0 = "";
        public string z0 = "";
        public string q1 = "";
        public string z1 = "";
    }
    class DMPA
    {
        public string states;
        public string symbols;
        public string keyword;
        public string final_states;
        public string stack_symbols;

        public List<string> list_of_tacts;
        public Dictionary<string, Transition> transitions_dictionary;
        private readonly Dictionary<string, int> states_dictionary;
        private readonly Dictionary<string, int> symbols_dictionary;
        private readonly Dictionary<string, int> final_states_dictionary;
        private readonly Dictionary<string, int> stack_symbols_dictionary;
        public DMPA()
        {
            list_of_tacts = new List<string>();
            states_dictionary = new Dictionary<string, int>();
            symbols_dictionary = new Dictionary<string, int>();
            final_states_dictionary = new Dictionary<string, int>();
            stack_symbols_dictionary = new Dictionary<string, int>();
            transitions_dictionary = new Dictionary<string, Transition>();
        }
        public void Clear()
        {
            list_of_tacts.Clear();
            states_dictionary.Clear();
            symbols_dictionary.Clear();
            transitions_dictionary.Clear();
            final_states_dictionary.Clear();
            stack_symbols_dictionary.Clear();
        }
        public void Add_transition_to_transition_dictionary(string q0, string w0, string z0, string q1, string z1)
        {
            if (q0 != "" && q1 != "")
            {
                Transition t = new Transition
                {
                    q0 = q0,
                    w0 = w0,
                    z0 = z0,
                    q1 = q1,
                    z1 = z1
                };
                transitions_dictionary[Get_transition_secret_key(q0, w0, z0)] = t;
            }
            if (q0.Length > 1 || q1.Length > 1)
            {
                throw new Exception("Записано неизвестное состояние!");
            }
            if (z0.Length > 1)
            {
                throw new Exception("Можно считать только верхний символ стека!");
            }
            if (w0.Length > 1)
            {
                throw new Exception("Можно считать только один символ из цепочки!");
            }
            if (q0.Length != 0 && q0 == q1 && z0 == z1 && w0.Length == 0)
            {
                throw new Exception("Некорректное правило");
            }
            if (keyword == "")
			{
                if (transitions_dictionary.ContainsKey(Get_transition_secret_key(q0, "", z0)) && z0 == stack_symbols[0].ToString() && q0 == states[0].ToString())
                {
                    list_of_tacts.Add("(" + q0 + ", " + " " + ", " + z0 + ") |-");
                    list_of_tacts.Add("(" + q0 + ", " + " " + ", " + " " + ")");
                    throw new Exception("Цепочка принадлежит заданному языку");
                }
            }
        }
        private string Get_transition_secret_key(string q0, string w0, string z0)
        {
            return String.Concat(q0, w0, z0);
        }
        public void Exec()
        {
            Check_DMPA();
            Check_keyword();
        }
        private void Check_DMPA()
        {
            int error_row = 0;
            string wrong_symbol = "";

            for (int i = 0; i < states.Length; i++)
            {
                if (states_dictionary.ContainsKey(states[i].ToString()))
                {
                    throw new Exception("Во множестве состояний обнаружен дублирующий элемент '" + states[i] + "'");
                }
                states_dictionary[states[i].ToString()] = i;
            }
            for (int i = 0; i < symbols.Length; i++)
            {
                if (symbols_dictionary.ContainsKey(symbols[i].ToString()))
                {
                    throw new Exception("В алфавите обнаружен дублирующий элемент '" + symbols[i] + "'");
                }
                symbols_dictionary[symbols[i].ToString()] = i;
            }
            for (int i = 0; i < stack_symbols.Length; i++)
            {
                if (stack_symbols_dictionary.ContainsKey(stack_symbols[i].ToString()))
                {
                    throw new Exception("В алфавите магазина обнаружен дублирующий элемент '" + stack_symbols[i] + "'");
                }
                stack_symbols_dictionary[stack_symbols[i].ToString()] = i;
            }
            for (int i = 0; i < final_states.Length; i++)
            {
                if (final_states_dictionary.ContainsKey(final_states[i].ToString()))
                {
                    throw new Exception("В множестве конечных состояний обнаружен дублирующий элемент '" + final_states[i] + "'");
                }
                final_states_dictionary[final_states[i].ToString()] = i;
            }
            //поиск неизвестных символов во множестве конечных состояний
            foreach (char ch in final_states)
            {
                if (states.IndexOf(ch) == -1)
                {
                    throw new Exception("Во множестве конечных состояний обнаружен неизвестный символ: '" + ch + "' !");
                }
            }
            //поиск неизвестных символов в таблице переходов
            foreach (Transition t in transitions_dictionary.Values)
            {
                error_row++;
                if (states.IndexOf(t.q0) == -1)
                {
                    wrong_symbol = t.q0;
                }
                else if (states.IndexOf(t.q1) == -1)
                {
                    wrong_symbol = t.q1;
                }
                else if (symbols.IndexOf(t.w0) == -1)
                {
                    wrong_symbol = t.w0;
                }
                else if (symbols.IndexOf(t.z0) == -1 && stack_symbols.IndexOf(t.z0) == -1)
                {
                    wrong_symbol = t.z0;
                }
                else
                {
                    foreach (char ch in t.z1)
                    {
                        if (symbols.IndexOf(ch.ToString()) == -1 && stack_symbols.IndexOf(ch.ToString()) == -1)
                        {
                            wrong_symbol = ch.ToString();
                            break;
                        }
                    }
                }
                if (wrong_symbol != "")
                {
                    throw new Exception("В таблице переходов на строчке " + error_row + " обнаружен неизвестный символ: '" + wrong_symbol + "' !");
                }
            }
        }
        private void Check_keyword()
        {
            Stack<string> magazine = new Stack<string>();
            string q0 = states[0].ToString();
            string w0 = keyword[0].ToString();
            string z0 = stack_symbols[0].ToString();
            if (z0 != "")
            {
                magazine.Push(z0);
            }
            Transition current_transition;
            int counter = 0;
            int current_symbol = 0;
            do
            {
                list_of_tacts.Add("(" + q0 + ", " + WordToString(keyword, current_symbol) + ", " + MagazineToString(magazine) + ") |-");
                z0 = magazine.Pop();
                if (symbols.IndexOf(w0) == -1)
                {
                    throw new Exception("В проверяемой цепочке обнаружен неизвестный символ: '" + w0 + "' !");
                }
                if (transitions_dictionary.ContainsKey(Get_transition_secret_key(q0, w0, z0)))
                {
                    current_transition = transitions_dictionary[Get_transition_secret_key(q0, w0, z0)];
                    w0 = ++current_symbol >= keyword.Length ? "" : keyword[current_symbol].ToString();
                    q0 = current_transition.q1;
                    PushToStack(magazine, current_transition.z1);
                }
                else if (transitions_dictionary.ContainsKey(Get_transition_secret_key(q0, "", z0)))
                {
                    current_transition = transitions_dictionary[Get_transition_secret_key(q0, "", z0)];
                    q0 = current_transition.q1;
                    PushToStack(magazine, current_transition.z1);
                }
                else
                {
                    throw new Exception("Отсутствует переход для q0 = '" + q0 + "', w0 = '" + w0 + "', z0 = '" + z0 + "'.");
                }
                counter++;
                if (counter > 1000)
				{
                    break;
				}
            }
            while (!IsFinal(q0, w0, z0, magazine));

            if (current_symbol < keyword.Length && magazine.Count == 0)
            {
                list_of_tacts.Add("(" + q0 + ", " + WordToString(keyword, current_symbol) + ", " + MagazineToString(magazine) + ")");
                throw new Exception("Цепочка не прочитана до конца");
            }
            if ((counter > 1000) || (final_states.IndexOf(q0) == -1 && magazine.Count == 0))
            {
                throw new Exception("Автомат не перешел в одно из конечных состояний");
            }

            counter = 0;
            if (magazine.Count != 0)
            {
                do
                {
                    list_of_tacts.Add("(" + q0 + ", " + WordToString(keyword, current_symbol) + ", " + MagazineToString(magazine) + ") |-");
                    z0 = magazine.Pop();

                    if (transitions_dictionary.ContainsKey(Get_transition_secret_key(q0, "", z0)))
                    {
                        current_transition = transitions_dictionary[Get_transition_secret_key(q0, "", z0)];
                        q0 = current_transition.q1;
                        PushToStack(magazine, current_transition.z1);
                    }
                    else
                    {
                        throw new Exception("Стек не опустошен. Отсутствует переход для q0 = '" + q0 + "', w0 = '" + w0 + "', z0 = '" + z0 + "'.");
                    }
                    counter++;
                    if (magazine.Count == 0)
                    {
                        break;
                    }
                } while (counter < 1000);
            }

            list_of_tacts.Add("(" + q0 + ", " + WordToString(keyword, current_symbol) + ", " + MagazineToString(magazine) + ")");

            if ((counter > 1000) || (final_states.IndexOf(q0) == -1 && magazine.Count == 0))
            {
                throw new Exception("Автомат не перешел в одно из конечных состояний");
            }
        }
        private string MagazineToString(Stack<string> stack)
        {
            string res = "";
            foreach (string sym in stack)
            {
                res += sym;
            }
            return res == "" ? "" : res;
        }
        private string WordToString(string word, int startInd)
        {
            if (startInd >= word.Length)
            {
                return "";
            }
            else
            {
                return word.Substring(startInd);
            }
        }
        private void PushToStack(Stack<string> stack, string str)
        {
            foreach (char ch in str.Reverse())
            {
                if (ch.ToString() != "")
                {
                    stack.Push(ch.ToString());
                }
            }
        }

        private bool IsFinal(string q0, string w0, string z0, Stack<string> stack)
        {
            return stack.Count == 0 || (final_states.IndexOf(q0) != -1 && w0 == "" && stack_symbols.IndexOf(z0) != -1);
        }
    }
}
