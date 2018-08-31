using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Threading;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Graph;
using System.Threading.Tasks;

using System.Text;
using System.Diagnostics;
using System.Drawing;
using ByteSizeLib;
using System.IO;
using System.Windows.Forms;

namespace Meissner.MicrosoftPlanner
{
    public interface IIssueCreationViewModel { }
    public class IssueCreationViewModel : IIssueCreationViewModel, INotifyPropertyChanged
    {
        private string _neuAufgabe;
        private string _mailSubject;
        private GraphServiceClient _graphClient;
        public ObservableCollection<ModelViewAttachment> Attachments { get; private set; }
        public DelegateCommand<ModelViewAttachment> RemoveAttachmentCommand { get; set; }
        public List<CustomPercentComplete> CustomPercentCompleteGroups { get; set; }
        public PlannerCategoryDescriptions plannerCategoryDescriptions;
        public List<CustomCategory> CustomCategoryGroups { get; set; }
        public DelegateCommand SubmitCommand { get; set; }

        public IssueCreationViewModel(GraphServiceClient graphClient, string taskTitle, string mailSubject,
                IEnumerable<ModelViewAttachment> attachments)
        {
            if (graphClient == null)
                throw new ArgumentNullException("graphClient");
            _graphClient = graphClient;
            _taskTitle = taskTitle;
            _mailSubject = mailSubject;
            Attachments = new ObservableCollection<ModelViewAttachment>(attachments);
            Initialize();
        }

        private void Initialize()
        {
            CustomCategoryGroups = new List<CustomCategory>();
            UploadPath = new List<string>() { (_mailSubject + "\n") };
            PlannerGroups = _graphClient.Groups.Request().GetAsync().Result.ToList();
            SubmitCommand = new DelegateCommand(CreateOrChangeIssue, CanCreateOrChangeIssue);
            RemoveAttachmentCommand = new DelegateCommand<ModelViewAttachment>(attachment => Attachments.Remove(attachment));
            SelectedGroup = PlannerGroups.First();
            SelectedPlan = PlannerPlans.First();
            _neuAufgabe = "---Neue Aufgabe---";
            ActionsVisible = Visibility.Hidden;
            CustomPercentCompleteGroups = new List<CustomPercentComplete>()
            {
                new CustomPercentComplete("Nicht begonnen", 0),
                new CustomPercentComplete("In Arbeit", 50),
                new CustomPercentComplete("Erledigt", 100)
            };
            SelectedPercentComplete = CustomPercentCompleteGroups.First();
            //SelectedPriority = Priorities.First(p => p.Name == "Normal");
        }

        private void CreateOrChangeIssue()
        {
            if (_selectedTask.Title.Equals(_neuAufgabe))
            {
                CreateIssue();
            }
            else
            {
                ChangeIssue();
            }
            #region ungenutzte Befehle
            //var taskWithConversationThreadId = _graphClient.Planner.Tasks[_selectedTask.Id].Request().GetAsync().Result;
            // PlannerChecklistItems
            //Post post = new Post() { Body = new ItemBody() { Content = String.Join("\n", UploadPath) } };
            //var aaeee = _graphClient.Groups[_selectedGroup.Id].Threads.Request().GetAsync().Result;
            //var getPlannerTask = _graphClient.Planner.Tasks[_selectedTask.Id].Request().GetAsync().Result;
            //_graphClient.Planner.Tasks[_selectedTask.Id].Details.Request().GetAsync().Result.GetEtag();
            //var getTaskDetails = _graphClient.Planner.Tasks[_selectedTask.Id].Details.Request(new List<Option> { new HeaderOption("If-Match", taskPlannerDetailsETag) }).UpdateAsync(plannerTaskDetails).Result;
            //_graphClient.Groups[_selectedGroup.Id].Threads[_selectedTask.ConversationThreadId].Reply(post).Request().PostAsync();
            #endregion
        }

