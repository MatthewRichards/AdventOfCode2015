using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.FSharp.Collections;

namespace AdventOfCode2015
{
  static class EnumerableExtensions
  {
    public static Tuple<IEnumerable<T>, IEnumerable<T>> Fork<T>(this IEnumerable<T> input)
    {
      var cachedInput = SeqModule.Cache(input);
      return Tuple.Create(
        cachedInput.Where((val, idx) => idx % 2 == 0),
        cachedInput.Where((val, idx) => idx % 2 == 1));
    }

    public static IEnumerable<byte> ToNibbles(this IEnumerable<byte> input)
    {
      foreach (byte b in input)
      {
        yield return (byte)(b >> 4);
        yield return (byte)(b & 0xF);
      }
    }
  }
}
