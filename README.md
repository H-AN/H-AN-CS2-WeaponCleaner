# H-AN-CS2-WeaponCleaner
华仔/H-AN CS2武器自动清理 WeaponCleaner 可配置的地面武器自动清理

使用此插件需要安装CounterStrikeSharp

第一次使用将自动生成配置文件 

配置文件路径 configs/HanWeaponClear/HanWeaponClear.json

以下为默认配置 

PlayerDeathClear = false, //是否启用死亡立即触发武器清除 适用于清除死亡掉落与不使用循环Timer的情况

ClearByTimer = true, //是否启循环Timer触发定时清理

ClearTimer = 20.0f, //设定定时清理的间隔时间

AdminOrdersCanUse = true, //是否允许管理员使用命令直接清除地面武器 

AdminClearOrders = "css_clear", //设定管理员清除的指令默认css_clear可以输入clear和css_clear游戏内聊天框!clear或者控制台css_clear

RoundStartClear = false, //是否启用回合开始直接清理一次地面武器适用于清理自带武器的地图 将地图清理为没有武器

PrintClearMessage = true, //是否开启清理武器播报

ClearMessage = "[华仔]地面武器已全部清理!!"//自定义清理武器播报打印在游戏内的信息
