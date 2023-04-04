using System.Collections.Generic;
using R_CommonFrontBackAPI;

namespace GSM00200Commmon
{
    public interface IGSM00210 : R_IServiceCRUDBase<GSM00210DTO>
    {
        IAsyncEnumerable<GSM00210DTOnon> GetTableDTList();
    }
}