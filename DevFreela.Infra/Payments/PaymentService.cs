using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace DevFreela.Infra.Payments;

public class PaymentService : IPaymentService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _paymentsBaseUrl;

    public PaymentService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _paymentsBaseUrl = configuration.GetSection("Services:Payments").Value!;
    }

    public async Task<bool> ProcessPayment(PaymentModel model)
    {
        var url = $"{_paymentsBaseUrl}/api/payments";
        var paymentInfoJson = JsonSerializer.Serialize(model);
        var paymentInfoContent = new StringContent(paymentInfoJson, Encoding.UTF8, "application/json");

        var httpClient = _httpClientFactory.CreateClient("Payments");
        var response = await httpClient.PostAsync(url, paymentInfoContent);

        return response.IsSuccessStatusCode;
    }
}
