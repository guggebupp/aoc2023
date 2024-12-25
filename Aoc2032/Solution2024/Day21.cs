





using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text;

namespace Aoc2023.Solution2024
{
    internal class Day21
    {
        private static string day = "21";
        private static string fileName = "day" + day + ".txt";

        public static void solve()
        {
            solve1();
            solve2();
        }

        public static void solve1()
        {
            var total = 0;
            var lines = Util.readFile(fileName);
            var codes = new List<string>();
            foreach (var line in lines)
            {
                if (line != "")
                {
                    codes.Add(line);    
                }
            }            
            
            foreach (var code in codes)
            {
                var robotACurrent = getNumericCoord('A');
                var roboACurrentRow = robotACurrent / 1000;
                var roboACurrentCol = robotACurrent % 1000;
                var robotBSequence = new List<char>();
                foreach (var c in code) {
                    var nextCood = getNumericCoord(c);
                    var nextRow = nextCood / 1000;
                    var nextCol = nextCood % 1000;
                    if (roboACurrentRow < 3 && nextCol == 0 )
                    {
                        while (nextCol != roboACurrentCol || nextRow != roboACurrentRow)
                        {
                            while (nextCol < roboACurrentCol && (roboACurrentRow != 3 || roboACurrentCol != 1))
                            {
                                robotBSequence.Add('<');
                                roboACurrentCol--;
                            }
                            while (nextCol > roboACurrentCol)
                            {
                                robotBSequence.Add('>');
                                roboACurrentCol++;
                            }
                            while (nextRow < roboACurrentRow)
                            {
                                robotBSequence.Add('^');
                                roboACurrentRow--;
                            } while (nextRow > roboACurrentRow && (roboACurrentRow != 2 || roboACurrentCol != 0))
                            {
                                robotBSequence.Add('v');
                                roboACurrentRow++;
                            }


                        }
                    }
                    else
                    {
                        while (nextCol != roboACurrentCol || nextRow != roboACurrentRow)
                        {
                            while (nextRow < roboACurrentRow)
                            {
                                robotBSequence.Add('^');
                                roboACurrentRow--;
                            } while (nextRow > roboACurrentRow && (roboACurrentRow != 2 || roboACurrentCol != 0))
                            {
                                robotBSequence.Add('v');
                                roboACurrentRow++;
                            }
                            while (nextCol < roboACurrentCol && (roboACurrentRow != 3 || roboACurrentCol != 1))
                            {
                                robotBSequence.Add('<');
                                roboACurrentCol--;
                            }
                            while (nextCol > roboACurrentCol)
                            {
                                robotBSequence.Add('>');
                                roboACurrentCol++;
                            }
                        }
                    }


                        if (nextCol == roboACurrentCol && nextRow == roboACurrentRow) {
                        robotBSequence.Add('A');
                    }
                }
                var robotBCurrent = getControlCoord('A');
                var roboBCurrentRow = robotBCurrent / 1000;
                var roboBCurrentCol = robotBCurrent % 1000;
                var robotCSequence = new List<char>();
                foreach (var s in robotBSequence)
                {
                    Console.Write(s);
                }
                Console.WriteLine();
                foreach (var s in robotBSequence)
                {
                    var nextCood = getControlCoord(s);
                    var nextRow = nextCood / 1000;
                    var nextCol = nextCood % 1000;
                    if (!(roboBCurrentRow == 1 && nextRow == 0 && nextCol == 0))
                    {
                        while (nextCol != roboBCurrentCol || nextRow != roboBCurrentRow)
                        {
                            while (nextRow < roboBCurrentRow && (roboBCurrentCol != 0 || roboBCurrentRow != 1))
                            {
                                robotCSequence.Add('^');
                                roboBCurrentRow--;
                            } while (nextRow > roboBCurrentRow)
                            {
                                robotCSequence.Add('v');
                                roboBCurrentRow++;
                            }
                            while (nextCol < roboBCurrentCol && (roboBCurrentCol != 1 || roboBCurrentRow != 0))
                            {
                                robotCSequence.Add('<');
                                roboBCurrentCol--;
                            }
                            while (nextCol > roboBCurrentCol)
                            {
                                robotCSequence.Add('>');
                                roboBCurrentCol++;
                            }

                        }
                        
                    } else
                    {
                        while (nextCol != roboBCurrentCol || nextRow != roboBCurrentRow)
                        {
                            while (nextCol < roboBCurrentCol && (roboBCurrentCol != 1 || roboBCurrentRow != 0))
                            {
                                robotCSequence.Add('<');
                                roboBCurrentCol--;
                            }
                            while (nextCol > roboBCurrentCol)
                            {
                                robotCSequence.Add('>');
                                roboBCurrentCol++;
                            }
                            while (nextRow < roboBCurrentRow && (roboBCurrentCol != 0 || roboBCurrentRow != 1))
                            {
                                robotCSequence.Add('^');
                                roboBCurrentRow--;
                            } while (nextRow > roboBCurrentRow)
                            {
                                robotCSequence.Add('v');
                                roboBCurrentRow++;
                            }
                            

                        }
                        
                    }

                    if (nextCol == roboBCurrentCol && nextRow == roboBCurrentRow)
                    {
                        robotCSequence.Add('A');
                    }




                }
                foreach (var s in robotCSequence)
                {
                    Console.Write(s);
                }
                Console.WriteLine();
                var robotCCurrent = getControlCoord('A');
                var roboCCurrentRow = robotCCurrent / 1000;
                var roboCCurrentCol = robotCCurrent % 1000;
                var robotDSequence = new List<char>();
                foreach (var s in robotCSequence)
                {
                    var nextCood = getControlCoord(s);
                    var nextRow = nextCood / 1000;
                    var nextCol = nextCood % 1000;
                    //if (!(roboCCurrentCol == 0 && roboCCurrentRow == 1))
                    if (roboCCurrentRow == 1 && nextRow == 0 && nextCol == 0)

                    {
                        while ((nextCol != roboCCurrentCol || nextRow != roboCCurrentRow))
                        {
                            while (nextCol < roboCCurrentCol && (roboCCurrentCol != 1 || roboCCurrentRow != 0))
                            {
                                robotDSequence.Add('<');
                                roboCCurrentCol--;
                            }
                            while (nextCol > roboCCurrentCol)
                            {
                                robotDSequence.Add('>');
                                roboCCurrentCol++;
                            }
                            while (nextRow < roboCCurrentRow && (roboCCurrentCol != 0 || roboCCurrentRow != 1))
                            {
                                robotDSequence.Add('^');
                                roboCCurrentRow--;
                            } while (nextRow > roboCCurrentRow)
                            {
                                robotDSequence.Add('v');
                                roboCCurrentRow++;
                            }
                        }

                        
                    } else
                    {
                        while ((nextCol != roboCCurrentCol || nextRow != roboCCurrentRow))
                        {
                            while (nextRow < roboCCurrentRow && (roboCCurrentCol != 0 || roboCCurrentRow != 1))
                            {
                                robotDSequence.Add('^');
                                roboCCurrentRow--;
                            } while (nextRow > roboCCurrentRow)
                            {
                                robotDSequence.Add('v');
                                roboCCurrentRow++;
                            }
                            while (nextCol < roboCCurrentCol && (roboCCurrentCol != 1 || roboCCurrentRow != 0))
                            {
                                robotDSequence.Add('<');
                                roboCCurrentCol--;
                            }
                            while (nextCol > roboCCurrentCol)
                            {
                                robotDSequence.Add('>');
                                roboCCurrentCol++;
                            }
                            
                        }
                        
                    }
                    if (nextCol == roboCCurrentCol && nextRow == roboCCurrentRow)
                    {
                        robotDSequence.Add('A');
                    }

                }
                foreach (var s in robotDSequence)
                {
                    Console.Write(s);
                }
                Console.WriteLine();
                var count = robotDSequence.Count;
                var val = Int32.Parse(code.Split('A')[0]);
                Console.WriteLine("A: " + count + " - " + val + " - " + count*val);
                total += robotDSequence.Count * Int32.Parse(code.Split('A')[0]);
            }
            

            Console.WriteLine("Day{0}-1: {1}", day, total);
        }
        

