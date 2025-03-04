using LibNetCube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace FaceViewer.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        public FaceViewModel Face { get; set; }
        public string FaceName { get { return Faces[FaceIndex]; } }

        public ICommand RotateFace { get; }

        private int FaceIndex = 0;

        private List<string> Faces = ["Front", "Right", "Back", "Left", "Top", "Bottom"];

        public MainViewModel()
        {
            CubeFace faceVal = GetFaceEnum();
            Face = new FaceViewModel(faceVal);
            RotateFace = new RelayCommand(RotateFaceDisplayed);
        }

        public void UpdateFace()
        {
            Face.Face = GetFaceEnum();
            Face.Update();
        }

        public CubeFace GetFaceEnum()
        {
            switch (FaceName)
            {
                case "Front":
                    return CubeFace.Front;
                case "Right":
                    return CubeFace.Right;
                case "Back":
                    return CubeFace.Back;
                case "Left":
                    return CubeFace.Left;
                case "Top":
                    return CubeFace.Top;
                default:
                    return CubeFace.Bottom;
            }
        }

        public void RotateFaceDisplayed()
        {
            FaceIndex = (FaceIndex + 1) % Faces.Count;
            UpdateFace();
            OnPropertyChanged("FaceName");
        }
    }
}
