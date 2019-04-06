namespace Czar.Cms.ViewModels
{
    public class BaseResult
    {
        /// <summary>
        /// 结果编码
        /// </summary>
        public int ResultCode { get; set; } = ResultCodeAddMsgKeys.CommonObjectSuccessCode;

        public string ResultMsg { get; set; } = ResultCodeAddMsgKeys.CommonObjectSuccessMsg;

        public BaseResult()
        {
            
        }

        public BaseResult(int resultCode,string resultMsg)
        {
            ResultCode = resultCode;
            ResultMsg = resultMsg;
        }

    }
}