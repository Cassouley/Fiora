using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace Fiora
{
    class Program
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        public static Spell.Skillshot Q;
        public static Spell.Skillshot W;
        public static Spell.Active E;
        public static Spell.Targeted R;
        public static Menu TeemoMenu, ComboMenu, HarassMenu, FarmMenu, FleeMenu;

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            TargetSelector2.init();
            Bootstrap.Init(null);

            Q = new Spell.Skillshot(SpellSlot.Q, 600, SkillShotType.Linear, 500, 1500, 75);
            W = new Spell.Skillshot(SpellSlot.W, 750, SkillShotType.Linear, 500, 1500, 75);
            E = new Spell.Active(SpellSlot.E);
            R = new Spell.Targeted(SpellSlot.R, 400);

            TeemoMenu = MainMenu.AddMenu("Fiora", "Fiora");
            TeemoMenu.AddGroupLabel("Fiora");
            TeemoMenu.AddSeparator();
            TeemoMenu.AddLabel("Always Remember, Buck Frandon.");
            TeemoMenu.AddLabel("Made By Fluxy");

            ComboMenu = TeemoMenu.AddSubMenu("Combo", "Combo");
            ComboMenu.AddGroupLabel("Combo Settings");
            ComboMenu.AddSeparator();
            ComboMenu.Add("useQCombo", new CheckBox("Use Q"));
            ComboMenu.Add("useECombo", new CheckBox("Use E"));
            ComboMenu.Add("useWCombo", new CheckBox("Use W"));
            ComboMenu.Add("useRCombo", new CheckBox("Use R"));

            HarassMenu = TeemoMenu.AddSubMenu("Harass", "Harass");
            HarassMenu.AddGroupLabel("Harass Settings");
            HarassMenu.AddSeparator();
            HarassMenu.Add("useQHarass", new CheckBox("Use Q"));
            HarassMenu.Add("useWHarass", new CheckBox("Use W"));

            FarmMenu = TeemoMenu.AddSubMenu("Farm", "Farm");
            FarmMenu.AddGroupLabel("Farming Settings");
            FarmMenu.AddSeparator();
            FarmMenu.Add("useQFarmLH", new CheckBox("Use Q for LastHit"));
            FarmMenu.Add("useQFarmWC", new CheckBox("Use Q for WaveClear"));

            FleeMenu = TeemoMenu.AddSubMenu("Flee", "Flee");
            FleeMenu.AddGroupLabel("Flee Settings");
            FleeMenu.AddSeparator();
            FleeMenu.Add("useRFlee", new CheckBox("Use R"));
            FleeMenu.Add("useWFlee", new CheckBox("Use W"));

            Game.OnTick += Game_OnTick;
        }

        private static void Game_OnTick(EventArgs args)
        {
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                StateHandler.Combo();
            }
            else if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
            {
                StateHandler.Harass();
            }
            else if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                StateHandler.WaveClear();
            }
            else if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit))
            {
                StateHandler.LastHit();
            }
            else if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
            {
                StateHandler.Flee();
            }
        }
    }
}
