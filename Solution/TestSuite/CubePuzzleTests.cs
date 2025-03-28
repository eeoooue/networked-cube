namespace TestSuite;
using LibNetCube;

[TestClass]
public class CubePuzzleTests
{
    [TestMethod]
    public void TestReset()
    {
        CubePuzzle puzzle = new CubePuzzle();

        CubeMove move = CubeMove.U;
        puzzle.PerformMove(move);

        puzzle.Reset();
        CubeState state = puzzle.GetState();

        int[,] expectedTopFace = { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
        int[,] expectedBottomFace = { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
        int[,] expectedLeftFace = { { 3, 3, 3 }, { 3, 3, 3 }, { 3, 3, 3 } };
        int[,] expectedRightFace = { { 4, 4, 4 }, { 4, 4, 4 }, { 4, 4, 4 } };
        int[,] expectedFrontFace = { { 2, 2, 2 }, { 2, 2, 2 }, { 2, 2, 2 } };
        int[,] expectedBackFace = { { 5, 5, 5 }, { 5, 5, 5 }, { 5, 5, 5 } };

        CollectionAssert.AreEqual(expectedTopFace, state.GetFace(CubeFace.Top));
        CollectionAssert.AreEqual(expectedBottomFace, state.GetFace(CubeFace.Bottom));
        CollectionAssert.AreEqual(expectedLeftFace, state.GetFace(CubeFace.Left));
        CollectionAssert.AreEqual(expectedRightFace, state.GetFace(CubeFace.Right));
        CollectionAssert.AreEqual(expectedFrontFace, state.GetFace(CubeFace.Front));
        CollectionAssert.AreEqual(expectedBackFace, state.GetFace(CubeFace.Back));
    }

    [TestMethod]
    public void TestResetWithoutAnyMoves()
    {
        CubePuzzle puzzle = new CubePuzzle();

        puzzle.Reset();
        CubeState state = puzzle.GetState();

        int[,] expectedTopFace = { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
        int[,] expectedBottomFace = { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
        int[,] expectedLeftFace = { { 3, 3, 3 }, { 3, 3, 3 }, { 3, 3, 3 } };
        int[,] expectedRightFace = { { 4, 4, 4 }, { 4, 4, 4 }, { 4, 4, 4 } };
        int[,] expectedFrontFace = { { 2, 2, 2 }, { 2, 2, 2 }, { 2, 2, 2 } };
        int[,] expectedBackFace = { { 5, 5, 5 }, { 5, 5, 5 }, { 5, 5, 5 } };

        CollectionAssert.AreEqual(expectedTopFace, state.GetFace(CubeFace.Top));
        CollectionAssert.AreEqual(expectedBottomFace, state.GetFace(CubeFace.Bottom));
        CollectionAssert.AreEqual(expectedLeftFace, state.GetFace(CubeFace.Left));
        CollectionAssert.AreEqual(expectedRightFace, state.GetFace(CubeFace.Right));
        CollectionAssert.AreEqual(expectedFrontFace, state.GetFace(CubeFace.Front));
        CollectionAssert.AreEqual(expectedBackFace, state.GetFace(CubeFace.Back));
    }

}
