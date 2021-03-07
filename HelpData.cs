using WPFMVVMHelper;

namespace DiplomaData
{
    public class HelpData : peremlog
    {
        #region HelpText

        private string helpText;

        /// <summary>впомагательный текст</summary>
        public string HelpText { get => helpText; set => Set(ref helpText, value); }

        #endregion HelpText
    }
}