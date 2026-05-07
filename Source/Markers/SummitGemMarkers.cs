using System.Collections;

namespace Celeste.Mod.SteamTimeline.Markers;

public static class SummitGemMarkers {
	private static IEnumerator OnSmash(
		On.Celeste.SummitGem.orig_SmashRoutine orig, SummitGem self, Player player, Level level
	) {
		AddTimelineMarker("steam_gem", "SummitGemMarker".Localize(), null, 1, ClipPriority.Standard);

		return orig(self, player, level);
	}

	[OnLoad] internal static void OnLoad() => On.Celeste.SummitGem.SmashRoutine += OnSmash;
	[OnUnload] internal static void OnUnload() => On.Celeste.SummitGem.SmashRoutine -= OnSmash;
}
