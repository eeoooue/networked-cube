using LibNetCube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceViewer.ViewModels
{
    internal class FaceViewModel
    {
        public CubeState? CubeState;

        public CubeFace Face;




        public CubeState? TryGetCubeState()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            TryGetCubeState();
        }

        public int GetFacePiece(int i, int j)
        {
            return 0;
        }

    }
}
