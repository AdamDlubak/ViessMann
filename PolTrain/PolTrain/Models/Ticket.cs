namespace PolTrain.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public int SeatsAmt { get; set; }
        public decimal Price { get; set; }
        public int ConcessionId { get; set; }
        public virtual Concession Concession { get; set; }
        public int LineId { get; set; }
        public virtual Line Line { get; set; }
        public int ReservationId { get; set; }
        public virtual Reservation Reservation { get; set; }
    }
}