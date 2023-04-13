using R_CommonFrontBackAPI;
using SAB00300Common.DTOs;

namespace SAB00300Common
{
    public interface ISAB00300 : R_IServiceCRUDBase<SAB00300DTO>
    {
        SAB00300ListDTO<SAB00300DTO> GetAllRegions();
    }
    public interface ISAB00310 : R_IServiceCRUDBase<SAB00310DTO>
    {
        SAB00300ListDTO<SAB00310DTO> GetAllTerritories();
        SAB00300ListDTO<SAB00310DTO> GetAllTerritoriesByRegion(int piRegionId);
    }
}