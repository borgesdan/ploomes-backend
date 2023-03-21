using Ploomes.Application.Repositories;

namespace Ploomes.Application.Services
{
    public class SellerService
    {
        private readonly UserRepository _userRepository;

        public SellerService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    }
}