        private void CreateIssue()
        {
            HochladenAsync(Attachments, SelectedBucket.Name, SelectedCategory.Description,_selectedGroup.Id);

            PlannerAppliedCategories appliedCategories=CreateAppliedCategories(_selectedCategory);
            Conversation conversation = CreateConversation(UploadPath, _selectedTask.Title);
            var newConversationId = _graphClient.Groups[_selectedGroup.Id].Conversations.Request().AddAsync(conversation).Result.Id;
            PlannerTask plannerTask = new PlannerTask()
            {
                PlanId = _selectedPlan.Id,
                BucketId = _selectedBucket.Id,
                Title = _taskTitle,
                ConversationThreadId = newConversationId,
                PercentComplete = SelectedPercentComplete.Value,
                DueDateTime = _selectedDueDate,
                AppliedCategories = appliedCategories
            };
            var createdTask = _graphClient.Planner.Tasks.Request().AddAsync(plannerTask).Result;
            string createdTaskDetailsEtag = "";
            while (createdTask!=null && createdTaskDetailsEtag.Equals(""))
            {
                try
                {  createdTaskDetailsEtag = _graphClient.Planner.Tasks[createdTask.Id].Details.Request().GetAsync().Result.GetEtag(); }
                catch (Exception e)
                {
                    Thread.Sleep(500);
                }
            }

            PlannerTaskDetails plannerTaskDetails = new PlannerTaskDetails() { Description = _description};
            _graphClient.Planner.Tasks[createdTask.Id].Details.Request()
                .Header("If-Match", createdTaskDetailsEtag)
                .UpdateAsync(plannerTaskDetails);
            System.Windows.MessageBox.Show(string.Format("task {0} created.", createdTask.Title));
        }

        private void ChangeIssue()
        {
            HochladenAsync(Attachments, _selectedBucket.Name, _selectedCategory.Description, _selectedGroup.Id);
            PlannerAppliedCategories appliedCategories = CreateAppliedCategories(_selectedCategory);
            Conversation conversation = CreateConversation(UploadPath, _selectedTask.Title);
            var taskDetailsETag = _graphClient.Planner.Tasks[_selectedTask.Id].Details.Request().GetAsync().Result.GetEtag();
            var currentConversationthreadId = _graphClient.Planner.Tasks[_selectedTask.Id].Request().GetAsync().Result.ConversationThreadId;
            PlannerTask plannerTask = new PlannerTask()
            {
                PercentComplete = SelectedPercentComplete.Value,
                DueDateTime = _selectedDueDate,
                AppliedCategories = appliedCategories
            };
            if (currentConversationthreadId != null)
            {
              _graphClient.Groups[_selectedGroup.Id].Threads[currentConversationthreadId].Reply(conversation.Threads[0].Posts[0]).Request().PostAsync();
            }
            else
            {
                var neuConversationThreadId = _graphClient.Groups[_selectedGroup.Id].Conversations.Request().AddAsync(conversation).Result.Id;
                plannerTask.ConversationThreadId = neuConversationThreadId;//neue Thread zugewiesen
            }

            _graphClient.Planner.Tasks[_selectedTask.Id].Request()
                .Header("If-Match", _selectedTask.GetEtag())
                .Header("Prefer", "return=representation")
                .UpdateAsync(plannerTask);
            var currentTaskDetailsEtag = _graphClient.Planner.Tasks[_selectedTask.Id].Details.Request().GetAsync().Result.GetEtag();
            PlannerTaskDetails plannerTaskDetails = new PlannerTaskDetails() { Description = _description };
            _graphClient.Planner.Tasks[_selectedTask.Id].Details.
                Request()
                .Header("If-Match", currentTaskDetailsEtag)
                .UpdateAsync(plannerTaskDetails);
            System.Windows.MessageBox.Show(string.Format("task {0} changed.", _selectedTask.Title));
        }

