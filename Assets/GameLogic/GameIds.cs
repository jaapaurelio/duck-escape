using UnityEngine;
using System.Collections;

public class GameIds {
	// Achievements IDs (as given by Developer Console)
	public class Achievements {
		public const string NotADisaster = "PLACEHOLDER";
		public const string PointBlank = "PLACEHOLDER";
		public const string FullCombo = "PLACEHOLDER";
		public const string ClearAllLevels = "PLACEHOLDER";
		public const string PerfectAccuracy = "PLACEHOLDER";
		
		public static string[] ForRank = {
			"PLACEHOLDER",
			"PLACEHOLDER",
			"PLACEHOLDER"
		};
		public static int[] RankRequired = { 3, 6, 10 };
		
		public static string[] ForTotalStars = {
			"PLACEHOLDER",
			"PLACEHOLDER",
			"PLACEHOLDER"
		};
		public static int[] TotalStarsRequired = { 12, 24, 36 };
		
		// incrementals:
		public static string[] IncGameplaySeconds = {
			"PLACEHOLDER",
			"PLACEHOLDER",
			"PLACEHOLDER"
		};
		public static string[] IncGameplayRounds = {
			"PLACEHOLDER",
			"PLACEHOLDER",
			"PLACEHOLDER"
		};
	}
	
	// Leaderboard ID (as given by Developer Console)
	public static string LeaderboardId = "CgkIrPzUj8oPEAIQBg";
}
