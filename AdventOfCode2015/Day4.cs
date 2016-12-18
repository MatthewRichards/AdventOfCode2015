using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
  class Day4
  {
    public void Run()
    {
      const string key = "iwrupvqb";
      var md5 = new MD5CryptoServiceProvider();
      var threeZeroes = new byte[] { 0, 0, 0 };
      var fiveZeroes = new byte[] { 0, 0, 0, 0, 0 };

      int part1 = From(0).First(n => md5.ComputeHash(Encoding.ASCII.GetBytes(key + n)).ToNibbles().Take(5).SequenceEqual(fiveZeroes));
      int part2 = From(part1).First(n => md5.ComputeHash(Encoding.ASCII.GetBytes(key + n)).Take(3).SequenceEqual(threeZeroes));

      Console.WriteLine($"Part 1: {part1}");
      Console.WriteLine($"Part 2: {part2}");
      Console.ReadKey();
    }

    private IEnumerable<int> From(int start)
    {
      while (true)
      {
        yield return start;
        start++;
      }
    }

  }
}
