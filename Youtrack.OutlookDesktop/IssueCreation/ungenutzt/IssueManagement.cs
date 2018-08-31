//using System;
//using System.Collections.Generic;
//using System.Dynamic;
//using System.Linq;
//using System.Net;
//using System.Threading;
//using System.Web;

//using EasyHttp.Http;

//using RestSharp;

//using YouTrackSharp.Infrastructure;

//using HttpException = EasyHttp.Infrastructure.HttpException;

//namespace Youtrack.OutlookDesktop.Issues
//{
//    public class IssueManagement
//    {
//        private static readonly List<string> PresetFields = new List<string>
//                                                            {
//                                                                    "assignee",
//                                                                    "priority",
//                                                                    "type",
//                                                                    "subsystem",
//                                                                    "state",
//                                                                    "fixVersions",
//                                                                    "affectsVersions",
//                                                                    "fixedInBuild",
//                                                                    "summary",
//                                                                    "description",
//                                                                    "project",
//                                                                    "permittedgroup"
//                                                            };

//        private readonly IConnection _connection;

//        public IssueManagement(IConnection connection)
//        {
//            _connection = connection;
//        }

//        public void ApplyCommand(string issueId, string command, string comment, bool disableNotifications = false, string runAs = "")
//        {
//            if (!_connection.IsAuthenticated)
//                throw new InvalidRequestException(Language.YouTrackClient_CreateIssue_Not_Logged_In);

//            try
//            {
//                dynamic commandMessage = new ExpandoObject();
//                //command = "Zeit 2018-04-05";
//                commandMessage.command = command;
//                commandMessage.comment = comment;
//                if (disableNotifications)
//                    commandMessage.disableNotifications = disableNotifications;
//                if (!string.IsNullOrWhiteSpace(runAs))
//                    commandMessage.runAs = runAs;

//                _connection.Post(string.Format("issue/{0}/execute", issueId), commandMessage);
//            }
//            catch (HttpException httpException)
//            {
//                throw new InvalidRequestException(httpException.StatusDescription, httpException);
//            }
//        }

//        public void AttachFileToIssue(string issuedId, string path)
//        {
//            RestRequest restRequest = new RestRequest(string.Format("/issue/{0}/attachment", issuedId), Method.POST);

//            foreach (Cookie cookie in _connection.AuthenticationCookie)
//                restRequest.AddCookie(cookie.Name, cookie.Value);

//            restRequest.AddFile("File", path);

//            RestClient restClient = new RestClient(_connection.BaseUri);
//            restClient.Execute(restRequest);

//            //_connection.PostFile(string.Format("issue/{0}/attachment", issuedId), path);

//            //if (_connection.HttpStatusCode != HttpStatusCode.Created)
//            //{
//            //    throw new InvalidRequestException(_connection.HttpStatusCode.ToString());
//            //}
//        }

//        public bool CheckIfIssueExists(string issueId)
//        {
//            try
//            {
//                _connection.Head(string.Format("issue/{0}/exists", issueId));
//                return _connection.HttpStatusCode == HttpStatusCode.OK;
//            }
//            catch (HttpException httpException)
//            {
//                throw new InvalidRequestException(httpException.StatusDescription, httpException);
//            }
//        }

//        public string CreateIssue(Issue issue)
//        {
//            if (!_connection.IsAuthenticated)
//                throw new InvalidRequestException(Language.YouTrackClient_CreateIssue_Not_Logged_In);

//            try
//            {
//                ExpandoObject fieldList = issue.ToExpandoObject();

//                dynamic response = _connection.Post("issue", fieldList, HttpContentTypes.ApplicationJson);

//                Dictionary<string, object> customFields = fieldList.Where(field => !PresetFields.Contains(field.Key.ToLower()))
//                        .ToDictionary(field => field.Key, field => field.Value);


//                foreach (KeyValuePair<string, object> customField in customFields)
//                {
//                    ApplyCommand(response.id, string.Format("{0} {1}", customField.Key, customField.Value), string.Empty);
//                }
//                return response.id;
//            }
//            catch (HttpException httpException)
//            {
//                throw new InvalidRequestException(httpException.StatusDescription, httpException);
//            }
//        }