        private static int getControlCoord(char v)
        {
            int row = 0;
            int col = 0;
            if ( v == 'A')
            {
                row = 0;
                col = 2;
            }
            else if (v == '^')
            {
                row = 0;
                col = 1;
            }
            else if (v == '<')
            {
                row = 1;
                col = 0;
            }
            else if (v == 'v')
            {
                row = 1;
                col = 1;
            }
            else if (v == '>')
            {
                row = 1;
                col = 2;
            }
            return row * 1000 + col;
        }

        private static int getNumericCoord(char v)
        {
            int row = 0;
            int col = 0;
            if (v == 'A')
            {
                row = 3;
                col = 2;
            }
            else if (v == '0')
            {
                row = 3;
                col = 1;
            }
            else if (v == '3')
            {
                row = 2;
                col = 2;
            }
            else if (v == '2')
            {
                row = 2;
                col = 1;
            }
            else if (v == '1')
            {
                row = 2;
                col = 0;
            }
            else if (v == '6')
            {
                row = 1;
                col = 2;
            }
            else if (v == '5')
            {
                row = 1;
                col = 1;
            }
            else if (v == '4')
            {
                row = 1;
                col = 0;
            }
            else if (v == '9')
            {
                row = 0;
                col = 2;
            }
            else if (v == '8')
            {
                row = 0;
                col = 1;
            }
            else if (v == '7')
            {
                row = 0;
                col = 0;
            }
            return row * 1000 + col;
        }

