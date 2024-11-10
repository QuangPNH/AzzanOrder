using Microsoft.AspNetCore.Mvc.RazorPages;

namespace payos.Models
{
    public class Model
    {
        internal int totalPages;


        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public int maxPageNav { get; set; }

        public int anIntegerUsedForCountingNumberOfPageQueuedForTheList { get; set; }
        public int anIntegerUsedForKnowingWhatTheCurrentPageOfTheList { get; set; }
        public int thisIntegerIsUsedForKnowingTheMaxNumberOfPageNavButtonShouldBeDisplayed { get; set; }
        public IEnumerable<About> abouts { get; set; }
        public IEnumerable<Bank> banks { get; set; }
        public IEnumerable<Employee> employees { get; set; }
        public IEnumerable<Feedback> feedbacks { get; set; }
        public IEnumerable<ItemCategory> itemCategories { get; set; }
        public IEnumerable<Member> members { get; set; }
        public IEnumerable<MemberVoucher> memberVouchers { get; set; }
        public IEnumerable<Notification> notifications { get; set; }
        public IEnumerable<Order> orders { get; set; }
        public IEnumerable<Owner> owners { get; set; }
        public IEnumerable<Promotion> promotions { get; set; }
        public IEnumerable<Role> roles { get; set; }
        public IEnumerable<Table> tables { get; set; }
        public IEnumerable<Voucher> vouchers { get; set; }
        public IEnumerable<VoucherDetail> voucherDetails { get; set; }
        public IEnumerable<Api.Bank> bankData { get; set; }
        public IEnumerable<Api.Datum> bankDatums{ get; set; }

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
        public Table table { get; set; }
        public Voucher voucher { get; set; }
        public VoucherDetail voucherDetail { get; set; }
        public Api.Bank theBank { get; set; }
        public Api.Datum bankDatum { get; set; }



    }
}
