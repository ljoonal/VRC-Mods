using MelonLoader;
using UnityEngine;
//using UIExpansionKit.API;

[assembly: MelonInfo(typeof(TrustColorChanger.TrustColorChangerMod), TrustColorChanger.BuildInfo.name, TrustColorChanger.BuildInfo.version, TrustColorChanger.BuildInfo.authors, TrustColorChanger.BuildInfo.downloadLink)]
[assembly: MelonGame("VRChat", "VRChat")]
[assembly: MelonPriority(-10)]
//[assembly: MelonOptionalDependencies("UIExpansionKit")]
namespace TrustColorChanger
{

	public static class BuildInfo
	{
		public const string authors = "ljoonal";

		public const string company = null;

		public const string downloadLink = "https://vrc.ljoonal.xyz";

		public const string name = "Trust Color Changer";

		public const string version = "0.2.0";
	}

	public class TrustColorChangerMod : MelonMod
	{
		// To track when disabling should restore colors, in case someone disables this mod and has other color modifying mods.
		private bool alreadyTouchedColors = false;

		// Try to grab the original colors as early as possible to not have other mods overwrite them.
		static TrustColorChangerMod() => Config.Init();
		public override void OnApplicationStart() => Config.OnApplicationStart();

		// Because frigging emm loves to override the values even if the color changing is disabled, cannot use OnApplicationStart
		public override void OnSceneWasInitialized(int buildIndex, string sceneName) => UpdateColors();
		public override void OnPreferencesSaved() => UpdateColors();

		private void UpdateColors()
		{
			if (Config.TrustColorsEnabled) PatchColors();
			else if (alreadyTouchedColors) RestoreColors();
		}

		private void PatchColors()
		{
			alreadyTouchedColors = true;
			foreach (TrustColoringRank rank in System.Enum.GetValues(typeof(TrustColoringRank)))
			{
				rank.SetVRCPlayer(rank.GetPreference());
			}
		}

		private void RestoreColors()
		{
			foreach (TrustColoringRank rank in System.Enum.GetValues(typeof(TrustColoringRank)))
			{
				rank.SetVRCPlayer(rank.GetOriginal());
			}
		}
	}
}
