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

        [TestMethod]
        public void ParseMoveSequence_StandartMoveSequence_ReturnsCorrectList()
        {
            CollectionAssert.AreEqual(new List<CubeMove>() 
            { CubeMove.R, CubeMove.U, CubeMove.L, CubeMove.D, CubeMove.F, CubeMove.B },
            MoveParser.ParseMoveSequence("R U L D F B"));

            CollectionAssert.AreEqual(new List<CubeMove>()
            { CubeMove.L, CubeMove.U, CubeMove.L, CubeMove.U, CubeMove.L, CubeMove.U },
            MoveParser.ParseMoveSequence("L U L U L U"));

            CollectionAssert.AreEqual(new List<CubeMove>()
            { CubeMove.D, CubeMove.R, CubeMove.U, CubeMove.F, CubeMove.U, CubeMove.B },
            MoveParser.ParseMoveSequence("D R U F U B"));
        }

        [TestMethod]
        public void ParseMoveSequence_MixedMoves_ReturnsCorrectList()
        {
            CollectionAssert.AreEqual(new List<CubeMove>()
            { CubeMove.TwoR, CubeMove.UPrime, CubeMove.L, CubeMove.TwoD, CubeMove.FPrime, CubeMove.TwoB },
            MoveParser.ParseMoveSequence("R2 U' L D2 F' B2"));

            CollectionAssert.AreEqual(new List<CubeMove>()
            { CubeMove.LPrime, CubeMove.TwoU, CubeMove.L, CubeMove.U, CubeMove.TwoL, CubeMove.UPrime },
            MoveParser.ParseMoveSequence("L' U2 L U L2 U'"));

            CollectionAssert.AreEqual(new List<CubeMove>()
            { CubeMove.D, CubeMove.RPrime, CubeMove.UPrime, CubeMove.F, CubeMove.TwoU, CubeMove.TwoB },
            MoveParser.ParseMoveSequence("D R' U' F U2 B2"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseMoveSequence_NullInput_ThrowsArgumentException()
        {
            MoveParser.ParseMoveSequence(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseMoveSequence_EmptyInput_ThrowsArgumentException()
        {
            MoveParser.ParseMoveSequence("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseMoveSequence_WhitespaceInput_ThrowsArgumentException()
        {
            MoveParser.ParseMoveSequence("      ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseMoveSequence_InvalidMoveFormat_ThrowsArgumentException()
        {
            MoveParser.ParseMoveSequence("AA RR UU BB");
        }

    }
} 