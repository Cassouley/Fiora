﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace Fiora
{
    class StateHandler
    {
        public static AIHeroClient _Player { get { return ObjectManager.Player; } }
        public static float GetDynamicRange()
        {
            if (Program.Q.IsReady())
            {
                return Program.Q.Range;
            }
            return _Player.GetAutoAttackRange();
        }
        public static void Combo()
        {
            var target = TargetSelector2.GetTarget(GetDynamicRange() + 100, DamageType.Magical);
            if (target == null) return;

            if (Program.ComboMenu["useQCombo"].Cast<CheckBox>().CurrentValue && Program.Q.IsReady() && target.IsValidTarget(Program.Q.Range))
            {
                Program.Q.Cast(target);
            }
            else if (Program.ComboMenu["useECombo"].Cast<CheckBox>().CurrentValue && Program.E.IsReady() && target.IsValidTarget(Program.E.Range))
            {
                Program.E.Cast();
            }
            else if(Program.ComboMenu["useWCombo"].Cast<CheckBox>().CurrentValue && Program.W.IsReady() && target.IsValidTarget(Program.W.Range))
            {
                Program.W.Cast(target);
            }
            else if (Program.ComboMenu["useRCombo"].Cast<CheckBox>().CurrentValue && Program.R.IsReady() && target.IsValidTarget(Program.R.Range))
            {
                Program.R.Cast(target);
            }
        }

        public static void Harass()
        {
            var target = TargetSelector2.GetTarget(GetDynamicRange() + 100, DamageType.Magical);
            if (target == null) return;

            if (Program.HarassMenu["useQHarass"].Cast<CheckBox>().CurrentValue && Program.Q.IsReady() && target.IsValidTarget(Program.Q.Range))
            {
                Program.Q.Cast(target);
            }
            else if (Program.HarassMenu["useWHarass"].Cast<CheckBox>().CurrentValue && Program.W.IsReady() && target.Distance(_Player) > _Player.GetAutoAttackRange(target))
            {
                Program.W.Cast(target);
            }
        }

        public static void LastHit()
        {
            if (Program.FarmMenu["useQFarmLH"].Cast<CheckBox>().CurrentValue && Program.Q.IsReady())
            {
                var minion = ObjectManager.Get<Obj_AI_Minion>().FirstOrDefault(a => a.IsEnemy && a.Health <= QDamage(a));
                if (minion == null) return;
                Program.Q.Cast(minion);
            }
        }

        public static void WaveClear()
        {
            if (Program.FarmMenu["useQFarmWC"].Cast<CheckBox>().CurrentValue && Program.Q.IsReady())
            {
                var minion = ObjectManager.Get<Obj_AI_Minion>().FirstOrDefault(a => a.IsEnemy && a.Health <= QDamage(a));
                if (minion == null) return;
                Program.Q.Cast(minion);
            }
        }

        public static void Flee()
        {
            if (Program.FleeMenu["useRFlee"].Cast<CheckBox>().CurrentValue && Program.R.IsReady())
            {
                Program.R.Cast(_Player.Position);
            }
            if (Program.FleeMenu["useWFlee"].Cast<CheckBox>().CurrentValue && Program.W.IsReady())
            {
                Program.W.Cast();
            }
        }

        public static float QDamage(Obj_AI_Base target)
        {
            return _Player.CalculateDamageOnUnit(target, DamageType.Magical,
                (float) (new[] {80, 125, 170, 215, 260}[Program.Q.Level] + 0.8*_Player.FlatMagicDamageMod));
        }
    }
}
