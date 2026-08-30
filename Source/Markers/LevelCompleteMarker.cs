namespace Celeste.Mod.SteamTimeline.Markers;

public static class LevelCompleteMarker {
	private static void OnLevelComplete(Level level) {
		// We set this in StrawberryMarkers when the golden is collected, same time as that marker.
		// Theoretically if you collect the golden and die this would persist and cancel again.
		// I don't think this is a particularly big issue though.
		if (SteamTimeline.Session.CollectedGolden)
			return;

		Session session = level.Session;
		AreaData area = AreaData.Get(session);
		AreaMode areaMode = session.Area.Mode;

		var levelName = area.Name.DialogCleanOrNull() ?? area.Name;

		var sideMarker = "";
		if (areaMode is not AreaMode.Normal)
			sideMarker = "SideMarker".Localize().Replace("((A))", ((char) ('A' + areaMode)).ToString());

		var markerDialogKey = "LevelCompleteMarker";
		if (session.FullClear)
			markerDialogKey = "LevelFullClearMarker";

		AddTimelineMarker(
			"steam_flag",
			markerDialogKey.Localize()
			               .Replace("((levelName))", levelName)
			               .Replace("((sideMarker))", sideMarker)
			               .Trim(),
			null, 2, ClipPriority.Featured
		);
	}

	[OnLoad] internal static void Load() => Everest.Events.Level.OnComplete += OnLevelComplete;
	[OnUnload] internal static void Unload() => Everest.Events.Level.OnComplete -= OnLevelComplete;
}
