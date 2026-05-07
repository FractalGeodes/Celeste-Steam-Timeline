namespace Celeste.Mod.SteamTimeline.Markers;

public static class LevelCompleteMarker {
	private static void OnLevelComplete(Level level) {
		Session session = level.Session;
		AreaData area = AreaData.Get(session);
		AreaMode areaMode = session.Area.Mode;

		var levelName = area.Name.DialogCleanOrNull() ?? area.Name;

		var sideMarker = "";
		if (areaMode is not AreaMode.Normal)
			sideMarker = "SideMarker".Localize().Replace("((A))", ((char) ('A' + areaMode)).ToString());

		AddTimelineMarker(
			"steam_flag",
			"LevelCompleteMarker".Localize()
			                     .Replace("((levelName))", levelName)
			                     .Replace("((sideMarker))", sideMarker)
			                     .Trim(),
			null, 2, ClipPriority.Featured
		);
	}

	[OnLoad] internal static void Load() => Everest.Events.Level.OnComplete += OnLevelComplete;
	[OnUnload] internal static void Unload() => Everest.Events.Level.OnComplete -= OnLevelComplete;
}
