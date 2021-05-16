using System.Windows;
using System.Windows.Controls;

namespace DiplomaData.UserControls
{
    class FileLink : TextBlock
    {
        public FileLink() : base()
        {
            AllowDrop = true;
            Drop += txtTarget_Drop;
        }

        private void txtTarget_Drop(object sender, DragEventArgs e)
        {
            ((TextBlock)sender).Text = (string)e.Data.GetData(DataFormats.Text);
        }

    }
}
