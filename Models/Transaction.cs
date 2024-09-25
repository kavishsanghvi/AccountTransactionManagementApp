using System.ComponentModel.DataAnnotations;

public class Transaction
{
    [Key]
    public int id { get; set; }
    public string description { get; set; }
    public string debit_credit { get; set; } // "debit" or "credit"
    public decimal amount { get; set; }

    public int account_id { get; set; }
}
