using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Collections.ObjectModel;

namespace SM_APP

{
    public partial class MainWindow : Window
    {
        // Public properties to expose XAML controls for testing
        public TextBox TxtProductID { get { return txtProductID; } }
        public TextBox TxtProductName { get { return txtProductName; } }
        public TextBox TxtPrice { get { return txtPrice; } }
        public TextBox TxtQuantity { get { return txtQuantity; } }
        public TextBox TxtDescription { get { return txtDescription; } }
        public ComboBox CmbSupplier { get { return cmbSupplier; } }
        public Image ImgProduct { get { return imgProduct; } }
        public Button BtnAddImage { get { return btnAddImage; } }
        public Button BtnSubmit { get { return btnSubmit; } }
        public TextBox CurrentStock { get { return currentStock; } }
        public ListBox ListBoxProducts { get { return listBoxProducts; } }


        public ObservableCollection<Product> products = new ObservableCollection<Product>();

        public MainWindow()
        {
            InitializeComponent();
        }

        public void MenuItem_About_Click(object sender, RoutedEventArgs e)
        {
            AboutUsWindow aboutWindow = new AboutUsWindow();
            aboutWindow.ShowDialog();
        }

        public void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            listBoxProducts.SelectedItem = null;

            txtProductID.Clear();
            txtProductName.Clear();
            txtPrice.Clear();
            txtDescription.Clear();
            txtQuantity.Clear();
            currentStock.Text = string.Empty;
            cmbSupplier.SelectedIndex = -1;
            imgProduct.Source = null;
            btnAddImage.Visibility = Visibility.Visible;
            btnSubmit.Visibility = Visibility.Visible;
        }
        public void Button_AddProduct_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtProductID.Text) ||
                string.IsNullOrWhiteSpace(txtProductName.Text) ||
                string.IsNullOrWhiteSpace(txtPrice.Text) ||
                string.IsNullOrWhiteSpace(txtQuantity.Text) ||
                string.IsNullOrWhiteSpace(txtDescription.Text) ||
                cmbSupplier.SelectedItem == null)

            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            int productId;
            if (!int.TryParse(txtProductID.Text, out productId))
            {
                MessageBox.Show("Product ID must be a valid integer.");
                return;
            }

            decimal price;
            if (!decimal.TryParse(txtPrice.Text, out price))
            {
                MessageBox.Show("Price must be a valid decimal number.");
                return;
            }

            int quantity;
            if (!int.TryParse(txtQuantity.Text, out quantity))
            {
                MessageBox.Show("Quantity must be a valid integer.");
                return;
            }

            string productName = txtProductName.Text;
            string description = txtDescription.Text;
            string supplier = cmbSupplier.SelectedItem.ToString();

            var product = new Product
            {
                ProductID = productId,
                ProductName = productName,
                Price = price,
                Quantity = quantity,
                Supplier = supplier,
                Description = description,
                Image = imgProduct.Source as BitmapImage
            };

            listBoxProducts.Items.Add(product);

            txtProductID.Clear();
            txtProductName.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
            txtDescription.Clear();
            cmbSupplier.SelectedIndex = -1;
            imgProduct.Source = null;
            btnAddImage.Visibility = Visibility.Visible;

            MessageBox.Show("Product added successfully.");
        }

        public void Button_AddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage(new Uri(openFileDialog.FileName));
                imgProduct.Source = bitmap;
                btnAddImage.Visibility = Visibility.Collapsed;
            }
        }

        public void ListBoxProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxProducts.SelectedItem is Product selectedProduct)
            {
                currentStock.Text = selectedProduct.Quantity.ToString();
                txtDescription.Text = selectedProduct.Description;
                imgProduct.Source = selectedProduct.Image;
                btnAddImage.Visibility = Visibility.Collapsed;
                btnSubmit.Visibility = Visibility.Collapsed;
            }
        }

        public void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!listBoxProducts.IsMouseOver)
            {
                listBoxProducts.SelectedItem = null;

                txtProductID.Clear();
                txtProductName.Clear();
                txtPrice.Clear();
                txtDescription.Clear();
                txtQuantity.Clear();
                currentStock.Text = string.Empty;
                cmbSupplier.SelectedIndex = -1;
                imgProduct.Source = null;
                btnAddImage.Visibility = Visibility.Visible;
                btnSubmit.Visibility = Visibility.Visible;
            }
        }
    }


    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Supplier { get; set; }
        public string Description { get; set; }
        public BitmapImage Image { get; set; }

        public override string ToString()
        {
            return $"ID: {ProductID}, Name: {ProductName}, Price: {Price}, Quantity: {Quantity} (see more...)";
        }
    }

}