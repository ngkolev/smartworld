using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SmartWorld.UI.ViewModel
{
    public class ElementViewModel
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Radius { get; set; }
        public Brush Color { get; set; }
    }
}
