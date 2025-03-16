using LibNetCube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSuite
{
    [TestClass]
    public class MoveParserTests
    {
        [TestMethod]
        public void ParseMove_StandardMoves_ReturnsCorrectEnum()
        {
            Assert.AreEqual(CubeMove.U, MoveParser.ParseMove("U"));
            Assert.AreEqual(CubeMove.D, MoveParser.ParseMove("D"));
            Assert.AreEqual(CubeMove.L, MoveParser.ParseMove("L"));
            Assert.AreEqual(CubeMove.R, MoveParser.ParseMove("R"));
            Assert.AreEqual(CubeMove.F, MoveParser.ParseMove("F"));
            Assert.AreEqual(CubeMove.B, MoveParser.ParseMove("B"));
            Assert.AreEqual(CubeMove.M, MoveParser.ParseMove("M"));
        }

        [TestMethod]
        public void ParseMove_PrimeMoves_ReturnsCorrectEnum()
        {
            Assert.AreEqual(CubeMove.UPrime, MoveParser.ParseMove("U'"));
            Assert.AreEqual(CubeMove.DPrime, MoveParser.ParseMove("D'"));
            Assert.AreEqual(CubeMove.LPrime, MoveParser.ParseMove("L'"));
            Assert.AreEqual(CubeMove.RPrime, MoveParser.ParseMove("R'"));
            Assert.AreEqual(CubeMove.FPrime, MoveParser.ParseMove("F'"));
            Assert.AreEqual(CubeMove.BPrime, MoveParser.ParseMove("B'"));
            Assert.AreEqual(CubeMove.MPrime, MoveParser.ParseMove("M'"));
        }

        [TestMethod]
        public void ParseMove_DoubleMoves_ReturnsCorrectEnum()
        {
            Assert.AreEqual(CubeMove.TwoU, MoveParser.ParseMove("U2"));
            Assert.AreEqual(CubeMove.TwoD, MoveParser.ParseMove("D2"));
            Assert.AreEqual(CubeMove.TwoL, MoveParser.ParseMove("L2"));
            Assert.AreEqual(CubeMove.TwoR, MoveParser.ParseMove("R2"));
            Assert.AreEqual(CubeMove.TwoF, MoveParser.ParseMove("F2"));
            Assert.AreEqual(CubeMove.TwoB, MoveParser.ParseMove("B2"));
            Assert.AreEqual(CubeMove.TwoM, MoveParser.ParseMove("M2"));
        }

        [TestMethod]
        public void ParseMove_LowercaseInput_ReturnsCorrectEnum()
        {
            Assert.AreEqual(CubeMove.U, MoveParser.ParseMove("u"));
            Assert.AreEqual(CubeMove.UPrime, MoveParser.ParseMove("u'"));
            Assert.AreEqual(CubeMove.TwoU, MoveParser.ParseMove("u2"));
        }

        [TestMethod]
        public void ParseMove_InputWithWhitespace_ReturnsCorrectEnum()
        {
            Assert.AreEqual(CubeMove.U, MoveParser.ParseMove(" U "));
            Assert.AreEqual(CubeMove.UPrime, MoveParser.ParseMove(" U' "));
            Assert.AreEqual(CubeMove.TwoU, MoveParser.ParseMove(" U2 "));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseMove_NullInput_ThrowsArgumentException()
        {
            MoveParser.ParseMove(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseMove_EmptyInput_ThrowsArgumentException()
        {
            MoveParser.ParseMove("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseMove_WhitespaceInput_ThrowsArgumentException()
        {
            MoveParser.ParseMove("   ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseMove_InvalidMove_ThrowsArgumentException()
        {
            MoveParser.ParseMove("X"); 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseMove_InvalidFormat_ThrowsArgumentException()
        {
            MoveParser.ParseMove("U3"); 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseMove_TooLong_ThrowsArgumentException()
        {
            MoveParser.ParseMove("UU"); 
        }
    }
} 