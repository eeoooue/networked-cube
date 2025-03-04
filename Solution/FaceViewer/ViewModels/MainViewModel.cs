using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace FaceViewer.ViewModels
{
    internal class MainViewModel
    {
        public FaceViewModel Face { get; set; }

        public MainViewModel()
        {
            Face = new FaceViewModel();
        }

        public void UpdateFace()
        {
            Face.Update();
        }
    }
}
