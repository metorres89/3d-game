using System;

public static class GameplayState
{
	public enum StateType {
		INITIAL,
		PLAYING,
		PAUSE,
		WIN,
		GAME_OVER
	};

	public static StateType CurrentState = StateType.INITIAL;
	public static string ResultTitle = "";
	public static int TotalEnemies = 0;
	public static int KilledEnemies = 0;
	public static int TotalHostages = 0;
	public static int RecoveredHostages = 0;
	public static int RescuedHostages = 0;
	public static int DeadHostages = 0;
	public static float SuccessShoots = 0.0f;
	public static int TotalShoots = 0;


	public static void Reset() {
		CurrentState = StateType.INITIAL;
		ResultTitle = "";
		TotalEnemies = 0;
		KilledEnemies = 0;
		TotalHostages = 0;
		RecoveredHostages = 0;
		RescuedHostages = 0;
		DeadHostages = 0;
		SuccessShoots = 0.0f;
		TotalShoots = 0;
	}

	public static bool AllHostagesHasBeenRescued() {
		return TotalHostages == RescuedHostages;
	}

	public static float GetShootPrecision() {
		return SuccessShoots / TotalShoots;
	}
}