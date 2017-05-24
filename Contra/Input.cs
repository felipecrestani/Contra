using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Contra
{
    public class Input
    {
        public Keys Left { get; set; }
        public Keys Right { get; set; }
        public Keys Up { get; set; }
        public Keys Down { get; set; }

        public Keys Jump { get; set; }
        public Keys Shoot { get; set; }
    }
}
