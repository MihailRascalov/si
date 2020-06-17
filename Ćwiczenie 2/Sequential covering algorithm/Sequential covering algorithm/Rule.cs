using System.Collections.Generic;
using System.Linq;

namespace Sequential_covering_algorithm
{
    public class Rule
    {
        public Dictionary<int, string> descriptors = new Dictionary<int, string>();
        public string decision;
        public int support;

        public override string ToString()
        {
            string result = string.Empty;
            string ruleToPrint = "(a" + (descriptors.First().Key + 1) + "=" + descriptors.First().Value + ")";
            for (int i = 1; i < descriptors.Count; i++)
            {
                int attributeNumber = descriptors.Keys.ElementAt(i) + 1;
                string attributeValue = descriptors.Values.ElementAt(i);

                ruleToPrint += " ^ (a" + attributeNumber + "=" + attributeValue + ")";
            }
            ruleToPrint += " => (d=" + decision + ")";

            if (support > 1)
                ruleToPrint += " [" + support + "]";
            return ruleToPrint;
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

        public List<int> GenerateCoverage(string[][] objects, List<int> mask)
        {
            int temporary = 0;
            foreach (var obj in objects)
            {
                if (ifObjFulfillRule(obj))
                    mask.Remove(temporary);
                temporary++;
            }
            return mask;
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

        public bool ifConflictingRule(string[][] objects)
        {
            foreach (var obj in objects)
                if (ifObjFulfillRule(obj) && decision != obj.Last())
                    return false;
            return true;
        }
    }
}