using HarmonyLib;
using System.Reflection;
using UnityEngine;
using Verse;

namespace PRV2E
{
    [StaticConstructorOnStartup]
    public class PRV2EMod : Mod
    {
        public static PRV2EMod Instance;

        // 缓存的设置实例，供运行时代码（如 HediffComp_Regression）实时读取
        internal PRV2ESettingsTab Settings;

        public PRV2EMod(ModContentPack content) : base(content)
        {
            Instance = this;

            // 预加载设置，确保 PatchOperationCheckSetting 应用时设置已就绪
            Settings = GetSettings<PRV2ESettingsTab>();

            // 初始化Harmony
            var harmony = new Harmony("tourswen.EndfieldPerlica");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public override string SettingsCategory()
        {
            return "RimVore2-pardon's-Extend (PRV2E)";
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Settings.DoSettingsWindowContents(inRect);
        }
    }
}
