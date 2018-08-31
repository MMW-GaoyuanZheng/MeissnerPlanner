using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Outlook;
using Microsoft.Graph;

namespace Meissner.MicrosoftPlanner
{
    class Mail
    {
        public MailItem Item { get; private set; }
        public bool ItemSelected { get; private set; }
        public IEnumerable<Microsoft.Office.Interop.Outlook.Attachment> Attachments { get; private set; }
        public string MailSubject { get; private set; }
        public Mail()
        {
            _application = GetApplicationObject();
            _olkExplor = _application.ActiveExplorer();
            _olkSelect = _olkExplor.Selection;
            if (_olkSelect.Count == 0)
            {
                ItemSelected = false;
                MailSubject = ""; 
            }
            else
            {
                ItemSelected = true;
                MailSubject = getSubjectFromFirstMailItem();
                Item = (MailItem)_olkSelect[1];
            }
           Attachments = Item.Attachments.Cast<Microsoft.Office.Interop.Outlook.Attachment>();
        }
        private Microsoft.Office.Interop.Outlook.Application _application;
        private Microsoft.Office.Interop.Outlook.Explorer _olkExplor;
        private Microsoft.Office.Interop.Outlook.Selection _olkSelect;
        private Microsoft.Office.Interop.Outlook.Application GetApplicationObject()
        {
            Microsoft.Office.Interop.Outlook.Application application = null;
            // Check whether there is an Outlook process running.
            if (Process.GetProcessesByName("OUTLOOK").Count() > 0)
            {
                // If so, use the GetActiveObject method to obtain the process and cast it to an Application object.
                application = Marshal.GetActiveObject("Outlook.Application") as Microsoft.Office.Interop.Outlook.Application;
            }
            else
            {
                // If not, create a new instance of Outlook and log on to the default profile.
                application = new Microsoft.Office.Interop.Outlook.Application();
                Microsoft.Office.Interop.Outlook.NameSpace nameSpace = application.GetNamespace("MAPI");
                nameSpace.Logon("", "", Missing.Value, Missing.Value);
                nameSpace = null;
            }
            // Return the Outlook Application object.
            return application;
        }
        private string getSubjectFromFirstMailItem()
        {
            MailItem myMailItem = (MailItem)_olkSelect[1];
            return myMailItem.Subject;
        }
    }
    public partial class MainProgram : Form
    {
        GraphServiceClient _graphClient = null;
        public MainProgram()
        {
            InitializeComponent();
            _graphClient = GraphClientHelper.GetAuthenticatedClient();
            _graphClient.Groups.Request().GetAsync();
        }  
        private void button1_Click(object sender, EventArgs e)
        {
            if (_graphClient == null) return;
            bool IsOutlookClosed= PopWarning(shouldPopWarning:!processRunning("OUTLOOK"), message:"Bitte OUTLOOK öffnen");
            if (IsOutlookClosed) return;
            Mail mail = new Mail();
            bool IsMailNotSelected=PopWarning(shouldPopWarning:!mail.ItemSelected, message:"Bitte eine Email anwählen");
            if (IsMailNotSelected)return;
            List<ModelViewAttachment> modelViewAttachments = new List<ModelViewAttachment>();
            InitializeModelViewAttachments(mail, modelViewAttachments, CreateTempDictionary());
            OpenViewModel( _graphClient, "", mail.MailSubject, modelViewAttachments);
            //Directory.Delete(localTempDirectory, true); 
            #region Local Methode
                bool PopWarning(bool shouldPopWarning, string message)
                    {
                        bool popWarning;
                        if (shouldPopWarning)
                        {
                            const string caption = "Warnung";
                            var result = System.Windows.Forms.MessageBox.Show(message, caption,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            popWarning = true;
                            return popWarning;
                        }
                        else
                        {
                            popWarning = false;
                            return popWarning;
                        }
                        
                    }
                bool processRunning(string processName)
                    {
                        foreach (Process proc in Process.GetProcesses())
                        {
                            if (proc.ProcessName.Contains(processName))
                                return true;
                        }
                        return false;
                    }
                string CreateTempDictionary()
                    {
                        string localTempDirectory = string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        @"\TempMicrosoftPlannerAttachments\",
                        Guid.NewGuid(),
                        "\\");
                        System.IO.Directory.CreateDirectory(localTempDirectory);
                        return localTempDirectory;
                    }
                void InitializeModelViewAttachments(Mail localMail, List<ModelViewAttachment> localModelViewAttachments, string localTempDirectory)
                {
                    foreach (Microsoft.Office.Interop.Outlook.Attachment attachment in localMail.Attachments)
                    {
                        if (attachment.Size < 10000 && attachment.FileName.Contains("image00"))
                            continue;
                        ModelViewAttachment modelViewAttachment = new ModelViewAttachment();
                        localModelViewAttachments.Add(modelViewAttachment);
                        modelViewAttachment.DisplayName = attachment.DisplayName;
                        modelViewAttachment.FilePath = string.Concat(localTempDirectory, attachment.FileName);
                        attachment.SaveAsFile(modelViewAttachment.FilePath);
                    }
                    ModelViewAttachment modelViewAttachment1 = new ModelViewAttachment();
                    modelViewAttachment1.DisplayName = "Mail";
                    modelViewAttachment1.FilePath = string.Concat(localTempDirectory, "Mail.msg");
                    localMail.Item.SaveAs(modelViewAttachment1.FilePath);
                    localModelViewAttachments.Add(modelViewAttachment1);
                }
            #endregion
        } 
        private void button_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
                e.Effect = System.Windows.Forms.DragDropEffects.All;
            else
                e.Effect = System.Windows.Forms.DragDropEffects.None;
        }
        private void button_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (_graphClient == null) return;
            string[] filesAndDictionaries = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop, false);
            List<string> files = getListWithOnlyFiles(filesAndDictionaries);
            List<ModelViewAttachment> modelViewAttachments = new List<ModelViewAttachment>();
            InitializeModelViewAttachments(modelViewAttachments, files);
            OpenViewModel(_graphClient, "", "", modelViewAttachments);   
            #region Local Methode
            List<string> getListWithOnlyFiles(string[] localFilesAndDictionaries)
            {

                List<string> localFiles=new List<string>();
                for (int i = 0; i < localFilesAndDictionaries.Length; i++)
                {
                    // get the file attributes for file or directory
                    FileAttributes attr = System.IO.File.GetAttributes(localFilesAndDictionaries[i]);

                    if (attr.HasFlag(FileAttributes.Directory))
                        continue;//wenn it is a folder, then spring over
                    else
                        localFiles.Add(localFilesAndDictionaries[i]);
                }
                return localFiles;
            }
            void InitializeModelViewAttachments(List<ModelViewAttachment> localModelViewAttachments, List<string> localFiles)
            {
                foreach (string file in localFiles)
                {
                    ModelViewAttachment modelViewAttachment = new ModelViewAttachment();
                    modelViewAttachment.DisplayName = Path.GetFileName(file);
                    modelViewAttachment.FilePath = file;
                    localModelViewAttachments.Add(modelViewAttachment);
                }
            }
            #endregion
        }
        private void OpenViewModel(GraphServiceClient graphClient,string taskTitle,string mailSubject,List<ModelViewAttachment> attachments)
        {
            IssueCreationViewModel viewModel = new IssueCreationViewModel(graphClient, taskTitle, mailSubject, attachments);
            IssueCreationView view = new IssueCreationView();
            view.ViewModel = viewModel;
            view.ShowDialog();
        }

        private void CollectionOfCommands()
        {
            //        MailItem myMailItem = (MailItem)olkSelect[1];
            //        //Inspectors inspectors = application.Inspectors;
            //        //MailItem myMailItem = (MailItem)inspectors;

            //        string mailSubject = myMailItem.Subject;
            //        //string mailBody = myMailItem.Body;
            //        IEnumerable<Microsoft.Office.Interop.Outlook.Attachment> attachments = myMailItem.Attachments.Cast<Microsoft.Office.Interop.Outlook.Attachment>();

            //        string localTempDirectory = string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            //                @"\TempYouTrackAttachments\",
            //                Guid.NewGuid(),
            //                "\\");
            //        Directory.CreateDirectory(localTempDirectory);

            //        List<MailAttachment> mailAttachments = new List<MailAttachment>();
            //        foreach (Microsoft.Office.Interop.Outlook.Attachment attachment in attachments)
            //        {
            //            if (attachment.Size < 10000 && attachment.FileName.Contains("image00"))
            //                continue;
            //            MailAttachment mailAttachment = new MailAttachment();
            //            mailAttachments.Add(mailAttachment);
            //            mailAttachment.DisplayName = attachment.DisplayName;
            //            mailAttachment.FilePath = string.Concat(localTempDirectory, attachment.FileName);
            //            attachment.SaveAsFile(mailAttachment.FilePath);
            //        }
            //        if (myMailItem.Body.Length > 2) //wenn in der Email-Body gar nichts steht, wird die Email nicht beigefügt
            //        {
            //            MailAttachment mail = new MailAttachment();
            //            mailAttachments.Add(mail);
            //            mail.DisplayName = "Mail";
            //            mail.FilePath = string.Concat(localTempDirectory, "Mail.msg");
            //            myMailItem.SaveAs(mail.FilePath);
            //        }

            //        _mailSubject = mailSubject;
            //        _mailAttachments = mailAttachments;

            //        //Directory.Delete(localTempDirectory, true);            
            //        IssueCreationViewModel viewModel = new IssueCreationViewModel(_graphClient, "", _mailSubject, _mailAttachments);
            //        IssueCreationView view = new IssueCreationView();
            //        view.ViewModel = viewModel;
            //        view.ShowDialog();
            //Inspectors inspectors = application.Inspectors;
            //MailItem myMailItem = (MailItem)inspectors;
            //string mailBody = myMailItem.Body;
        }
    }
}

