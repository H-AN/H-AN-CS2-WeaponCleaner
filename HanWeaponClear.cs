using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Modules.Timers;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Admin;

namespace WeaponClear;

public class HanWeaponClear : BasePlugin
{
    public override string ModuleName => "[华仔]地面武器清理";
    public override string ModuleVersion => "1.0";
    public override string ModuleAuthor => "By : 华仔H-AN";
    public override string ModuleDescription => "清理地面武器,QQ群107866133";

    HanWeaponClearCFG CFG = HanWeaponClearCFG.Load(); //加载配置文件
    public string? AdminOrders; 
    public string? ServerMessage; 
    private static HanWeaponClear _instance = null!;
    public HanWeaponClear()
    {
        _instance = this; 
    }

    private CounterStrikeSharp.API.Modules.Timers.Timer? ClearAllweapon { get; set; } = null;
    public override void Load(bool hotReload)
    { 
        AdminOrders = !string.IsNullOrEmpty(CFG.AdminClearOrders) ? CFG.AdminClearOrders : "css_clear";
        ServerMessage = !string.IsNullOrEmpty(CFG.ClearMessage) ? CFG.ClearMessage : "[华仔]地面武器已全部清理!!";
        AddCommand($"{AdminOrders}", "ClearWeapon", clearweapon); 
        RegisterEventHandler<EventRoundStart>(OnRoundStart);
        RegisterEventHandler<EventPlayerDeath>(OnPlayerDeath);
    }


    private HookResult OnRoundStart(EventRoundStart @event, GameEventInfo info)
    { 
        if(CFG.RoundStartClear) //回合开始 清理1次是否开启 应用场景为清理 地面上有武器的地图 令地图开局武器消失
        {
            RemoveWeaponsOnTheGround();
        }
        if(CFG.ClearByTimer) //循环Timer清理 是否开启 
        {
            ClearAllweapon?.Kill(); //回合开始清理Timer
            ClearAllweapon = null;
            ClearAllweapon = AddTimer(CFG.ClearTimer, RemoveWeaponsOnTheGround, TimerFlags.REPEAT|TimerFlags.STOP_ON_MAPCHANGE);  //根据配置文件生成Timer
        }
        return HookResult.Continue;
    }

    private HookResult OnPlayerDeath(EventPlayerDeath @event, GameEventInfo info)
    {
        if(CFG.PlayerDeathClear) //回合开始 清理1次是否开启 应用场景为清理 地面上有武器的地图 令地图开局武器消失
        {
            RemoveWeaponsOnTheGround();
        }
        return HookResult.Continue;
    }

    [RequiresPermissions(@"css/slay")]
    private void clearweapon(CCSPlayerController? client, CommandInfo info)
    {
        if (client == null) return;
        if(!CFG.AdminOrdersCanUse) return; //是否允许管理员使用清理命令 
        RemoveWeaponsOnTheGround();
    }


    public static void RemoveWeaponsOnTheGround()
    {
        var entities = Utilities.FindAllEntitiesByDesignerName<CCSWeaponBaseGun>("weapon_");
        foreach (var entity in entities)
        {
            if (!entity.IsValid)
            {
                continue;
            }
            if (entity.State != CSWeaponState_t.WEAPON_NOT_CARRIED)
            {
                continue;
            }
            if (entity.DesignerName.StartsWith("weapon_") == false)
            {
                continue;
            }
            if (GetPlayerCount() <= 0) //如果服务器没有玩家 不执行清理
            {
                continue;
            }
            entity.Remove();
        }
        if(_instance.CFG.PrintClearMessage) //是否开启清理全局公告
        {
            Server.PrintToChatAll($"{_instance.ServerMessage}");
        }
        
    }

    public static int GetPlayerCount() 
    {
        return Utilities.GetPlayers().Count;
    }
    
    
}




