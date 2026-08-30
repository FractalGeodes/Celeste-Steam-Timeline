namespace Celeste.Mod.SteamTimeline.Markers;

public static class StrawberryMarkers {
	private static void OnStrawberryCollect(On.Celeste.Strawberry.orig_OnCollect orig, Strawberry self) {
		orig(self);

		// I want to kill the 1A winged golden.
		// TODO: Modded winged goldens
		if (self.SourceData?.Name == "memorialTextController") {
			AddTimelineMarker("steam_crown", "WingedGoldenMarker".Localize(), null, 2, ClipPriority.Featured);
		}
		else if (self.Golden) {
			AddTimelineMarker("steam_crown", "GoldenMarker".Localize(), null, 2, ClipPriority.Featured);
			SteamTimeline.Session.CollectedGolden = true;
		}
		else if (self.Moon) {
			AddTimelineMarker("steam_achievement", "MoonberryMarker".Localize(), null, 2, ClipPriority.Featured);
		}
		else {
			AddTimelineMarker("steam_ribbon", "StrawberryMarker".Localize(), null, 1, ClipPriority.Standard);
		}
	}

	[OnLoad] internal static void Load() => On.Celeste.Strawberry.OnCollect += OnStrawberryCollect;
	[OnUnload] internal static void Unload() => On.Celeste.Strawberry.OnCollect -= OnStrawberryCollect;
}
