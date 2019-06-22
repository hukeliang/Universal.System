using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using System.Text;

namespace Universal.System.WebApi.Extensions
{
    public static class ModelExtension
    {
        /// <summary>
        /// 验证模型错误,返回全部错误
        /// </summary>
        /// <param name="modelStateDictionary"></param>
        /// <returns></returns>
        public static string ToErrorMessageALl(this ModelStateDictionary modelStateDictionary)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (ModelStateEntry modelstate in modelStateDictionary.Values)
            {
                if (modelstate != null && modelstate.ValidationState == ModelValidationState.Invalid)
                {
                    foreach (ModelError error in modelstate.Errors)
                    {
                        stringBuilder.AppendLine(error.ErrorMessage);
                    }
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// 验证模型 返回单个错误
        /// </summary>
        /// <param name="modelStateDictionary"></param>
        /// <returns></returns>
        public static string ToErrorMessage(this ModelStateDictionary modelStateDictionary)
        {

            foreach (ModelStateEntry modelstate in modelStateDictionary.Values)
            {
                if (modelstate != null && modelstate.ValidationState == ModelValidationState.Invalid)
                {
                    return modelstate.Errors.FirstOrDefault().ErrorMessage;
                }   
            }

            return string.Empty;
        }
    }
}
