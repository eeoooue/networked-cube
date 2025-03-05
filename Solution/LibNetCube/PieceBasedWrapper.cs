using LibNetCube.PuzzlePieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibNetCube
{
    internal class PieceBasedWrapper
    {
        public List<AnchorPiece> AnchorPieces;
        public List<CornerPiece> CornerPieces;
        public List<EdgePiece> EdgePieces;

        public PieceBasedWrapper()
        {
            AnchorPieces = CreateAnchors();
            CornerPieces = CreateCorners();
            EdgePieces = CreateEdges();
        }

        public List<AnchorPiece> CreateAnchors()
        {
            List<AnchorPiece> result = new List<AnchorPiece>();

            AnchorPiece top = new AnchorPiece(CubeFace.Top, 0);
            AnchorPiece bottom = new AnchorPiece(CubeFace.Bottom, 0);

            AnchorPiece left = new AnchorPiece(CubeFace.Left, 0);
            AnchorPiece right = new AnchorPiece(CubeFace.Right, 0);

            AnchorPiece front = new AnchorPiece(CubeFace.Front, 0);
            AnchorPiece back = new AnchorPiece(CubeFace.Back, 0);

            return result;
        }

        public List<CornerPiece> CreateCorners()
        {
            List<CornerPiece> result = new List<CornerPiece>();

            // top 4

            List<CubeFace> facesA = [CubeFace.Top, CubeFace.Top, CubeFace.Top];
            List<int> valuesA = [0, 0, 0];
            CornerPiece pieceA = new CornerPiece(facesA, valuesA);
            result.Add(pieceA);

            List<CubeFace> facesB = [CubeFace.Top, CubeFace.Top, CubeFace.Top];
            List<int> valuesB = [0, 0, 0];
            CornerPiece pieceB = new CornerPiece(facesB, valuesB);
            result.Add(pieceB);

            List<CubeFace> facesC = [CubeFace.Top, CubeFace.Top, CubeFace.Top];
            List<int> valuesC = [0, 0, 0];
            CornerPiece pieceC = new CornerPiece(facesC, valuesC);
            result.Add(pieceC);

            List<CubeFace> facesD = [CubeFace.Top, CubeFace.Top, CubeFace.Top];
            List<int> valuesD = [0, 0, 0];
            CornerPiece pieceD = new CornerPiece(facesD, valuesD);
            result.Add(pieceD);


            // bottom 4

            List<CubeFace> facesE = [CubeFace.Top, CubeFace.Top, CubeFace.Top];
            List<int> valuesE = [0, 0, 0];
            CornerPiece pieceE = new CornerPiece(facesE, valuesE);
            result.Add(pieceE);

            List<CubeFace> facesF = [CubeFace.Top, CubeFace.Top, CubeFace.Top];
            List<int> valuesF = [0, 0, 0];
            CornerPiece pieceF = new CornerPiece(facesF, valuesF);
            result.Add(pieceF);

            List<CubeFace> facesG = [CubeFace.Top, CubeFace.Top, CubeFace.Top];
            List<int> valuesG = [0, 0, 0];
            CornerPiece pieceG = new CornerPiece(facesG, valuesG);
            result.Add(pieceG);

            List<CubeFace> facesH = [CubeFace.Top, CubeFace.Top, CubeFace.Top];
            List<int> valuesH = [0, 0, 0];
            CornerPiece pieceH = new CornerPiece(facesH, valuesH);
            result.Add(pieceH);

            return result;
        }

        public List<EdgePiece> CreateEdges()
        {
            List<EdgePiece> result = new List<EdgePiece>();

            // top 4

            List<CubeFace> facesA = [CubeFace.Top, CubeFace.Top];
            List<int> valuesA = [0, 0];
            EdgePiece pieceA = new EdgePiece(facesA, valuesA);
            result.Add(pieceA);

            List<CubeFace> facesB = [CubeFace.Top, CubeFace.Top];
            List<int> valuesB = [0, 0];
            EdgePiece pieceB = new EdgePiece(facesB, valuesB);
            result.Add(pieceB);

            List<CubeFace> facesC = [CubeFace.Top, CubeFace.Top];
            List<int> valuesC = [0, 0];
            EdgePiece pieceC = new EdgePiece(facesC, valuesC);
            result.Add(pieceC);

            List<CubeFace> facesD = [CubeFace.Top, CubeFace.Top];
            List<int> valuesD = [0, 0];
            EdgePiece pieceD = new EdgePiece(facesD, valuesD);
            result.Add(pieceD);

            // middle 4

            List<CubeFace> facesE = [CubeFace.Top, CubeFace.Top];
            List<int> valuesE = [0, 0];
            EdgePiece pieceE = new EdgePiece(facesE, valuesE);
            result.Add(pieceE);

            List<CubeFace> facesF = [CubeFace.Top, CubeFace.Top];
            List<int> valuesF = [0, 0];
            EdgePiece pieceF = new EdgePiece(facesF, valuesF);
            result.Add(pieceF);

            List<CubeFace> facesG = [CubeFace.Top, CubeFace.Top];
            List<int> valuesG = [0, 0];
            EdgePiece pieceG = new EdgePiece(facesG, valuesG);
            result.Add(pieceG);

            List<CubeFace> facesH = [CubeFace.Top, CubeFace.Top];
            List<int> valuesH = [0, 0];
            EdgePiece pieceH = new EdgePiece(facesH, valuesH);
            result.Add(pieceH);

            // bottom 4

            List<CubeFace> facesI = [CubeFace.Top, CubeFace.Top];
            List<int> valuesI = [0, 0];
            EdgePiece pieceI = new EdgePiece(facesI, valuesI);
            result.Add(pieceI);

            List<CubeFace> facesJ = [CubeFace.Top, CubeFace.Top];
            List<int> valuesJ = [0, 0];
            EdgePiece pieceJ = new EdgePiece(facesJ, valuesJ);
            result.Add(pieceJ);

            List<CubeFace> facesK = [CubeFace.Top, CubeFace.Top];
            List<int> valuesK = [0, 0];
            EdgePiece pieceK = new EdgePiece(facesK, valuesK);
            result.Add(pieceK);

            List<CubeFace> facesL = [CubeFace.Top, CubeFace.Top];
            List<int> valuesL = [0, 0];
            EdgePiece pieceL = new EdgePiece(facesL, valuesL);
            result.Add(pieceL);

            return result;
        }
    }
}
