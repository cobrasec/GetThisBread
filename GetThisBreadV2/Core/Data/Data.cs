using System.Threading.Tasks;
using System.Linq;
using GetThisBreadV2.Core.Currency;
using GetThisBreadV2.Data.Database;


namespace GetThisBreadV2.Data
{
    public static class Data
    {
        public async Task GetCoins(ulong UserId)
        {

        }

        public async void SaveCoins(ulong UserId, int Amount)
        {
            using (var DbContext = new SQLiteDBContext())
            {
                if (DbContext.BreadCoin.Where(x => x.UserId == UserId).Count() < 1)

                //The user doesn't have a row yet, creat one for them
                DbContext.BreadCoin.Add(new Coin
                {
                    UserId = UserId, 
                    Amount = Amount
                });
            }else
            {
                Coin Current = DbContext
            }    

        }

    }
}
