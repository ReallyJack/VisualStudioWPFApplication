using System.Diagnostics;
using System.Windows;
using SM_APP;
using static System.Net.Mime.MediaTypeNames;

namespace SM_Test

{
    [Apartment(ApartmentState.STA)]
    public class Tests
    {
        public MainWindow mainWindow;

        [SetUp]
        public void Setup()
        {
            mainWindow = new MainWindow();
        }

        [Test]
        public void Test_ClearButton()
        {
            mainWindow.TxtProductID.Text = "123";
            mainWindow.TxtProductName.Text = "Test Product";
            mainWindow.TxtPrice.Text = "10.50";
            mainWindow.TxtQuantity.Text = "5";
            mainWindow.CmbSupplier.SelectedIndex = 1;
            mainWindow.TxtDescription.Text = "Test Description";

            mainWindow.Button_Clear_Click(null, null);

            Assert.AreEqual(string.Empty, mainWindow.TxtProductID.Text);
            Assert.AreEqual(string.Empty, mainWindow.TxtProductName.Text);
            Assert.AreEqual(string.Empty, mainWindow.TxtPrice.Text);
            Assert.AreEqual(string.Empty, mainWindow.TxtQuantity.Text);
            Assert.AreEqual(string.Empty, mainWindow.TxtDescription.Text);
            Assert.AreEqual(-1, mainWindow.CmbSupplier.SelectedIndex);
            Assert.AreEqual(string.Empty, mainWindow.CurrentStock.Text);
            Assert.IsNull(mainWindow.ImgProduct.Source);
        }

        [Test]
        public void Test_SubmitButton()
        {       
            mainWindow.TxtProductID.Text = "123";
            mainWindow.TxtProductName.Text = "Test Product";
            mainWindow.TxtPrice.Text = "10.50";
            mainWindow.TxtQuantity.Text = "5";
            mainWindow.CmbSupplier.SelectedIndex = 1;
            mainWindow.TxtDescription.Text = "Test Description";
           
            mainWindow.Button_AddProduct_Click(null, null);

            Assert.AreEqual(1, mainWindow.ListBoxProducts.Items.Count);

            Assert.AreEqual(string.Empty, mainWindow.TxtProductID.Text);
            Assert.AreEqual(string.Empty, mainWindow.TxtProductName.Text);
            Assert.AreEqual(string.Empty, mainWindow.TxtPrice.Text);
            Assert.AreEqual(string.Empty, mainWindow.TxtQuantity.Text);
            Assert.AreEqual(string.Empty, mainWindow.TxtDescription.Text);
            Assert.AreEqual(-1, mainWindow.CmbSupplier.SelectedIndex);
            Assert.AreEqual(string.Empty, mainWindow.CurrentStock.Text);
            Assert.IsNull(mainWindow.ImgProduct.Source);
        }

        [Test]
        public void Test_AboutUsButton()
        {
            mainWindow.MenuItem_About_Click(null, null);

            AboutUsWindow aboutWindow = new AboutUsWindow();
            Assert.NotNull(aboutWindow);
        }
    }

}