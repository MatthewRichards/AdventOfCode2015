﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
  class Day3
  {
    public void Run()
    {
      var initialPosition = Tuple.Create(0, 0);
      var visitedHouses = new HashSet<Tuple<int, int>> { initialPosition };

      var xMoves = new Dictionary<char, int> { { '^', -1 }, { 'v', 1 }, { '>', 0 }, { '<', 0 } };
      var yMoves = new Dictionary<char, int> { { '^', 0 }, { 'v', 0 }, { '>', 1 }, { '<', -1 } };

      Func<Tuple<int, int>, char, Tuple<int, int>> visitingAlgorithm = (pos, move) => {
        var newPosition = Tuple.Create(pos.Item1 + xMoves[move], pos.Item2 + yMoves[move]);
        visitedHouses.Add(newPosition);
        return newPosition;
      };

      var moves = Input.Fork();

      var santaTask = Task.Factory.StartNew(() => moves.Item1.Aggregate(initialPosition, visitingAlgorithm));
      var roboTask = Task.Factory.StartNew(() => moves.Item2.Aggregate(initialPosition, visitingAlgorithm));

      var santaEnd = santaTask.Result;
      var roboEnd = roboTask.Result;
      
      Console.WriteLine($"Houses visited: {visitedHouses.Count}");
      Console.ReadKey();
    }

    private readonly Regex Parser = new Regex(@"(?<l>\d+)x(?<w>\d+)x(?<h>\d+)", RegexOptions.Compiled);

    private const string Input = @"v>v<vvv<<vv^v<v>vv>v<<<^^^^^<<^<vv>^>v^>^>^>^>^><vvvv<^>^<<^><<<^vvvv>^>^><^v^><^<>^^>^vvv^<vv>>^>^^<>><>^>vvv>>^vv>^<><>^<v^>^>^><vv^vv^>><<^><<v>><>^<^>>vvv>v>>>v<<^<><^<v<>v>^^v^^^<^v^^>>><^>^>v<>^<>>^>^^v^><v<v>>><>v<v^v>^v<>>^><v>^<>v^>^<>^v^^^v^^>>vv<<^^><^<vvv>^>^^<^>>^^^^^v^<v>vv<>>v^v<^v^^<><^<^vv^><>><><>v>vvv^vv^^<<><<vvv><<^v^><v<>vvv^<^>vvvv^>^>>^v^<v^vv<^^v<>v>vv^<>><v<<<^v^<<><v<^<^<><^^^>^>>v>^>v^<>v><^<^<v^>^^vv<^^<>v^v^vv<>>>>v^v<>><^^v>vv^^>v^v>v<vv>>v>><v^v^v>vv>^^>^v><<vv^v^^vv<^v><^<<v<v^>vv^^^<v^>v>v^^^>><^^<v^<^>>v><vv<v^^>^^v>>v^^^<^^v>^v>><^<^<>>v<<^^vv>^^^v<^<^<v<v^^vv>^vv^>>v^><v>><<<>^vv^<^<>v^^<<<v<^>^><><v^^>>^^^<^vv<^^^>><^^v>^^v^<v^v^>^^<v>^<^v<^<<<<^<v^>v^<^^<>^^>^><<>>^v><>><^<v><^^^>>vv>^><vv>^^^^^v^vvv><><^<^>v>v^v^>^><><^<^><>v<<vv<^>><>^v^^v>^<<<>^v^>^<<v^vv<>v^<v^^vv><<v^<>>>^<v>vv>v>>>^<^>><vv<>>>>v<v>>>^v>v><>>vvv<^^><<^>^>v<^vvvv<v><vv<><^^^v^^^>v^v<>v<^^v>>><>v<v^>>v><v^>>^^<v<<<^<v<><^^v><<v^><<<<^vv<^<>^><vv<<<<^>>>^v>^v>vv>^v<>v>v<v><^>>v>>^>^><^<v^v^>^v<><><^^>^<vvvv^^<>^^^>vv^v^v>^v^^v^^v><v^<^<>><^<v>v>>vv<<v>>vvvv<vv><>>^v^>^>>v^v^<<<vv<><v<<>>>^v<<v>^^vv^><>v>^>v><<<<<<<^>^^v^<<^^>>vvv^<><>><>^^v<<vv><^^v<^^><vv>v^>>>v^v><v^v<^>v^><>v<<>v>^^v><<<<><^v^v>>^<>^<<>^<v<<>>v<<>><^<<<<^v>^<^v>v>vv<v<v<<>^>v<^<<>v^<vvvv^>v>><<v><v<>v>v>>v^vvv^^>>>v^<^<<^^<<<><v>v^<<v<<<>v<^^<><v<v^^<v>^>v>>v<>^>^^>>^v<<>v^^^>>>^vv<^v<v>^>v>^><>v^^<>^^v^^vv^<^>^<<>><<^>^v>>><<<vvvv><<><v<^v^v<vvv^<><<^<vv><v^v^v^>v>v^<vvv^><^><^<vv><>>v^>^^^<>><v^<^^^<>v<<v<^v>>>^>>v^><<>vvv><^>>v><v><>v>>^>v><<><<>^<>^^^vv><v^>v^^>>^>^<^v<v<^^<^vvvv>v<v>^>v^>^><^<vvvv><^><><<v<>v<v^><^<v^>^v^^<<<<^><^^<^><>>^v<<^<<^vv>v>>v<^<^vv>><v<vv>v<v<v>^v<>^>v<>^v<<<v>>^^v>>><vvv>v^>^v^v>^^^v<vvvv>><^>vvv^<vv^^vv><<<>v<>v>^<vvv^<^<v<v<^vv^^>>vv^<^^v^><^^^^^v<^<v<^>>>vv^v^>^<v>^<><v^<^v>>><^v^<<v<<v<>v>^v<v^v>>^^v<<v<v<<>>>vv>>^v>>^<<<<^><<<><^^>>v<>^vvvv>v^^^>^^^>^<vvvv><^^v<v<>v<^v^v<<v^^^v^<v<^v>v^^<>^>^<^v>vv<v^vv<^<<>v><<^><><^^v<<><^^><>^v>^<><<^<^^<<>vv<>^^<<^>><<<>>vvv>^>v^^v^><<^>v>^>^<^<<>v<^>vv^v^v<>vv<<v>vv<vv><^>v^<>^vv^v^<v<^>>>>v^v><^<><<>vv^<vvv^>>vvv^>v>>><^^vv<vvvv>v<^<^>>^^>^^vv>>><^v<>^v^<<>v^^^<v>^>>^<^<v>>^v<^^^<v>^v>^>>v<vv>>^<v^<<>>^>>><v>v^<<^<v>>^<<^^<>v<^v<^<>v^v>^^v<vvvv>^vv>vvv>v^<^>><v^^vv<<<^>vvvv<>>^^<>v^<><>v<^<>v<>^>v<>vv<v^v>>v<v<^<v^^v^vv^vvv><^^>v>><>>^<^^<>>^>^<v^>>vvv^v><v>>^>^>v><><<><vv^v>v<>^v<^vv^^^<>^^<<^^^v<>><v<^<^<^<^^><v^v<^>v^>vvvv>^^v^>^<v<^^^>>^<<vv^<><><^^^^<<>^<><v>vv^<><^>^^<>v^<>>>v><>vvvvv>v>v^^>^<<vvvv<>vv>>v<<^<>^^^v^<><>>^<<<v<v<>>>><><v>v<v<>>^>^^^^vv^^<<><^^<<vv<^<>v>vv<v<><<<^<<v<<<<>v<>>^<^>^>><v>v>><^^<>><<<><<><v^^v<<><^<^v<v^><^^v<<>><<<<^>v^<v>><v^><v<vvv>v^v^<v><<>>v<><<v>^<>><>>^><>v^v>v<<>v<>v^^><<>>>v<<>>>>^>v>><v<<>>>vv>v>^<^^^<>v<v>^<^^v^vvv^>vv>^<v><vvvv>^<<>vvv<<<vv>^^<^>^>>v>v<<<<<>^^vv^>>v>^<^<v^v^>^v>>v>^v<><>^<^>v>v<<<^^^v>^<<<>vvv^v^^>^>>^>v>v<>^^><>>v>^>v<<<^^^v^<v^vv>><><^<^<><vvv<v^>>^v>vv<^v<<^vv>v^<<v>v>v>^v^>^v<<^v^vv>v<v>^<<><v^>>v<>><v<<<^v<<>vvv^<vv<vvv<<>^vv^^v><^>v^vv<<v^<<^^^<^<>^^<<>v<><<v>^><>^<><<v<v^^>vv<>^<v<^<vvv>vv>v><^^v<>><^v^v><><>><v<v>vv<>>><v^^v<>><<^>>><^^^vvv<<<vv<<^v<<<>><<vv>>>>v<<<<<vv><><v>v^^<<^vv^<vv<>>vv>^<>^v^^<>^^^vv>v^^<v<><v>v<v>>^v<v<>>^<v^^><>v^^^>v^^v<vv><^>v^v^<>v>v<v<^^>>v<^^vv^v<^^^^vv<<><<^>>^^<<v^^<<^>v^>>^^^><^^>^v^v>^<<v<vv<<<v<^^^>^>>^v<>^<^>v>^>^v^<^^^<^vv<v><^^>>v<v>^>^v^>>>>^v>^^<<^<v^v<^<<v<<^><^^<v^<><v>v^<<v^^<><<>>><vv<<><>^<>>>v<<v^^^v^^<<<vv<<^<^<^vv^<><><<^^<^^>v^>^<v<>>v^v<><<v>^^v>^<^<vvv<v>v^v>>>^^<^<v^>^vv<<<v<<>^><><^<>v>>>v<v^<>v>><^^^v^^^v<^^<vv^^^>v>v<>>^^<><>v>^<v<>^>>>><>v>^v>^vv^v<vv<<^^>><v<>^>^^<v<^>^<vvv>><>^<<>>><<<><>^^<<<v<>v^>v>v<v>^^^>^>^v<<>v>vv>><<<v>^^<v><vv<<v^^>^>>^><^>v<^<^v>><^^>v<vv^^><><>^><<><>v^>v<><^^>><>^<^^v<^<<v>><v><<<^^<<v<^vv^v<>><>>>^>v<vvv^>^<><v^><^<<^vv<^v^v^v<>v^^v>v^<^>^vv^>>><<>v^vv^<>^v^><<v^v<v>v^<><>>v^v^><>v^vvv^^^<<^<<v<<v<^vv^>>v^v>^^<v<>><>v>>v^<>^>v>^>><<>v^v><^v>v>>><v<v><^<^^>vv<v><^>^<^>^^v><><v<^^v<<><^<<v^<v<<><^^vvv^v>^>^<>>vv>v^^v^^vv<^^>><v^^vv><^v>v^<<v<^v>vvv<>>^v><<>^v<<<>^><^vv><<^^<v^>v<<v>^vv<>^v>>>><<<<^^<^v>^<^^<^<^^>>^^v>^^^^v^^^<<>^^vv<<v^^><v>><^<<><>^>v<>>v^^^>^v^^v^<v^v>v>>>>>^v>^>^^<vvv^^<v^<<<v<<>v>><^^^v<<^^<v>>^<^<^><^<<v^v><<vv<^<>>v>v>^v<><<v>^>vv^v<v>v><^<v>><>^<vv<v^^^^v<^^>><<^^>v>v>^^^<>v>^v^^>vv^vv<^^>><>^>^<>v>><>^v<<v>v>^><^^^v^<vv><<^v^>v^>vv>v^<>v><vv><^v>v<><v^v^v<^v<>^v<v^<<><<v>>^v><v>^^<>vvv^>^<<v^>><^>><^<>^v<v<v<^vvv<><<^v^<v>><<<v>^<^<v>v>^vv^v>v<^^vv<<vvv^<v>><>vv^>v<<>v<vvvv>>v>^^>>><<<^>^vv>><v>^^^>v<^vv<>v<<<v<<<<v>>>>^<^^^^>v<^^<><v>v>v<v^>vv^>v>v<^>^v^<>v>>vvv>^^><^vvv>><>>>^<<^<v<>>>v^^><v<v>>^><>v<^^v^<<v><>^<>>><^v^v>>>^vvvv^<><<<v<^>>v>^v^<v<v<<^<<v^vv^v>v<v<>>v<v^<<<><v^>><^<<^>^^><v>v<^v^<^>v>^<<v>v^<>v^<>vv^<>^>^>v^>^vv<>^^<<>>v<>^v<><v^><><<<vv>v>v^>vv^><<<<v>^v<><>^^<vv>v^^v^^^<v<^^><v^v<>><v<vv>^<>>><vv<^v<<>>^><>>v<v^v^>>>v<<>v<<<<<<<^v<<^^^v<^v<>v^^<<<^<>>v^vv<v>^<^^<^^<<^>vv><^<^^v<<<^><^v<^><>v<vv^>^v^^>>><<vv^^v><^<<^<>>^>>^<<<<v^vv<>>>v>^v>><>v>>v>><>v>><^^><v>^^vv<^^<^>vv><<^>><<><v>^vvv><^v^>vvv^>>^<><^>^<<>>v^v>v<<>^>>^>v<^^<^<<>^^v<vvvvv^^^<^<>^^v>v<>^<^^<<v>v^^vvv^^v>^vv<v^>^<>v<^v^>^<v><v<<<^v<v<v^^<vvv>vv<<vv>v^<<v<^<vv><^>^><^^<^^<<v^^<v^v<v^^^^>^>vv^<>^<>^>^^<^v><<<^>vv^vv>v^v<>^^v^<^^^vvv^><v^<v^^<v<>v^<><>v>vv<^v^>>^v<^^vv>vv>^>><<<<v^^<^><>^><>>v<>>v>^v<^vv>^^>^<^<<v^>>v^v<^^v<vv<^<><^^>^^<>^^^<vv<v<<^^>^>^vv<^>><^<vvv^<>>vv^><v>v^>^vv>^>v^^<>>^v<>>v<^>^v>vv^<vv<^^>>^<v>>>>vvv>vv>^><^v<<<>^^v>v^v<^^^v^^>^><<^^>^<v>><^^^^^<v<vv<v<^<>^^<^v<^>>vv>>^v^vv<>><>^>>>^<v>^^^^><^<<<v<>^v<><vvv^<^^>vv^>>v<vvvv><v^v><^vv<^v<><vvv<vv>v<>^v^<<>>>>v^^>^vv<<vvv<^^><v><><<>v^v<^<^>><vv>^^><^>^><<><v<^v^><^<><>vv>>>>^><<^^^<^v^>^>^^>^<^><v><^^<^^<>><><v>><<<>^>^^v<>^<<<v>>vv>^>>^>^<>>vv<^^vv<>v<>^^>^v<v^^^^v<>^<v>v^v>^^^<v>v<<<^vv^><>^<v>>^^vv>v^<<^><>>vv^^^^^>v>>v<<<>^<vvv<<><><^v<^v<^>^<>^vvv>^>v><<<vv<>v>vv<v<<v>^<^^>v^v>^<^v^<<vvv^^<>^v<<^>^<><>^^<>>^^<^v^<^<v<><<^><v<>v^^>v^v^^^<^v<<^v>^>>^^^^^><<<vv^>>v^><v^^vv><>v^^<^v<^<v^^><<v>v^^^><^^^><<<<<>^<<^<>>v<<v^v^^v<<>^<vv>>><^^^<>>>>vvv>v<>>>v^v^v<^<<^>^<<>v>>^>^^><^><<^v^^<^<>v^v>vv<>>>>>>v<<><v^<v<>>^^>v<<<>^<<v><^><<^v>vv>>>><><>v^<^v><v^<<<<^v><^>v>>^^^v<^>>^>>v<<^<<>vvv>>^v<>>^v><<<^v^v<><v>^vvv<v<v>^^^<><vv^<<>vvv<v<^^v^^><v<^v<^v^<v<^>^^^>>v>^<v^>>^<><<><vv<>vv>^v^>>^<<v<^^v>v<v<vvv>><><<><vvvvv<^v<^>^^><>^<<>^v<<>>v^vv<<>^^v^v^v><^>v>v<^<<^<^>vv>^v<<^>^>>v^<<v^>v^^v^^<v^v>>><vv><<<>^v>><><v<vv<^>v<>><^v>^^v<<<<^v^vv<<<<><><^<^<^v><<^^v^<<<<<^^><^^>vv<v<^<v>v<^><><v<>vvv^<vv>v^>^>^^^v<<^<^^>vv<v^v^v>^vv^><^v^<<>v<^^>^vv<<>^<<><^>v^<<^<>v><><>v<<^^><^^^v>>v>^vv<v^>>^v^^<><<<<<^>^v^<^<^^>^vv<^>v^^v^<>v<><v>v^v>vvv><><<><>vv<vvv^v>^^>^^^<><^>^^^>v<vvvv<>vv<v<v^^>><>v<>>v^>v^^vv^>v>>><v<<<<v<^v>><^^>^v^v<v^v^^^vvv>>>vv<^>><<<^>><^<^>^<^>^>>v^<^<>^<^^<><vvv^^<>^<>>><<v>^<^<v<<><^<<^><^^>vv<>^^><v^v<vv<^<vvv<<^>v^>>v>>>v<<^vv^<><>>>^^<^v^>>^>>><<v<<^<vv><^<>^>>^v>>><^^^<<<vv<<v<v>^vv><><<>^^^<>^<vv^<^<<v>^^><vv>><>>>^>vv>^<^<>>^<^^><v>v^><v>vv><><>>><><<^^v<<^v<v>vv<><><<^v>^v<>^<^^^v^>^<^><^v>v>^v<>><^^v^^^^^<><v<>>vvv<v^^<>v>>>>^<<><^v>vv>>^^><<><><^^^<^<^<<^v>^^^><v>>>>><<v<v>v^^^<>>v<vv<^<>v^^^v<><^>v>><<><>v<^><<>>><>v>^<>>^>v^v<<<<>^<v^vv^>vv<<><v^vv<v<v<<>>>>>vv<><>^<^v>vv^<<v<^v^^<<^<<^^v^>>><<>^<>><^>>><v<>><<>^^>><<<^^^^^v>>^<<>>vvvv<^v<v^^<^>^vv<vv<>v<<<^><>>>>vv^<^v>v<^<>^v>>^<^^v^>>><>^^<^v>>v<<>vv<vvvv<>vv>^><>v^<>^<<^vv<v^^v<vvvv><^>>^v^>^^<<<^>>^^>^<^^<^<<<v^<^^v<<vv^<<^^^vv><v<vv^>v^^v<v>^^<^v<^>>><<>vv<<^><<v^v^^^v<vv>^>vv<^>>^<v<>vv>>>^>>><<v<^<>^<<<>>^<<>><^<<^^^>>v^^>v<<<>v>v>v<v<^>^<>>>^vvv><<^^<<><v<><^<v<vvv>v>>>>vv^^v<v<^<^><v>^v<<v<vv>>v>v<<<<><<>vv<><^^^<>>v<v<vvv><v^<vv^>>><v^^<>>>^^<><^<^v^><vv>>^^v>^<<v^>v>^^>^v^<v<^<v^v><>>v^^<^v^^<<>^^>v^^>><<<<^<^^v>^^v>v<<vv^^vv>^>v^<v<v><>vv>>^<v^v^<v<^>^v>v^^>vvvvv<v><<>vv>vvvvvv>>v>>^^^<v>vv^^><<v>>v^^^^v>vv>v<^v>>>>^>^><v^>^<v<vv>v>^>><v>><<>>^vv<vv^^<^^>>>>><><<^<v<><<v>^><^vv^v>>>>>v>^>^<vv>^v^>v<^v^<^<<vv<<>v<>>^vv<<>^v^v>><><<>>v^^<<>^^<v><>v<<^^<^^>^^>^<^><>>v<>>^^<^>><<<v<>>>^v^>v>v<<^^<<^>v<v^>>v^^v^^<<>^v>v><v^>v<^^>^<vv><vv^<>v<><^<<<vv<<v>v<^<<<<^^>v^v^^><<><^^^<v>v^^>>>vvv><>vv<>>^^v^v<<^>v^^v^>vv>^<<v<^<v^>^^<<v<^^>^v^^<^^v<<>>vv<^>>^><><>v>>v<>^<v^^><<>>>";
  }
}
