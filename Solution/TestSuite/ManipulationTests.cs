using LibNetCube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSuite
{
    [TestClass]
    public class ManipulationTests
    {
        [TestMethod]
        public void ExpectedSidesChangeUponRotatingTopFace()
        {
            CubePuzzle puzzleA = new CubePuzzle();
            CubePuzzle puzzleB = new CubePuzzle();

            CubeState stateBefore = new CubeState(new CubeState(puzzleA.GetState()));
            puzzleB.PerformMove(CubeMove.U);
            CubeState stateAfter = new CubeState(new CubeState(puzzleB.GetState()));

            List<CubeFace> changed = [CubeFace.Back, CubeFace.Front, CubeFace.Left, CubeFace.Right];
            foreach (CubeFace face in changed)
            {
                int[,] before = stateBefore.GetFace(face);
                int[,] after = stateAfter.GetFace(face);
                bool facesMatch = FacesMatch(before, after);
                Assert.IsFalse(facesMatch);
            }

            List<CubeFace> matching = [CubeFace.Top, CubeFace.Bottom];
            foreach (CubeFace face in matching)
            {
                int[,] before = stateBefore.GetFace(face);
                int[,] after = stateAfter.GetFace(face);
                bool facesMatch = FacesMatch(before, after);
                Assert.IsTrue(facesMatch);
            }
        }

        [TestMethod]
        public void FourRotationsCauseNoChange()
        {
            CubePuzzle puzzle = new CubePuzzle();

            int[,] backBefore = puzzle.ReadFace(CubeFace.Back);
            int[,] frontBefore = puzzle.ReadFace(CubeFace.Front);
            int[,] leftBefore = puzzle.ReadFace(CubeFace.Left);
            int[,] rightBefore = puzzle.ReadFace(CubeFace.Right);
            int[,] topBefore = puzzle.ReadFace(CubeFace.Top);
            int[,] bottomBefore = puzzle.ReadFace(CubeFace.Bottom);

            puzzle.PerformMove(CubeMove.U);
            puzzle.PerformMove(CubeMove.U);
            puzzle.PerformMove(CubeMove.U);
            puzzle.PerformMove(CubeMove.U);

            int[,] backAfter = puzzle.ReadFace(CubeFace.Back);
            int[,] frontAfter = puzzle.ReadFace(CubeFace.Front);
            int[,] leftAfter = puzzle.ReadFace(CubeFace.Left);
            int[,] rightAfter = puzzle.ReadFace(CubeFace.Right);
            int[,] topAfter = puzzle.ReadFace(CubeFace.Top);
            int[,] bottomAfter = puzzle.ReadFace(CubeFace.Bottom);

            bool backMatches = FacesMatch(backBefore, backAfter);
            bool frontMatches = FacesMatch(frontBefore, frontAfter);
            bool leftMatches = FacesMatch(leftBefore, leftAfter);
            bool rightMatches = FacesMatch(rightBefore, rightAfter);
            bool topMatches = FacesMatch(topBefore, topAfter);
            bool bottomMatches = FacesMatch(bottomBefore, bottomAfter);

            Assert.IsTrue(backMatches);
            Assert.IsTrue(frontMatches);
            Assert.IsTrue(leftMatches);
            Assert.IsTrue(rightMatches);
            Assert.IsTrue(topMatches);
            Assert.IsTrue(bottomMatches);
        }


        private bool FacesMatch(int[,] expected, int[,] actual)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (expected[i, j] != actual[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
