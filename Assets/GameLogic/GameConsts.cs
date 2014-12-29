using UnityEngine;
using System.Collections;

public class GameConsts {

	// Play Games plugin debug logs enabled?
	public const bool PlayGamesDebugLogsEnabled = false;

	public const float AimInitialSpeed = 1.5f;
	public const float AimLevelSpeedIncreaser = 0.3f;
	public const float AimFollowingDuckIncreaser = 0.4f;
	public const float MinRamdomDirection = 0.1f;
	public const float MaxRamdomDirection = 0.4f;

	public const float DuckInitialSpeed = 1.3f;
	public const float DuckLevelSpeedIncrease = 0.3f;
	public const float DuckSpeedIncrease = 0.5f;
	
	public static float LevelTime = 20.0f;	// secconds
	public static int[] LevelUps = { 20, 40, 60, 100, 160, 260 };	// secconds
	public static float TimeBeforeAim = 2.0f;	// secconds


}
