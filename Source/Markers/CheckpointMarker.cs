namespace Celeste.Mod.SteamTimeline.Markers;

public static class CheckpointMarker {
	private static void OnUpdate(On.Celeste.Checkpoint.orig_Update orig, Checkpoint self) {
		var level = self.Scene as Level;
		var levelName = level?.Session.LevelData.Name;
		var player = level?.Tracker.GetEntity<Player>();

		SteamTimelineSession modSession = SteamTimeline.Session;

		if (player is null || level.Transitioning || player.CollideCheck<CheckpointBlockerTrigger>() ||
		    modSession.CheckpointsSeen.Contains(levelName))
			return;

		AreaKey areaKey = level.Session.Area;

		var checkpointName = AreaData.GetCheckpointName(areaKey, levelName);

		AddTimelineMarker("steam_", "CheckpointMarker".Localize().Replace("((checkpointName))", checkpointName), null, 2,
			ClipPriority.Standard);

		SteamTimeline.Session.CheckpointsSeen.Add(levelName);

		orig(self);
	}

	[OnLoad] internal static void OnLoad() => On.Celeste.Checkpoint.Update += OnUpdate;
	[OnUnload] internal static void OnUnload() => On.Celeste.Checkpoint.Update -= OnUpdate;
}
