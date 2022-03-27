using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace TerrariansConstruct.API.Commands {
	internal class DisableOreTracking : ModCommand {
		public override CommandType Type => CommandType.Chat;

		public override string Usage => "[c/ff6a00:Usage: /trackores <true|false>]";

		public override string Command => "trackores";

		public override string Description => "Toggles whether ore placement is tracked. False disables the tracking, true enables it.";

		public override void Action(CommandCaller caller, string input, string[] args) {
			if (args.Length != 1) {
				caller.Reply("Expected only one boolean argument", Color.Red);
				return;
			}

			if (!bool.TryParse(args[0], out bool enable)) {
				caller.Reply("Argument must be a boolean string", Color.Red);
				return;
			}

			CoreMod.disableOreTracking = !enable;

			caller.Reply("Tracking flag was modified successfully.", Color.Green);
		}
	}
}
