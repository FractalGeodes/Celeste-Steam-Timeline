using System;
using System.Collections.Generic;

namespace Celeste.Mod.SteamTimeline;

public class SteamTimelineSession : EverestModuleSession {
	public bool CollectedGolden;
	public DateTime RoomStartTime;
	public HashSet<string> CheckpointsSeen { get; set; } = [];
}
