using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WPFMVVMHelper;

namespace DiplomaData
{

    public class HelpObservableCollection<T> : ObservableCollection<T>
    {
        public lamdaCommand<T> LCAdd { get; }

        public lamdaCommand<T> LCRemove { get; }

        public HelpObservableCollection():base()
        {
            LCAdd = new lamdaCommand<T>(base.Add);
            LCRemove = new lamdaCommand<T>(r => base.Remove(r));
            
        }
    }


    public class Test : peremlog
    {
        public static RoutedUICommand routed { get; } = new RoutedUICommand("текст", "сасать",typeof(string));
         


        #region Topics
        private HelpObservableCollection<Topic> topics;
        /// <summary>темы</summary>
        public HelpObservableCollection<Topic> Topics { get => topics; set => Set(ref topics,value); }
        #endregion

        public lamdaCommand<Topic> AddTopic { get; }

        public lamdaCommand<Topic> DeleteTopic { get; }


        #region Diplomas
        private ObservableCollection<Diplomas> diplomas;
        /// <summary>Список дипломов</summary>
        public ObservableCollection<Diplomas> Diplomas { get => diplomas; set => diplomas = value; }
        #endregion

        public Test()
        {
            Topics = new HelpObservableCollection<Topic>();
            Diplomas = new ObservableCollection<Diplomas>();
            Random r = new Random();
            Topics.Add(new Topic("полёт на луну", "нужно полететь на луну"));
            Topics.Add(new Topic("танцы насмерь", "гравное не умереть"));
            Topics.Add(new Topic("телепорт", "разработать телепорт"));
            Topics.Add(new Topic("взлом реальность", "взломать матрицу"));
            Topics.Add(new Topic("доказательство математики", "токазать что матиматика существует"));
            Topics.Add(new Topic("задание", "опиание задачи"));
            Topics.Add(new Topic("машина времени", "разработать и проверить машину времени"));
            Topics.Add(new Topic("трактор на радио упровлении", "трактор с упровлением с умных часов"));
            Topics.Add(new Topic("дизайн среднивекового приложения", "создать приложение которым могли бы пользоваться луди из средневикоаья"));
            Topics.Add(new Topic("уничтожить мир", "необходимо уничтожеть весь мир, при этом оставшись в живих"));
            Topics.Add(new Topic("танец для инволидов", "техника танца которую смогут иполнить извалиды колясочники"));
             



            AddTopic = new lamdaCommand<Topic>(Topics.Add);
            DeleteTopic = new lamdaCommand<Topic>(del);
            //var rr = ApplicationCommands.Undo.;            


            
        }

        private void del(Topic obj)
        {
                Topics.Remove(obj);
        }


        private string RandomName(Random random) //Случайный текст, size - длина, lowerCase - большие или маленькие буквы (true-большие,false-маленькие)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < random.Next(50,250); i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }


            return builder.ToString();
        }
    }

    public class Diplomas
    {
        private string name;
        private Topic topic;
        private DateTime date;

        public Diplomas(string Name, Topic Topic, DateTime Date)
        {
            this.Name = Name;
            this.Topic = Topic;
            this.Date = Date;
        }

        public string Name { get => name; set => name = value; }
        public Topic Topic { get => topic; set => topic = value; }
        public DateTime Date { get => date; set => date = value; }

        public int countST { get ; set ; }


    }

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
