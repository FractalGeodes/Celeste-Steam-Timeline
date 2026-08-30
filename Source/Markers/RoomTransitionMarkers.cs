using System;
using Microsoft.Xna.Framework;

namespace Celeste.Mod.SteamTimeline.Markers;

public static class RoomTransitionMarkers {
	private static void OnTransition(Level level, LevelData next, Vector2 direction) {
		DateTime roomStartTime = SteamTimeline.Session.RoomStartTime;
		DateTime currentTime = DateTime.UtcNow;

		SteamTimeline.Session.RoomStartTime = currentTime;

		// Ignore room transitions for golden runs
		var player = level.Tracker.GetEntity<Player>();
		if (player.Leader.Followers.Exists(follower => follower.Entity is Strawberry { Golden: true }))
			return;

		if (SteamTimeline.Settings.RoomClearMarkers == SteamTimelineSettings.RoomClearMode.None ||
		    currentTime - roomStartTime < TimeSpan.FromSeconds(5))
			return;

		var markerText = "RoomClearMarker".Localize().Replace("((roomName))", level.Session.LevelData.Name);

		if (SteamTimeline.Settings.RoomClearMarkers.HasFlag(SteamTimelineSettings.RoomClearMode.Instant))
			AddTimelineMarker(
				"steam_completed",
				markerText,
				null,
				1,
				ClipPriority.Standard
			);

		if (SteamTimeline.Settings.RoomClearMarkers.HasFlag(SteamTimelineSettings.RoomClearMode.Range))
			AddTimelineMarker(
				"steam_completed",
				markerText,
				null,
				1,
				ClipPriority.Standard,
				(float) (roomStartTime - currentTime).TotalSeconds,
				(float) (currentTime - roomStartTime).TotalSeconds
			);
	}

	// We want the marked range to start from the latest respawn, so we reset every spawn.
	private static void OnSpawn(Player obj) {
		SteamTimeline.Session.RoomStartTime = DateTime.UtcNow;
	}

	[OnLoad]
	internal static void OnLoad() {
		Everest.Events.Level.OnTransitionTo += OnTransition;
		Everest.Events.Player.OnSpawn += OnSpawn;
	}

	[OnUnload]
	internal static void OnUnload() {
		Everest.Events.Level.OnTransitionTo -= OnTransition;
		Everest.Events.Player.OnSpawn -= OnSpawn;
	}
}
