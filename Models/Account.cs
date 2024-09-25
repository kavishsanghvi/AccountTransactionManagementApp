using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Account
{
    [Key]
    public int id { get; set; }
    public string name { get; set; }
    public string number { get; set; }
    public decimal current_balance { get; set; }
    public decimal overdraft_limit { get; set; }

    public List<Transaction> Transactions { get; set; }
}
