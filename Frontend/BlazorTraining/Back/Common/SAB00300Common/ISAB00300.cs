using System.Collections.Generic;
using R_CommonFrontBackAPI;
using SAB00300Common.DTOs;

namespace SAB00300Common
{
    public interface ISAB00300 : R_IServiceCRUDBase<SAB00300DTO>
    {
        SAB00300ListDTO<SAB00300DTO> GetAllRegion();
        IAsyncEnumerable<SAB00300DTO> GetAllRegionStream();

    }
}