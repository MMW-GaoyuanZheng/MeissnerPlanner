//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Linq;
//using System.Windows;

//using Microsoft.Practices.Prism.Commands;

//using Youtrack.OutlookDesktop.Issues;
//using Youtrack.OutlookDesktop.Projects;

//namespace Youtrack.OutlookDesktop
//{
//    public interface IIssueCreationViewModel { }

//    public class IssueCreationViewModel : IIssueCreationViewModel, INotifyPropertyChanged
//    {
//        private readonly IssueManagement _issueManagement;
//        private readonly ProjectManagement _projectManagement;
//        private IEnumerable<Assignee> _assignees;
//        private string _description;
//        private ProjectIssueTypes _selectedIssueType;
//        private ProjectPriority _selectedPriority;
//        private ProjectState _selectedState;
//        private Project _selectedProject;
//        private string _summary;
//        private CustomType _selectedDepartment;
//        private DateTime? _selectedDueDate;
//        private Assignee _selectedAssignee;
//        private DateTime _ConvertObjectToDateformat;

//        public IssueCreationViewModel(ProjectManagement projectManagement, IssueManagement issueManagement, string summary, string description,
//                IEnumerable<MailAttachment> attachments)
//        {
//            if (projectManagement == null)
//                throw new ArgumentNullException("projectManagement");
//            if (issueManagement == null)
//                throw new ArgumentNullException("issueManagement");

//            _projectManagement = projectManagement;
//            _issueManagement = issueManagement;
//            _summary = summary;
//            _description = description;
//            Attachments = new ObservableCollection<MailAttachment>(attachments);

//            Initialize();
//        }

//        public IEnumerable<Assignee> Assignees
//        {
//            get { return _assignees; }
//            set
//            {
//                _assignees = value;
//                OnPropertyChanged("Assignees");
//            }
//        }

//        public ObservableCollection<MailAttachment> Attachments { get; private set; }

//        public string Description
//        {
//            get { return _description; }
//            set
//            {
//                _description = value;
//                SubmitCommand.RaiseCanExecuteChanged();
//            }
//        }

//        public IEnumerable<ProjectIssueTypes> IssueTypes { get; set; }
//        public IEnumerable<ProjectPriority> Priorities { get; set; }
//        public IEnumerable<ProjectState> States { get; set; }
//        public IEnumerable<Project> Projects { get; set; }
//        public DelegateCommand<MailAttachment> RemoveAttachmentCommand { get; set; }

//        //public Assignee SelectedAssignee
//        //{
//        //    get { return _selectedAssignee; }
//        //    set
//        //    {
//        //        _selectedAssignee = value;
//        //        _state = SelectedAssignee == null
//        //                ? "Submitted"
//        //                : "Accepted";
//        //    }
//        //}

//        public Assignee SelectedAssignee
//        {
//            get { return _selectedAssignee; }
//            set
//            {
//                _selectedAssignee = value;
//                SubmitCommand.RaiseCanExecuteChanged();
//            }
//        }

//        public ProjectIssueTypes SelectedIssueType
//        {
//            get { return _selectedIssueType; }
//            set
//            {
//                _selectedIssueType = value;
//                SubmitCommand.RaiseCanExecuteChanged();
//            }
//        }

//        public ProjectPriority SelectedPriority
//        {
//            get { return _selectedPriority; }
//            set
//            {
//                _selectedPriority = value;
//                SubmitCommand.RaiseCanExecuteChanged();
//            }
//        }
//        public ProjectState SelectedState
//        {
//            get { return _selectedState; }
//            set
//            {
//                _selectedState = value;
//                SubmitCommand.RaiseCanExecuteChanged();
//            }
//        }
//        public Project SelectedProject
//        {
//            get { return _selectedProject; }
//            set
//            {
//                _selectedProject = value;
//                UpdateAssignees(value);
//                SubmitCommand.RaiseCanExecuteChanged();
//            }
//        }

//        public DelegateCommand SubmitCommand { get; set; }

//        public string Summary
//        {
//            get { return _summary; }
//            set
//            {
//                _summary = value;
//                SubmitCommand.RaiseCanExecuteChanged();
//            }
//        }

//        public IEnumerable<CustomType> Departments
//        {
//            get; private set;
//        }

//        public CustomType SelectedDepartment
//        {
//            get { return _selectedDepartment; }
//            set
//            {
//                _selectedDepartment = value;
//                SubmitCommand.RaiseCanExecuteChanged();
//            }
//        }

//        public DateTime? SelectedDueDate
//        {
//            get { return _selectedDueDate; }
//            set
//            {
//                _selectedDueDate = value;
//                SubmitCommand.RaiseCanExecuteChanged();
//            }
//        }

//        #region INotifyPropertyChanged Members

//        public event PropertyChangedEventHandler PropertyChanged;

//        #endregion

//        private bool CanCreateIssue()
//        {
//            return SelectedIssueType != null
//                   && SelectedProject != null
//                   && SelectedPriority != null
//                   && SelectedState != null
//                   && !string.IsNullOrEmpty(Summary)
//                   && !string.IsNullOrEmpty(Description)
//                    ;
//        }

//        private void CreateIssue()
//        {
//            dynamic issue = new Issue();
//            //if (SelectedAssignee != null)
//            //    issue.Assignee = SelectedAssignee.Login;
//            issue.Summary = Summary;
//            issue.Description = Description;
//            issue.ProjectShortName = SelectedProject.ShortName;
//            issue.Type = SelectedIssueType.Name;
//            if (SelectedAssignee != null)
//                issue.Assignee = SelectedAssignee.Login;
//            //issue.Department = SelectedDepartment.Value;
//            //issue.State = _state;

//            //issue.DueDate=new DateTime(2008, 1, 1, 18, 9, 1, 500).ToString("yyyy-MM-dd");
//            if (SelectedDueDate != null)
//            {
//                _ConvertObjectToDateformat = (DateTime)SelectedDueDate;
//                issue.DueDate = _ConvertObjectToDateformat.ToString("yyyy-MM-dd");
//            }

//            issue.Priority = SelectedPriority.Name;
//            issue.State = SelectedState.Name;


//            string issueId = _issueManagement.CreateIssue(issue);
//            foreach (MailAttachment attachment in Attachments)
//                _issueManagement.AttachFileToIssue(issueId, attachment.FilePath);

//            MessageBox.Show(string.Format("Issue {0} created.", issueId));
//        }

//        private void Initialize()
//        {
//            SubmitCommand = new DelegateCommand(CreateIssue, CanCreateIssue);
//            RemoveAttachmentCommand = new DelegateCommand<MailAttachment>(attachment => Attachments.Remove(attachment));

//            Projects = _projectManagement.GetProjects();
//            IssueTypes = _projectManagement.GetIssueTypes();
//            //Departments = _projectManagement.GetCustomTypes("Departments");
//            Priorities = _projectManagement.GetPriorities();
//            States = _projectManagement.GetStates();
//            SelectedIssueType = IssueTypes.First();
//            SelectedPriority = Priorities.First(p => p.Name == "Normal");
//            SelectedDueDate = null;
//            SelectedState = States.First(p => p.Name == "Submitted");


//        }

//        protected virtual void OnPropertyChanged(string propertyName)
//        {
//            PropertyChangedEventHandler handler = PropertyChanged;
//            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
//        }

//        private void UpdateAssignees(Project project)
//        {
//            SelectedAssignee = null;
//            Assignees = _projectManagement.GetProjectAssignees(project);
//        }
//    }
//}