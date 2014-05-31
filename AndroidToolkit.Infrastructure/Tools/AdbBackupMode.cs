using AndroidToolkit.Infrastructure.Helpers;

namespace AndroidToolkit.Infrastructure.Tools
{
    public enum AdbBackupMode
    {
        [EnumDescription("All")]
        All = 0,
        [EnumDescription("Apps")]
        Apps = 1,
        [EnumDescription("System apps")]
        SystemApps = 2,
        [EnumDescription("Apps without system apps")]
        AppsWithoutSystemApps = 3,
        [EnumDescription("SD")]
        SDCard = 4
    }
}
