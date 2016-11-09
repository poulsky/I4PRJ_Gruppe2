using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BargainBarterV2.Models
{
    public class TradeRequest
    {
        public int TradeRequestId { get; set; }
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [EnumDataType(typeof(States))]
        public States RequestStates { get; set; }


        public enum States { Traded, Pending, Received }
        
        public virtual List<BarterAdd> BarterAdds { get; set; } = new List<BarterAdd>();
        
    }
}