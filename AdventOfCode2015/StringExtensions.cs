using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
  static class StringExtensions
  {
    public static IEnumerable<string> SplitLines(this string input)
    {
      return input.Split(new[] { Environment.NewLine, "\n" }, StringSplitOptions.None);
    }
  }
}
