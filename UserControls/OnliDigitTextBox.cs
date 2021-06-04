using System.Windows.Controls;
using System.Windows.Input;

namespace DiplomaData.UserControls
{
    internal class OnliDigitTextBox : TextBox
    {
        public OnliDigitTextBox() : base() => 
            PreviewTextInput += TextBox_PreviewTextInput;

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) => 
            e.Handled = !char.IsDigit(e.Text[0]);
    }
}