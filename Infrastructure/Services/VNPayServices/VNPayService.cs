using Application.IServices.VNPayService;
using Infrastructure.Configurations.VNPay;
using Newtonsoft.Json;

namespace Infrastructure.Services.VNPayServices;

public class VNPayService : IVNPayService
{
    private readonly VNPayConfig _vnpayConfig;
    
    public VNPayService(VNPayConfig vnpayConfig)
    {
        _vnpayConfig = vnpayConfig;
    }
    
    public string CreatePaymentRequest(double amount, string orderId)
    {
        var request = new
        {
        };
        
        return "";
    }

    public async Task HandleVNPayResponse(Dictionary<string, string> vnpayData)
    {
        throw new NotImplementedException();
    }

    public bool ValidateVNPayResponse(Dictionary<string, string> vnpayData)
    {
        throw new NotImplementedException();
    }
}