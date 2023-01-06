using com.zibra.liquid.Solver;

namespace com.zibra.liquid
{
    internal class ZibraAiPackageInfo : IPackageInfo
    {
        public string displayName => "Zibra Liquids";
        public string description =>
            "Real-time liquid simulation plugin (GPU), powered by AI. New game mechanics & gameplay, graphics refining, game performance improvement. Ease of use.";
        public string version => ZibraLiquid.PluginVersion;
    }
}
