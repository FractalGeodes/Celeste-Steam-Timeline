using System.Collections.Generic;

namespace Celeste.Mod.SteamTimeline;

public class SteamTimelineSession : EverestModuleSession {
	public HashSet<string> CheckpointsSeen { get; set; } = [];
}
