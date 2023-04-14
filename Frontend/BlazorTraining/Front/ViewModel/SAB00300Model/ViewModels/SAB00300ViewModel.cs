using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using SAB00300Common.DTOs;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SAB00300Model.ViewModels
{
    public class SAB00300ViewModel : R_ViewModel<SAB00300DTO>
    {
        private SAB00300Model _SAB00300Model = new SAB00300Model();

        public ObservableCollection<SAB00300DTO> RegionList { get; set; } = new ObservableCollection<SAB00300DTO>();

        public SAB00300DTO Region = new SAB00300DTO();

        public async Task GetRegionList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _SAB00300Model.GetAllRegionAsync();
                RegionList = new ObservableCollection<SAB00300DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetRegion(int piRegionId)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new SAB00300DTO { RegionID = piRegionId };
                var loResult = await _SAB00300Model.R_ServiceGetRecordAsync(loParam);

                Region = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveRegion(SAB00300DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _SAB00300Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                Region = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteRegion(int categoryId)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new SAB00300DTO { RegionID = categoryId };
                await _SAB00300Model.R_ServiceDeleteAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}