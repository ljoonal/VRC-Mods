using System.Linq;
using System.Reflection;
using HarmonyLib;
using MelonLoader;
using UnityEngine;
using VRC;

[assembly: MelonInfo(typeof(BreakYoutubeDL.BreakYoutubeDLMod), BreakYoutubeDL.BuildInfo.name, BreakYoutubeDL.BuildInfo.version, BreakYoutubeDL.BuildInfo.authors, BreakYoutubeDL.BuildInfo.downloadLink)]
[assembly: MelonGame("VRChat", "VRChat")]
namespace BreakYoutubeDL
{

	public static class BuildInfo
	{
		public const string authors = "ljoonal";

		public const string company = null;

		public const string downloadLink = "https://vrc.ljoonal.xyz";

		public const string name = "BreakYoutubeDL";

		public const string version = "0.0.1";
	}

	public class BreakYoutubeDLMod : MelonMod
	{
		public override void OnApplicationStart()
		{

			MethodInfo[] youtubeDlMethods = typeof(YoutubeDL).GetMethods()
				.Where(mb => mb.Name.StartsWith("Method_Private_Void_"))
				.ToArray();

			HarmonyMethod breakMethod = new HarmonyMethod(typeof(BreakYoutubeDLMod)
				.GetMethod(nameof(NeverRun), BindingFlags.NonPublic | BindingFlags.Static));

			foreach (var youtubeDlMethod in youtubeDlMethods)
			{
				HarmonyInstance.Patch(youtubeDlMethod, prefix: breakMethod);
			}
		}

		private static bool NeverRun()
		{
			return false;
		}
	}
}
