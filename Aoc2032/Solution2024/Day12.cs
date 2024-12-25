




using System.Runtime.ExceptionServices;

namespace Aoc2023.Solution2024
{
    internal class Day12
    {
        private static string day = "12";
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
            var handledRegion = getRegions(lines);
            handledRegion = mergeRegions(handledRegion);
            
            foreach (var region in handledRegion) { 
                foreach ( var fence in region.Value)
                {
                    var fenceLength = 0;
                    foreach (var pos in fence)
                    {
                        var line = pos / 1000;
                        var col = pos % 1000;
                        //Up
                        var testLine = line - 1;
                        var testCol = col;
                        if ( !fence.Contains(testLine*1000+testCol )) {
                            fenceLength += 1;
                        }
                        //Ner
                        testLine = line + 1;
                        testCol = col;
                        if (!fence.Contains(testLine * 1000 + testCol)) {
                            fenceLength += 1;
                        }
                        //Höger
                        testLine = line;
                        testCol = col+1;
                        if (!fence.Contains(testLine * 1000 + testCol))
                        {
                            fenceLength += 1;
                        }
                        //Vänster
                        testLine = line;
                        testCol = col - 1;
                        if (!fence.Contains(testLine * 1000 + testCol))
                        {
                            fenceLength += 1;
                        }
                    }
                    //Console.WriteLine("Reg: " + region.Key + " length " + fence.Count + " size " + fenceLength);
                    total += fenceLength*fence.Count;
                }
            }
            Console.WriteLine("Day12-1: " + total);
        }               

        public static void solve2()
        {
            var total = 0;
            var lines = Util.readFile(fileName);
            var handledRegion = getRegions(lines);
            handledRegion = mergeRegions(handledRegion);            
            foreach (var region in handledRegion)
            {
                foreach (var fence in region.Value)
                {
                    var fenceLength = 0;
                    foreach (var pos in fence.OrderBy(a => a))
                    {
                        var line = pos / 1000;
                        var col = pos % 1000;
                        var hasUp = fence.Contains((line - 1) * 1000 + col);
                        var hasDown = fence.Contains((line + 1) * 1000 + col);
                        var hasLeft = fence.Contains(line * 1000 + col - 1);
                        var hasRight = fence.Contains(line * 1000 + col + 1);
                        var hasUpLeft = fence.Contains((line-1) * 1000 + col - 1);
                        var hasUpRight = fence.Contains((line - 1) * 1000 + col + 1);
                        var hasDownLeft = fence.Contains((line + 1) * 1000 + col - 1);
                        var hasDownRight = fence.Contains((line + 1) * 1000 + col + 1);
                        if (!hasUp && !hasDown && !hasLeft && !hasRight)
                        {
                            fenceLength += 4;
                        }
                        else if (!hasUp && !hasDown && !hasLeft)
                        {
                            fenceLength += 3;
                        }
                        else if (!hasUp && !hasRight && !hasLeft)
                        {
                            fenceLength += 3;
                        }
                        else if (!hasUp && !hasDown && !hasRight)
                        {
                            if (hasUpLeft && hasDownLeft)
                            {
                                fenceLength += 3;
                            }
                            else if (hasUpLeft || hasDownLeft)
                            {
                                fenceLength += 2;
                            }
                            else
                            {
                                fenceLength += 1;
                            }
                        }
                        else if (!hasDown && !hasLeft && !hasRight)
                        {
                            if (hasUpLeft && hasUpRight)
                            {
                                fenceLength += 3;
                            }
                            else if (hasUpLeft || hasUpRight)
                            {
                                fenceLength += 2;
                            }
                            else
                            {
                                fenceLength += 1;
                            }
                        }
                        //Left saknas
                        else if (!hasLeft && !hasUp)
                        {
                            fenceLength += 2;
                        }
                        else if (!hasLeft && !hasDown)
                        {
                            fenceLength += hasUpLeft ? 2 : 1;
                        }
                        else if (!hasLeft && !hasRight)
                        {
                            if (hasUpLeft && hasUpRight)
                            {
                                fenceLength += 2;
                            }
                            else if (hasUpLeft || hasUpRight)
                            {
                                fenceLength += 1;
                            }
                            else
                            {
                                fenceLength += 0;
                            }
                        }
                        else if (!hasLeft)
                        {
                            fenceLength += hasUpLeft ? 1 : 0;
                        }
                        //Upp saknas, left finns
                        else if (!hasUp)
                        {
                            if (!hasRight)
                            {
                                fenceLength += hasUpLeft ? 2 : 1;
                            }
                            else //Down saknas
                            {
                                fenceLength += (hasDownLeft && !hasDown) ? 1 : 0;
                                fenceLength += hasUpLeft ? 1 : 0;
                            }
                        }
                        //Right saknas, Upp finns, left finns
                        else if (!hasRight)
                        {
                            fenceLength += hasUpRight ? 1 : 0;
                            fenceLength += (!hasDown && hasDownLeft) ? 1 : 0;
                        }
                        //Bara down saknas
                        else if (!hasDown)
                        {
                            fenceLength += hasDownLeft ? 1 : 0;
                        }



                    }
                    //Console.WriteLine("Reg: " + region.Key + " length " + fence.Count + " size " + fenceLength);
                    total += fenceLength * fence.Count;
                }
            }
            Console.WriteLine("Day12-2: " + total);
        }
        private static Dictionary<char, List<List<int>>> getRegions(List<string> lines)
        {
            var handledRegion = new Dictionary<char, List<List<int>>>();
            for (int line = 0; line < lines.Count; line++)
            {
                for (int column = 0; column < lines[line].Length; column++)
                {
                    var key = lines[line][column];
                    if (handledRegion.ContainsKey(key))
                    {
                        var regions = handledRegion.GetValueOrDefault(key);
                        var existingRegion = false;
                        foreach (var region in regions)
                        {
                            var lineTest = line - 1;
                            var colTest = column;
                            if (region.Contains(lineTest * 1000 + colTest))
                            {
                                existingRegion = true;
                            }
                            lineTest = line;
                            colTest = column - 1;
                            if (region.Contains(lineTest * 1000 + colTest))
                            {
                                existingRegion = true;
                            }
                            if (existingRegion)
                            {
                                region.Add(line * 1000 + column);
                                break;
                            }
                        }

                        if (!existingRegion)
                        {
                            var fence = new List<int>();
                            fence.Add(line * 1000 + column);
                            regions.Add(fence);
                        }
                    }
                    else
                    {
                        var fence = new List<int>();
                        fence.Add(line * 1000 + column);
                        var regions = new List<List<int>>();
                        regions.Add(fence);
                        handledRegion.Add(key, regions);
                    }
                }
            }
            return handledRegion;
        }

