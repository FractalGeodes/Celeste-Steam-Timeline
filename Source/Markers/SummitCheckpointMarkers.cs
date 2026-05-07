namespace Celeste.Mod.SteamTimeline.Markers;

public static class SummitCheckpointMarkers {
	private static void OnUpdate(On.Celeste.SummitCheckpoint.orig_Update orig, SummitCheckpoint self) {
		var player = self.CollideFirst<Player>();
		if (!self.Activated && player != null && player.OnGround() && player.Speed.Y >= 0.0)
			AddTimelineMarker(
				$"steam_{self.Number}",
				"SummitCheckpointMarker".Localize().Replace("((flagNumber))", self.Number.ToString()),
				null,
				1,
				ClipPriority.Standard
			);
		orig(self);
	}

	[OnLoad] internal static void OnLoad() => On.Celeste.SummitCheckpoint.Update += OnUpdate;
	[OnUnload] internal static void OnUnload() => On.Celeste.SummitCheckpoint.Update -= OnUpdate;
}
