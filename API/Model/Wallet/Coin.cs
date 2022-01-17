using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model;

public class Coin{

    public int Id { get; set; }
    public string Name { get; set; }
    public double ConvertionToEuro { get; set; }
    public ICollection<WalletCoin> WalletCoin { get; set; }    

}