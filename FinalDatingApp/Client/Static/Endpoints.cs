using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalDatingApp.Client.Static
{
    public static class Endpoints
    {
        private static readonly string Prefix = "api";

        public static readonly string PersonsEndpoint = $"{Prefix}/persons";
        public static readonly string PreferencesEndpoint = $"{Prefix}/preferences";
        public static readonly string MediasEndpoint = $"{Prefix}/medias";
        public static readonly string MatchsEndpoint = $"{Prefix}/matchs";
        public static readonly string MessagesEndpoint = $"{Prefix}/messages";
    }
}
