using System.Reflection;
using HarmonyLib;
using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(NoDetailsForClienters.NoDetailsForClientersMod), NoDetailsForClienters.BuildInfo.name, NoDetailsForClienters.BuildInfo.version, NoDetailsForClienters.BuildInfo.authors, NoDetailsForClienters.BuildInfo.downloadLink)]
[assembly: MelonGame("VRChat", "VRChat")]
namespace NoDetailsForClienters
{

	public static class BuildInfo
	{
		public const string authors = "ljoonal";

		public const string company = null;

		public const string downloadLink = "https://vrc.ljoonal.xyz";

		public const string name = "No Details For Clienters";

		public const string version = "0.1.1";
	}

	public class NoDetailsForClientersMod : MelonMod
	{
		private static MelonPreferences_Category PreferencesCategory;
		private static MelonPreferences_Entry<float> PreferenceFPS, PreferenceFPSVariance;
		private static MelonPreferences_Entry<int> PreferencePing, PreferencePingVariance, PreferencVarianceMin, PreferencVarianceMax;

		// A value to be added to the configured FPS spoof value
		private static float VarianceFPS = 0f;
		// A value to be added to the configured ping spoof value
		private static int VariancePing = 0;
		// Time for when next variance update can be run after.
		private System.DateTime _next_variance_update_after = System.DateTime.Now;

		private const string PreferencesIdentifier = "NoDetailsForClienters";

		public override void OnApplicationStart()
		{
			// Preferences setup
			PreferencesCategory = MelonPreferences.CreateCategory(PreferencesIdentifier, BuildInfo.name);
			PreferenceFPS = PreferencesCategory.CreateEntry("SpoofFPS", -1f, "FPS to spoof to (disable with < 0)");
			PreferencePing = PreferencesCategory.CreateEntry("SpoofPing", -1, "Ping to spoof to (disable with < 0)");
			PreferenceFPSVariance = PreferencesCategory.CreateEntry("SpoofFPSVariance", 0f, "Max random addition to spoofed FPS (disable with <= 0)");
			PreferencePingVariance = PreferencesCategory.CreateEntry("SpoofPingVariance", 0, "Max random addition to spoofed ping (disable with <= 0");
			PreferencVarianceMin = PreferencesCategory.CreateEntry("VarianceMinInterval", 1000, "Min interval variance");
			PreferencVarianceMax = PreferencesCategory.CreateEntry("VarianceMaxInterval", 2000, "Max interval variance");

			var patchingSuccess = true;

			try // Patch `UnityEngine.Time.smoothDeltaTime` to use our Harmony PatchFPS Prefix
			{
				HarmonyInstance.Patch(
					typeof(Time).GetProperty("smoothDeltaTime").GetGetMethod(),
					prefix: new HarmonyMethod(typeof(NoDetailsForClientersMod).GetMethod("PatchFPS", BindingFlags.NonPublic | BindingFlags.Static))
				);
			}
			catch (System.Exception ex)
			{
				patchingSuccess = false;
				LoggerInstance.Error($"Failed to patch FPS: {ex}");
			}

			try // Patch `ExitGames.Client.Photon.PhotonPeer.RoundTripTime` to use our Harmony PatchPing Prefix
			{
				HarmonyInstance.Patch(
					typeof(ExitGames.Client.Photon.PhotonPeer).GetProperty("RoundTripTime").GetGetMethod(),
					prefix: new HarmonyMethod(typeof(NoDetailsForClientersMod).GetMethod("PatchPing", BindingFlags.NonPublic | BindingFlags.Static))
				);
			}
			catch (System.Exception ex)
			{
				patchingSuccess = false;
				LoggerInstance.Error($"Failed to patch ping: {ex}");
			}

			if (patchingSuccess) LoggerInstance.Msg("Applied successfully.");
		}

		// No need to run variance updates on so often as Update, so using OnFixedUpdate
		public override void OnFixedUpdate()
		{
			// Only run variance every so often so that it doesn't jitter so much in an obvious way.
			if (_next_variance_update_after < System.DateTime.Now)
			{
				var rng = new System.Random();
				_next_variance_update_after =
					System.DateTime.Now.AddMilliseconds(rng.Next(PreferencVarianceMin.Value, PreferencVarianceMax.Value));

				if (PreferenceFPSVariance.Value <= 0) VarianceFPS = 0f;
				else VarianceFPS = (PreferenceFPSVariance.Value) * (float)rng.NextDouble();

				if (PreferencePingVariance.Value <= 0) VariancePing = 0;
				else VariancePing = rng.Next(0, PreferencePingVariance.Value);
			}
		}

		// The patch for spoofing FPS
		private static bool PatchFPS(ref float __result)
		{
			// Run original getter if spoofing is disabled
			if (PreferenceFPS.Value < 0) return true;
			// Otherwise use our value and don't run original getter.
			__result = 1f / (PreferenceFPS.Value + VarianceFPS);
			return false;
		}

		// The patch for spoofing ping
		private static bool PatchPing(ref int __result)
		{
			// Run original getter if spoofing is disabled
			if (PreferencePing.Value < 0) return true;
			// Otherwise use our value and don't run original getter.
			__result = PreferencePing.Value + VariancePing;
			return false;
		}
	}
}
