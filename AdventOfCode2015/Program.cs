using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
  class Program
  {
    static void Main()
    {
      var latestSolution = typeof(Program).Assembly.GetTypes().Where(t => t.Name.StartsWith("Day")).OrderByDescending(t => int.Parse(t.Name.Substring(3))).First();

      var instance = latestSolution.GetConstructor(new Type[0]).Invoke(new object[0]);
      latestSolution.GetMethods().Single(m => m.Name == "Run").Invoke(instance, new object[0]);
    }
  }
}
