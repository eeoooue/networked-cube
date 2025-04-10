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

    [TestMethod]
    public void TestResetAfterMultipleMoves()
    {
        CubePuzzle puzzle = new CubePuzzle();

        puzzle.PerformMove(CubeMove.U);
        puzzle.PerformMove(CubeMove.R);
        puzzle.PerformMove(CubeMove.F);

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
    public void TestResetAfterSettingCustomState()
    {
        CubePuzzle puzzle = new CubePuzzle();

        int[,] customTopFace = { { 6, 6, 6 }, { 6, 6, 6 }, { 6, 6, 6 } };
        int[,] customBottomFace = { { 7, 7, 7 }, { 7, 7, 7 }, { 7, 7, 7 } };
        int[,] customLeftFace = { { 8, 8, 8 }, { 8, 8, 8 }, { 8, 8, 8 } };
        int[,] customRightFace = { { 9, 9, 9 }, { 9, 9, 9 }, { 9, 9, 9 } };
        int[,] customFrontFace = { { 10, 10, 10 }, { 10, 10, 10 }, { 10, 10, 10 } };
        int[,] customBackFace = { { 11, 11, 11 }, { 11, 11, 11 }, { 11, 11, 11 } };

        List<int[,]> customFaces = new List<int[,]> { customTopFace, customBottomFace, customLeftFace, customRightFace, customFrontFace, customBackFace };
        CubeState customState = new CubeState(customFaces);
        puzzle.SetState(customState);

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