        private Conversation CreateConversation(List<string> UploadPath, string topic)
        {            // Build the conversation
            Conversation conversation = new Conversation()
            {
                Topic = topic,
                // Conversations have threads
                Threads = new ConversationThreadsCollectionPage()
            };
            conversation.Threads.Add(new ConversationThread()
            {
                // Threads contain posts
                Posts = new ConversationThreadPostsCollectionPage()
            });
            conversation.Threads[0].Posts.Add(new Post()
            {
                // Posts contain the actual content
                Body = new ItemBody() { Content = String.Join("\n", UploadPath), ContentType = BodyType.Text }
            });
            return conversation;
        }

        private PlannerAppliedCategories CreateAppliedCategories(CustomCategory selectedCategory)
        {
            PlannerAppliedCategories appliedCategories = new PlannerAppliedCategories();
            System.Reflection.PropertyInfo[] propertyInfos = appliedCategories.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.PropertyType == typeof(bool?))
                {
                    if (propertyInfo.Name.Equals(selectedCategory.Name))
                        propertyInfo.SetValue(appliedCategories, selectedCategory.Value);
                    else
                        propertyInfo.SetValue(appliedCategories, false);
                }
            }
            return appliedCategories;
        }

        private bool CanCreateOrChangeIssue()
        {
            if (ActionsVisible==Visibility.Hidden)
                return  SelectedPlan != null
                   && SelectedBucket != null
                   && SelectedTask != null
                   && SelectedCategory != null
                    ;
            else
                return !string.IsNullOrEmpty(TaskTitle)
                       && SelectedPlan != null
                       && SelectedBucket != null
                       && SelectedTask != null
                       && SelectedCategory != null
                        ;
        }

        #region Properties of the control elements

        private List<Group> _plannerGroups;
        public List<Group> PlannerGroups
        {
            get { return _plannerGroups; }
            set
            {
                _plannerGroups = value;
                OnPropertyChanged("PlannerGroups");
            }
        }

        private List<PlannerPlan> _plannerPlans;
        public List<PlannerPlan> PlannerPlans
        {
            get { return _plannerPlans; }
            set
            {
                _plannerPlans = value;
                OnPropertyChanged("PlannerPlans");
            }
        }

        private List<PlannerBucket> _plannerBuckets;
        public List<PlannerBucket> PlannerBuckets
        {
            get { return _plannerBuckets; }
            set
            {
                _plannerBuckets = value;
                OnPropertyChanged("PlannerBuckets");
            }
        }

        private List<PlannerTask> _plannerTasks;
        public List<PlannerTask> PlannerTasks
        {
            get { return _plannerTasks; }
            set
            {
                _plannerTasks = value;
                OnPropertyChanged("PlannerTasks");
            }
        }

        private string _description = "";
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
                SubmitCommand.RaiseCanExecuteChanged();
            }
        }

        private DateTime? _selectedDueDate;
        public DateTime? SelectedDueDate
        {
            get { return _selectedDueDate; }
            set
            {
                _selectedDueDate = value;
                OnPropertyChanged("SelectedDueDate");
                SubmitCommand.RaiseCanExecuteChanged();
            }
        }

        private CustomPercentComplete _selectedPercentComplete;
        public CustomPercentComplete SelectedPercentComplete
        {
            get { return _selectedPercentComplete; }
            set
            {
                _selectedPercentComplete = value;
                OnPropertyChanged("SelectedPercentComplete");
                SubmitCommand.RaiseCanExecuteChanged();
            }
        }

        private CustomCategory _selectedCategory;
        public CustomCategory SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
                SubmitCommand.RaiseCanExecuteChanged();
            }
        }

        private string _taskTitle;
        public string TaskTitle
        {
            get { return _taskTitle; }
            set
            {
                _taskTitle = value;
                SubmitCommand.RaiseCanExecuteChanged();
            }
        }

        private Group _selectedGroup;
        public Group SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                _selectedGroup = value;

                if (value != null)
                    UpdatePlans(value);
                else
                    PlannerPlans = null;
                SubmitCommand.RaiseCanExecuteChanged();
            }
        }

        private PlannerPlan _selectedPlan;
        public PlannerPlan SelectedPlan
        {
            get { return _selectedPlan; }
            set
            {
                _selectedPlan = value;

                if (value != null)
                {
                    UpdateBuckets(value);
                    UpdateCategories(value);
                }
                else
                    PlannerBuckets = null;

                SubmitCommand.RaiseCanExecuteChanged();
            }
        }

        private PlannerBucket _selectedBucket;
        public PlannerBucket SelectedBucket
        {
            get { return _selectedBucket; }
            set
            {
                _selectedBucket = value;

                if (value != null)
                    UpdateTasks(value);
                else
                    PlannerTasks = null;

                SubmitCommand.RaiseCanExecuteChanged();
            }
        }

        private PlannerTask _selectedTask;
        public PlannerTask SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                if (value != null)
                {
                    UpdateDueDate(value);
                    UpdateCustomPercentComplete(value);
                    UpdateTaskDetail(value);//update the selected category
                    UpdateDescription(value);
                    if (value.Title == _neuAufgabe)
                    ActionsVisible = Visibility.Visible;
                    else
                        ActionsVisible = Visibility.Hidden;
                    SubmitCommand.RaiseCanExecuteChanged();
                }
                else
                {
                    SelectedCategory = null;
                    Description = "";
                }

            }
        }

        private Visibility _actionsVisible;
        public Visibility ActionsVisible
        {
            get
            {
                return _actionsVisible;
            }
            set
            {
                _actionsVisible = value;
                OnPropertyChanged("ActionsVisible");
            }
        }
        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Updatefuction
        private void UpdatePlans(Group group)
        {
            SelectedPlan = null;
            PlannerPlans = _graphClient.Groups[group.Id].Planner.Plans.Request().GetAsync().Result.ToList();
        }

        private void UpdateBuckets(PlannerPlan plan)
        {
            SelectedBucket = null;
            PlannerBuckets = _graphClient.Planner.Plans[plan.Id].Buckets.Request().GetAsync().Result.ToList();
            var categoryDescriptions = _graphClient.Planner.Plans[plan.Id].Details.Request().GetAsync().Result.CategoryDescriptions;

        }

        private void UpdateCategories(PlannerPlan plan)
        {
            SelectedCategory = null;
            CustomCategoryGroups.Clear();
            var categoryDescriptions = _graphClient.Planner.Plans[plan.Id].Details.Request().GetAsync().Result.CategoryDescriptions;
            System.Reflection.PropertyInfo[] propertyInfos = categoryDescriptions.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.PropertyType == typeof(string))
                {
                    string value = (string)propertyInfo.GetValue(categoryDescriptions);
                    CustomCategoryGroups.Add(new CustomCategory(propertyInfo.Name, value, true));
                }

            }
        }

        private void UpdateTasks(PlannerBucket bucket)
        {
            SelectedTask = null;
            var getPlannerTasks = _graphClient.Planner.Buckets[bucket.Id].Tasks.Request().GetAsync().Result.ToList();
            getPlannerTasks.Insert(0, new PlannerTask() { Title = _neuAufgabe });
            PlannerTasks = getPlannerTasks;
            //zweimal aktualisieren gehts nicht. deswegen hier wird ein Zwischenvariable eingeführt.
            //OnPropertyChanged("PlannerTasks");
        }

        private void UpdateDueDate(PlannerTask task)
        {
            if (task.DueDateTime == null)
                SelectedDueDate = null;
            else
                SelectedDueDate = task.DueDateTime.Value.DateTime;
        }

        private void UpdateDescription(PlannerTask task)
        {
            if (task.Title.Equals(_neuAufgabe))
                Description = "";
            else
                Description = _graphClient.Planner.Tasks[_selectedTask.Id].Details.Request().GetAsync().Result.Description;
        }

        private void UpdateCustomPercentComplete(PlannerTask task)
        {
            if (task.Title == _neuAufgabe)
            {
                SelectedPercentComplete = CustomPercentCompleteGroups[0];
                return;
            }

            foreach (CustomPercentComplete i in CustomPercentCompleteGroups)
            {
                if (i.Value == task.PercentComplete)
                {
                    SelectedPercentComplete = i;
                    return;
                }

            }
        }

        private void UpdateTaskDetail(PlannerTask task)
        {

            SelectedCategory = null;
            if (task.Title.Equals(_neuAufgabe))
                return;
            PlannerAppliedCategories appliedCategories = task.AppliedCategories;
            System.Reflection.PropertyInfo[] propertyInfos = appliedCategories.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.PropertyType == typeof(bool?))
                {
                    if (propertyInfo.GetValue(appliedCategories) == null)
                        continue;
                    else if ((bool)propertyInfo.GetValue(appliedCategories) == true)
                    {
                        foreach (CustomCategory i in CustomCategoryGroups)
                        {
                            if (i.Name == propertyInfo.Name)
                            {
                                SelectedCategory = i;
                                return;
                            }
                        }
                    }
                }
            }
            return;
        }
        #endregion 

        #region Customclass
        public class CustomPercentComplete
        {
            public string Name { get; set; }
            public int Value { get; set; }
            public CustomPercentComplete(string name, int value)
            {
                this.Name = name;
                this.Value = value;
            }
        }

        public class CustomCategory
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Value { get; set; }
            public CustomCategory(string name, string description, bool value)
            {
                this.Name = name;
                this.Value = value;
                this.Description = description;
            }
        }

        enum Category
        {
            Category1,
            Category2,
            Category3,
            Category4,
            Category5,
            Category6,
        }
        #endregion

        #region UploadFiles to Cloud
        private string[] selectedFiles = new string[0];
        Form formForMessage = new Form();
        private StringBuilder errorMessages = new StringBuilder();


        public async Task HochladenAsync(ObservableCollection<ModelViewAttachment> attachments, string bucket, string category,string groupId)//string mailFilePath
        {
            //OpenFileDialog fileDialog = new OpenFileDialog();
            //fileDialog.Filter = "*.epub | *.*";
            //fileDialog.InitialDirectory = mailFilePath;
            //fileDialog.Multiselect = true;
            //if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    selectedFiles = fileDialog.FileNames;
            //}
            foreach (var attachment in attachments)
            {
                Array.Resize(ref selectedFiles, selectedFiles.Length + 1);
                selectedFiles[selectedFiles.Length - 1] = attachment.FilePath;
                string uploadPath = $"/General/" + bucket + $"/" + category + $"/{DateTime.Now.ToString("ddMMyyyy")}/" + Uri.EscapeUriString(attachment.DisplayName);
                UploadPath.Add(uploadPath);
            }
            //if (selectedFiles != null && selectedFiles.Count() > 0)
            //{
            //    List<CustomeName> lstItems = new List<CustomeName>();
            //    var fileInfo = new FileInfo(selectedFiles[0]);
            //    dirName = $"Directory : {fileInfo.DirectoryName}";
            //    foreach (var file in selectedFiles)
            //    {
            //        lstItems.Add(new CustomeName() { Name = (new FileInfo(file)).Name });
            //    }
            //}


            try
            {
                //spinner.Visibility = Visibility.Visible;
                //spinner.Spin = true;
                //btnUpload.IsEnabled = false;
                //filesbtn.IsEnabled = false;
                if (_graphClient == null)
                {
                    _graphClient = GraphClientHelper.GetAuthenticatedClient();
                }
                var count = 100 / selectedFiles.Count();
                if (System.IO.File.Exists("log.txt"))
                {
                    System.IO.File.Delete("log.txt");
                }
                foreach (var file in selectedFiles)
                {
                    var fileName = Path.GetFileName(file);
                    try
                    {
                        if (file != null && file.Contains("."))
                        {
                            await UploadFilesToOneDrive(fileName, file, bucket, category, _graphClient,groupId);
                            //progressBar.Value += count;
                        }
                    }
                    catch (Exception ex)
                    {
                        errorMessages.AppendLine($"File: {fileName} upload failed:");
                        errorMessages.AppendLine($"Message :{ ex.Message }");
                        errorMessages.AppendLine($"{ ex.StackTrace }");
                        System.IO.File.AppendAllText("log.txt", errorMessages.ToString());
                        System.Windows.MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                        continue;
                    }
                }
                if (!System.IO.File.Exists("log.txt"))
                {
                    System.Windows.MessageBox.Show("Successfully uploaded");
                }
            }
            catch (Exception ex)
            {
                errorMessages.AppendLine($"Message :{ ex.Message }");
                errorMessages.AppendLine($"{ ex.StackTrace }");
                System.IO.File.AppendAllText("log.txt", errorMessages.ToString());
                System.Windows.MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                //dirName.Content = "Directory: ";
                //lstView1.ItemsSource = null;
                selectedFiles = new string[0];
                //btnUpload.IsEnabled = true;
                //filesbtn.IsEnabled = true;
                //spinner.Spin = false;
                //spinner.Visibility = Visibility.Hidden;
                //progressBar.Value = 0;
                if (System.IO.File.Exists("log.txt"))
                {
                    var result = Process.Start("log.txt");
                    Thread.Sleep(5000);
                    if (result.HasExited)
                    {
                        System.IO.File.Delete("log.txt");
                    }
                }
            }
        }

        private static List<string> UploadPath { set; get; }
        /// <summary>
        /// UploadFiles to Onedrive Less than 4MB only
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="filePath"></param>
        /// <param name="graphClient"></param>
        /// <returns></returns>
        private static async Task UploadFilesToOneDrive(string fileName, string filePath, string bucket, string category, GraphServiceClient graphClient,string groupId)
        {
            try
            {
                string uploadPath = $"/General/" + bucket + $"/" + category + $"/{DateTime.Now.ToString("ddMMyyyy")}/" + Uri.EscapeUriString(fileName);
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    if (stream != null)
                    {
                        var fileSize = ByteSize.FromBytes(stream.Length);
                        if (fileSize.MegaBytes > 4)
                        {
                            //var session = await graphClient.Drive.Root.ItemWithPath(uploadPath).CreateUploadSession().Request().PostAsync();
                            var session = await graphClient.Groups[groupId].Drive.Root.ItemWithPath(uploadPath).CreateUploadSession().Request().PostAsync();
                            var maxSizeChunk = 320 * 4 * 1024;
                            var provider = new ChunkedUploadProvider(session, graphClient, stream, maxSizeChunk);
                            var chunckRequests = provider.GetUploadChunkRequests();
                            var exceptions = new List<Exception>();
                            var readBuffer = new byte[maxSizeChunk];
                            DriveItem itemResult = null;
                            //upload the chunks
                            foreach (var request in chunckRequests)
                            {
                                // Do your updates here: update progress bar, etc.
                                // ...
                                // Send chunk request
                                var result = await provider.GetChunkRequestResponseAsync(request, readBuffer, exceptions);

                                if (result.UploadSucceeded)
                                {
                                    itemResult = result.ItemResponse;
                                }
                            }

                            // Check that upload succeeded
                            if (itemResult == null)
                            {
                                await UploadFilesToOneDrive(fileName, filePath, bucket, category, graphClient,groupId);
                            }
                        }
                        else
                        {
                            //await graphClient.Drive.Root.ItemWithPath(uploadPath).Content.Request().PutAsync<DriveItem>(stream);
                            await graphClient.Groups[groupId].Drive.Root.ItemWithPath(uploadPath).Content.Request().PutAsync<DriveItem>(stream);
                            //graphClient.Groups[].Drive.Items
                        }
                    }
                }
            }
            catch
            {
                throw;
            }

        }
        #endregion
    }
}