            public static void solve2()
        {
            long total = 0;
            var lines = Util.readFile(fileName);
            var codes = new List<string>();
            foreach (var line in lines)
            {
                if (line != "")
                {
                    codes.Add(line);
                }
            }

            foreach (var code in codes)
            {
                var robotACurrent = getNumericCoord('A');
                var roboACurrentRow = robotACurrent / 1000;
                var roboACurrentCol = robotACurrent % 1000;
                var robotBSequence = new List<char>();
                foreach (var c in code)
                {
                    var nextCood = getNumericCoord(c);
                    var nextRow = nextCood / 1000;
                    var nextCol = nextCood % 1000;
                    if (roboACurrentRow < 3 && nextCol == 0)
                    {
                        while (nextCol != roboACurrentCol || nextRow != roboACurrentRow)
                        {
                            while (nextCol < roboACurrentCol && (roboACurrentRow != 3 || roboACurrentCol != 1))
                            {
                                robotBSequence.Add('<');
                                roboACurrentCol--;
                            }
                            while (nextCol > roboACurrentCol)
                            {
                                robotBSequence.Add('>');
                                roboACurrentCol++;
                            }
                            while (nextRow < roboACurrentRow)
                            {
                                robotBSequence.Add('^');
                                roboACurrentRow--;
                            }
                           
                            while (nextRow > roboACurrentRow && (roboACurrentRow != 2 || roboACurrentCol != 0))
                            {
                                robotBSequence.Add('v');
                                roboACurrentRow++;
                            }


                        }
                    }
                    else
                    {
                        while (nextCol != roboACurrentCol || nextRow != roboACurrentRow)
                        {
                            while (nextRow < roboACurrentRow)
                            {
                                robotBSequence.Add('^');
                                roboACurrentRow--;
                            } while (nextRow > roboACurrentRow && (roboACurrentRow != 2 || roboACurrentCol != 0))
                            {
                                robotBSequence.Add('v');
                                roboACurrentRow++;
                            }
                            while (nextCol < roboACurrentCol && (roboACurrentRow != 3 || roboACurrentCol != 1))
                            {
                                robotBSequence.Add('<');
                                roboACurrentCol--;
                            }
                            while (nextCol > roboACurrentCol)
                            {
                                robotBSequence.Add('>');
                                roboACurrentCol++;
                            }
                        }
                    }


                    if (nextCol == roboACurrentCol && nextRow == roboACurrentRow)
                    {
                        robotBSequence.Add('A');
                    }
                }
                /**foreach (char c in robotBSequence)
                {
                    Console.Write(c);
                }
                Console.WriteLine();**/
                StringBuilder stringBuilder = new StringBuilder();
                var robotSeqStr = "";
                foreach (char c in robotBSequence)
                {
                    robotSeqStr += c;
                }
                //stringBuilderIn.Append(robotBSequence.ToString());
                long sum = 0;
                var dict = new Dictionary<string, long>();
                sum += SplitAndTranslate(robotSeqStr, 0, 25, dict);                
                
                                
                long count = sum;
                var val = Int32.Parse(code.Split('A')[0]);
                Console.WriteLine("A: " + count + " - " + val + " - " + count * val);
                total += count * Int32.Parse(code.Split('A')[0]);
            }
            Console.WriteLine("Day{0}-2: {1}", day, total);
        }

