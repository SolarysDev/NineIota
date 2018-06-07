using System;

using ZeroTwo.Resources;
namespace ZeroTwo
{
    class Config
    {
        public static Passwords Keys;
        public static BotFlags Flags;

        static Config()
        {
            Keys = ConfigLoader.LoadFrom<Passwords>("Resources/keys.json");

            Flags = new BotFlags
            {
                EnableBeta = false,
                PushToBotlist = false,
                UpdatePresence = true
            };
        }
    }
}
