namespace TranScopeCommon
{
    public interface ITranScope
    {
        TranScopeDataDTO ProcessWthoutTransaction(int poProcessRecordCount);
    }
}