        private static long SplitAndTranslate(string robotSeqStr, int deep, int maxDeep, Dictionary<string, long> dict)
        {
            long count = 0;
            var parts = robotSeqStr.Split('A');
            if ( parts.Length > 1) 
            {
                    foreach (var p in parts)
                    {
                        if (dict.ContainsKey(deep + p))
                        {
                        count += dict[deep + p];
                        } else {
                            var res = SplitAndTranslate(p, deep, maxDeep, dict);
                            dict.Add(deep + p, res);
                            count += res;
                        }
                        
                    }
            } else if (parts[0] != "")
            {
                var output = "";
                var part = parts[0] + "A";
                output += TransLate('A', part[0]);
                for (var i = 1; i < part.Length; i++) {
                    output += TransLate(part[i-1], part[i]);
                }                
                if ( deep+1 < maxDeep)
                {
                    if (dict.ContainsKey(deep + output))
                    {
                        count += dict[deep + output];
                    } else {
                        var res = SplitAndTranslate(output, deep + 1, maxDeep, dict);
                    dict.Add(deep + output, res);
                        count += res;
                    }                    
                } else
                {
                    return output.Length;
                }

            }
                               
            return count;
        }

        private static string TransLate(char in1, char in2)
        {
            var input = "" + in1 + in2;
            if ( input == "AA")
            {
                return "A";
            }
            if ( input == "A^")
            {
                return "<A";
            }
            if (input == "A>")
            {
                return "vA";
            }
            if (input == "Av")
            {
                return "v<A";
            }
            if (input == "A<")
            {
                return "v<<A";
            }
            if (input == "^^")
            {
                return "A";
            }
            if (input == "^A")
            {
                return ">A";
            }
            if (input == "^>")
            {
                return ">vA";
            }
            if (input == "^v")
            {
                Console.WriteLine("ERROR: " + input);
                return "vA";
            }
            if (input == "^<")
            {
                return "v<A";
            }
            if (input == ">>")
            {
                return "A";
            }
            if (input == ">A")
            {
                return "^A";
            }
            if (input == ">^")
            {
                return "^<A";
            }
            if (input == ">v")
            {
                return "<A";
            }
            if (input == "><")
            {
                Console.WriteLine("ERROR: " + input);
                return "<<A";
            }
            if (input == "vv")
            {
                return "A";
            }
            if (input == "vA")
            {
                return ">^A";
            }
            if (input == "v>")
            {
                return ">A";
            }
            if (input == "v^")
            {
                Console.WriteLine("ERROR: " + input);
                return "^A";
            }
            if (input == "v<")
            {
                return "<A";
            }
            if (input == "<<")
            {
                return "A";
            }
            if (input == "<v")
            {
                return ">A";
            }
            if (input == "<>")
            {
                Console.WriteLine("ERROR: " + input);
                return ">>A";
            }
            if (input == "<^")
            {
                return ">^A";
            }
            if (input == "<A")
            {
                return ">>^A";
            }
            return "";

        }
        
        
    }
}
