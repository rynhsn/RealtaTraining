using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using SAB00300Common.DTOs;

namespace SAB00300Back;

public class SAB00300Cls : R_BusinessObject<SAB00300DTO>
{
    protected override SAB00300DTO R_Display(SAB00300DTO poEntity)
    {
        var loEx = new R_Exception();
        SAB00300DTO loResult = null;

        try
        {
            var loDb = new R_Db();
            var loConn = loDb.GetConnection("NorthwindConnectionString");

            var lcQuery = $"SELECT * FROM Region (NOLOCK) WHERE RegionID = {poEntity.RegionID}";
            loResult = loDb.SqlExecObjectQuery<SAB00300DTO>(lcQuery, loConn, true).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loResult;
    }

    protected override void R_Saving(SAB00300DTO poNewEntity, eCRUDMode poCRUDMode)
    {
        var loEx = new R_Exception();

        try
        {
            string lcQuery = "";
            var loDb = new R_Db();
            var loConn = loDb.GetConnection("NorthwindConnectionString");

            if (poCRUDMode == eCRUDMode.AddMode)
            {
                lcQuery = "INSERT INTO Region (RegionDescription) ";
                lcQuery += $"VALUES ('{poNewEntity.RegionDescription}')";
                loDb.SqlExecNonQuery(lcQuery, loConn, true);

                return;
            }

            lcQuery = $"UPDATE Region SET RegionDescription = '{poNewEntity.RegionDescription}'";
            lcQuery += $"WHERE RegionID = {poNewEntity.RegionID} ";
            loDb.SqlExecNonQuery(lcQuery, loConn, true);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    protected override void R_Deleting(SAB00300DTO poEntity)
    {
        var loEx = new R_Exception();

        try
        {
            var loDb = new R_Db();
            var loConn = loDb.GetConnection("NorthwindConnectionString");

            var lcQuery = $"DELETE FROM Region WHERE RegionID = {poEntity.RegionID}";
            loDb.SqlExecNonQuery(lcQuery, loConn, true);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    public List<SAB00300DTO> GetCategories()
    {
        var loEx = new R_Exception();
        List<SAB00300DTO> loResult = null;

        try
        {
            var loDb = new R_Db();
            var loConn = loDb.GetConnection("NorthwindConnectionString");

            var lcQuery = $"SELECT * FROM Region (NOLOCK)";
            loResult = loDb.SqlExecObjectQuery<SAB00300DTO>(lcQuery, loConn, true);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loResult;
    }
}