using Adamson_Graduation_Software.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Adamson_Graduation_Software.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _corPath = string.Empty;
        private DirectoryInfo _directoryPath;
        private ObservableCollection<String> _graduateNames = new ObservableCollection<string>();
        private ObservableCollection<String> _coursesNames = new ObservableCollection<string>();
        private ObservableCollection<String> _dateNames = new ObservableCollection<string>();
        private ImageSource _graduateImage;
        private FileInfo[] _images;
        private DirectoryInfo[] _courses;
        private DirectoryInfo[] _dates;
        private string[] _extensions = new[] { ".jpg", ".png" };
        private RelayCommand _findGraduateCommand;
        private int _selectedCourseIndex = 0;
        private int _selectedDateIndex = 0;
        private int _selectedNameIndex = 0;
        private bool _isCourseVisible = true;
        private bool _isDateVisible = true;
        private bool _isGraduateVisible = true;
        private bool _isIntroVisible = true;
        private string _graduateName;
        private string _courseName;
        private string _dateName;


        public string DateName
        {
            get
            {
                return _dateName;
            }
            set
            {
                _dateName = value;
                OnPropertyChanged();
            }
        }

        public string CourseName
        {
            get
            {
                return _courseName;
            }
            set
            {
                _courseName = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Graduate's name
        /// </summary>
        public string GraduateName
        {
            get
            {
                return _graduateName;
            }
            set
            {
                _graduateName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Graduate's name
        /// </summary>
        public ObservableCollection<String> GraduateNames
        {
            get
            {
                return _graduateNames;
            }
            set
            {
                _graduateNames = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<String> CourseNames
        {
            get
            {
                return _coursesNames;
            }
            set
            {
                _coursesNames = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<String> DateNames
        {
            get
            {
                return _dateNames;
            }
            set
            {
                _dateNames = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Graduate's Image
        /// </summary>
        public ImageSource GraduateImage
        {
            get
            {
                return _graduateImage;
            }
            set
            {
                _graduateImage = value;
                OnPropertyChanged();
            }
        }

        public FileInfo[] Images
        {
            get
            {
                return _images;
            }
            set
            {
                _images = value;
                OnPropertyChanged();
            }
        }

        public DirectoryInfo[] Courses
        {
            get
            {
                return _courses;
            }
            set
            {
                _courses = value;
                OnPropertyChanged();
            }
        }

        public DirectoryInfo[] Dates
        {
            get
            {
                return _dates;
            }
            set
            {
                _dates = value;
                OnPropertyChanged();
            }
        }

        public int SelectedCourseIndex
        {
            get
            {
                return _selectedCourseIndex;
            }
            set
            {
                _selectedCourseIndex = value;
                getDateNames();
                OnPropertyChanged();
            }
        }
        public int SelectedDateIndex
        {
            get
            {
                return _selectedDateIndex;
            }
            set
            {
                _selectedDateIndex = value;
                getGraduateNames();
                OnPropertyChanged();
            }
        }

        public int SelectedNameIndex
        {
            get
            {
                return _selectedNameIndex;
            }
            set
            {
                _selectedNameIndex = value;
                OnPropertyChanged();
            }
        }

        public bool IsCourseVisible
        {
            get
            {
                return _isCourseVisible;
            }
            set
            {
                _isCourseVisible = value;
                OnPropertyChanged();
            }
        }

        public bool IsDateVisible
        {
            get
            {
                return _isDateVisible;
            }
            set
            {
                _isDateVisible = value;
                OnPropertyChanged();
            }
        }

        public bool IsGraduateVisible
        {
            get
            {
                return _isGraduateVisible;
            }
            set
            {
                _isGraduateVisible = value;
                OnPropertyChanged();
            }
        }
        public bool IsIntroVisible
        {
            get
            {
                return _isIntroVisible;
            }
            set
            {
                _isIntroVisible = value;
                OnPropertyChanged();
            }
        }


        public ICommand FindGraduateCommand
        {
            get
            {
                if (_findGraduateCommand == null)
                {
                    _findGraduateCommand = new RelayCommand(param => this.search(),
                        param => true);
                }
                return _findGraduateCommand;
            }
        }

        public MainViewModel()
        {
            _corPath = "C:\\Users\\DELL\\Documents\\Graduates";

            initialize();
        }

        private void initialize()
        {
            _directoryPath = new DirectoryInfo(_corPath);

            getCoursesNames();
        }

        private void getCoursesNames()
        {
            try
            {
                Courses = _directoryPath.GetDirectories().ToArray();

                CourseNames.Clear();
                DateNames.Clear();
                GraduateNames.Clear();

                foreach (var course in Courses)
                {
                    if (course != null)
                        CourseNames.Add(course.Name);
                }

                _selectedDateIndex = -1;
                getDateNames();
            }
            catch
            {
            }
        }


        private void getDateNames()
        {
            try
            {
                var coursePath = new DirectoryInfo(_corPath + "\\" + Courses[SelectedCourseIndex]);
                Dates = coursePath.GetDirectories().ToArray();

                DateNames.Clear();
                GraduateNames.Clear();

                foreach (var date in Dates)
                {
                    if (date != null)
                        DateNames.Add(date.Name);
                }

                _selectedNameIndex = -1;
                getGraduateNames();
            }
            catch
            {

            }
        }


        private void getGraduateNames()
        {
            try
            {
                if (_selectedDateIndex != -1)
                {
                    var datePath = new DirectoryInfo(_corPath + "\\" + Courses[SelectedCourseIndex] + "\\" + Dates[SelectedDateIndex]);
                    Images = datePath.GetFiles().Where(f => _extensions.Contains(f.Extension.ToLower())).ToArray();

                    GraduateNames.Clear();

                    foreach (var image in Images)
                    {
                        if (image != null)
                            GraduateNames.Add(image.Name);
                    }
                }
            }
            catch
            {

            }
        }

        private void search()
        {
            IsCourseVisible = false;
            IsDateVisible = false;
            IsGraduateVisible = false;
            IsIntroVisible = false;

            if (_selectedCourseIndex == -1 && SelectedDateIndex == -1 && SelectedNameIndex == -1)
            {
                setCourseDetails();
                IsIntroVisible = true;

            }
            else if (SelectedDateIndex == -1 && SelectedNameIndex == -1)
            {
                setCourseDetails();
                IsCourseVisible = true;

            }
            else if (SelectedNameIndex == -1)
            {
                setDateDetails();
                IsDateVisible = true;
            }
            else
            {
                setGraduateDetails();
                IsGraduateVisible = true;
            }
        }

        private void setCourseDetails()
        {
            CourseName = Courses[SelectedCourseIndex]?.Name;
        }

        private void setDateDetails()
        {
            DateName = Dates[SelectedDateIndex]?.Name;
        }

        public void NavigateKey(int inc)
        {
            if (inc == 1)
            {
                if (SelectedDateIndex == -1)
                {
                    _selectedDateIndex = 0;
                }
                else if (SelectedNameIndex == -1)
                {
                    getGraduateNames();
                    _selectedNameIndex = 0;
                }
                else if (SelectedNameIndex > -1 && SelectedNameIndex < (Images.Count() - 1))
                {
                    _selectedNameIndex++;
                }
                else if (SelectedDateIndex > -1 && SelectedDateIndex < (Dates.Count() - 1))
                {
                    _selectedNameIndex = -1;
                    _selectedDateIndex++;
                }
                else if (SelectedCourseIndex > -1 && SelectedCourseIndex < (Courses.Count() - 1))
                {
                    _selectedNameIndex = -1;
                    _selectedDateIndex = -1;
                    _selectedCourseIndex++;
                    getDateNames();
                }
            }
            else
            {
                if (SelectedCourseIndex == -1 && SelectedDateIndex == -1 && SelectedNameIndex == -1)
                {

                }
                else if (SelectedNameIndex > 0 && SelectedNameIndex <= (Images.Count() - 1))
                {
                    _selectedNameIndex--;
                }
                else if (SelectedNameIndex == 0 && SelectedDateIndex >= 0 && SelectedCourseIndex >= 0)
                {
                    _selectedNameIndex = -1;
                }
                else if (SelectedNameIndex == -1 && SelectedDateIndex >= 0 && SelectedCourseIndex >= 0)
                {
                    _selectedDateIndex = -1;
                }
                else if (SelectedNameIndex == -1 && SelectedDateIndex >= -1 && SelectedCourseIndex >= 0)
                {
                    _selectedCourseIndex--;
                    _selectedDateIndex = Dates.Count() - 1;
                    getDateNames();
                    _selectedNameIndex = Images.Count() - 1;
                }
            }

            search();
        }

        private void setGraduateDetails()
        {
            var graduate = Images[SelectedNameIndex];
            GraduateName = removeExtension(graduate?.Name);
            var datePath = new DirectoryInfo(_corPath + "\\" + Courses[SelectedCourseIndex] + "\\" + Dates[SelectedDateIndex]);
            GraduateImage = new BitmapImage(new Uri(datePath + "\\" + graduate?.Name));

        }


        private string removeExtension(string name)
        {
            if (name != null)
            {
                foreach (string extension in _extensions)
                {
                    name = name.Replace(extension, string.Empty);
                }

                return name;
            }

            return string.Empty;
        }

        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged([CallerMemberName]string name = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
