namespace Application.IServices.VNPayService;

public interface IVNPayService
{
    string CreatePaymentRequest(double amount, string orderId);
    Task HandleVNPayResponse(Dictionary<string, string> vnpayData);
    bool ValidateVNPayResponse(Dictionary<string, string> vnpayData);
}