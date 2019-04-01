namespace Czar.Cms.ViewModels
{
    public class TableDataModel
    {

        public int code { get; set; } = 0;

        public string msg { get; set; } = "操作成功";

        public int count { get; set; }

        public dynamic data { get; set; }
    }
}