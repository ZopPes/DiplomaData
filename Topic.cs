using WPFMVVMHelper;

namespace DiplomaData
{
    public class Topic : peremlog
    {

        #region Title
        private string title;
        /// <summary>Заголовок</summary>
        public string Title { get => title; set => Set(ref title ,value); }
        #endregion


        #region Description
        private string description;
        /// <summary>описание</summary>
        public string Description { get => description; set => Set(ref description ,value); }
        #endregion

        #region Approved
        private bool approved;
        /// <summary>описание</summary>
        public bool Approved { get => approved; set => Set(ref approved ,value); }
        #endregion


        #region Used
        private bool used;
        /// <summary>испльзуется</summary>
        public bool Used { get => used; set =>Set(ref used ,value); }
        #endregion

        public Topic(string title, string description)
        {
            Title = title;
            Description = description;
        }
        public Topic()
        {
            Title = string.Empty;
            Description = string.Empty;
        }



    }

    

   
}
