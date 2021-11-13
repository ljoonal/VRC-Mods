using UnityEngine;
namespace TrustColorChanger
{
	public enum TrustColoringRank
	{
		Staff,
		Friend,
		TrustedUser,
		KnownUser,
		User,
		NewUser,
		Visitor,
		Nuisance
	}

	static class TrustColorRankUtils
	{

		/*
		field_Internal_Static_Color_7 Yellow.... Friends again? Maybe veteran?
		field_Internal_Static_Color_9 Transparent Gray.... Background/offline?
		*/


		public static void SetVRCPlayer(this TrustColoringRank rank, Color value)
		{
			switch (rank)
			{
				case TrustColoringRank.Staff:
					VRCPlayer.field_Internal_Static_Color_0 = value;
					break;
				case TrustColoringRank.Friend:
					VRCPlayer.field_Internal_Static_Color_1 = value;
					break;
				case TrustColoringRank.TrustedUser:
					VRCPlayer.field_Internal_Static_Color_6 = value;
					break;
				case TrustColoringRank.KnownUser:
					VRCPlayer.field_Internal_Static_Color_5 = value;
					break;
				case TrustColoringRank.User:
					VRCPlayer.field_Internal_Static_Color_4 = value;
					break;
				case TrustColoringRank.NewUser:
					VRCPlayer.field_Internal_Static_Color_3 = value;
					break;
				case TrustColoringRank.Visitor:
					VRCPlayer.field_Internal_Static_Color_2 = value;
					break;
				case TrustColoringRank.Nuisance:
					VRCPlayer.field_Internal_Static_Color_8 = value;
					break;
				default:
					throw new System.Exception("Invalid trust rank!");
			}
		}

		public static Color GetVRCPlayer(this TrustColoringRank rank)
		{
			return rank switch
			{
				TrustColoringRank.Staff => VRCPlayer.field_Internal_Static_Color_0,
				TrustColoringRank.Friend => VRCPlayer.field_Internal_Static_Color_1,
				TrustColoringRank.TrustedUser => VRCPlayer.field_Internal_Static_Color_6,
				TrustColoringRank.KnownUser => VRCPlayer.field_Internal_Static_Color_5,
				TrustColoringRank.User => VRCPlayer.field_Internal_Static_Color_4,
				TrustColoringRank.NewUser => VRCPlayer.field_Internal_Static_Color_3,
				TrustColoringRank.Visitor => VRCPlayer.field_Internal_Static_Color_2,
				TrustColoringRank.Nuisance => VRCPlayer.field_Internal_Static_Color_8,
				_ => throw new System.Exception("Invalid trust rank!"),
			};
		}
	}
}