//        public void Delete(string id)
//        {
//            _connection.Delete(string.Format("issue/{0}", id));
//        }

//        public void DeleteComment(string issueId, string commentId, bool deletePermanently)
//        {
//            _connection.Delete(string.Format("issue/{0}/comment/{1}?permanently={2}", issueId, commentId, deletePermanently));
//        }

//        /// <summary>
//        ///     Retrieves a list of issues
//        /// </summary>
//        /// <param name="projectIdentifier">Project Identifier</param>
//        /// <param name="max">[Optional] Maximum number of issues to return. Default is int.MaxValue</param>
//        /// <param name="start">[Optional] The number by which to start the issues. Default is 0. Used for paging.</param>
//        /// <returns>List of Issues</returns>
//        public IEnumerable<Issue> GetAllIssuesForProject(string projectIdentifier, int max = int.MaxValue, int start = 0)
//        {
//            return
//                    _connection.Get<MultipleIssueWrapper, Issue>(string.Format("project/issues/{0}?max={1}&after={2}",
//                            projectIdentifier,
//                            max,
//                            start));
//        }

//        /// <summary>
//        ///     Retrieve comments for a particular issue
//        /// </summary>
//        /// <param name="issueId"></param>
//        /// <returns></returns>
//        public IEnumerable<Comment> GetCommentsForIssue(string issueId)
//        {
//            return _connection.Get<IEnumerable<Comment>>(String.Format("issue/comments/{0}", issueId));
//        }

//        /// <summary>
//        ///     Retrieve an issue by id
//        /// </summary>
//        /// <param name="issueId">Id of the issue to retrieve</param>
//        /// <returns>An instance of Issue if successful or InvalidRequestException if issues is not found</returns>
//        public Issue GetIssue(string issueId)
//        {
//            try
//            {
//                dynamic response = _connection.Get<Issue>(String.Format("issue/{0}", issueId));
//                return response;
//            }
//            catch (HttpException exception)
//            {
//                throw new InvalidRequestException(
//                        String.Format(Language.YouTrackClient_GetIssue_Issue_not_found___0_, issueId),
//                        exception);
//            }
//        }

//        public int GetIssueCount(string searchString)
//        {
//            string encodedQuery = HttpUtility.UrlEncode(searchString);

//            try
//            {
//                int count = -1;

//                while (count < 0)
//                {
//                    var countObject = _connection.Get<Count>(string.Format("issue/count?filter={0}", encodedQuery));

//                    count = countObject.Entity.Value;
//                    Thread.Sleep(3000);
//                }

//                return count;
//            }
//            catch (HttpException httpException)
//            {
//                throw new InvalidRequestException(httpException.StatusDescription, httpException);
//            }
//        }

//        public IEnumerable<Issue> GetIssuesBySearch(string searchString, int max = int.MaxValue, int start = 0)
//        {
//            string encodedQuery = HttpUtility.UrlEncode(searchString);

//            return
//                    _connection.Get<MultipleIssueWrapper, Issue>(string.Format("project/issues?filter={0}&max={1}&after={2}",
//                            encodedQuery,
//                            max,
//                            start));
//        }

//        /// <summary>
//        /// This POST method allows updating issue summary and/or description, only. To update issue fields, please use method to Apply Command to an Issue. 
//        /// </summary>
//        /// <param name="issueId"></param>
//        /// <param name="summary"></param>
//        /// <param name="description"></param>
//        public void UpdateIssue(string issueId, string summary, string description)
//        {
//            if (!_connection.IsAuthenticated)
//                throw new InvalidRequestException(Language.YouTrackClient_CreateIssue_Not_Logged_In);

//            try
//            {
//                dynamic commandMessage = new ExpandoObject();

//                commandMessage.summary = summary;
//                commandMessage.description = description;

//                _connection.Post(string.Format("issue/{0}", issueId), commandMessage);
//            }
//            catch (HttpException httpException)
//            {
//                throw new InvalidRequestException(httpException.StatusDescription, httpException);
//            }
//        }
//    }
//}