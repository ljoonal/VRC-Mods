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

		public const string version = "0.1.1";
	}

	public class TrustColorChangerMod : MelonMod
	{
		private const string preferencesIdentifier = "TrustColorChanger";
		private static MelonPreferences_Category preferencesCategory;
		private static MelonPreferences_Entry<bool> enabled;
		public static MelonPreferences_Entry<Color> nuisanceColor, visitorColor, newUserColor, userColor, knownUserColor, trustedUserColor, friendColor, staffColor;
		public static Color ogNuisanceColor, ogVisitorColor, ogNewUserColor, ogUserColor, ogKnownUserColor, ogTrustedUserColor, ogFriendColor, ogStaffColor;
		// To track when disabling should restore colors, in case someone disables this mod and has other color modifying mods.
		private bool alreadyTouchedColors = false;

		// Try to grab the original colors as early as possible to not have other mods overwrite them.
		static TrustColorChangerMod()
		{
			// Unmapped/Unused
			// field_Internal_Static_Color_7 Yellow.... Friends again? Maybe veteran?
			// field_Internal_Static_Color_9 Transparent Gray.... Background/offline?
			ogNuisanceColor = VRCPlayer.field_Internal_Static_Color_8;
			ogVisitorColor = VRCPlayer.field_Internal_Static_Color_2;
			ogNewUserColor = VRCPlayer.field_Internal_Static_Color_3;
			ogUserColor = VRCPlayer.field_Internal_Static_Color_4;
			ogKnownUserColor = VRCPlayer.field_Internal_Static_Color_5;
			ogTrustedUserColor = VRCPlayer.field_Internal_Static_Color_6;
			ogFriendColor = VRCPlayer.field_Internal_Static_Color_1;
			ogStaffColor = VRCPlayer.field_Internal_Static_Color_0;
		}

		public override void OnApplicationStart()
		{
			preferencesCategory = MelonPreferences.CreateCategory(preferencesIdentifier, TrustColorChanger.BuildInfo.name);
			enabled = preferencesCategory.CreateEntry("Enabled", true, "Enabled (restart for full disabling)");
			nuisanceColor = preferencesCategory.CreateEntry("NuisanceColor", ogNuisanceColor, "Color for nuisances");
			visitorColor = preferencesCategory.CreateEntry("VisitorColor", ogVisitorColor, "Color for visitors");
			newUserColor = preferencesCategory.CreateEntry("NewUserColor", ogNewUserColor, "Color for new users");
			userColor = preferencesCategory.CreateEntry("UserColor", ogUserColor, "Color for users");
			knownUserColor = preferencesCategory.CreateEntry("KnownUserColor", ogKnownUserColor, "Color for known users");
			trustedUserColor = preferencesCategory.CreateEntry("TrustedUserColor", ogTrustedUserColor, "Color for trusted users");
			friendColor = preferencesCategory.CreateEntry("FriendColor", ogFriendColor, "Color for friends");
			staffColor = preferencesCategory.CreateEntry("StaffColor", ogStaffColor, "Color for staff");
		}

		// Because frigging emm loves to override the values even if the color changing is disabled, cannot use OnApplicationStart
		public override void OnSceneWasInitialized(int buildIndex, string sceneName)
		{
			updateColors();
		}

		private void updateColors()
		{
			if (enabled.Value) patchColors();
			else if (alreadyTouchedColors) restoreColors();
		}

		public override void OnPreferencesSaved()
		{
			updateColors();
		}

		private void patchColors()
		{
			alreadyTouchedColors = true;
			VRCPlayer.field_Internal_Static_Color_0 = staffColor.Value;
			VRCPlayer.field_Internal_Static_Color_1 = friendColor.Value;
			VRCPlayer.field_Internal_Static_Color_2 = visitorColor.Value;
			VRCPlayer.field_Internal_Static_Color_3 = newUserColor.Value;
			VRCPlayer.field_Internal_Static_Color_4 = userColor.Value;
			VRCPlayer.field_Internal_Static_Color_5 = knownUserColor.Value;
			VRCPlayer.field_Internal_Static_Color_6 = trustedUserColor.Value;
			VRCPlayer.field_Internal_Static_Color_8 = nuisanceColor.Value;
		}

		private void restoreColors()
		{
			VRCPlayer.field_Internal_Static_Color_0 = ogStaffColor;
			VRCPlayer.field_Internal_Static_Color_1 = ogFriendColor;
			VRCPlayer.field_Internal_Static_Color_2 = ogVisitorColor;
			VRCPlayer.field_Internal_Static_Color_3 = ogNewUserColor;
			VRCPlayer.field_Internal_Static_Color_4 = ogUserColor;
			VRCPlayer.field_Internal_Static_Color_5 = ogKnownUserColor;
			VRCPlayer.field_Internal_Static_Color_6 = ogTrustedUserColor;
			VRCPlayer.field_Internal_Static_Color_8 = ogNuisanceColor;
		}

		/* TODO; Re-evaluate UIX Color settings support in the mod if it doesn't get added to UIX after a while
				public void UIManagerInit()
				{
					ICustomLayoutedMenu settingsCategory = ExpansionKitApi.GetSettingsCategory(preferencesIdentifier);
				}
		*/
	}
}
