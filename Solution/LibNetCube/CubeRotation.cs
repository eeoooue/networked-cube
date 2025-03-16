using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibNetCube
{
    internal static class CubeRotation
    {
        public static void RotateFaceClockwise(CubeState state, CubeFace face)
        {
            switch (face)
            {
                case CubeFace.Top:
                    RotateTopFaceClockwise(state);
                    return;
                case CubeFace.Bottom:
                    RotateBottomFaceClockwise(state);
                    return;
                case CubeFace.Left:
                    RotateLeftFaceClockwise(state);
                    return;
                case CubeFace.Right:
                    RotateRightFaceClockwise(state);
                    return;
                case CubeFace.Front:
                    RotateFrontFaceClockwise(state);
                    return;
                case CubeFace.Back:
                    RotateBackFaceClockwise(state);
                    return;
                default:
                    return;
            }
        }

        private static void RotateTopFaceClockwise(CubeState state)
        {
            PochmanWrapper wrapper = new PochmanWrapper(state);

            StringBuilder sources = new StringBuilder();
            sources.Append("AaBbCcDd"); // original top ring
            sources.Append("QqR"); // left row
            sources.Append("EeF"); // front row
            sources.Append("IiJ"); // right row
            sources.Append("MmN"); // back right

            StringBuilder destinations = new StringBuilder();
            destinations.Append("BbCcDdAa"); // rotated top ring
            destinations.Append("MmN"); // left row becomes back row
            destinations.Append("QqR"); // front row becomes left row
            destinations.Append("EeF"); // right row becomes front row
            destinations.Append("IiJ"); // back row becomes right row

            wrapper.ApplyMove(sources.ToString(), destinations.ToString());
        }

        private static void RotateBottomFaceClockwise(CubeState state)
        {
            PochmanWrapper wrapper = new PochmanWrapper(state);

            StringBuilder sources = new StringBuilder();
            sources.Append("UuVvWwXx"); // 
            sources.Append("TsS"); // 
            sources.Append("HgG"); // 
            sources.Append("LkK"); // 
            sources.Append("PoO"); // 

            StringBuilder destinations = new StringBuilder();
            destinations.Append("VvWwXxUu"); // 
            destinations.Append("HgG"); // 
            destinations.Append("LkK"); // 
            destinations.Append("PoO"); // 
            destinations.Append("TsS"); // 

            wrapper.ApplyMove(sources.ToString(), destinations.ToString());
        }

        private static void RotateFrontFaceClockwise(CubeState state)
        {
            PochmanWrapper wrapper = new PochmanWrapper(state);

            StringBuilder sources = new StringBuilder();
            sources.Append("EeFfGgHh"); // 
            sources.Append("DcC"); // 
            sources.Append("IlL"); // 
            sources.Append("VuU"); // 
            sources.Append("SrR"); // 

            StringBuilder destinations = new StringBuilder();
            destinations.Append("FfGgHhEe"); // 
            destinations.Append("IlL"); // 
            destinations.Append("VuU"); // 
            destinations.Append("SrR"); // 
            destinations.Append("DcC"); // 

            wrapper.ApplyMove(sources.ToString(), destinations.ToString());
        }

        private static void RotateBackFaceClockwise(CubeState state)
        {
            PochmanWrapper wrapper = new PochmanWrapper(state);

            StringBuilder sources = new StringBuilder();
            sources.Append("MmNnOoPp"); // 
            sources.Append("QtT"); // 
            sources.Append("XwW"); // 
            sources.Append("KjJ"); // 
            sources.Append("BaA"); // 

            StringBuilder destinations = new StringBuilder();
            destinations.Append("PpMmNnOo"); // 
            destinations.Append("XwW"); // 
            destinations.Append("KjJ"); // 
            destinations.Append("BaA"); // 
            destinations.Append("QtT"); // 

            wrapper.ApplyMove(sources.ToString(), destinations.ToString());
        }

        private static void RotateLeftFaceClockwise(CubeState state)
        {
            PochmanWrapper wrapper = new PochmanWrapper(state);

            StringBuilder sources = new StringBuilder();
            sources.Append("QqRrSsTt"); // 
            sources.Append("AdD"); // 
            sources.Append("EhH"); // 
            sources.Append("UxX"); // 
            sources.Append("OnN"); // 

            StringBuilder destinations = new StringBuilder();
            destinations.Append("RrSsTtQq"); // 
            destinations.Append("EhH"); // 
            destinations.Append("UxX"); // 
            destinations.Append("OnN"); // 
            destinations.Append("AdD"); // 

            wrapper.ApplyMove(sources.ToString(), destinations.ToString());
        }

        private static void RotateRightFaceClockwise(CubeState state)
        {
            PochmanWrapper wrapper = new PochmanWrapper(state);

            StringBuilder sources = new StringBuilder();
            sources.Append("IiJjKkLl"); // 
            sources.Append("CbB"); // 
            sources.Append("MpP"); // 
            sources.Append("WvV"); // 
            sources.Append("GfF"); // 

            StringBuilder destinations = new StringBuilder();
            destinations.Append("JjKkLlIi"); // 
            destinations.Append("MpP"); // 
            destinations.Append("WvV"); // 
            destinations.Append("GfF"); // 
            destinations.Append("CbB"); // 

            wrapper.ApplyMove(sources.ToString(), destinations.ToString());
        }
    }
}
