using Microsoft.Xna.Framework;
using static Celeste.Mod.SteamTimeline.SteamTimelineSettings;

namespace Celeste.Mod.SteamTimeline.Markers;

public static class DeathMarkers {
	private static PlayerDeadBody OnDeath(
		On.Celeste.Player.orig_Die orig,
		Player self,
		Vector2 direction,
		bool evenIfInvincible,
		bool registerDeathInStats
	) {
		// `Player.Die()` clears out the followers so we check this beforehand.
		var wasGolden = self.Leader.Followers.Exists(x => x.Entity is Strawberry { Golden: true, Winged: false });
		PlayerDeadBody pdb = orig(self, direction, evenIfInvincible, registerDeathInStats);

		// 🎊 we didn't die 🎊 (so no markers are needed)
		if (pdb is null) return null;

		DeathMarkerMode markerMode = SteamTimeline.Settings.DeathMarkerModeSlider;

		// WORKAROUND: Until Everest pull request 1101 is merged we're doing this.

		// if (wasGolden && markerMode.HasFlag(DeathMarkerMode.Goldens))
		if (wasGolden && markerMode != DeathMarkerMode.Never)
			AddTimelineMarker("steam_death", "GoldenDeathMarker".Localize(), null, 1, ClipPriority.Standard);

		// else if (markerMode.HasFlag(DeathMarkerMode.Always))
		else if (markerMode == DeathMarkerMode.Always)
			AddTimelineMarker("steam_death", "DeathMarker".Localize(), null, 0, ClipPriority.None);

		return pdb;
	}

	[OnLoad] internal static void Load() => On.Celeste.Player.Die += OnDeath;
	[OnUnload] internal static void Unload() => On.Celeste.Player.Die -= OnDeath;
}
