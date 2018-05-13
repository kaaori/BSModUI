using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRUI;
using IllusionPlugin;

namespace BSModUI
{
   public interface ModGui : IPlugin
    {
        string Author { get; }
        string Image { get;}
       // VRUIViewController CustomViewController { get; set; }
        bool isEnabled { get; }
    }
   
    
}
