using LibNetCube;
using System.Data;

namespace TestSuite
{
    [TestClass]
    public sealed class SerializationTests
    {
        [TestMethod]
        public void CanSerializeDeserializeCube()
        {
            int[,] a = new int[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            int[,] b = new int[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
            int[,] c = new int[,] { { 2, 2, 2 }, { 2, 2, 2 }, { 2, 2, 2 } };
            int[,] d = new int[,] { { 3, 3, 3 }, { 3, 3, 3 }, { 3, 3, 3 } };
            int[,] e = new int[,] { { 4, 4, 4 }, { 4, 4, 4 }, { 4, 4, 4 } };
            int[,] f = new int[,] { { 5, 5, 5 }, { 5, 5, 5 }, { 5, 5, 5 } };

            List<int[,]> faces = new List<int[,]>() { a, b, c, d, e, f };
            CubeState expected = new CubeState(faces);
            byte[] bytes = expected.Serialize();
            CubeState actual = new CubeState(bytes);
            AssertCubesMatch(expected, actual);
        }

        [TestMethod]
        public void CanSerializeDeserializeFace()
        {
            int[,] expected = new int[,] {{ 0, 1, 0 }, { 1, 0, 3 }, { 2, 5, 6 }};
            byte[] bytes = CubeState.SerializeFace(expected);
            int[,] actual = CubeState.DeserializeFace(bytes);
            AssertFacesMatch(expected, actual);
        }

        private void AssertCubesMatch(CubeState expected, CubeState actual)
        {
            List<CubeFace> faces = CubeState.GetFaceNames();
            foreach(CubeFace face in faces)
            {
                int[,] e = expected.GetFace(face);
                int[,] a = actual.GetFace(face);
                AssertFacesMatch(e, a);
            }
        }

        private void AssertFacesMatch(int[,] expected, int[,] actual)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Assert.AreEqual(expected[i, j], actual[i, j]);
                }
            }
        }
    }
}
