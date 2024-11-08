namespace AzzanOrder.ManagerOwner.DTOs
{
    public class MemberVoucherDTO
    {
        public int MemberId { get; set; }
        public int VoucherDetailId { get; set; }
        public bool? IsActive { get; set; }
        public int? OrderId { get; set; }
    }
}
