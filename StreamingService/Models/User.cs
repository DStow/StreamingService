using System;
using System.ComponentModel.DataAnnotations;

namespace StreamingService.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
        public Guid SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }
        public int FreeSongs { get; set; }
        public int RemainingSongsThisMonth { get; set; }

        public virtual void ResetRemainingSongsThisMonth()
        {
            this.RemainingSongsThisMonth = FreeSongs;
        }
    }


    // This should ideally be a limited and unlimited user
    // limited user would have a free songs and remaining songs property
    //public class UnlimittedUser : User
    //{
    //    public override void ResetRemainingSongsThisMonth()
    //    {
    //        this.RemainingSongsThisMonth = 0;
    //    }
    //}
}
