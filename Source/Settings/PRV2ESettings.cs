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
        public bool EnableFasterRecover = true;

        public float RegressionStrength = 30f;

        public override void ExposeData()
        {
            base.ExposeData();
            
            Scribe_Values.Look(ref EnableReformPatch, "PRV2E_EnableReformPatch", true);
            Scribe_Values.Look(ref EnableRegressionHediff, "PRV2E_EnableRegressionHediff", true);
            Scribe_Values.Look(ref EnableFasterRecover, "PRV2E_EnableFasterRecover", true);

            Scribe_Values.Look(ref RegressionStrength, "PRV2E_RegressionStrength", 30f);
        }

        public void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(inRect);

            listing.Label("PRV2E.Settings.Heading".Translate());
            listing.Label("PRV2E.Settings.RestartNote".Translate());
            listing.Gap();

            listing.CheckboxLabeled("PRV2E.Settings.EnableReformPatch".Translate(), ref EnableReformPatch);
            listing.CheckboxLabeled("PRV2E.Settings.EnableRegressionHediff".Translate(), ref EnableRegressionHediff);
            listing.CheckboxLabeled("PRV2E.Settings.EnableFasterRecover".Translate(), ref EnableFasterRecover);
            //listing.CheckboxLabeled("PRV2E.Settings.EnableReplacedNutrition".Translate(), ref PRV2E.EnableReplacedNutrition);

            listing.Gap();
            listing.Label("PRV2E.Settings.RegressionSlider".Translate() + ": " + RegressionStrength.ToString("0"));
            float newRegression = listing.Slider(RegressionStrength, 1f, 120f);
            RegressionStrength = Mathf.Round(newRegression);

            listing.End();
        }
    }
}
