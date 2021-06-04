using System.Windows.Controls;
using System.Windows.Input;

namespace DiplomaData.UserControls
{
    public class OnliLetterTextBox : TextBox
    {
        public OnliLetterTextBox() : base() => 
            PreviewTextInput += TextBox_PreviewTextInput;

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (var i in e.Text)
                e.Handled |= !char.IsLetter(i);
        }
    }
}