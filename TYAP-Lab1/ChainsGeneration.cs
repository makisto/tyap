using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TYAP_Lab1
{
    class ChainsGeneration
    {
        public List<string> chains_list;
        private Dictionary<string, string[]> rules_dictionary;

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

            foreach (string rule_right_part in rules_dictionary[start_non_term])
			{
                if (0 == Is_valid(rule_right_part) && rule_right_part.Length >= min_chain_length)
				{
                    chains_list.Add(rule_right_part == "" ? EMPTY_STR : rule_right_part);            
				}
				else if (-2 == Is_valid(rule_right_part))
				{
                    non_term_chains.Add(rule_right_part);
				}
            }

            while (recursion < MAX_RECURSION && chains_list.Count < 50000)
            {
                recursion++;
                List<string> sub_non_term_chains = new List<string>();
                foreach (string non_term_chain in non_term_chains)
                {
                    chain_prefix = "";
					for (int i = 0; i < non_term_chain.Length; i++)
                    {
                        if (!rules_dictionary.ContainsKey(non_term_chain[i].ToString()))
                        {
                            chain_prefix += non_term_chain[i];
                        }
                        else
                        {
                            foreach (string rule_right_part in rules_dictionary[non_term_chain[i].ToString()])
                            {
                                res = chain_prefix + rule_right_part + non_term_chain.Substring(i + 1);

                                if (0 == Is_valid(res))
                                {
                                    if (chains_list.Contains(res == "" ? EMPTY_STR : res) || res.Length < min_chain_length)
                                    {
                                        break;
					                }
									else
									{
                                        chains_list.Add(res == "" ? EMPTY_STR : res);
                                    }                                
                                }
                                else if (-2 == Is_valid(res))
                                {
                                    sub_non_term_chains.Add(res);
                                }
                            }
                            break;
                        }
                    }
                }
                non_term_chains.Clear();
                non_term_chains = sub_non_term_chains.Distinct().ToList();
            }
            MessageBox.Show(chains_list.Count.ToString());
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
    }
}
