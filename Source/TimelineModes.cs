namespace Celeste.Mod.SteamTimeline;

public static class TimelineModes {
	// I don't know why Rider wants me to format the name like this.
	private static bool _paused;

	private static void OnLevelEnter(Session session, bool fromSaveData) {
		SetTimelineMode(TimelineMode.Playing);
		_paused = false;
	}

	private static void OnLevelExit(Level level, LevelExit exit, LevelExit.Mode mode, Session session, HiresSnow snow) =>
		SetTimelineMode(TimelineMode.Menus);

	private static void OnPause(Level level, int startIndex, bool minimal, bool quickReset) {
		SetTimelineMode(TimelineMode.Menus);
		_paused = true;
	}

	private static void OnUnpause(Level level) {
		SetTimelineMode(TimelineMode.Playing);
		_paused = false;
	}

	// Retrying doesn't trigger OnUnpause, so we use OnRespawn.
	private static void OnRespawn(Player player) {
		// Setting the timeline state repeatedly creates seams in the timeline,
		// so we only do this if we were paused beforehand.
		if (!_paused) return;
		SetTimelineMode(TimelineMode.Playing);
		_paused = false;
	}

	private static void OnTitleScreen(On.Celeste.OuiTitleScreen.orig_ctor orig, OuiTitleScreen self) {
		SetTimelineMode(TimelineMode.Menus);
		orig(self);
	}

	[OnLoad]
	internal static void OnLoad() {
		SetTimelineMode(TimelineMode.LoadingScreen);
		Everest.Events.Level.OnEnter += OnLevelEnter;
		Everest.Events.Level.OnExit += OnLevelExit;
		Everest.Events.Level.OnPause += OnPause;
		Everest.Events.Level.OnUnpause += OnUnpause;
		Everest.Events.Player.OnSpawn += OnRespawn;
		On.Celeste.OuiTitleScreen.ctor += OnTitleScreen;
	}

	[OnUnload]
	internal static void OnUnload() {
		Everest.Events.Level.OnEnter -= OnLevelEnter;
		Everest.Events.Level.OnExit -= OnLevelExit;
		Everest.Events.Level.OnPause -= OnPause;
		Everest.Events.Level.OnUnpause -= OnUnpause;
		Everest.Events.Player.OnSpawn -= OnRespawn;
		On.Celeste.OuiTitleScreen.ctor -= OnTitleScreen;
	}
}
