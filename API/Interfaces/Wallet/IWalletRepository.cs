using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Model;

namespace API.Interfaces
{
    public interface IWalletRepository
    {
        public WalletRepository(DataContext context, IMapper mapper);
        public void AddWallet(Wallet wallet);
        public Task<Wallet> GetWalletByIdAsync(string appUserId);
        public Task<bool> checkWalletByIdAsync(string appUserId);
        public Task<List<WalletDTO>> GetListWalletsAsync();
        
    }
}