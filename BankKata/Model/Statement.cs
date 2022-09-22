namespace bank_kata.Model
{
    public class Statement
    {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public OperationType Operation { get; set; }
        public decimal Ammount { get; set; }
        public decimal Balance { get; set; }
        public DateTime DateTime { get; set; }
    }
}
