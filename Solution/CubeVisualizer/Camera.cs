using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CubeVisualizer
{
    class Camera
    {
        public Vector3 Position { get; set; }
        private Vector3 _Target;

        private float _AspectRatio;
        private float _FieldOfView;

        private Vector3 _UpDirection;

        public Matrix View { get { return Matrix.CreateLookAt(Position, _Target, _UpDirection); } }
        public Matrix Projection { get { return Matrix.CreatePerspectiveFieldOfView(_FieldOfView, _AspectRatio, 0.1f, 100f); } }

        public Camera(Vector3 pPos, Vector3 pTarget, float pAspect, float pFOV)
        {
            Position = pPos;
            _Target = pTarget;
            _AspectRatio = pAspect;
            _FieldOfView = pFOV;
            _UpDirection = Vector3.UnitY;
        }

        public void FlipUpDirection() { _UpDirection = -1.0f * _UpDirection; }
    }
}
