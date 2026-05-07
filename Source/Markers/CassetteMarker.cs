using System.Collections;

namespace Celeste.Mod.SteamTimeline.Markers;

public static class CassetteMarker {
	private static IEnumerator OnCollect(On.Celeste.Cassette.orig_CollectRoutine orig, Cassette self, Player player) {
		AreaData area = AreaData.Get(player.level.Session);

		AddTimelineMarker(
			"steam_bookmark",
			"CassetteMarker".Localize(),
			"CassetteMarker_Description".Localize().Replace("((levelName))", area.Name.DialogCleanOrNull() ?? area.Name),
			1,
			ClipPriority.Standard
		);

		return orig(self, player);
	}

	[OnLoad] internal static void OnLoad() => On.Celeste.Cassette.CollectRoutine += OnCollect;
	[OnUnload] internal static void OnUnload() => On.Celeste.Cassette.CollectRoutine -= OnCollect;
}
