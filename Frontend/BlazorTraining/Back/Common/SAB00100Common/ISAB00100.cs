using R_CommonFrontBackAPI;
using SAB00100Common.DTOs;

namespace SAB00100Common
{
    public interface ISAB00100 : R_IServiceCRUDBase<SAB00100DTO>
    {
        SAB00100ListEmployeeDTO GetAllEmployee();
    }
}
