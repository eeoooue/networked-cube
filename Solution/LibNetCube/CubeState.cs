using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibNetCube
{
    public class CubeState
    {
        public const int FaceWidth = 3;
        public const int FaceCount = 6;

        private Dictionary<CubeFace, int[,]> Faces = new Dictionary<CubeFace, int[,]>();

        public CubeState(CubeState state)
        {
            List<CubeFace> names = GetFaceNames();
            for (int i = 0; i < names.Count; i++)
            {
                CubeFace name = names[i];
                Faces[name] = state.ReadFace(name);
            }
        }

        public CubeState(List<int[,]> faces)
        {
            List<CubeFace> names = GetFaceNames();
            for(int i=0; i<names.Count; i++)
            {
                CubeFace name = names[i];
                Faces[name] = faces[i];
            }
        }

        public CubeState(byte[] payload)
        {
            List<CubeFace> names = GetFaceNames();
            foreach (CubeFace name in names)
            {
                Faces[name] = DeserializeFace(payload, name);
            }
        }

        public static List<CubeFace> GetFaceNames()
        {
            List<CubeFace> result = new List<CubeFace>()
            {
                CubeFace.Back,
                CubeFace.Bottom,
                CubeFace.Front,
                CubeFace.Left,
                CubeFace.Right,
                CubeFace.Top,
            };

            return result;
        }

        public int[,] GetFace(CubeFace face)
        {
            return Faces[face];
        }

        public int[,] ReadFace(CubeFace face)
        {
            int[,] result = new int[FaceWidth, FaceWidth];
            int[,] source = GetFace(face);
            for (int i = 0; i < FaceWidth; i++)
            {
                for (int j = 0; j < FaceWidth; j++)
                {
                    result[i, j] = source[i, j];
                }
            }

            return result;
        }

        public int GetPiece(CubeFace face, int i, int j)
        {
            int[,] values = GetFace(face);
            return values[i, j];
        }

        public void SetPiece(int value, CubeFace face, int i, int j)
        {
            int[,] values = GetFace(face);
            values[i, j] = value;
        }

        public byte[] Serialize()
        {
            int m = FaceCount;
            int n = FaceWidth * FaceWidth;
            byte[] result = new byte[m * n];

            List<CubeFace> names = GetFaceNames();
            for(int i=0; i<m; i++)
            {
                CubeFace name = names[i];
                byte[] bytes = SerializeFace(name);
                int p = i * n;
                for(int j=0; j<n; j++)
                {
                    result[p + j] = bytes[j];
                }
            }

            return result;
        }

        private byte[] SerializeFace(CubeFace face)
        {
            int[,] values = Faces[face];
            return SerializeFace(values);
        }

        public static byte[] SerializeFace(int[,] face)
        {
            byte[] result = new byte[FaceWidth * FaceWidth];

            int p = 0;
            for(int i=0; i<FaceWidth; i++)
            {
                for(int j=0; j<FaceWidth; j++)
                {
                    byte encoding = GetFaceEncoding(face[i, j]);
                    result[p++] = encoding;
                }
            }

            return result;
        }

        private static byte GetFaceEncoding(int value)
        {
            return (byte)value;
        }

        private static int[,] DeserializeFace(byte[] payload, CubeFace face)
        {
            int n = FaceWidth * FaceWidth;
            byte[] section = new byte[n];
            int p = (int)face * n;
            for(int j=0; j<n; j++)
            {
                section[j] = payload[p + j];
            }

            return DeserializeFace(section);
        }

        public static int[,] DeserializeFace(byte[] payload)
        {
            int[,] result = new int[FaceWidth, FaceWidth];
            int p = 0;

            for (int i = 0; i < FaceWidth; i++)
            {
                for (int j = 0; j < FaceWidth; j++)
                {
                    byte value = payload[p++];
                    result[i, j] = (int)value;
                }
            }

            return result;
        }
    }
}
