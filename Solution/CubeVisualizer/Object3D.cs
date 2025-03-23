using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace CubeVisualizer
{
    class Object3D
    {
        Model _Mesh;
        Color _Color;

        Vector3 _Position;
        Vector3 _Rotation;
        Vector3 _Scale;

        public Matrix World { get { return Matrix.CreateScale(_Scale) * Matrix.CreateFromYawPitchRoll(_Rotation.X, _Rotation.Y, _Rotation.Z)  * Matrix.CreateTranslation(_Position); } }

        public Object3D(Model pMesh, Color pColor, Vector3 pPosition, Vector3 pRotation, Vector3 pScale)
        {
            _Mesh = pMesh;
            _Color = pColor;
            _Position = pPosition;
            _Rotation = pRotation;
            _Scale = TranslateScaleToWorld(pScale);
        }

        public Object3D(Model pMesh, Color pColor) : this(pMesh, pColor, Vector3.Zero, Vector3.Zero, Vector3.One) { }

        public void Draw(Camera pCamera)
        {
            foreach (ModelMesh mesh in _Mesh.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects.Cast<BasicEffect>())
                {
                    effect.EnableDefaultLighting();
                    effect.TextureEnabled = false;

                    effect.World = World;
                    effect.View = pCamera.View;
                    effect.Projection = pCamera.Projection;
                    effect.DiffuseColor = _Color.ToVector3();
                }
                mesh.Draw();
            }
        }

        public void SetColor(Color pColor)
        {
            _Color = pColor;
        }

        public void SetPosition(Vector3 pPosition)
        {
            _Position = pPosition;
        }

        public void SetRotation(Vector3 pRotation)
        {
            _Rotation = pRotation;
        }

        public void SetScale(Vector3 pScale)
        {
            _Scale = TranslateScaleToWorld(pScale);
        }

        public void SetScale(float pScale)
        {
            _Scale = TranslateScaleToWorld(new Vector3(pScale, pScale, pScale));
        }

        // These methods are used to scale the object to the correct size
        private Vector3 TranslateScaleToWorld(Vector3 pScale)
        {
            BoundingBox boundingBox = CalculateBoundingBox(_Mesh);
            Vector3 modelSize = boundingBox.Max - boundingBox.Min;
            return pScale / modelSize;
        }

        static private BoundingBox CalculateBoundingBox(Model model)
        {
            BoundingBox boundingBox = new BoundingBox();
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                {
                    VertexBuffer vertexBuffer = meshPart.VertexBuffer;
                    Vector3[] vertices = new Vector3[vertexBuffer.VertexCount];
                    vertexBuffer.GetData(vertices);
                    boundingBox = BoundingBox.CreateMerged(boundingBox, BoundingBox.CreateFromPoints(vertices));
                }
            }
            return boundingBox;
        }
    }
}
