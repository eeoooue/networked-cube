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
            sources.Append("BbCcDdAa"); // rotated top ring
            sources.Append("MmN"); // left row becomes back row
            sources.Append("QqR"); // front row becomes left row
            sources.Append("EeF"); // right row becomes front row
            sources.Append("IiJ"); // back row becomes right row

            wrapper.ApplyMove(sources.ToString(), destinations.ToString());
        }


        public static void RotateFrontFaceClockwise(CubeState state)
        {
            PochmanWrapper wrapper = new PochmanWrapper(state);

            StringBuilder sources = new StringBuilder();
            sources.Append("EeFfGgHh"); // 
            sources.Append("DdC"); // 
            sources.Append("IlJ"); // 
            sources.Append("VuU"); // 
            sources.Append("SrR"); // 

            StringBuilder destinations = new StringBuilder();
            sources.Append("FfGgHhEe"); // 
            sources.Append("IlJ"); // 
            sources.Append("VuU"); // 
            sources.Append("SrR"); // 
            sources.Append("DdC"); // 

            wrapper.ApplyMove(sources.ToString(), destinations.ToString());
        }
    }
}
