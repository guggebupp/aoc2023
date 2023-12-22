using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023.Solution
{
    internal class Day19
    {
        private static string day = "19";
        private static string fileName = "day" + day + ".txt";
        public static void solve1()
        {
            long total = 0;
            var lines = Util.readFile(fileName);
            var workflows = new Dictionary<string, string>();
            var inputs = new List<string>();
            foreach (var line in lines)
            {
                if (line != "")
                {
                    if (line[0] != '{')
                    {
                        var p = line.Split('{');
                        workflows.Add(p[0], p[1]);
                    }
                    else
                    {
                        inputs.Add(line);
                    }
                }
            }

            var acceptedInputs = new List<string>();
            foreach (var input in inputs)
            {
                var p = input.Split('{')[1].Split('}')[0].Split(",");
                var x = int.Parse(p[0].Split("=")[1]);
                var m = int.Parse(p[1].Split("=")[1]);
                var a = int.Parse(p[2].Split("=")[1]);
                var s = int.Parse(p[3].Split("=")[1]);
                var result = "X";
                var step = "in";
                var index = 0;
                while (result != "A" && result != "R")
                {
                    var rule = workflows[step];
                    //1. Check first rule
                    if (rule.Split(",").Length <= index)
                    {
                        result = "A";
                        continue;
                    }
                    var ruleX = rule.Split(",")[index];
                    if (ruleX.Contains(">") || ruleX.Contains("<"))
                    {
                        var op = ruleX[1];
                        var ruleIndataType = ruleX[0];
                        var ruleValue = int.Parse(ruleX.Split(":")[0].Split(op)[1]);
                        var nexRule = ruleX.Split(":")[1].Split("}")[0];
                        var inValue = 0;
                        if (ruleIndataType == 'a') { inValue = a; }
                        else if (ruleIndataType == 'x') { inValue = x; }
                        else if (ruleIndataType == 'm') { inValue = m; }
                        else if (ruleIndataType == 's') { inValue = s; }
                        if (op == '>')
                        {
                            if (inValue > ruleValue)
                            {
                                index = 0;
                                var nextStep = ruleX.Split(":")[1];
                                if (nextStep == "A" || nextStep == "R")
                                {
                                    result = nextStep;
                                }
                                else
                                {
                                    step = nexRule;
                                }
                            }
                            else
                            {
                                index++;
                            }
                        }
                        if (op == '<')
                        {
                            if (inValue < ruleValue)
                            {
                                index = 0;
                                var nextStep = ruleX.Split(":")[1];
                                if (nextStep == "A" || nextStep == "R")
                                {
                                    result = nextStep;
                                }
                                else
                                {
                                    step = nexRule;
                                }
                            }
                            else
                            {
                                index++;
                            }
                        }
                    }
                    else if (ruleX.Split("}")[0] == "A" || ruleX.Split("}")[0] == "R")
                    {
                        result = ruleX.Split("}")[0];
                    }
                    else
                    {
                        step = ruleX.Split("}")[0];
                        index = 0;
                    }
                }
                if (result == "A")
                {
                    acceptedInputs.Add(input);
                }
            }
            foreach (var input in acceptedInputs)
            {
                var results = input.Split("{")[1].Split("}")[0].Split(",");
                foreach (var result in results)
                {
                    total += int.Parse(result.Split("=")[1]);
                }
            }

            Console.WriteLine("Day" + day + "-1: " + total);
        }

        public static void solve2()
        {
            long total = 0;
            var lines = Util.readFile(fileName);
            var workflows = new Dictionary<string, string>();
            foreach (var line in lines)
            {
                if (line != "")
                {
                    if (line[0] != '{')
                    {
                        var p = line.Split('{');
                        workflows.Add(p[0], p[1].Split("}")[0]);
                    }
                }
            }
            var xmin = 0;
            var xmax = 4000;
            var mmin = 0;
            var mmax = 4000;
            var amin = 0;
            var amax = 4000;
            var smin = 0;
            var smax = 4000;
            var step = "in";
            total += getCombos(xmin, xmax, mmin, mmax, amin, amax, smin, smax, workflows, step, 0, "");


            Console.WriteLine("Day" + day + "-2: " + total);
        }

        private static long getCombos(int xmin, int xmax, int mmin, int mmax, int amin, int amax, int smin, int smax, Dictionary<string, string> workflows, string step, int index, string handledSteps)
        {            
            long numbers = 0;
            var result = "X";

            while (result != "A" && result != "R")
            {                
                var rule = workflows[step];
                //1. Check first rule
                if (rule.Split(",").Length <= index)
                {
                    result = "A";
                    continue;
                }
                var ruleX = rule.Split(",")[index];
                if (ruleX.Contains(">") || ruleX.Contains("<"))
                {
                    var op = ruleX[1];
                    var ruleIndataType = ruleX[0];
                    var ruleValue = int.Parse(ruleX.Split(":")[0].Split(op)[1]);
                    var nexRule = ruleX.Split(":")[1].Split("}")[0];
                    var inMinValue = 0;
                    var inMaxValue = 0;
                    if (ruleIndataType == 'x') { inMinValue = xmin; inMaxValue = xmax; }
                    else if (ruleIndataType == 'a') { inMinValue = amin; inMaxValue = amax; }
                    else if (ruleIndataType == 's') { inMinValue = smin; inMaxValue = smax; }
                    else if (ruleIndataType == 'm') { inMinValue = mmin; inMaxValue = mmax; }
                    if (inMinValue > inMaxValue)
                    {
                        Console.WriteLine("ERROR");
                    }
                    if (op == '>')
                    {
                        if (inMaxValue > ruleValue)
                        {
                            var prevStep = step;
                            var prevIndex = index;

                            index = 0;
                            var nextStep = ruleX.Split(":")[1];
                            if (nextStep == "A" || nextStep == "R")
                            {
                                result = nextStep;
                            }
                            else
                            {
                                step = nexRule;
                            }

                            // Add under
                            if (inMinValue <= ruleValue)
                            {
                                if (ruleIndataType == 'x') { xmax = ruleValue; }
                                else if (ruleIndataType == 'a') { amax = ruleValue; }
                                else if (ruleIndataType == 's') { smax = ruleValue; }
                                else if (ruleIndataType == 'm') { mmax = ruleValue; }                                
                                numbers += getCombos(xmin, xmax, mmin, mmax, amin, amax, smin, smax, workflows, prevStep, prevIndex, handledSteps);                                
                            }
                            handledSteps += step + ":" + op + ",";
                            inMinValue = ruleValue + 1;
                            if (ruleIndataType == 'x') { xmin = inMinValue; xmax = inMaxValue; }
                            else if (ruleIndataType == 'a') { amin = inMinValue; amax = inMaxValue; }
                            else if (ruleIndataType == 's') { smin = inMinValue; smax = inMaxValue; }
                            else if (ruleIndataType == 'm') { mmin = inMinValue; mmax = inMaxValue; }
                        }
                        else
                        {
                            index++;
                        }
                    }
                    if (op == '<')
                    {
                        if (inMinValue < ruleValue)
                        {
                            var prevStep = step;
                            var prevIndex = index;

                            index = 0;
                            var nextStep = ruleX.Split(":")[1];
                            if (nextStep == "A" || nextStep == "R")
                            {
                                result = nextStep;
                            }
                            else
                            {
                                step = nexRule;
                            }
                            //Add over
                            if (inMaxValue >= ruleValue)
                            {
                                if (ruleIndataType == 'x') { xmin = ruleValue; }
                                else if (ruleIndataType == 'a') { amin = ruleValue; }
                                else if (ruleIndataType == 's') { smin = ruleValue; }
                                else if (ruleIndataType == 'm') { mmin = ruleValue; }                                
                                numbers += getCombos(xmin, xmax, mmin, mmax, amin, amax, smin, smax, workflows, prevStep, prevIndex, handledSteps);                                
                            }
                            handledSteps += step + ":" + op + ",";
                            inMaxValue = ruleValue - 1;
                            if (ruleIndataType == 'x') { xmin = inMinValue; xmax = inMaxValue; }
                            else if (ruleIndataType == 'a') { amin = inMinValue; amax = inMaxValue; }
                            else if (ruleIndataType == 's') { smin = inMinValue; smax = inMaxValue; }
                            else if (ruleIndataType == 'm') { mmin = inMinValue; mmax = inMaxValue; }
                        }
                        else
                        {
                            index++;
                        }
                    }
                }
                else if (ruleX.Split("}")[0] == "A" || ruleX.Split("}")[0] == "R")
                {
                    result = ruleX.Split("}")[0];
                }
                else
                {
                    handledSteps += step + ",";
                    step = ruleX.Split("}")[0];
                    index = 0;
                }
            }
            if (result == "A")
            {
                long xdiff = (xmax - (xmin == 0 ? 1 : xmin) + 1);
                long adiff = (amax - (amin == 0 ? 1 : amin) + 1);
                long sdiff = (smax - (smin == 0 ? 1 : smin) + 1);
                long mdiff = (mmax - (mmin == 0 ? 1 : mmin) + 1);
                long val1 = (xdiff == 0 ? 1 : xdiff) * (adiff == 0 ? 1 : adiff);
                long val2 = (sdiff == 0 ? 1 : sdiff) * (mdiff == 0 ? 1 : mdiff);
                long newVal = val1 * val2;
                numbers += newVal;
            }            
            return numbers;
        }        
    }
}
