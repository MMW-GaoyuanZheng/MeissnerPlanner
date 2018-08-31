using System;

namespace Meissner.MicrosoftPlanner
{
    public class ModelViewAttachment
    {
        public Guid FileId { get; set; }
        public string FileName { get; set; }
        public string DisplayName { get; set; }
        public string Path { get; set; }
        public string FilePath { get; set; }
    }
}