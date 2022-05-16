using Exiled.API.Features;
using Exiled.CreditTags;
using System;
using System.IO;

namespace PlayerList
{
   public class Plugin : Plugin<Config>
    {


        public override string Name { get; } = "PlayerList";
        public override string Author => "Zunek";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(5, 0, 0);

        public static Plugin Singleton;


        public override void OnEnabled()
        {

            Singleton = this;



            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Singleton = null;





            base.OnDisabled();
        }
    }
}