using System;
using Microsoft.Xna.Framework;

namespace Celeste.Mod.SteamTimeline.Markers;

public static class RoomTransitionMarkers {
	private static void OnTransition(Level level, LevelData next, Vector2 direction) {
		TimeSpan roomStartTime = TimeSpan.FromTicks(SteamTimeline.Session.RoomStartTime);
		TimeSpan currentTime = TimeSpan.FromTicks(level.Session.Time);

		SteamTimeline.Session.RoomStartTime = level.Session.Time;

		if (!SteamTimeline.Settings.RoomClearMarkers || currentTime - roomStartTime < TimeSpan.FromSeconds(5))
			return;

		AddTimelineMarker(
			"steam_completed",
			"RoomClearMarker".Localize()
			                 .Replace("((roomName))", level.Session.LevelData.Name),
			null,
			1,
			ClipPriority.Standard,
			(float) (roomStartTime - currentTime).TotalSeconds,
			(float) (currentTime - roomStartTime).TotalSeconds
		);
	}

	// We want the marked range to start from the latest respawn, so we reset every spawn.
	private static void OnSpawn(Player obj) {
		SteamTimeline.Session.RoomStartTime = ((Level) obj.Scene).Session.Time;
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
