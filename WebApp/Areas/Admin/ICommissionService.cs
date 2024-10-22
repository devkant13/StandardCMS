using StandardCMS.DB.Models;

namespace WebApp.Areas.Admin
{
    public interface ICommissionService
    {
        Task ApplyCommission(Sale sale);
    }
}
