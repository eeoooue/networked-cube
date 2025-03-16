using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibNetCube
{
    internal static class CubeRotation
    {
        public static void RotateTopFaceClockwise(CubeState state)
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

        public static void RotateFrontFaceClockwise(CubeState state)
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

        public static void RotateBottomFaceClockwise(CubeState state)
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
    }
}
