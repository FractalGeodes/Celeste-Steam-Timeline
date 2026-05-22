namespace Celeste.Mod.SteamTimeline;

public static class TimelineModes {
	private static void OnLevelEnter(Session session, bool fromSaveData) => SetTimelineMode(TimelineMode.Playing);
	private static void OnLevelExit(Level level, LevelExit exit, LevelExit.Mode mode, Session session, HiresSnow snow) =>
		SetTimelineMode(TimelineMode.Menus);
	private static void OnPause(Level level, int startIndex, bool minimal, bool quickReset) =>
		SetTimelineMode(TimelineMode.Menus);
	private static void OnUnpause(Level level) => SetTimelineMode(TimelineMode.Playing);
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
		On.Celeste.OuiTitleScreen.ctor += OnTitleScreen;
	}

	[OnUnload]
	internal static void OnUnload() {
		Everest.Events.Level.OnEnter -= OnLevelEnter;
		Everest.Events.Level.OnExit -= OnLevelExit;
		Everest.Events.Level.OnPause -= OnPause;
		Everest.Events.Level.OnUnpause -= OnUnpause;
		On.Celeste.OuiTitleScreen.ctor -= OnTitleScreen;
	}
}
