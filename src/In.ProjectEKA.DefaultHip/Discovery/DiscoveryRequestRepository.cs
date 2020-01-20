namespace In.ProjectEKA.DefaultHip.Discovery
{
    using System.Threading.Tasks;
    using Database;
    using Microsoft.EntityFrameworkCore;
    using Model;

    public class DiscoveryRequestRepository : IDiscoveryRequestRepository
    {
        private readonly DiscoveryContext discoveryContext;

        public DiscoveryRequestRepository(DiscoveryContext discoveryContext)
        {
            this.discoveryContext = discoveryContext;
        }

        public async Task Add(DiscoveryRequest discoveryRequest)
        {
            discoveryContext.DiscoveryRequest.Add(discoveryRequest);
            await discoveryContext.SaveChangesAsync();
        }

        public async Task Delete(string transactionId, string consentManagerUserId)
        {
            var discoveryRequest = await discoveryContext.DiscoveryRequest
                .FirstAsync(request =>
                    request.TransactionId == transactionId &&
                    request.ConsentManagerUserId == consentManagerUserId);
            discoveryContext.Remove(discoveryRequest);
            await discoveryContext.SaveChangesAsync();
        }
    }
}