using CRUDCommon;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace CRUDBack;

public class CRUDCls:R_BusinessObject<CustomerDTO>
{
    public List<CustomerStreamDTO> CustomerListDb(CustomerListDBParameterDTO poParameter)
    {
        R_Exception loException = new R_Exception();
        List<CustomerStreamDTO> loRtn = null;
        string lcCmd;
        R_Db loDb;
        
        try
        {
            lcCmd = "SELECT CCOMPANY_ID, CustomerID, CustomerName " +
                    "FROM TrainCustomer(nolock) " +
                    "WHERE CCOMPANY_ID = {0}";
            loDb = new R_Db();
            loRtn = loDb.SqlExecObjectQuery<CustomerStreamDTO>(lcCmd, poParameter.CCOMPANY_ID);
            //{0} pada lcCmd akan diganti dengan nilai dari poParameter.CCOMPANY_ID
            
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();

        return loRtn;
    }

    protected override CustomerDTO R_Display(CustomerDTO poEntity)
    {
        R_Exception loException = new R_Exception();
        CustomerDTO loRtn = null;
        string lcCmd;
        R_Db loDb;
        try
        {
            lcCmd = "SELECT * FROM TrainCustomer(nolock) " +
                    "WHERE CCOMPANY_ID = {0} " +
                    "AND CustomerID = {1}";
            loDb = new R_Db();
            loRtn = loDb.SqlExecObjectQuery<CustomerDTO>(lcCmd, poEntity.CCOMPANY_ID, poEntity.CustomerID).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();

        return loRtn;
    }

    protected override void R_Saving(CustomerDTO poNewEntity, eCRUDMode poCRUDMode)
    {
        throw new NotImplementedException();
    }

    protected override void R_Deleting(CustomerDTO poEntity)
    {
        throw new NotImplementedException();
    }
}