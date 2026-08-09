using RimWorld;
using UnityEngine;
using Verse;

namespace PRV2E
{
    /// <summary>
    /// PRV2E 的 Mod 设置。
    /// 玩家可以在游戏内 Mod 设置界面调整 VoreStagePatch 等开关。
    /// 注意：Patch 在游戏启动时应用，修改这些开关后需要重启游戏才会生效。
    /// </summary>
    internal class PRV2ESettingsTab : ModSettings
    {
       
        public bool EnableReformPatch = true;
        public bool EnableRegressionHediff = true;

        /// <summary>逆生长 (Regression) 强度倍率，1 = 原版强度</summary>
        public float RegressionStrength = 30f;

        public override void ExposeData()
        {
            base.ExposeData();
            
            Scribe_Values.Look(ref EnableReformPatch, "EnableReformPatch", true);
            Scribe_Values.Look(ref EnableRegressionHediff, "EnableRegressionHediff", true);
            Scribe_Values.Look(ref RegressionStrength, "RegressionStrength", 30f);
        }

        public void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(inRect);

            listing.Label("以下开关控制 VoreStagePatch 是否生效。");
            listing.Label("修改后需要<color=#FF7F7F> 重启游戏 </color>才会生效。");
            listing.Gap();

            
            listing.CheckboxLabeled("启用 替换重塑机制", ref EnableReformPatch);
            listing.CheckboxLabeled("启用 子宫年龄衰退", ref EnableRegressionHediff);

            listing.Gap();
            listing.Label("逆生长强度倍率（调整后实时生效）");
            RegressionStrength = listing.SliderLabeled("逆生长强度倍率", RegressionStrength, 1, 120, 2, "0");

            listing.End();
        }
    }
}
