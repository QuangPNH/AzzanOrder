

namespace AzzanOrder.ManagerOwner.Models
{
    public class Model
    {

        public int anIntegerUsedForCountingNumberOfPageQueuedForTheList { get; set; }
        public int anIntegerUsedForKnowingWhatTheCurrentPageOfTheList { get; set; }
        public int thisIntegerIsUsedForKnowingTheMaxNumberOfPageNavButtonShouldBeDisplayed { get; set; }
        public IEnumerable<Employee> employees { get; set; }


        
        public About about { get; set; }
        public Bank bank { get; set; }
        public Employee employee { get; set; }
        public Feedback feedback { get; set; }
        public ItemCategory itemCategory { get; set; }
        public Member member { get; set; }
        public MemberVoucher memberVoucher { get; set; }
        public Notification notification { get; set; }
        public Order order { get; set; }
        public Owner owner { get; set; }
        public Promotion promotion { get; set; }
        public Role role { get; set; }
        public Table Table { get; set; }
        public Voucher voucher { get; set; }
        public VoucherDetail voucherDetail { get; set; }
    }
}
