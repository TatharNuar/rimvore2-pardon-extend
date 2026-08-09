using System.Xml;
using Verse;

// match/nomatch 由 RimWorld 的 patch 加载器通过反射赋值，编译器无法感知
#pragma warning disable 0649

namespace PRV2E
{
    /// <summary>
    /// 根据 Mod 设置决定是否应用某个 Patch 操作。
    /// 玩家可以在游戏内 Mod 设置界面切换对应开关（需要重启游戏生效）。
    ///
    /// 用法示例：
    /// <![CDATA[
    /// <Operation Class="PRV2E.PatchOperationCheckSetting">
    ///     <settingName>EnableReformPatch</settingName>
    ///     <match Class="PatchOperationReplace">
    ///         <xpath>...</xpath>
    ///         <value>...</value>
    ///     </match>
    ///     <!-- nomatch 可选：设置关闭时执行；省略则关闭时不应用任何改动 -->
    /// </Operation>
    /// ]]>
    /// </summary>
    public class PatchOperationCheckSetting : PatchOperation
    {
        // 对应 PRV2ESettingsTab 中开关字段的名字
        public string settingName;

        // 由 RimWorld 的 patch 加载器按 XML 子节点名反射填充
        private PatchOperation match;
        private PatchOperation nomatch;

        protected override bool ApplyWorker(XmlDocument xml)
        {
            bool enabled = GetEnabled();
            if (enabled)
            {
                if (match != null)
                {
                    return match.Apply(xml);
                }
                return true;
            }
            // 设置关闭时：如果提供了 nomatch 则执行它，否则视为成功（不应用任何改动）
            if (nomatch != null)
            {
                return nomatch.Apply(xml);
            }
            return true;
        }

        public override void Complete(string modIdentifier)
        {
            if (match != null)
            {
                match.Complete(modIdentifier);
            }
            if (nomatch != null)
            {
                nomatch.Complete(modIdentifier);
            }
        }

        private bool GetEnabled()
        {
            PRV2ESettingsTab settings = LoadedModManager.GetMod<PRV2EMod>()?.GetSettings<PRV2ESettingsTab>();
            if (settings == null)
            {
                // 读取不到设置时保持默认行为（应用 patch）
                return true;
            }

            switch (settingName)
            {
                case nameof(PRV2ESettingsTab.EnableReformPatch):
                    return settings.EnableReformPatch;
                case nameof(PRV2ESettingsTab.EnableRegressionHediff):
                    return settings.EnableRegressionHediff;
                default:
                    // 未识别的设置名：默认应用（保持行为不变）
                    return true;
            }
        }
    }
}
