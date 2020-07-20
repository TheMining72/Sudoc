using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TShockAPI;
using Terraria;
using TerrariaApi.Server;
using Terraria.Chat;
using Terraria.Chat.Commands;

namespace Sudoc
{
    [ApiVersion(2, 1)]
    public class Sudoc : TerrariaPlugin
    {
        /// <summary>
        /// Gets the author(s) of this plugin
        /// </summary>
        public override string Author => "TheMining72";

        /// <summary>
        /// Gets the description of this plugin.
        /// A short, one lined description that tells people what your plugin does.
        /// </summary>
        public override string Description => "Allows you to talk as another player.";

        /// <summary>
        /// Gets the name of this plugin.
        /// </summary>
        public override string Name => "Sudoc";

        /// <summary>
        /// Gets the version of this plugin.
        /// </summary>
        public override Version Version => new Version(1, 0, 0, 0);

        /// <summary>
        /// Initializes a new instance of the TestPlugin class.
        /// This is where you set the plugin's order and perfrom other constructor logic
        /// </summary>
        public Sudoc(Main game) : base(game)
        {

        }

        /// <summary>
        /// Handles plugin initialization. 
        /// Fired when the server is started and the plugin is being loaded.
        /// You may register hooks, perform loading procedures etc here.
        /// </summary>
        public override void Initialize()
        {
            Commands.ChatCommands.Add(new Command("sudoc.sudoc", SudocCommand, "sudoc"));
        }

        /// <summary>
        /// Handles plugin disposal logic.
        /// *Supposed* to fire when the server shuts down.
        /// You should deregister hooks and free all resources here.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Deregister hooks here
            }
            base.Dispose(disposing);
        }

        public static void SudocCommand(CommandArgs e)
        {
            if (e.Parameters.Count < 1)
            {
                e.Player.SendErrorMessage("Invalid Syntax! Proper Syntax: /sudoc <Player Name> <message...>");
                return;
            }

            if (TSPlayer.FindByNameOrID(e.Parameters[0]).Count < 1)
            {
                e.Player.SendErrorMessage("No player found");
                return;
            }

            if (TSPlayer.FindByNameOrID(e.Parameters[0]).Count > 1)
            {
                e.Player.SendMultipleMatchError(TSPlayer.FindByNameOrID(e.Parameters[0]));
                return;
            }

            var msg = string.Join(" ", e.Parameters, 1, e.Parameters.Count - 1);
            var plr = TSPlayer.FindByNameOrID(e.Parameters[0])[0];

            TSPlayer.All.SendMessage(String.Format("{0}{1}{2}: {3}", plr.Group.Prefix, plr.Name, plr.Group.Suffix, msg), new Microsoft.Xna.Framework.Color(plr.Group.R, plr.Group.G, plr.Group.B));
            TSPlayer.Server.SendMessage(String.Format("{0}{1}{2}: {3}", plr.Group.Prefix, plr.Name, plr.Group.Suffix, msg), new Microsoft.Xna.Framework.Color(plr.Group.R, plr.Group.G, plr.Group.B));
        }
    }
}