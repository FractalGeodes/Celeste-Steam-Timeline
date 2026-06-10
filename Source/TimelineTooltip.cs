using System;
using Microsoft.Xna.Framework;

namespace Celeste.Mod.SteamTimeline;

public static class TimelineTooltip {
	private static string LevelName(Session session, LevelData levelData = null) {
		AreaData areaData = AreaData.Get(session);
		AreaMode areaMode = session.Area.Mode;
		// Account for OnTransition where `session` still points to the previous room, not the next room
		levelData ??= session.LevelData;

		var levelName = areaData.Name.DialogCleanOrNull() ?? areaData.Name;
		var sideMarker = "";
		if (areaMode is not AreaMode.Normal)
			sideMarker = "SideMarker".Localize().Replace("((A))", ((char) ('A' + areaMode)).ToString());
		var roomName = levelData.Name;

		return (SteamTimeline.Settings.RoomsInTooltip ? "InGameTooltip_WithRoom" : "InGameTooltip")
		       .Localize()
		       .Replace("((roomName))", roomName)
		       .Replace("((levelName))", levelName)
		       .Replace("((sideMarker))", sideMarker)
		       .Trim();
	}

	private static void OnEnter(Session session, bool fromSaveData) {
		try { SetTimelineTooltip(LevelName(session)); }
		// session.LevelData fails if the specified level doesn't exist (i.e. disabled mod).
		// This crashes through Everest's handling of a missing level for some reason. So we have to handle it as well.
		catch (Exception e) {
			Logger.Warn("SteamTimeline",
				$"Encountered {e.GetType()} in `OnEnter`. If you just loaded a disabled map there's no problem here.");
		}
	}

	// There's a joke one could make here.
	private static void OnTransition(Level level, LevelData next, Vector2 direction) =>
		SetTimelineTooltip(LevelName(level.Session, next));

	private static void OnExit(Level level, LevelExit exit, LevelExit.Mode mode, Session session, HiresSnow snow) =>
		ClearTimelineTooltip();

	[OnLoad]
	internal static void Load() {
		Everest.Events.Level.OnEnter += OnEnter;
		Everest.Events.Level.OnTransitionTo += OnTransition;
		Everest.Events.Level.OnExit += OnExit;
	}

	[OnUnload]
	internal static void Unload() {
		Everest.Events.Level.OnEnter -= OnEnter;
		Everest.Events.Level.OnTransitionTo -= OnTransition;
		Everest.Events.Level.OnExit -= OnExit;
	}
}
