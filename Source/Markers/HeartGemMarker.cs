using Celeste.Mod.Meta;

namespace Celeste.Mod.SteamTimeline.Markers;

public static class HeartGemMarker {
	private static void OnCollect(On.Celeste.HeartGem.orig_Collect orig, HeartGem self, Player player) {
		orig(self, player);

		Session session = (self.Scene as Level)?.Session;
		MapMetaModeProperties meta = session?.MapData?.Meta;
		// Don't bother adding a Heart collection marker if the level is about to end.
		if (meta?.HeartIsEnd != null && meta.HeartIsEnd.Value && !self.IsFake)
			return;
		// Also account for vanilla's hardcoded end on Heart logic.
		if (session?.Area.GetLevelSet() == "Celeste" && session.Area.Mode != AreaMode.Normal)
			return;

		string poemId = null;

		if (self.Scene is Level level) {
			AreaKey area = level.Session.Area;

			poemId = "poem_" + AreaData.Get(level).Mode[(int) area.Mode].PoemID;
		}

		AddTimelineMarker(
			"steam_heart", (self.IsFake ? "HeartGemMarker_Fake" : "HeartGemMarker").Localize(), poemId.DialogCleanOrNull(),
			2, ClipPriority.Featured
		);
	}

	[OnLoad] internal static void Load() => On.Celeste.HeartGem.Collect += OnCollect;
	[OnUnload] internal static void Unload() => On.Celeste.HeartGem.Collect -= OnCollect;
}
