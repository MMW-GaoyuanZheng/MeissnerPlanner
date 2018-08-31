using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;
using ByteSizeLib;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using Microsoft.Graph;

namespace Meissner.MicrosoftPlanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class DatenInOneCloudLaden
    {
        GraphServiceClient _graphClient = null;
        private string[] selectedFiles = new string[0];
        Form formForMessage = new Form();
        private StringBuilder errorMessages = new StringBuilder();
        public DatenInOneCloudLaden(GraphServiceClient graphClient)
        {
            this._graphClient = graphClient;
        }
        string dirName;
        public async Task HochladenAsync(string mailFilePath)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "*.epub | *.*";
            fileDialog.InitialDirectory = mailFilePath;
            fileDialog.Multiselect = true;
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                selectedFiles = fileDialog.FileNames;
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
                            await UploadFilesToOneDrive(fileName, file, _graphClient);
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


        //private void filesbtn_Click(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog fileDialog = new OpenFileDialog();
        //    fileDialog.Filter = "*.epub | *.*";
        //    fileDialog.InitialDirectory = "C:\\Users\\Tom\\Desktop";
        //    fileDialog.Multiselect = true;
        //    if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        selectedFiles = fileDialog.FileNames;
        //    }

        //    if (selectedFiles != null && selectedFiles.Count() > 0)
        //    {
        //        List<CustomeName1> lstItems = new List<CustomeName1>();
        //        var fileInfo = new FileInfo(selectedFiles[0]);
        //        dirName = $"Directory : {fileInfo.DirectoryName}";
        //        foreach (var file in selectedFiles)
        //        {
        //            lstItems.Add(new CustomeName1() { Name = (new FileInfo(file)).Name });
        //        }

        //        //lstView1.ItemsSource = lstItems;
        //    }

        //}

        //private async void btnUpload_Click(object sender, RoutedEventArgs e)
        //{
        //    if (selectedFiles == null || selectedFiles.Count() == 0)
        //    {
        //        System.Windows.MessageBox.Show("Please select atleast one file to upload!", "Stop!", MessageBoxButton.OK, MessageBoxImage.Warning);
        //        return;
        //    }

        //    try
        //    {
        //        //spinner.Visibility = Visibility.Visible;
        //        //spinner.Spin = true;
        //        //btnUpload.IsEnabled = false;
        //        //filesbtn.IsEnabled = false;
        //        if (_graphClient == null)
        //        {
        //            _graphClient = GraphClientHelper.GetAuthenticatedClient();
        //        }
        //        var count = 100 / selectedFiles.Count();
        //        if (System.IO.File.Exists("log.txt"))
        //        {
        //            System.IO.File.Delete("log.txt");
        //        }
        //        foreach (var file in selectedFiles)
        //        {
        //            var fileName = Path.GetFileName(file);
        //            try
        //            {
        //                if (file != null && file.Contains("."))
        //                {

        //                    await UploadFilesToOneDrive(fileName, file, _graphClient);
        //                    //progressBar.Value += count;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                errorMessages.AppendLine($"File: {fileName} upload failed:");
        //                errorMessages.AppendLine($"Message :{ ex.Message }");
        //                errorMessages.AppendLine($"{ ex.StackTrace }");
        //                System.IO.File.AppendAllText("log.txt", errorMessages.ToString());
        //                System.Windows.MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
        //                continue;
        //            }
        //        }
        //        if (!System.IO.File.Exists("log.txt"))
        //        {
        //            System.Windows.MessageBox.Show("Successfully uploaded");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //spinner.Spin = false;
        //        //spinner.Visibility = Visibility.Hidden;
        //        errorMessages.AppendLine($"Message :{ ex.Message }");
        //        errorMessages.AppendLine($"{ ex.StackTrace }");
        //        System.IO.File.AppendAllText("log.txt", errorMessages.ToString());
        //        System.Windows.MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //    finally
        //    {
        //        //dirName.Content = "Directory: ";
        //        //lstView1.ItemsSource = null;
        //        selectedFiles = new string[0];
        //        //btnUpload.IsEnabled = true;
        //        //filesbtn.IsEnabled = true;
        //        //spinner.Spin = false;
        //        //spinner.Visibility = Visibility.Hidden;
        //        //progressBar.Value = 0;
        //        if (System.IO.File.Exists("log.txt"))
        //        {
        //            var result = Process.Start("log.txt");
        //            Thread.Sleep(5000);
        //            if (result.HasExited)
        //            {
        //                System.IO.File.Delete("log.txt");
        //            }
        //        }
        //    }
        //}


        /// <summary>
        /// UploadFiles to Onedrive Less than 4MB only
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="filePath"></param>
        /// <param name="graphClient"></param>
        /// <returns></returns>
        private static async Task UploadFilesToOneDrive(string fileName, string filePath, GraphServiceClient graphClient)
        {
            try
            {
                var uploadPath = $"/CodeUploads/{DateTime.Now.ToString("ddMMyyyy")}/" + Uri.EscapeUriString(fileName);


                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    if (stream != null)
                    {
                        var fileSize = ByteSize.FromBytes(stream.Length);
                        if (fileSize.MegaBytes > 4)
                        {
                            var session = await graphClient.Drive.Root.ItemWithPath(uploadPath).CreateUploadSession().Request().PostAsync();
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
                                await UploadFilesToOneDrive(fileName, filePath, graphClient);
                            }
                        }
                        else
                        {
                            //await graphClient.Drive.Root.ItemWithPath(uploadPath).Content.Request().PutAsync<DriveItem>(stream);
                            //await graphClient.Groups["36c450de-af05-4dd1-bed8-9c7b6d7ca3e3"].Drive.Root.ItemWithPath(uploadPath).Content.Request().PutAsync<DriveItem>(stream);
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


    }

    public class CustomeName
    {
        public string Name { get; set; }
    }
}
