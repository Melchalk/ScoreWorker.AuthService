using AuthService.Broker.Models.Requests;
using AuthService.Token.Helpers.Interfaces;
using MassTransit;

namespace AuthService.Broker.Consumers;

public class CheckTokenConsumer(ITokenValidator tokenValidator) : IConsumer<CheckTokenRequest>
{
    public Task Consume(ConsumeContext<CheckTokenRequest> context)
    {
        throw new NotImplementedException();
    }
}
