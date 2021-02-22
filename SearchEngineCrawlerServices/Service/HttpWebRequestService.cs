using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using SearchEngineCrawlerServices.Contract;
using SearchEngineCrawlerServices.Dto;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SearchEngineCrawlerServices.Service
{
    public class HttpWebRequestService : IHttpWebRequestService
    {
        public async Task<IServiceResult<string>> GetPageAsStringAsync(string pageUrl, string queryString = "")
        {
            List<string> errorMessages = new List<string>();
            if (!validatePageUrl(pageUrl, out errorMessages))
            {
                return new ServiceResult<string>(errorMessages);
            }


            try
            {
                var request = WebRequest.Create(@$"{pageUrl}?{queryString}");
                var response = await request.GetResponseAsync();
                using StreamReader streamReader = new StreamReader(response.GetResponseStream());

                return new ServiceResult<string>(streamReader.ReadToEnd());
            }

            catch (Exception e)
            {
                return new ServiceResult<string>(new List<string> { e.Message });
            }

        }

        private bool validatePageUrl(string pageUrl, out List<string> errorMessages)
        {
            bool isValid = true;
            errorMessages = new List<string>();
            if (string.IsNullOrEmpty(pageUrl))
            {
                errorMessages.Add("Page URL cannot be empty.");
                isValid = false;
            }

            if (!Regex.IsMatch(pageUrl, @"^((http|https)://)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)$"))
            {
                errorMessages.Add("Page URL is not valid.");
                isValid = false;
            }

            return isValid;
        }

    }
}
