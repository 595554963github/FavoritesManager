using System.Windows;
using System.Windows.Controls;

namespace FavoritesManager
{
    public partial class ResourceEditDialog : Window
    {
        public ResourceItem ResourceItem { get; private set; }

        public ResourceEditDialog()
        {
            InitializeComponent();
            InitializeControls();
            ResourceItem = new ResourceItem();
        }
        public ResourceEditDialog(ResourceItem item) : this()
        {
            ResourceItem = item;
            PopulateFields();
        }
        private void InitializeControls()
        {
            cmbCategory.Items.Add("免费资源");
            cmbCategory.Items.Add("付费资源");
            cmbCategory.SelectedIndex = 0;

            rbFree.IsChecked = true;
        }
        private void PopulateFields()
        {
            txtName.Text = ResourceItem.Name;
            txtUrl.Text = ResourceItem.Url;
            txtDescription.Text = ResourceItem.Description;


            if (ResourceItem.Category == "Paid")
                cmbCategory.SelectedIndex = 1;
            else
                cmbCategory.SelectedIndex = 0;

            if (ResourceItem.Type == "Paid")
                rbPaid.IsChecked = true;
            else
                rbFree.IsChecked = true;
        }
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("请输入资源名称", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUrl.Text))
            {
                MessageBox.Show("请输入资源网址", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtUrl.Focus();
                return;
            }

            ResourceItem.Name = txtName.Text.Trim();
            ResourceItem.Url = txtUrl.Text.Trim();
            ResourceItem.Description = txtDescription.Text.Trim();
            ResourceItem.Category = cmbCategory.SelectedIndex == 1 ? "Paid" : "Free";
            ResourceItem.Type = rbPaid.IsChecked == true ? "Paid" : "Free";

            DialogResult = true;
            Close();
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}