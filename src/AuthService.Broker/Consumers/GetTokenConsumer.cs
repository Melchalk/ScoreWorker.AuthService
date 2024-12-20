using AuthService.Broker.Models.Requests;
using MassTransit;

namespace AuthService.Broker.Consumers;

public class GetTokenConsumer : IConsumer<GetTokenRequest>
{
    public Task Consume(ConsumeContext<GetTokenRequest> context)
    {
        throw new NotImplementedException();
    }
}
