namespace Czar.Cms.ViewModels
{
    public class BaseResult
    {
        /// <summary>
        /// 结果编码
        /// </summary>
        public int ResultCode { get; set; } = ResultCodeAddMsgKeys.CommonObjectSuccessCode;

        public string ResultMsg { get; set; } = ResultCodeAddMsgKeys.CommonObjectSuccessMsg;

        public string ReturnUrl { get; set; } = "/";

        public BaseResult()
        {
            
        }

        public BaseResult(int resultCode,string resultMsg)
        {
            ResultCode = resultCode;
            ResultMsg = resultMsg;
        }
        public BaseResult(int resultCode, string resultMsg,string returnUrl)
        {
            ResultCode = resultCode;
            ResultMsg = resultMsg;
            ReturnUrl = returnUrl;
        }

    }
}