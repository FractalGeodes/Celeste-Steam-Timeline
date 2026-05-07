using Microsoft.Xna.Framework;

namespace Celeste.Mod.SteamTimeline.Markers;

public static class RoomTransitionMarkers {
	private static void OnTransition(Level level, LevelData next, Vector2 direction) {
		if (!SteamTimeline.Settings.RoomClearMarkers)
			return;

		AddTimelineMarker(
			"steam_completed", "RoomClearMarker".Localize().Replace("((roomName))", level.Session.LevelData.Name),
			null, 1, ClipPriority.Standard
		);
	}

	[OnLoad] internal static void OnLoad() => Everest.Events.Level.OnTransitionTo += OnTransition;
	[OnUnload] internal static void OnUnload() => Everest.Events.Level.OnTransitionTo -= OnTransition;
}
