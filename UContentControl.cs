using Microsoft.Office.Interop.Word;
using WPFMVVMHelper;

namespace DiplomaData.Отчёты
{
    public class UContentControl : peremlog
    {
        public ContentControl Content { get; set; }


        public string Text { get => Content.Range.Text;
            set
            {
                Content.Range.Text = value;
                OnPropertyChanged(nameof(Text));
            } }

        public UContentControl(ContentControl content)
        {
            Content = content;
        }
    }
}