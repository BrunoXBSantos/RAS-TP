namespace API.DTOs;
public class TakeMoneyWalletDTO
{
    [Required]
    public float val { get; set; }
    [Required]
    public float type { get; set; }
    [Required]
    public WalletDTO wallet { get; set; }

}