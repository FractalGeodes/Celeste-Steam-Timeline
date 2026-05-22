#nullable enable
using Steamworks;

namespace Celeste.Mod.SteamTimeline;

public static class Utils {
	public static void AddTimelineMarker(
		string icon,
		string title,
		string? description,
		uint priority,
		ETimelineEventClipPriority possibleClip,
		float startOffset = 0,
		float duration = 0
	) {
		Steamworks.SteamTimeline.AddTimelineEvent(icon, title, description, priority, startOffset, duration,
			possibleClip);
		Logger.Verbose("SteamTimeline", $"({icon}) {title} : {description} | {startOffset} seconds ago for {duration}");
	}

	public static void SetTimelineTooltip(string description) {
		Steamworks.SteamTimeline.SetTimelineStateDescription(description, 0);
	}

	public static void ClearTimelineTooltip() {
		Steamworks.SteamTimeline.ClearTimelineStateDescription(0);
	}

	public static void SetTimelineMode(ETimelineGameMode mode) {
		Steamworks.SteamTimeline.SetTimelineGameMode(mode);
	}

	internal static string Localize(this string input) =>
		$"SteamTimeline_{input}".DialogCleanOrNull() ?? $"Dialog Missing: SteamTimeline_{input}";
}

public static class ClipPriority {
	public const ETimelineEventClipPriority None = ETimelineEventClipPriority.k_ETimelineEventClipPriority_None;
	public const ETimelineEventClipPriority Standard = ETimelineEventClipPriority.k_ETimelineEventClipPriority_Standard;
	public const ETimelineEventClipPriority Featured = ETimelineEventClipPriority.k_ETimelineEventClipPriority_Featured;
}

public static class TimelineMode {
	public const ETimelineGameMode Invalid = ETimelineGameMode.k_ETimelineGameMode_Invalid;
	public const ETimelineGameMode Playing = ETimelineGameMode.k_ETimelineGameMode_Playing;
	public const ETimelineGameMode Staging = ETimelineGameMode.k_ETimelineGameMode_Staging;
	public const ETimelineGameMode Menus = ETimelineGameMode.k_ETimelineGameMode_Menus;
	public const ETimelineGameMode LoadingScreen = ETimelineGameMode.k_ETimelineGameMode_LoadingScreen;
	public const ETimelineGameMode Max = ETimelineGameMode.k_ETimelineGameMode_Max;
}
