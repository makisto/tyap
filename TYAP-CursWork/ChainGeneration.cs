using System.Linq;
using System.Collections.Generic;
using System.IO;
using System;
using System.Windows.Forms;

namespace TYAP_CursWork
{
    class ChainsGeneration
    {
        public Form1 former = null;
        public string log_buffer = "";
        public List<string> chains_list;
        public Dictionary<string, string[]> rules_dictionary;

        public uint max_chain_length = 0;
        public uint min_chain_length = 0;

        public string term = "";
        public string non_term = "";
        public string start_non_term = "";

        public const string EMPTY_STR = "λ";
        private const int MAX_RECURSION = 10000;
        public void Init()
        {
            chains_list = new List<string>();
            rules_dictionary = new Dictionary<string, string[]>();
        }
        public void AddRuleDescription(string key, string description)
        {
            rules_dictionary[key] = description.Split('|');
        }
        public void RemoveAllRules()
        {
            chains_list.Clear();
            rules_dictionary.Clear();
        }
        public void GenerateChains()
        {
            string res;
            int recursion = 0;
            string chain_prefix;
            List<string> non_term_chains = new List<string>();

            log_buffer += "Генерируем цепочки:\n";

            log_buffer += "Смотрим правила для целевого символа:\n";
            foreach (string rule_right_part in rules_dictionary[start_non_term])
            {
                if (0 == Is_valid(rule_right_part) && rule_right_part.Length >= min_chain_length)
                {
                    log_buffer += "Сгенерирована цепочка " + rule_right_part + "\n";
                    chains_list.Add(rule_right_part == "" ? "" : rule_right_part);
                }
                else if (-2 == Is_valid(rule_right_part))
                {
                    log_buffer += "Цепочка с нетериналами " + rule_right_part + " добалвена \n";
                    non_term_chains.Add(rule_right_part);
                }
            }

            if (non_term_chains.Count != 0)
            {
                while (recursion <= MAX_RECURSION)
                {
                    recursion++;
                    List<string> sub_non_term_chains = new List<string>();
                    foreach (string non_term_chain in non_term_chains)
                    {
                        log_buffer += "Смотрим цепочку " + non_term_chain + ":\n";
                        chain_prefix = "";
                        for (int i = 0; i < non_term_chain.Length; i++)
                        {
                            if (!rules_dictionary.ContainsKey(non_term_chain[i].ToString()))
                            {
                                chain_prefix += non_term_chain[i];
                            }
                            else
                            {
                                log_buffer += "Смотрим правила для нетерминала " + non_term_chain[i].ToString() + ":\n";
                                foreach (string rule_right_part in rules_dictionary[non_term_chain[i].ToString()])
                                {
                                    log_buffer += "Правило " + rule_right_part + ":\n";
                                    res = chain_prefix + rule_right_part + non_term_chain.Substring(i + 1);
                                    log_buffer += "Получившаяся цепочка - " + res + ":\n";
                                    if (0 == Is_valid(res))
                                    {
                                        if (chains_list.Contains(res == "" ? "" : res) || res.Length < min_chain_length)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            chains_list.Add(res == "" ? "" : res);
                                            log_buffer += "Сгенерирована цепочка " + res + "\n";
                                            if (chains_list.Count >= 5)
                                            {
                                                break;
                                            }
                                        }
                                    }
                                    else if (-2 == Is_valid(res))
                                    {
                                        log_buffer += "Заносим " + res + " в список несгенерированных:\n";
                                        sub_non_term_chains.Add(res);
                                    }
                                    if (chains_list.Count >= 5)
                                    {
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        if (chains_list.Count >= 5)
                        {
                            break;
                        }
                    }
                    if (chains_list.Count >= 5)
                    {
                        break;
                    }
                    non_term_chains.Clear();
                    non_term_chains = sub_non_term_chains.Distinct().ToList();
                }
                log_buffer += "Процесс генерации закончен. Список сгенерированных цепочек:\n";
                foreach (string chain in chains_list)
                {
                    log_buffer += chain + "\n";
                }
            }
			else
			{
                log_buffer += "Процесс генерации закончен. Список сгенерированных цепочек:\n";
                foreach (string chain in chains_list)
				{
                    log_buffer += chain + "\n";
                }
			}
            Write_to_log();
        }
        private int Is_valid(string line)
        {
            int term_sym = 0;
            int non_term_sym = 0;
            foreach (char ch in line)
            {
                if (!rules_dictionary.ContainsKey(ch.ToString()))
                {
                    term_sym++;
                }
                else
                {
                    non_term_sym++;
                }
            }

            if (term_sym > max_chain_length)
            {
                return -1;
            }
            if ((term_sym + non_term_sym - 5) > max_chain_length)
            {
                return -1;
            }
            return (non_term_sym > 0) ? -2 : 0;
        }
        public void Write_to_log()
        {
            string dir_path = @"C:\CursWorkLogFile";
            string path = @"C:\CursWorkLogFile\log.txt";
            try
            {
                if (!Directory.Exists(dir_path))
                {
                    DirectoryInfo di = Directory.CreateDirectory(dir_path);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("The process failed: {0}");
            }
            finally { }
            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(log_buffer);
            }
        }
    }
}
