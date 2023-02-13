using NimbRepository.Model.Seller;

namespace Nimb.Models
{
    public class ClientViewModel
    {
        public ClientViewModel(Client client, int clientId)
        {
            Client = client;
            ClientId = clientId;
        }

        public Client Client { get; set; }
        public int ClientId { get; set; }
    }
}
