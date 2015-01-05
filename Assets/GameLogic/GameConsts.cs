using UnityEngine;
using System.Collections;

public class GameConsts {

	// Play Games plugin debug logs enabled?
	public const bool PlayGamesDebugLogsEnabled = true;

	public const float AimInitialSpeed = 1.5f;
	public const float AimLevelSpeedIncreaser = 0.25f;
	public const float AimFollowingDuckIncreaser = 0.4f;
	public const float MinRamdomDirection = 0.1f;
	public const float MaxRamdomDirection = 0.4f;

	public const float DuckInitialSpeed = 1.4f;
	public const float DuckLevelSpeedIncrease = 0.25f;
	public const float DuckSpeedIncrease = 0.4f;
	
	public static float LevelTime = 20.0f;	// secconds
	public static int[] LevelUps = { 20, 40, 80, 160, 320, 640, 1280 };	// secconds
	public static float TimeBeforeAim = 2.0f;	// secconds

	// Share Message
	public static string AppLinkAndroid = "http://goo.gl/Eo2S0Z";
	public static string ShareMessage = "Duck Escape is a fun game to play. Try it. " + AppLinkAndroid + " #duckescape";

	public const float TimeToFirstPlayServicesLogin = 0.5f; // secconds

	public const int TypeOfDeathAim = 0;
	public const int TypeOfDeathBorder = 1;

	// Ads
	public static string AdIdBanner = "ca-app-pub-1300480826628637/7593711709";
	public static string AdIdGameOver = "ca-app-pub-1300480826628637/3663039708";
	public static int NumberOfGamesToShowAds = 10;
}