using SQLite;
using Microsoft.Maui.Graphics;

public class Transaction
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } // Cash on Bank or Cash on Hand
                                     // public Color TextColor => Amount >= 0 ? Colors.Green : Colors.Red;
                                     //  public string TextColor { get; set; }
                                     //public Color TextColor { get; set; }
    public string TextColor => Amount >= 0 ? "#00FF00" : "#FF0000"; // Green for positive, red for negative



}
