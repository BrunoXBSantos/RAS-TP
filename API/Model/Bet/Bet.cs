using System.ComponentModel.DataAnnotations;

namespace API.Model;
public class Bet
{
    public int Id { get; set; }
    public double value { get; set; }
    public int coinID { get; set; }

    [RegularExpression("1|X|2", ErrorMessage = "The type must be either '1', '2' or 'X' only.")]
    public string Result { get; set; }

    // Table AppUser
    public AppUser appUser { get; set; }
    public int appUserId { get; set; }

    // table BetState
    public BetState betState { get; set; }
    public int betStateId { get; set; }

    // table Event
    public EventDB _event { get; set; }
    public int _eventId { get; set; }


}