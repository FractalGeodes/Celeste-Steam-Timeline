using System;

namespace Celeste.Mod.SteamTimeline;

// ReSharper disable once ClassNeverInstantiated.Global
// (Everest will handle it)
public class SteamTimeline : EverestModule {
	public SteamTimeline() {
		Instance = this;
#if DEBUG
		// debug builds use verbose logging
		Logger.SetLogLevel(nameof(SteamTimeline), LogLevel.Verbose);
#else
        // release builds use info logging to reduce spam in log files
        Logger.SetLogLevel(nameof(SteamTimeline), LogLevel.Info);
#endif
	}

	// (Everest needs to use this)
	// ReSharper disable once MemberCanBePrivate.Global
	public static SteamTimeline Instance { get; private set; }

	public override Type SettingsType => typeof(SteamTimelineSettings);
	public static SteamTimelineSettings Settings => (SteamTimelineSettings) Instance._Settings;

	public override Type SessionType => typeof(SteamTimelineSession);
	public static SteamTimelineSession Session => (SteamTimelineSession) Instance._Session;

	public override void Load() => LifecycleMethods.OnLoad();

	public override void Unload() => LifecycleMethods.OnUnload();
}
