using System.Collections.Generic;
using MelonLoader;
using UnityEngine;

namespace TrustColorChanger
{
	public static class Config
	{
		public static bool TrustColorsEnabled { get => trustColorsEnabled.Value; }

		public static Color GetPreference(this TrustColoringRank rank)
		{
			return trustColorPrefs[rank].Value;
		}

		public static Color GetOriginal(this TrustColoringRank rank)
		{
			return originalColors[rank];
		}

		private const string preferencesIdentifier = "TrustColorChanger";
		private static MelonPreferences_Category preferencesCategory;
		private static MelonPreferences_Entry<bool> trustColorsEnabled;
		private static readonly Dictionary<TrustColoringRank, MelonPreferences_Entry<Color>> trustColorPrefs = new();
		private static readonly Dictionary<TrustColoringRank, Color> originalColors = new();

		// Try to grab the original colors as early as possible to not have other mods overwrite them.
		public static void Init()
		{
			foreach (TrustColoringRank rank in System.Enum.GetValues(typeof(TrustColoringRank)))
			{
				originalColors.Add(rank, rank.GetVRCPlayer());
			}
		}

		public static void OnApplicationStart()
		{
			preferencesCategory = MelonPreferences.CreateCategory(preferencesIdentifier, BuildInfo.name);
			trustColorsEnabled = preferencesCategory.CreateEntry("TrustColorsEnabled", true, "Enable trust color changes");

			foreach (TrustColoringRank rank in System.Enum.GetValues(typeof(TrustColoringRank)))
			{
				trustColorPrefs.Add(rank,
					preferencesCategory.CreateEntry(
						$"{rank}Color", rank.GetOriginal(), $"Color for {rank}"
					)
				);
			}
		}
	}
}
