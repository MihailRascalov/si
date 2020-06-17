using System;
using System.Collections.Generic;

namespace k_NN
{
    class Metrics
    {
        static double F_Max(List<double> list)
        {
            double maximum = 0;
            for (int i = 0; i < list.Count; i++)
                if (maximum < list[i])
                    maximum = list[i];
            return maximum;
        }
        public double Chebyshev_metric(string[] test_object, string[] training_object)
        {
            double result;
            var variable = new List<double>();
            for (int i = 0; i < test_object.Length - 1; i++)
                variable.Add(Math.Abs((Convert.ToDouble(test_object[i])) - Convert.ToDouble(training_object[i])));
            result = F_Max(variable);
            return result;
        }
        public double Canberr_metric(string[] test_object, string[] training_object)
        {
            double result;
            double variable = 0;
            for (int i = 0; i < test_object.Length - 1; i++)
                variable += Math.Abs(((Convert.ToDouble(test_object[i]) - Convert.ToDouble(training_object[i]))) / ((Convert.ToDouble(test_object[i]) + Convert.ToDouble(training_object[i]))));
            result = variable;
            return result;
        }
        public double Euclid_metric(string[] test_object, string[] training_object)
        {
            double result;
            double variable = 0;
            for (int i = 0; i < test_object.Length - 1; i++)
                variable += Math.Pow((Convert.ToDouble(test_object[i]) - Convert.ToDouble(training_object[i])), 2);
            result = Math.Sqrt(variable);
            return result;
        }
        public double Pearson_metric(string[] test_object, string[] training_object)
        {
            double result;
            double x = 0;
            double y = 0;
            double n = 0;
            double rxy = 0;
            for (int i = 0; i < test_object.Length - 1; i++)
            {
                x += (Convert.ToDouble(test_object[i]));
                y += (Convert.ToDouble(training_object[i]));
                n++;
            }
            x = (1 / n) * x;
            y = (1 / n) * y;
            double up, up1, down, down1 = 0;
            for (int i = 0; i < test_object.Length - 1; i++)
            {
                up = ((Convert.ToDouble(test_object[i])) - x);
                down = Math.Sqrt((1 / n) * Math.Pow(Convert.ToDouble(test_object[i]), -x));
                up1 = ((Convert.ToDouble(training_object[i])) - y);
                down1 = Math.Sqrt((1 / n) * Math.Pow(Convert.ToDouble(training_object[i]), -y));
                rxy += ((up / down) * (up1 / down1));
            }
            rxy = Math.Abs((1 / n) * rxy);
            result = rxy;
            return result;
        }
        public double Manhattan_metric(string[] test_object, string[] training_object)
        {
            double result;
            double variable = 0;
            for (int i = 0; i < test_object.Length - 1; i++)
                variable += Math.Abs((Convert.ToDouble(test_object[i]) - Convert.ToDouble(training_object[i])));
            result = variable;
            return result;
        }
    }
}