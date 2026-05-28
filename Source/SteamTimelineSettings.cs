using System;

namespace Celeste.Mod.SteamTimeline;

public class SteamTimelineSettings : EverestModuleSettings {
	[Flags]
	public enum DeathMarkerMode {
		Never = 0,
		Goldens = 1 << 0,
		Always = Goldens | (1 << 1)
	}

	[Flags]
	public enum RoomClearMode {
		None = 0,
		Instant = 1 << 0,
		Range = 1 << 1,
		Both = Instant | Range
	}

	[SettingSubText("ModOptions_SteamTimeline_RoomsInTooltip_Desc")]
	public bool RoomsInTooltip { get; set; } = false;

	[SettingSubText("ModOptions_SteamTimeline_DeathMarkerModeSlider_Desc")]
	public DeathMarkerMode DeathMarkerModeSlider { get; set; } = DeathMarkerMode.Goldens;

	[SettingSubText("ModOptions_SteamTimeline_RoomClearMarkers_Desc")]
	public RoomClearMode RoomClearMarkers { get; set; } = RoomClearMode.None;
}
