using LibNetCube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSuite
{
    [TestClass]
    public class ScrambleAlgorithmTests
    {
        [TestMethod]
        public void GenerateScramble_ReturnsNonEmptyList()
        {
            List<CubeMove> scramble = ScrambleAlgorithm.GenerateScramble();

            Assert.IsNotNull(scramble);
            Assert.IsTrue(scramble.Count > 0);
        }

        [TestMethod]
        public void GenerateScramble_DefaultLength_Returns20Moves()
        {
            List<CubeMove> scramble = ScrambleAlgorithm.GenerateScramble();

            Assert.AreEqual(20, scramble.Count);
        }

        [TestMethod]
        public void GenerateScramble_ContainsValidMoves()
        {
            List<CubeMove> scramble = ScrambleAlgorithm.GenerateScramble();

            foreach (CubeMove move in scramble)
            {
                Assert.IsTrue(Enum.IsDefined(typeof(CubeMove), move), $"Move {move} is not a valid CubeMove");
            }
        }

        [TestMethod]
        public void GenerateScramble_AvoidsSameAxisConsecutiveMoves()
        {
            Dictionary<CubeMove, string> moveAxes = new Dictionary<CubeMove, string>
            {
                { CubeMove.U, "U" }, { CubeMove.UPrime, "U" }, { CubeMove.TwoU, "U" },
                { CubeMove.D, "D" }, { CubeMove.DPrime, "D" }, { CubeMove.TwoD, "D" },
                { CubeMove.L, "L" }, { CubeMove.LPrime, "L" }, { CubeMove.TwoL, "L" },
                { CubeMove.R, "R" }, { CubeMove.RPrime, "R" }, { CubeMove.TwoR, "R" },
                { CubeMove.F, "F" }, { CubeMove.FPrime, "F" }, { CubeMove.TwoF, "F" },
                { CubeMove.B, "B" }, { CubeMove.BPrime, "B" }, { CubeMove.TwoB, "B" },
                { CubeMove.M, "M" }, { CubeMove.MPrime, "M" }, { CubeMove.TwoM, "M" }
            };

            List<CubeMove> scramble = ScrambleAlgorithm.GenerateScramble();

            for (int i = 1; i < scramble.Count; i++)
            {
                string currentAxis = moveAxes[scramble[i]];
                string previousAxis = moveAxes[scramble[i - 1]];

                Assert.AreNotEqual(previousAxis, currentAxis,
                    $"Consecutive moves on same axis found: {scramble[i - 1]} followed by {scramble[i]}");
            }
        }

        [TestMethod]
        public void GenerateScramble_GeneratesRandomSequences()
        {
            List<CubeMove> scramble1 = ScrambleAlgorithm.GenerateScramble();
            List<CubeMove> scramble2 = ScrambleAlgorithm.GenerateScramble();

            bool areIdentical = true;

            if (scramble1.Count != scramble2.Count)
            {
                areIdentical = false;
            }
            else
            {
                for (int i = 0; i < scramble1.Count; i++)
                {
                    if (scramble1[i] != scramble2[i])
                    {
                        areIdentical = false;
                        break;
                    }
                }
            }

            Assert.IsFalse(areIdentical, "Two scrambles should not be identical");
        }
    }
}