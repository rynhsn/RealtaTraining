using R_CommonFrontBackAPI;
using SAB01300Common.DTOs;

namespace SAB01300Common
{
    public interface ISAB01310 : R_IServiceCRUDBase<SAB01310DTO>
    {
        SAB01300ListDTO<SAB01310DTO> GetAllProduct();

        SAB01300ListDTO<SAB01310DTO> GetAllProductByCategory(int piCategoryId);
    }
}
