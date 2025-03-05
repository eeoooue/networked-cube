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

        public PieceBasedWrapper(CubeState state)
        {
            AnchorPieces = CreateAnchors(state);
            CornerPieces = CreateCorners(state);
            EdgePieces = CreateEdges(state);
        }

        public List<AnchorPiece> CreateAnchors(CubeState state)
        {
            List<AnchorPiece> result = new List<AnchorPiece>()
            {
                ReadAnchor(state, "top"),
                ReadAnchor(state, "bottom"),
                ReadAnchor(state, "left"),
                ReadAnchor(state, "right"),
                ReadAnchor(state, "front"),
                ReadAnchor(state, "back"),
            };

            return result;
        }

        public AnchorPiece ReadAnchor(CubeState state, string identifier)
        {
            CubeFace face = CubeFace.Top;
            int value = 0;

            switch (identifier)
            {
                case "top":
                    face = CubeFace.Top;
                    value = 0;
                    break;
                case "bottom":
                    face = CubeFace.Bottom;
                    value = 0;
                    break;
                case "left":
                    face = CubeFace.Left;
                    value = 0;
                    break;
                case "right":
                    face = CubeFace.Right;
                    value = 0;
                    break;
                case "front":
                    face = CubeFace.Front;
                    value = 0;
                    break;
                case "back":
                    face = CubeFace.Back;
                    value = 0;
                    break;
                default:
                    break;

            }

            return new AnchorPiece(face, value);
        }

        public List<CornerPiece> CreateCorners(CubeState state)
        {
            List<CornerPiece> result = new List<CornerPiece>()
            {
                ReadCorner(state, "top-ne"),
                ReadCorner(state, "top-se"),
                ReadCorner(state, "top-sw"),
                ReadCorner(state, "top-nw"),

                ReadCorner(state, "bottom-ne"),
                ReadCorner(state, "bottom-se"),
                ReadCorner(state, "bottom-sw"),
                ReadCorner(state, "bottom-nw"),
            };

            return result;
        }


        public CornerPiece ReadCorner(CubeState state, string identifier)
        {
            List<CubeFace> faces = new List<CubeFace>();
            List<int> values = new List<int>();

            switch (identifier)
            {
                case "top-ne":
                    break;
                case "top-se":
                    break;
                case "top-sw":
                    break;
                case "top-nw":
                    break;

                case "bottom-ne":
                    break;
                case "bottom-se":
                    break;
                case "bottom-sw":
                    break;
                case "bottom-nw":
                    break;

                default:
                    break;
            }

            return new CornerPiece(faces, values);
        }


        public List<EdgePiece> CreateEdges(CubeState state)
        {
            List<EdgePiece> result = new List<EdgePiece>()
            {
                ReadEdge(state, "top-n"),
                ReadEdge(state, "top-e"),
                ReadEdge(state, "top-s"),
                ReadEdge(state, "top-w"),

                ReadEdge(state, "middle-ne"),
                ReadEdge(state, "middle-se"),
                ReadEdge(state, "middle-sw"),
                ReadEdge(state, "middle-nw"),

                ReadEdge(state, "bottom-n"),
                ReadEdge(state, "bottom-e"),
                ReadEdge(state, "bottom-s"),
                ReadEdge(state, "bottom-w"),
            };

            return result;
        }

        public EdgePiece ReadEdge(CubeState state, string identifier)
        {
            List<CubeFace> faces = new List<CubeFace>();
            List<int> values = new List<int>();

            switch (identifier)
            {
                case "top-n":
                    break;
                case "top-e":
                    break;
                case "top-s":
                    break;
                case "top-w":
                    break;

                case "middle-n":
                    break;
                case "middle-e":
                    break;
                case "middle-s":
                    break;
                case "middle-w":
                    break;

                case "bottom-n":
                    break;
                case "bottom-e":
                    break;
                case "bottom-s":
                    break;
                case "bottom-w":
                    break;

                default:
                    break;
            }

            return new EdgePiece(faces, values);
        }
    }
}
