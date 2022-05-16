using CommandSystem;
using Exiled.API.Features;
using Exiled.API.Extensions;
using RemoteAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerList
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Command : ICommand
    {
        public string[] Aliases => Plugin.Singleton.Config.CommandAlias;

        public string Description => Plugin.Singleton.Config.CommandDescription;

        string ICommand.Command => Plugin.Singleton.Config.CommandName;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender != null)
            {
                // RemoteAdmin
                if (sender is PlayerCommandSender _)
                {
                    if (Player.List.Count() >= 1)
                    {
                        int max = GameCore.ConfigFile.ServerConfig.GetInt("max_players", 20);
                        int cur = Player.List.Count();
                        TimeSpan duration = Round.ElapsedTime;
                        string seconds = duration.Seconds < 10 ? $"0{duration.Seconds}" : duration.Seconds.ToString();
                        string minutes = duration.Minutes < 10 ? $"0{duration.Minutes}" : duration.Minutes.ToString();
                        string msg;
                        msg = string.Format(Plugin.Singleton.Config.TextTimeFormat, minutes, seconds);
                        msg += string.Format(Plugin.Singleton.Config.TextTotalPlayersFormat, cur, max);
                        msg += Plugin.Singleton.Config.TextTitle;
                        foreach (Player ply in Player.List)
                        {
                            if (ply.RemoteAdminAccess && Plugin.Singleton.Config.RemarksStaff)
                            {
                                msg += string.Format(Plugin.Singleton.Config.TextperStaffPlayer, ply.Id, ply.Nickname, ply.Role);
                            }
                            else
                            {
                                msg += string.Format(Plugin.Singleton.Config.TextperPlayer, ply.Id, ply.Nickname, ply.Role);
                            }
                        }
                        response = msg;
                        return true;

                    }
                    else
                    {

                        string msg = Plugin.Singleton.Config.NoPlayersOnline;
                        response = msg;
                        return true;

                    }
                }
                else
                {
                    // Bot or Console
                    if (Player.List.Count() >= 1)
                    {
                        int max = GameCore.ConfigFile.ServerConfig.GetInt("max_players", 20);
                        int cur = Player.List.Count();
                        TimeSpan duration = Round.ElapsedTime;
                        string seconds = duration.Seconds < 10 ? $"0{duration.Seconds}" : duration.Seconds.ToString();
                        string minutes = duration.Minutes < 10 ? $"0{duration.Minutes}" : duration.Minutes.ToString();
                        string msg;

                        if (Plugin.Singleton.Config.UseDiscordCodeBlock)
                        {
                            msg = $"```{Plugin.Singleton.Config.WhatTypeOfCodeBlock}\n";
                            msg += string.Format(Plugin.Singleton.Config.TextTimeFormat, minutes, seconds);
                            msg += string.Format(Plugin.Singleton.Config.TextTotalPlayersFormat, cur, max);
                            msg += $"{Plugin.Singleton.Config.TextTitle}";

                            foreach (Player ply in Player.List)
                            {
                                if (ply.RemoteAdminAccess && Plugin.Singleton.Config.RemarksStaff)
                                {
                                    msg += string.Format(Plugin.Singleton.Config.TextperStaffPlayer, ply.Id, ply.Nickname, ply.Role);
                                }
                                else
                                {
                                    msg += string.Format(Plugin.Singleton.Config.TextperPlayer, ply.Id, ply.Nickname, ply.Role);
                                }
                            }
                            msg += "\n```";
                            response = msg;
                            return true;
                        }
                        else
                        {


                            msg = string.Format(Plugin.Singleton.Config.TextTimeFormat, minutes, seconds);
                            msg += string.Format(Plugin.Singleton.Config.TextTotalPlayersFormat, cur, max);
                            msg += $"{Plugin.Singleton.Config.TextTitle}";

                            foreach (Player ply in Player.List)
                            {
                                if (ply.RemoteAdminAccess && Plugin.Singleton.Config.RemarksStaff)
                                {
                                    msg += string.Format(Plugin.Singleton.Config.TextperStaffPlayer, ply.Id, ply.Nickname, ply.Role);
                                }
                                else
                                {
                                    msg += string.Format(Plugin.Singleton.Config.TextperPlayer, ply.Id, ply.Nickname, ply.Role);
                                }
                            }
                            response = msg;
                            return true;
                        }

                    }
                    else
                    {
                        if (Plugin.Singleton.Config.UseDiscordCodeBlock)
                        {
                            string msg = $"```{Plugin.Singleton.Config.WhatTypeOfCodeBlock}\n";
                            msg += Plugin.Singleton.Config.NoPlayersOnline;
                            msg += "\n```";
                            response = msg;
                            return true;
                        }
                        else
                        {
                            string msg = Plugin.Singleton.Config.NoPlayersOnline;
                            response = msg;
                            return true;
                        }
                    }
                }
            }
            else
            {
                response = "Sender is null";
                return false;
            }
        }
    }
}