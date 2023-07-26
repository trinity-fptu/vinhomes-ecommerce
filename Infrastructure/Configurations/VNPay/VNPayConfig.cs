using Newtonsoft.Json;

namespace Infrastructure.Configurations.VNPay;

public class VNPayConfig
{
    private readonly string _vnp_TmnCode = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("services.json"))!["vnp_TmnCode"];
    private readonly string _vnp_HashSecret = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("services.json"))!["vnp_HashSecret"];
    private readonly string _vnp_Url = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("services.json"))!["vnp_Url"];
    private readonly string _vnp_ReturnUrl = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("services.json"))!["vnp_ReturnUrl"];
    
}