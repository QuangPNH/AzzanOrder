using AzzanOrder.Data.Models;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace AzzanOrder.Data.DTO
{

    public static class EdmModel
    {
        public static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<VoucherDetail>("VoucherDetail");
            return modelBuilder.GetEdmModel();
        }
    }

}
