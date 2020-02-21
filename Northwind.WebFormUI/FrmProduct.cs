using Northwind.Business.Abstract;
using Northwind.Business.DependencyResolvers.Ninject;
using Northwind.Entities.Concrete;
using System;
using System.Windows.Forms;

namespace Northwind.WebFormUI
{
    public partial class FrmProduct : Form
    {
        public FrmProduct()
        {
            InitializeComponent();
            _productService = InstanceFactory.GetInstance<IProductService>();
            _categoryService = InstanceFactory.GetInstance<ICategoryService>();
        }

        private IProductService _productService;
        private ICategoryService _categoryService;

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadCategories();
        }

        private void LoadCategories()
        {
            cbxCategory.DataSource = _categoryService.GetAll();
            cbxCategory.DisplayMember = nameof(Category.CategoryName);
            cbxCategory.ValueMember = nameof(Category.CategoryId);

            cbxCategoryId.DataSource = _categoryService.GetAll();
            cbxCategoryId.DisplayMember = nameof(Category.CategoryName);
            cbxCategoryId.ValueMember = nameof(Category.CategoryId);

            cbxUpdCategoryId.DataSource = _categoryService.GetAll();
            cbxUpdCategoryId.DisplayMember = nameof(Category.CategoryName);
            cbxUpdCategoryId.ValueMember = nameof(Category.CategoryId);
        }

        private void LoadProducts()
        {
            dgwProduct.DataSource = _productService.GetAll();
        }

        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgwProduct.DataSource = _productService.GetProductsByCategory(Convert.ToInt32(cbxCategory.SelectedValue));
            }
            catch (Exception)
            {
            }
        }

        private void tbxProductName_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbxProductName.Text))
            {
                dgwProduct.DataSource = _productService.GetProductByProductName(tbxProductName.Text);
            }
            else
            {
                LoadProducts();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                _productService.Add(new Product
                {
                    CategoryId = Convert.ToInt32(cbxCategoryId.SelectedValue),
                    ProductName = tbxProductName1.Text,
                    QuantityPerUnit = tbxQuantityPerUnit.Text,
                    UnitPrice = Convert.ToDecimal(tbxUnitPrice.Text),
                    UnitsInStock = Convert.ToInt16(tbxStock.Text)
                });
                MessageBox.Show("Ürün kaydedildi...");
                LoadProducts();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                _productService.Update(new Product
                {
                    ProductId = Convert.ToInt32(dgwProduct.CurrentRow.Cells[0].Value),
                    ProductName = tbxUpdProductName.Text,
                    CategoryId = Convert.ToInt32(cbxUpdCategoryId.SelectedValue),
                    UnitsInStock = Convert.ToInt16(tbxUpdStockAmount.Text),
                    QuantityPerUnit = tbxUpdQuantityPerUnit.Text,
                    UnitPrice = Convert.ToDecimal(tbxUpdUnitPrice.Text)
                });
                MessageBox.Show("Ürün güncellendi...");
                LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgwProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgwProduct.CurrentRow;
            tbxUpdProductName.Text = row.Cells[2].Value.ToString();
            cbxUpdCategoryId.SelectedValue = row.Cells[1].Value;
            tbxUpdUnitPrice.Text = row.Cells[3].Value.ToString();
            tbxUpdQuantityPerUnit.Text = row.Cells[4].Value.ToString();
            tbxUpdStockAmount.Text = row.Cells[5].Value.ToString();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgwProduct.CurrentRow != null)
            {
                try
                {
                    _productService.Delete(new Product
                    {
                        ProductId = Convert.ToInt32(dgwProduct.CurrentRow.Cells[0].Value)
                    });
                    MessageBox.Show("Ürün silindi...");
                    LoadProducts();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}