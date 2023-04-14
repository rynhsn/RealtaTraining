using System.Collections.Generic;
using R_CommonFrontBackAPI;
using SAB00300Common.DTOs;

namespace SAB00300Common
{
    public interface ISAB00310 : R_IServiceCRUDBase<SAB00310DTO>
    {
        SAB00300ListDTO<SAB00310DTO> GetAllTerritory();
        SAB00300ListDTO<SAB00310DTO> GetAllTerritoryByRegion(int piRegionId);
        
        IAsyncEnumerable<SAB00310DTO> GetAllTerritoryByRegionStream();
    }
}