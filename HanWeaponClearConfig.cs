using CounterStrikeSharp.API.Core;
using System.Text.Json;

public class HanWeaponClearCFG
{
    public bool PlayerDeathClear { get; set; }
    public bool ClearByTimer { get; set; }
    public float ClearTimer { get; set; }
    public bool AdminOrdersCanUse { get; set; }
    public string? AdminClearOrders { get; set; }
    public bool RoundStartClear { get; set; }
    public bool PrintClearMessage { get; set; }
    public string? ClearMessage { get; set; }

    public static string ConfigPath { get; } = Path.Combine(
        Application.RootDirectory, 
        "configs/HanWeaponClear/HanWeaponClear.json"
    );

    public static HanWeaponClearCFG Load()
    {
        try
        {
            if (File.Exists(ConfigPath))
            {
                string json = File.ReadAllText(ConfigPath);
                var config = JsonSerializer.Deserialize<HanWeaponClearCFG>(json);
                if (config != null) return config;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"读取配置文件时发生错误: {ex.Message}，使用默认配置。");
        }

        var defaultConfig = new HanWeaponClearCFG
        {
            PlayerDeathClear = false,
            ClearByTimer = true,
            ClearTimer = 20.0f,
            AdminOrdersCanUse = true,
            AdminClearOrders = "css_clear",
            RoundStartClear = false,
            PrintClearMessage = true,
            ClearMessage = "[华仔]地面武器已全部清理!!"
        };
        Save(defaultConfig);
        return defaultConfig;
    }

    public static void Save(HanWeaponClearCFG config)
    {
        try
        {
            string directoryPath = Path.GetDirectoryName(ConfigPath)!; 
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                Console.WriteLine($"[华仔] 创建配置目录: {directoryPath}");
            }

            string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ConfigPath, json);
            Console.WriteLine("[华仔] 配置文件已保存。");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[华仔] 无法保存配置文件: {ex.Message}");
            Console.WriteLine($"堆栈跟踪: {ex.StackTrace}");
        }
    }
}