using UnityEngine;
using System.Collections;

public class GameConsts {

	// Play Games plugin debug logs enabled?
	public const bool PlayGamesDebugLogsEnabled = true;

	public const float AimInitialSpeed = 1.2f;
	public const float AimLevelSpeedIncreaser = 0.2f;
	public const float AimFollowingDuckIncreaser = 0.4f;

	public const float DuckInitialSpeed = 1f;
	public const float DuckLevelSpeedIncrease = 0.2f;
	public const float DuckSpeedIncrease = 0.5f;
	
	public static float LevelTime = 20.0f;	// secconds
	public static int[] LevelUps = { 20, 40, 80, 120 };
	public static float TimeBeforeAim = 2.0f;	// secconds

}