        private static Dictionary<char, List<List<int>>> mergeRegions(Dictionary<char, List<List<int>>> handledRegion)
        {
            //MergeRegions
            foreach (var region in handledRegion)
            {
                var mergeAble = true;
                while (mergeAble)
                {
                    var fenceIndex1 = 0;
                    var fenceIndex2 = 0;
                    for (int fenceIndex = 0; fenceIndex < region.Value.Count; fenceIndex++)
                    {
                        fenceIndex1 = fenceIndex;
                        var fence = region.Value[fenceIndex];

                        mergeAble = false;
                        foreach (var pos in fence)
                        {
                            var line = pos / 1000;
                            var col = pos % 1000;
                            //Up
                            var testLine = line - 1;
                            var testCol = col;
                            var mathingRegion = region.Value.FindIndex(a => a.Contains(testLine * 1000 + testCol));
                            if (mathingRegion != -1 && mathingRegion != fenceIndex)
                            {
                                mergeAble = true;
                                fenceIndex2 = mathingRegion;
                                break;
                            }
                            //Ner
                            testLine = line + 1;
                            testCol = col;
                            mathingRegion = region.Value.FindIndex(a => a.Contains(testLine * 1000 + testCol));
                            if (mathingRegion != -1 && mathingRegion != fenceIndex)
                            {
                                mergeAble = true;
                                fenceIndex2 = mathingRegion;
                                break;
                            }
                            //Höger
                            testLine = line;
                            testCol = col + 1;
                            mathingRegion = region.Value.FindIndex(a => a.Contains(testLine * 1000 + testCol));
                            if (mathingRegion != -1 && mathingRegion != fenceIndex)
                            {
                                mergeAble = true;
                                fenceIndex2 = mathingRegion;
                                break;
                            }
                            //Vänster
                            testLine = line;
                            testCol = col - 1;
                            mathingRegion = region.Value.FindIndex(a => a.Contains(testLine * 1000 + testCol));
                            if (mathingRegion != -1 && mathingRegion != fenceIndex)
                            {
                                mergeAble = true;
                                fenceIndex2 = mathingRegion;
                                break;
                            }
                        }
                        if (mergeAble)
                        {
                            break;
                        }

                    }
                    if (mergeAble)
                    {
                        var fence1 = region.Value[fenceIndex1];
                        fence1.AddRange(region.Value[fenceIndex2]);
                        if (fenceIndex2 > fenceIndex1)
                        {
                            region.Value.RemoveAt(fenceIndex2);
                            region.Value.RemoveAt(fenceIndex1);
                        }
                        else
                        {
                            region.Value.RemoveAt(fenceIndex1);
                            region.Value.RemoveAt(fenceIndex2);
                        }
                        region.Value.Add(fence1);
                    }
                }
            }
            return handledRegion;
        }
    }
}
