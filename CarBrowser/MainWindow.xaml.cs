using HelixToolkit.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // The Model that currently displaying
        private string CURRENT_MODEL = "D:\\WPFProjects\\CarBrowser\\CarBrowser\\SampleModels\\cube.stl";
        // Default Material for new loaded model: light blue color
        private Material DEFAULT_MATERIAL = MaterialHelper.CreateMaterial(new SolidColorBrush(Color.FromRgb(102, 153, 153)));
        private List<UploadedItem> CURRENT_RECENT_LIST = null;

        #region List of file paths
        // List of sample texture image's paths. Use to dynamically create buttons and apply textures to model (Because there are many)
        private List<string> LIST_OF_TEXTURE_PATH = new List<string>()
        {
            "D:\\WPFProjects\\CarBrowser\\CarBrowser\\SampleTextures\\sample1.png",
            "D:\\WPFProjects\\CarBrowser\\CarBrowser\\SampleTextures\\sample2.jpg",
            "D:\\WPFProjects\\CarBrowser\\CarBrowser\\SampleTextures\\sample3.jpg",
            "D:\\WPFProjects\\CarBrowser\\CarBrowser\\SampleTextures\\sample4.jpg",
            "D:\\WPFProjects\\CarBrowser\\CarBrowser\\SampleTextures\\sample5.jpg",
            "D:\\WPFProjects\\CarBrowser\\CarBrowser\\SampleTextures\\sample6.jpg",
            "D:\\WPFProjects\\CarBrowser\\CarBrowser\\SampleTextures\\sample7.jpg",
            "D:\\WPFProjects\\CarBrowser\\CarBrowser\\SampleTextures\\sample8.jpg"
        };
        // List of sample model's paths. Use to dynamically create buttons and load model (Because there are many)
        private List<string> LIST_OF_MODEL_PATH = new List<string>()
        {
            "D:\\WPFProjects\\CarBrowser\\CarBrowser\\SampleModels\\cube.jpg",
            "D:\\WPFProjects\\CarBrowser\\CarBrowser\\SampleModels\\sphere.jpg",
            "D:\\WPFProjects\\CarBrowser\\CarBrowser\\SampleModels\\cylinder.jpg",
            "D:\\WPFProjects\\CarBrowser\\CarBrowser\\SampleModels\\cone.jpg"
        };
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            // Add default model (a cube) to viewport
            viewPort3d.Children.Add(new ModelVisual3D { Content = Display3d(CURRENT_MODEL, DEFAULT_MATERIAL) });
        }

        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CreateListOfImageButton(LIST_OF_TEXTURE_PATH, 10);
            CreateListOfImageButton(LIST_OF_MODEL_PATH, 350);
        }

        #region Create Image Button

        /// <summary>
        ///     Create Image Button with Custom Style and Effect
        /// </summary>
        /// <param name="leftMargin"></param>
        /// <param name="sourcePath"></param>
        /// <returns></returns>
        private Image CreateImageButtonWithStyle(double leftMargin, string sourcePath)
        {
            Image imageBtn = new Image();
            imageBtn.Height = 34;
            imageBtn.Width = 34;
            imageBtn.Margin = new Thickness(leftMargin, 10, 0, 0);
            imageBtn.HorizontalAlignment = HorizontalAlignment.Left;
            imageBtn.VerticalAlignment = VerticalAlignment.Top;
            imageBtn.Stretch = Stretch.Fill;
            imageBtn.Source = new BitmapImage(new Uri(sourcePath, UriKind.Absolute));

            // Create Hover Glow Effect
            Style effect = CreateImageEffect();
            imageBtn.Style = effect;

            return imageBtn;
        }
        /// <summary>
        ///     Create Glow Effect when mouse is over button
        /// </summary>
        /// <returns></returns>
        private Style CreateImageEffect()
        {
            var effect = new DropShadowEffect()
            {
                ShadowDepth = 0,
                Color = Color.FromRgb(0, 51, 204),
                Opacity = 1,
                BlurRadius = 1,
            };

            var setter = new Setter()
            {
                Property = UIElement.EffectProperty,
                Value = effect,
            };

            var trigger = new Trigger()
            {
                Property = UIElement.IsMouseOverProperty,
                Value = true,
                Setters = { setter },
            };

            var style = new Style()
            {
                Triggers = { trigger },
            };

            return style;
        }

        /// <summary>
        ///     Create List of Button and add avent for each
        /// </summary>
        /// <param name="listOfPath"></param>
        /// <param name="startPosition"></param>
        private void CreateListOfImageButton(List<string> listOfPath, double startPosition)
        {
            foreach (string path in listOfPath)
            {
                Image imageBtn = CreateImageButtonWithStyle(startPosition, path);
                imageBtn.Name = "btnSample_" + startPosition;
                mainPanel.Children.Add(imageBtn);
                startPosition += 39;

                // Dynamically add event for each button
                imageBtn.MouseDown += (object sender, MouseButtonEventArgs e) =>
                {
                    Material material = DEFAULT_MATERIAL;
                    // Buttons on the right (startPosition >=350) is to change model
                    // Buttons on the left (startPosition < 350) is to change texture
                    if (Int32.Parse(imageBtn.Name.Split('_')[1]) >= 350)
                    {
                        CURRENT_MODEL = path.Split('.')[0] + ".stl";
                    }
                    else
                    {
                        material = new DiffuseMaterial(new ImageBrush(new BitmapImage(new Uri(path))));
                    }
                    RefreshViewport(CURRENT_MODEL, material);
                };
            }
        }
        #endregion

        #region Display 3D Modal
        /// <summary>
        ///     Create a 3D model when have its path and material
        /// </summary>
        /// <param name="modelPath">Path of the model (string)</param>
        /// <param name="material">Material created with Color, Texture, etc... (Material)</param>
        /// <returns></returns>
        private Model3D Display3d(string modelPath, Material material)
        {
            Model3D device = null;
            try
            {
                //Add rotate gesture 
                viewPort3d.RotateGesture = new MouseGesture(MouseAction.LeftClick);

                //Import 3D model file with material
                ModelImporter import = new ModelImporter() { DefaultMaterial = material };
                device = import.Load(modelPath);
            }
            catch (Exception e)
            {
                // Handle exception in case can not find the 3D model file
                MessageBox.Show("Exception Error : " + e.StackTrace);
            }
            return device;
        }

        /// <summary>
        ///     Clear Viewport, readd light and add new model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshViewport(string modelPath, Material material)
        {
            // Clear old stuff in view port
            viewPort3d.Children.Clear();
            // Re-add light
            viewPort3d.Children.Add(new SunLight());
            // Add new model that chosen
            viewPort3d.Children.Add(new ModelVisual3D() { Content = Display3d(modelPath, material) });
        }
        #endregion

        #region Events
        private void btnUpModel_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            OpenFileDialog dlg = new OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".stl";
            dlg.Filter = "STL Files (*.stl)|*.stl";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                CURRENT_MODEL = dlg.FileName;
                RefreshViewport(CURRENT_MODEL, DEFAULT_MATERIAL);
                AddToListRecentUpload(dlg);
            }
        }
        #endregion

        private void btnUpTexture_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            OpenFileDialog dlg = new OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                Material material = new DiffuseMaterial(new ImageBrush(new BitmapImage(new Uri(dlg.FileName))));
                RefreshViewport(CURRENT_MODEL, material);
                AddToListRecentUpload(dlg);
            }
        }

        private void AddToListRecentUpload(OpenFileDialog file)
        {
            string imagePath = file.FileName;
            if(file.DefaultExt == "stl")
            {
                imagePath = file.FileName.Split('.')[0] + ".jpg";
            }
            this.listRecentUpload.Items.Insert(0, new UploadedItem() { ThumbnailPath = imagePath, FileName = file.SafeFileName, FullPath = file.FileName });
            CURRENT_RECENT_LIST = listRecentUpload.Items.Cast<UploadedItem>().ToList();

        }
        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                var content = item.Content as UploadedItem;
                if(content.FileName.Split('.')[1] == "stl")
                {
                    CURRENT_MODEL = content.FullPath;
                    RefreshViewport(CURRENT_MODEL, DEFAULT_MATERIAL);
                }
                else
                {
                    Material material = new DiffuseMaterial(new ImageBrush(new BitmapImage(new Uri(content.FullPath))));
                    RefreshViewport(CURRENT_MODEL, material);
                }
            }
        }

        
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = (sender as TextBox).Text;
           
            listRecentUpload.Items.Clear();
            foreach (var item in CURRENT_RECENT_LIST)
            {
                if (searchText == "" || searchText == null)
                {
                    listRecentUpload.Items.Add(item);
                }
                else
                {
                    if (item.FileName.Contains(searchText))
                    {
                        listRecentUpload.Items.Add(item);
                    }
                }
            }
        }
    }
}
