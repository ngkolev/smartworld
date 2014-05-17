using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWorld.Core
{
    public interface IElement
    {
        Vector Position { get; }
        double Radius { get; }
    }
}
