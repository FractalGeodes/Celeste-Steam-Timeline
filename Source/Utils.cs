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

	internal static string Localize(this string input) =>
		$"SteamTimeline_{input}".DialogCleanOrNull() ?? $"Dialog Missing: SteamTimeline_{input}";
}

public static class ClipPriority {
	public const ETimelineEventClipPriority None = ETimelineEventClipPriority.k_ETimelineEventClipPriority_None;
	public const ETimelineEventClipPriority Standard = ETimelineEventClipPriority.k_ETimelineEventClipPriority_Standard;
	public const ETimelineEventClipPriority Featured = ETimelineEventClipPriority.k_ETimelineEventClipPriority_Featured;
}
