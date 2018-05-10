using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSModUI
{
    public class ModGui
    {
        /**
         * Mod extends ModGui with name/ver
         *
         * Setup adds new button to ModMenu UI
         * - 2 modes:
         *
         *      - Simple on/off toggle for mod
         *
         *      - New page with custom view controller ("advanced mode")
         */
        public string ModName { get; set; }
        public string ModVersion { get; set; }

        public ModGui()
        {
        }

        // Default for toggle box
        public void SetupUi(string modName, string pluginVersion)
        {

        }

        // Override for "advanced mode"
        public void SetupUi(string modName, string pluginVersion, CustomViewController customView)
        {

        }
    }
}
