
using KhalifaCommunityAPI.Models;
using KhalifaCommunityAPI.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Runtime.Serialization;

namespace KhalifaCommunity.Controllers
{
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// create response for request
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="message"></param>
        /// <param name="responseToReturn"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        internal static ApiResponse Response(MessageType messageType, string message = "", object responseToReturn = null, int totalRecord = 0)
        {
            return ApiHttpUtility.CreateResponse(isAuthenticated: true, messageType: messageType, message: message, responseToReturn: responseToReturn, total: totalRecord);
        }
    }
}