using System.Collections.Generic;
using System.Linq;

namespace Exhaustive_algorithm
{
    public class Rule
    {
        public Dictionary<int, string> descriptors = new Dictionary<int, string>();
        public string decision;
        public int support;

        public override string ToString()
        {
            string result = string.Empty;
            string ruleToPrint = "";
            if (descriptors.Count != 1)
            {
                for (int i = 0; i < descriptors.Count; i++)
                {
                    int attributeNumber = descriptors.Keys.ElementAt(i) + 1;
                    string attributeValue = descriptors.Values.ElementAt(i);

                    ruleToPrint += "(a" + attributeNumber + "=" + attributeValue + ")";
                    if(!(i == descriptors.Count - 1))
                        ruleToPrint += " ^ ";
                    if (i == descriptors.Count - 1)
                        ruleToPrint += " => (d=" + decision + ")";
                }
                if (support > 1)
                    result += ruleToPrint + " [" + support + "]" + "\r\n";
                else
                    result += ruleToPrint + "\r\n";
                ruleToPrint = "";
            }
            else
            {
                foreach (var x in descriptors)
                {
                    int attributeNumber = x.Key + 1;
                    string attributeValue = x.Value;
                    if (support > 1)
                        result += "(a" + attributeNumber + "=" + attributeValue + ")" + " => (d=" + decision + ")" + " [" + support + "]" + "\r\n";
                    else
                        result += "(a" + attributeNumber + "=" + attributeValue + ")" + " => (d=" + decision + ")" + "\r\n";
                }
            }
            return result;
        }

        public Rule(string[] obj, int[] combinations)
        {
            this.decision = obj.Last();
            for (int i = 0; i < combinations.Length; i++)
            {
                int attributeNumber = combinations[i];
                string attributeValue = obj[attributeNumber];
                this.descriptors.Add(attributeNumber, attributeValue);
            }
        }

        public bool ifRuleContainsRule(Rule r)
        {
            foreach (var x in r.descriptors)
            {
                if (!this.descriptors.ContainsKey(x.Key) || this.descriptors[x.Key] != x.Value)
                    return false;
            }
            return true;
        }

        public int RuleSupport(string[][] objects)
        {
            support = 0;
            foreach (var obj in objects)
                if (ifObjFulfillRule(obj))
                    support++;
            return support;
        }

        public bool ifObjFulfillRule(string[] obj)
        {
            foreach (var descriptor in this.descriptors)
                if (obj[descriptor.Key] != descriptor.Value)
                    return false;
            return true;
        }

        public bool ifRuleContainsRuleFromList(List<Rule> rules)
        {
            foreach (var x in rules)
                if (ifRuleContainsRule(x))
                    return true;
            return false;
        }
    }
}