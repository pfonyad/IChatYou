namespace IChatYou.App.Models
{
    public class AjaxBaseResultModel
    {
        public AjaxBaseResultModel()
        {

        }

        public AjaxBaseResultModel(bool isSuccess, object result, string exception)
        {
            IsSuccess = isSuccess;
            Result = result;
            Exception = exception;
        }

        public bool IsSuccess { get; set; } = false;

        public object Result { get; set; } = null;

        public string Exception { get; set; } = string.Empty;
    }
}